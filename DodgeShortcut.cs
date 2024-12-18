using System.Reflection;
using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using DodgeShortcut.Extensions;
using System.Collections.Generic;
using TMPro;


namespace DodgeShortcut
{
    /// <summary>
    ///     Direction to dodge in if not moving
    /// </summary>
    internal enum DodgeDir
    {
        CharacterDir = 0,
        LookDir = 1
    }

    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    internal sealed class DodgeShortcut : BaseUnityPlugin
    {
        public const string PluginName = "DodgeShortcut";
        internal const string Author = "Searica";
        public const string PluginGUID = $"{Author}.Valheim.{PluginName}";
        public const string PluginVersion = "1.3.1";

        internal static DodgeShortcut Instance;

        private static readonly string MainSection = "1 - Global";
        private static readonly string MechanicsSection = "Mechanics";

        internal ConfigEntry<bool> ModEnabled;
        internal ConfigEntry<KeyCode> DodgeKey;
        internal ConfigEntry<DodgeDir> DefaultDir;
        internal bool IsModEnabled => ModEnabled.Value;

        internal KeyCode GetDodgeKey() => DodgeKey.Value;

        internal DodgeDir GetDefaultDir() => DefaultDir.Value;

        public void Awake()
        {
            Instance = this;
            Log.Init(Logger);

            Config.Init(PluginGUID, false);
            SetUpConfig();
            Config.SaveOnConfigSet = true;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), harmonyInstanceId: PluginGUID);
            Game.isModded = true;

            Config.SetupWatcher();
        }

        public void SetUpConfig()
        {
            ModEnabled = Config.BindConfig(
                MainSection,
                "EnableMod",
                true,
                "Globally enable or disable this mod."
             );

            Log.Verbosity = Config.BindConfig(
                MainSection,
                "Verbosity",
                LogLevel.Low,
                "Low will log basic information about the mod. Medium will log information that " +
                "is useful for troubleshooting. High will log a lot of information, do not set " +
                "it to this without good reason as it will slow Down your game."
            );

            DodgeKey = Config.BindConfig(
                MechanicsSection,
                "DodgeShortcut",
                KeyCode.LeftAlt,
                "Set the key to press to dodge in the direction your character is moving." +
                "\nIf LeftAlt conflicts with other mods, I recommend setting the dodge key to the back button on your mouse."
             );


            DefaultDir = Config.BindConfig(
                MechanicsSection,
                "DefaultDodgeDir",
                DodgeDir.CharacterDir,
                "Default direction that character dodges in if the dodge shortcut key is pressed while not moving. " +
                "Can be set to the direction the character is facing, or the direction the camera is facing."
            );
        }

        public void OnDestroy()
        {
            Config.Save();
        }
    }

    [HarmonyPatch]
    internal static class KeyHintPatches
    {
        internal const string CombatHintsName = "CombatHints";
        internal const string KeyboardHintsName = "Keyboard";
        internal const string DodgeHintName = "Dodge";
        internal static readonly HashSet<string> KeepChildNames = new() { "Text", "key_bkg" };
        internal static GameObject DodgeKeyHint;



        [HarmonyPostfix]
        [HarmonyPatch(typeof(KeyHints), nameof(KeyHints.Awake))]
        internal static void KeyHints_Awake_Postfix(KeyHints __instance)
        {
            if (
                !__instance
                || !__instance.transform.TryGetChild(CombatHintsName, out Transform combatHints) 
                || !combatHints.TryGetChild(KeyboardHintsName, out Transform keyBoardHints)
                || !keyBoardHints.TryGetChild(DodgeHintName, out Transform dodgeHint)
            )
            {
                return;
            }
            DodgeKeyHint = dodgeHint.gameObject;

            // Remove everything other than the single key I want.
            for (int i = DodgeKeyHint.transform.childCount-1; i > -1 ; i--)
            {
                var child = DodgeKeyHint.transform.GetChild(i);
                if (KeepChildNames.Contains(child.name))
                {
                    continue;
                }
                Log.LogInfo($"Destroying child {child.name} of DodgeKeyHint");
                GameObject.DestroyImmediate(child.gameObject);
            }
            Log.LogInfo("Successfully modified Dodge Key Hint prefab.");

            UpdateDodgeKeyHint();
            DodgeShortcut.Instance.DodgeKey.SettingChanged += (obj, attr) =>
            {
                UpdateDodgeKeyHint();
            };
        }

        internal static void UpdateDodgeKeyHint()
        {
            if (!DodgeKeyHint 
                || !DodgeKeyHint.TryGetChild("Key", out Transform keyHint, breadth: false)
                || !keyHint.TryGetComponent(out TextMeshProUGUI tmpText))
            {
                Log.LogWarning("Failed to update Dodge Key Hint!");
                return;
            }
     
            tmpText.text = DodgeShortcut.Instance.GetDodgeKey().ToString();
            Log.LogInfo("Updated Dodge Key Hint!");
        }
    }

    [HarmonyPatch]
    internal static class DodgePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), nameof(Player.Update))]
        private static void DodgePatch()
        {
            if (!DodgeShortcut.Instance.IsModEnabled) { return; }

            if (Player.m_localPlayer == null)
            {
                Log.LogInfo("Local player is null.", LogLevel.High);
                return;
            }

            // want to make sure we don't dodge if sitting in a chair?
            if (
                Player.m_localPlayer.IsTeleporting()
                || Player.m_localPlayer.IsAttachedToShip()
                || Player.m_localPlayer.InPlaceMode()
                || Console.instance.m_chatWindow.gameObject.activeInHierarchy
                || Chat.instance.m_chatWindow.gameObject.activeInHierarchy
                || TextInput.IsVisible()
                || StoreGui.IsVisible()
                || InventoryGui.IsVisible()
                || Menu.IsVisible()
            )
            {
                return;
            }

            if (Input.GetKeyDown(DodgeShortcut.Instance.GetDodgeKey()))
            {
                Vector3 dodgeDir = Player.m_localPlayer.m_moveDir;
                dodgeDir.y = 0f;

                if (dodgeDir.magnitude < 0.1f)
                {
                    if (DodgeShortcut.Instance.GetDefaultDir() == DodgeDir.CharacterDir)
                    {
                        dodgeDir = Player.m_localPlayer.transform.rotation * Vector3.forward;
                    }
                    else
                    {
                        dodgeDir = Player.m_localPlayer.m_lookDir;
                    }
                    dodgeDir.y = 0f;
                }

                dodgeDir.Normalize();
                Log.LogInfo($"Dodge Vector: {dodgeDir}", LogLevel.Medium);
                Player.m_localPlayer.Dodge(dodgeDir);
            }
        }
    }

    /// <summary>
    ///     Log level to control output to BepInEx log
    /// </summary>
    internal enum LogLevel
    {
        Low = 0,
        Medium = 1,
        High = 2,
    }

    /// <summary>
    ///     Helper class for properly logging from static contexts.
    /// </summary>
    internal static class Log
    {
        #region Verbosity

        internal static ConfigEntry<LogLevel> Verbosity { get; set; }
        internal static LogLevel VerbosityLevel => Verbosity.Value;

        #endregion Verbosity

        private static ManualLogSource logSource;

        internal static void Init(ManualLogSource logSource)
        {
            Log.logSource = logSource;
        }

        internal static void LogDebug(object data) => logSource.LogDebug(data);

        internal static void LogError(object data) => logSource.LogError(data);

        internal static void LogFatal(object data) => logSource.LogFatal(data);

        internal static void LogInfo(object data, LogLevel level = LogLevel.Low)
        {
            if (VerbosityLevel >= level)
            {
                logSource.LogInfo(data);
            }
        }

        internal static void LogMessage(object data) => logSource.LogMessage(data);

        internal static void LogWarning(object data) => logSource.LogWarning(data);

        internal static void LogGameObject(GameObject prefab, bool includeChildren = false)
        {
            LogInfo("***** " + prefab.name + " *****");
            foreach (Component compo in prefab.GetComponents<Component>())
            {
                LogComponent(compo);
            }

            if (!includeChildren) { return; }

            LogInfo("***** " + prefab.name + " (children) *****");
            foreach (Transform child in prefab.transform)
            {
                LogInfo($" - {child.gameObject.name}");
                foreach (Component compo in child.gameObject.GetComponents<Component>())
                {
                    LogComponent(compo);
                }
            }
        }

        internal static void LogComponent(Component compo)
        {
            LogInfo($"--- {compo.GetType().Name}: {compo.name} ---");

            PropertyInfo[] properties = compo.GetType().GetProperties(ReflectionUtils.AllBindings);
            foreach (var property in properties)
            {
                try
                {
                    LogInfo($" - {property.Name} = {property.GetValue(compo)}");
                }
                catch (NullReferenceException e)
                {
                    LogInfo($" - {property.Name} = null");
                }
                
            }

            FieldInfo[] fields = compo.GetType().GetFields(ReflectionUtils.AllBindings);
            foreach (var field in fields)
            {
                try
                {
                    LogInfo($" - {field.Name} = {field.GetValue(compo)}");
                }
                catch (NullReferenceException e)
                {
                    LogInfo($" - {field.Name} = null");
                }
            }
        }
    }
}
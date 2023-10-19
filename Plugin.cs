using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Reflection;
using UnityEngine;


namespace DodgeButton
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginName = "DodgeShortcut";
        internal const string Author = "Searica";
        public const string PluginGuid = $"{Author}.Valheim.{PluginName}";
        public const string PluginVersion = "1.0.4";

        Harmony _harmony;
        public void Awake()
        {
            Log.Init(Logger);
            PluginConfig.Init(Config);
            PluginConfig.SetUpConfig();
            if (PluginConfig.IsModEnabled.Value)
            {
                _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), harmonyInstanceId: PluginGuid);
            }
        }

        public void OnDestroy()
        {
            PluginConfig.Save();
            _harmony?.UnpatchSelf();
        }
    }
}

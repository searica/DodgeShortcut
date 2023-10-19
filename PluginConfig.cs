using BepInEx.Configuration;
using UnityEngine;

namespace DodgeButton
{
    internal class PluginConfig
    {
        private static ConfigFile configFile;

        private const string MainSectionName = "\u200BGlobal";

        private const string MechanicsName = "Hotkey";
        public static ConfigEntry<bool> IsModEnabled { get; private set; }

        public static ConfigEntry<KeyboardShortcut> DodgeKey;

        private static readonly AcceptableValueList<bool> AcceptableToggleValuesList = new(new bool[] { false, true });

        internal static ConfigEntry<T> BindConfig<T>(string group, string name, T value, ConfigDescription description)
        {
            ConfigEntry<T> configEntry = configFile.Bind(group, name, value, description);
            return configEntry;
        }


        internal static ConfigEntry<T> BindConfig<T>(string group, string name, T value, string description) => BindConfig(group, name, value, new ConfigDescription(description));


        public static void Init(ConfigFile config)
        {
            configFile = config;
        }

        public static void SetUpConfig()
        {
            IsModEnabled = BindConfig(
                MainSectionName,
                "EnableMod",
                true,
                new ConfigDescription(
                    "Globally enable or disable this mod (restart required).",
                    AcceptableToggleValuesList
                )
             );

            DodgeKey = BindConfig(
                MechanicsName, 
                "DodgeButton",
                new KeyboardShortcut(KeyCode.LeftAlt, new KeyCode[0]),
                "Set the key to press to dodge in the direction your character is moving." +
                "\nIf LeftAlt conflicts with other mods, I recommend setting the dodge key to the back button on your mouse."
             );
        }

        public static void Save()
        {
            configFile.Save();
        }
    }
}

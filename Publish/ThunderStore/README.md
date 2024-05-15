# DodgeShortcut
Simple Valheim mod to add a configurable dodge button. Allows you to dodge with a single key press.

## Usage
- Install the mod using either r2modman or thunderstore mod manager.
- Run the game once to create the configuration file.
- Either use an in-game configuration manager to edit the default key-bind for dodging or edit the configuration file using your mod manager.
- While moving, press the configured dodge key to dodge in the direction your character is moving.
- While standing still press the configured dodge key to dodge in either the direction your character is facing or the direction the camera is pointed (default is the direction your character is facing).

## Configuration
Changes made to the configuration settings will be reflected in-game immediately (no restart required). The mod also has a built in file watcher so you can edit settings via an in-game configuration manager (changes applied upon closing the in-game configuration manager) or by changing values in the file via a text editor or mod manager.

### Global
**EnableMod**
- Globally enable or disable this mod.
    - Acceptable values: True, False
    - Default value: true

**Verbosity**
- Low will log basic information about the mod. Medium will log information that is useful for troubleshooting. High will log a lot of information, do not set it to this without good reason as it will slow down your game.
  - Acceptable values: Low, Medium, High
  - Default value: Low.

### Mechanics
**DodgeShortcut**
- Set the key to press to dodge in the direction your character is moving. If LeftAlt conflicts with other mods, I recommend setting the dodge key to the back button on your mouse.
    - Default value: LeftAlt       

**DefaultDodgeDir**
- Default direction that character dodges in if the dodge shortcut key is pressed while not moving. Can be set to the direction the character is facing, or the direction the camera is facing
    - Acceptable values: CharacterDir, LookDir
    - Default value: CharacterDir     

## Compatibility
Likely compatible with most mods as it touches very little of the game's code. May conflict with other mods that allow for custom inputs when dodging.

## Known Issues
None so far, tell me if you find any.

## Donations/Tips
My mods will always be free to use but if you feel like saying thanks you can tip/donate.

| My Ko-fi: | [![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/searica) |
|-----------|---------------|

## Source Code
Source code is available on Github.

| Github Repository: | <img height="18" src="https://github.githubassets.com/favicons/favicon-dark.svg"></img><a href="https://github.com/searica/DodgeShortcut"> DodgeShortcut</a> |
|-----------|---------------|

### Contributions
If you would like to provide suggestions, make feature requests, or reports bugs and compatibility issues you can either open an issue on the Github repository or tag me (@searica) with a message on my discord [Searica's Mods](https://discord.gg/sFmGTBYN6n).

I'm a grad student and have a lot of personal responsibilities on top of that so I can't promise I will respond quickly, but I do intend to maintain and improve the mod in my free time.

## Shameless Self Plug (Other Mods By Me)
If you like this mod you might like some of my other ones.

#### Building Mods
- [More Vanilla Build Prefabs](https://valheim.thunderstore.io/package/Searica/More_Vanilla_Build_Prefabs/)
- [Extra Snap Points Made Easy](https://valheim.thunderstore.io/package/Searica/Extra_Snap_Points_Made_Easy/)
- [TerrainTools](https://valheim.thunderstore.io/package/Searica/TerrainTools/)
- [BuildRestrictionTweaksSync](https://valheim.thunderstore.io/package/Searica/BuildRestrictionTweaksSync/)

#### Gameplay Mods
- [CameraTweaks](https://valheim.thunderstore.io/package/Searica/CameraTweaks/)
- [FortifySkillsRedux](https://valheim.thunderstore.io/package/Searica/FortifySkillsRedux/)
- [ProjectileTweaks](https://github.com/searica/ProjectileTweaks/)
- [SafetyStatus](https://valheim.thunderstore.io/package/Searica/SafetyStatus/)
- [SkilledCarryWeight](https://github.com/searica/SkilledCarryWeight/)

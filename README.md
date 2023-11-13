<!--HTML Magic -->
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<style> td, th {border: none!important;}</style>

# DodgeShortcut
Simple Valheim mod to add a configurable dodge button. Allows you to dodge with a single key press.

## Usage
- Install the mod using either r2modman or thunderstore mod manager.
- Run the game once to create the configuration file.
- Either use an in-game configuration manager to edit the default key-bind for dodging or edit the configuration file using your mod manager.
- Press the configured dodge key to dodge in the direction your character is moving, or if they are standing still then the direction your camera is looking.

### Configuration
**EnableMod**
- Globally enable or disable this mod.
    - Acceptable values: True, False
    - Default value: true

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
| Github Repository: | <button style="font-size:20px"><i class="fa fa-github"></i><a href="https://github.com/searica/DodgeShortcut"> DodgeShortcut</button> |
|-----------|---------------|

### Contributions
If you would like to provide suggestions, make feature requests, or reports bugs and compatibility issues you can either open an issue on the Github repository or tag me (@searica) with a message on my discord [Searica's Mods](https://discord.gg/sFmGTBYN6n).

I'm a grad student and have a lot of personal responsibilities on top of that so I can't promise I will respond quickly, but I do intend to maintain and improve the mod in my free time.

## Shameless Self Plug (Other Mods By Me)
If you like this mod you might like some of my other ones.

#### Building Mods
- [More Vanilla Build Prefabs](https://valheim.thunderstore.io/package/Searica/More_Vanilla_Build_Prefabs/)
- [Extra Snap Points Made Easy](https://valheim.thunderstore.io/package/Searica/Extra_Snap_Points_Made_Easy/)
- [BuildRestrictionTweaksSync](https://valheim.thunderstore.io/package/Searica/BuildRestrictionTweaksSync/)

#### Gameplay Mods
- [FortifySkillsRedux](https://valheim.thunderstore.io/package/Searica/FortifySkillsRedux/)
- [ProjectileTweaks](https://github.com/searica/ProjectileTweaks/)
- [SkilledCarryWeight](https://github.com/searica/SkilledCarryWeight/)
- [SafetyStatus](https://valheim.thunderstore.io/package/Searica/SafetyStatus/)
- [CameraTweaks](https://valheim.thunderstore.io/package/Searica/CameraTweaks/)
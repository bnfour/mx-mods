Unofficial quality of life modifications for the PC version of the hit video game Musynx (stylized as MUSYNX, sometimes also known as Musync) using MelonLoader.

# Disclaimers
- These mods are unofficial and are not associated with, related to, and/or endorsed by I-Inferno and/or Wave Game.
- USE AT YOUR OWN RISK. NO WARRANTIES.
- Please read [FAQ](#frequently-asked-questions).

# Mod list
There are currently five mods available in this repo: four are interface enhancements, and another one can be used to disable vSync and other FPS caps that are otherwise baked into the game.

- [Optional options](#optional-options) — allows to skip the settings screen on song select, starting the game immediately
- [Hidden cursor](#hidden-cursor) — hides the mouse cursor during gameplay
- [Plentiful stats](#plentiful-stats) — provides some data for the stats screen
- [Skin tweaks](#skin-tweaks) — improvements for some of the skins
- [VSync annihilator](#vsync-annihilator) — allows the game to run without vSync, with optional custom FPS cap (you probably won't need this)

## Optional options
Mod file: `OptionalOptions.dll`

This mod skips the settings screen on song select by default, starting the game using saved settings for theme, speed, and offsets.

Selecting a song with this mod will look like this:

https://github.com/bnfour/mx-mods/assets/853426/4dc917ce-85bf-4dc7-aec3-4a541e530e36

In this video, you can see [Hidden cursor](#hidden-cursor) kicking in as well.

To access the settings screen, hold Shift key when starting a song.

## Hidden cursor
Mod file: `HiddenCursor.dll`

This mod hides the cursor during gameplay, preventing stuff like this:

![sorry for potato quality -- this is a still frame from a video recording because i'm too lazy to disable cursor hiding in my screenshoter ¯\_(ツ)_/¯](readme-images/HiddenCursor/annoying-cursor.png)

The cursor is still shown when the game is paused, and in all other screens.

## Plentiful stats
Mod file: `PlentifulStats.dll`

This mod adds some extra features and fixes for the stats screen after a song has been played; all can be independently turned on or off.

### Available features

#### Extra data
The statistics display is changed:

| Before | After |
| --- | --- |
| ![i tend to get one great in any rhythm game i play](readme-images/PlentifulStats/vanilla-stats.png) | ![almost 122 and still one great smh my head](readme-images/PlentifulStats/cool-stats.png) |


1. Blue exacts are counted separately from cyan exacts  
(top middle of the pictures)
2. Previous best score is shown alongside the current one  
(bottom middle of the pictures)

#### Extra keybind
R key can be used to restart the song alongside F1 key. Due to implementation quirks, works with the in-game pause menu, too!

#### Localization fix
This feature (more of an unofficial bugfix, actually 🤓) changes the "NEXT" button being "继续" when the score is 120% or more and language other than Chinese is used:

| Lower score reference | Before | After |
| --- | --- | --- |
| ![image trivia: this is a relatively old screenshot i took when i just started playing](readme-images/PlentifulStats/en_next-reference.png) | ![this is a slightly older screenshot](readme-images/PlentifulStats/en_next-unfixed.png) | ![and this is a recent one, obviously](readme-images/PlentifulStats/en_next-fixed.png) |

I'd love to see an official fix for this implemented. ٩(◕‿◕)۶

### Configuration
All features of the mod can be toggled on and off independently via MelonLoader's preferences file, `UserData/MelonPreferences.cfg`. Launching the game with the mod installed should create the following section in the file:
```toml
[Bnfour_PlentifulStats]
# Enables R key to restart from the stats screen.
RToRestart = true
# Displays separate blue and cyan exacts counts.
SeparateExacts = true
# Displays previous best score at the stats screen.
PrevBest = true
# Fixes Next button switching to Chinese on 120+ scores.
EnNextFix = true
```

Set to `false` to disable a feature.

## Skin tweaks
Mod file: `SkinTweaks.dll`

This is a collection of improvements to in-game skins. All features can be turned on and off separately.

### Available features

#### Disabled mountain overlay for Ink2D
This disables the moving mountain overlay images on Ink2D for cleaner looks. I also find their sudden disappearance quite jarring (but you don't have to agree with me).

| Before | After |
| --- | --- |
| ![i tried to take similar screenshots, but it's not perfect](readme-images/SkinTweaks/UI0D_before.jpg) | ![once again, sorry for potato quality](readme-images/SkinTweaks/UI0D_after.jpg) |

#### Improved long note scoring for Techno2D (and probably STG2D)
This updates the score display present on Techno2D and STG2D whenever the long note is played at least partially. On some charts, long notes at the very end make skin's display score inconsistent with final results:

https://github.com/user-attachments/assets/445e69c4-f2a8-4841-8e70-336dbd5948b1

(Results screen modified by [Plentiful stats](#plentiful-stats))

Compare with the score being updated on long note releases (legacy/simple scoring):

https://github.com/user-attachments/assets/c5b4fe86-c2d2-45e8-a7a6-27cb0e60ebd4

Or with the score being updated on every combo tick:

https://github.com/user-attachments/assets/ec62569f-dde0-4d96-93f6-3837f2179225

> [!WARNING]
> STG2D support is not tested, as I do not currently own relevant DLC. It _should_ work as well as it does on Techno2D, but ¯\\\_(ツ)\_/¯, let me know if it's broken.

### Configuration
Both features of the mod can be toggled on and off independently via MelonLoader's preferences file, `UserData/MelonPreferences.cfg`. Launching the game with the mod installed should create the following section in the file:
```toml
[Bnfour_SkinTweaks]
# Removes moving white mountain overlays from Ink2D.
MountainRemoval = true
# Updates the score display on Techno2D and STG2D for long notes.
LongNoteScoring = true
# Updates the score display on Techno2D and STG2D for long notes every combo tick. If disabled, the score is updated on long note release.
AdvLongNoteScoring = true
```

Set to `false` to disable a feature. `AdvLongNoteScoring` controls scoring behavior (if it is enabled by `LongNoteScoring`):
- `true` to update on every combo tick (advanced scoring, see the third video demo)
- `false` to update on long note releases (legacy scoring, see the second video demo)

## VSync annihilator
Mod file: `VSyncAnnihilator.dll`

> [!WARNING]
> Please read the full description before installing!
>
> If you are content with your framerate, just skip this mod.
>
>This mod is intended for setups where both vSync FPS and FPS cap presets (60 and 120 available as of 2024-04-30 update) do not match actual display FPS. For me, [for whatever reason](https://youtu.be/iYWzMvlj2RQ), vSync is set to 120 FPS even for a 240 Hz display.
>
> Again, if you never thought you should change your FPS, this mod is not for you.

This mod disables vSync before the game even starts for _allegedly_ better performance, and provides a custom FPS cap instead of vanilla 60/120.

### Installation
The game engine _allegedly_ does not like changing the vSync setting at runtime — it may cause lags. All default game's quality presets enable vSync. The solution is to use the provided script, `resource-patcher.py`, to patch the highest quality preset to remove vSync. The mod will warn you in the console if this step was skipped. After an update to the game, the file must be patched again.

> [!TIP]
> The patching script is written in Python, so you'll need a reasonably recent version of it installed.

```bash
python resource-patcher.py path/to/steam/common/MUSYNX/MUSYNX_Data/globalgamemanagers
```
where `path/to/steam` is actual path to your Steam library. Please make sure to target `globalgamemanagers` file (no extension), **not** `globalgamemanagers.assets`, and **not** `globalgamemanagers.assets.resS`.

If the preset was patched successfully, its name in setup will change:

![yay](readme-images/VSyncAnnihilator/patched-preset.png)

Select it to start the game without vSync. The actual mod, if installed, will keep it disabled. (Without the mod, the game may eventually turn it on again, and/or force the 60/120 FPS cap found in its settings.)

The `globalgamemanagers` file can be restored to its original state by running the script on it again or using Steam's "verify integrity of game files" feature.

### Configuration
The mod's custom FPS cap (240 by default — solely because it's the value I use) can be adjusted via MelonLoader's preferences framework. Launching the game with the mod installed should generate the following section in `UserData/MelonPreferences.cfg` (you can also put it here in advance):
```toml
[Bnfour_VSyncAnnihilator]
# Frame limit not related to vSync. 0 for no limit -- the game will run as fast as it can, may break.
TargetFramerate = 240
```

The value of `TargetFramerate`, if non-zero, is passed to the engine as [`Application.targetFrameRate`](https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html).

If the config value is zero, the game runs as fast as it can. I wouldn't recommend this, as with really high FPS (I got 1500+) things seem to be messed up. Also note that, for technical reasons, the resulting FPS may differ from the cap (for me, 600 FPS cap results in about 640 FPS in game, and 240 FPS cap is 246 actual FPS).

## Experimental mod
This is not a mod intended for using. Rather, it's a developmental test bed for me to test random stuff without changing existing proper mods. A few first mods were prototyped as a single do-it-all unimaginable horror abomination here first and then rewritten to be modular cute things currently available in this repo; newer ones also were prototyped here.

The project contains the bare minimum for a mod that is successfully loaded; it does nothing except posting a single message in the log.

# Installation
These are [MelonLoader](https://melonwiki.xyz/) mods. In order to run these, you need to have it installed. Currently, 0.6.1 Open-Beta of MelonLoader is supported.  
Once you have MelonLoader installed, drop the DLLs of desired mods into the `Mods` folder. Remove to uninstall.  

> [!NOTE]
> For VSync annihilator (if you need it: probably not), refer to its own [installation section](#installation) for additional instructions.

Rather than downloading these, I suggest (reviewing the source and) building them yourself — this way you'll be sure the mods behave as described. See ["Building from source"](#building-from-source).  
Otherwise, please verify the downloads.

## Verification
Every published release is accompanied with SHA256 hashes of every DLL. MelonLoader does print these in console when loading mods, but I suggest to verify the hashes before installation.

# Frequently Asked Questions
(or, more accurately, "I thought you may want to know this")

### Is this cheating?
_tl;dr: no_

The mods provide no advantage for the actual gameplay, only some convinence in in the menus (and maybe a fix for broken vSync framerate). You still have to git gud to earn high scores.

Unless you count _any_ changes to the game for _any_ purpose as cheating, this is not cheating.

### Will I get banned for using these?
_tl;dr: probably not, but NO WARRANTIES; USE AT YOUR OWN RISK_

As I stated in previous question, I don't believe this is cheating. I've been using these for a while, and my account is still there. But there's a reason for the all-caps section of the license about having no warranties: the devs might think otherwise or break the compatibility (un)intentionally.

Remember that you're using the mods **at your own risk**.

### I have other mods. What about compatibility with them?
_tl;dr: ¯\\\_(ツ)\_/¯_

Some of the mods override vanilla methods, so other mods that use these may not work.

### My game is broken because of you and your mods, how can I fix this and blame you?
_tl;dr: uninstall, and remember: NO WARRANTIES_

If you just want to play the game, removing the mods (and maybe the modloader itself) is always an option.
* Please make sure you're using supported (**0.6.1**) version of MelonLoader.
* Try to remove mods not from this repo.
* Try to remove mods and/or modloader and check whether the vanilla game is broken too.

If none of these helps, feel free to submit an issue, unless it's already have been reported.

### There's nothing in the console after the actual game starts?
_tl;dr: `tail -f MelonLoader/Latest.log`_

For whatever reason, the built-in MelonLoader console does not display logs after the game (as opposed to the config app) starts. This includes messages about loading the mods and any messages (warnings, errors) they produce.

The log file, located at `MelonLoader/Latest.log`, is still updated in real time. You can use any tool to monitor the file contents, such as ubiquitous `tail`, for example.

# Building from source
This repo is a run-of-the-mill .NET solution targeting .NET 4.7.2.

The only gotcha is that some libraries required to build it are not included because of file size (and licensing) issues. Your installation of MelonLoader will generate them for you.

Copy everything from `MelonLoader/Managed`, `MelonLoader/net35`, and `MUSYNX_Data/Managed/` folders from the game install to the `references` folder of this repo. All the DLLs should be directly in the `references` folder, no subfolders.

This should cover the local references for all the projects. (Actually, **most** of the DLLs are not necessary to build the solution, I just don't plan on keeping an accurate and up to date list of required libraries.)

After that, just run `dotnet build`.

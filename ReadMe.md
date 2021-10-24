# Custom Trial

Allows creation of custom trials for the Colosseum of Fools.

**This mod is a work in progress, and may cease development at any time. If you wish to contribute, make a pull request on the GitHub page at: https://github.com/jngo102/HollowKnight.CustomTrial**

## Installing custom trials:
-	Move the `CustomTrial.GlobalSettings.json` file to your game's save location.
	Windows: `%APPDATA%\..\LocalLow\Team Cherry\Hollow Knight\`
	MacOS: `~/Library/Application Support/unity.Team Cherry.Hollow Knight/`
	Linux: `~/.config/unity3d/Team Cherry/Hollow Knight/`

## Creating custom trials:
-	Launch the game once with the custom trial mod installed. This will generate a template `CustomTrial.GlobalSettings.json` file.
-	Using the template, enter the details of the waves of enemies that you wish to spawn. It may help to learn about the JSON format:
	https://www.w3schools.com/js/js_json_intro.asp

	* Waves: The main container of all the waves in the trial sequence.
	* Enemies: Contains all the enemies for a particular wave.
	* Name: The name of the enemy as it appears in the Hunter's Journal.
	* Health: The amount of health for a particular enemy.
	* SpawnPosition: Where the enemy will spawn within the arena. Note that the bounds of the arena are around 87 to 118 horizontally (x), and 7 to 28 vertically (y).
	  Be sure that the spawn posiiton lies within these values or the enemy will spawn outside the arena.
	* PlatformSpawn: The location that a platform will spawn in a particular wave. There can be several of these at a time for multiple platforms.
	* CrowdAction: The audio that the crowd will play when the wave ends. Options are "Cheer", "Laugh" and "Gasp".
	* MusicLevel: The Colosseum music level, with higher levels corresponding to more intense music, up to level 6. Can also be "SILENT" for silence.
	* Cooldown: The cooldown between waves, in seconds.
	* DelayBetweenSpawns: The amount of time between spawns within a wave, in seconds.
	* WallCDistance: The distance that the ceiling wall will be from its rest position.
	* WallLDistance: The distance that the left wall will be from its rest position.
	* WallRDistance: The distance that the right wall will be from its rest position.
	* Spikes: Whether spikes should appear on the ground.

**NOTE: A graphical editor for creating custom trials used to be available in an attempt to make creating trials easier, but no longer exists for this version of the mod.**

## Troubleshooting
-	Issue: None of my mods loaded after installing CustomTrial, and the mod list at the top corner of the screen is gone.
	Solution: Re-run the game one more time, or check that your global settings JSON has correct syntax using a tool like jsonlint: https://jsonlint.com/
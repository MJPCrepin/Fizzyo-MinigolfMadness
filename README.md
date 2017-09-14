Minigolf Madness
====
A Unity game created by Manuel Crepin (manuel@crep.in) for the Fizzyo Project

## Known bugs / to do
* Trails not showing in shop preview in UWP builds on large screens
  * Shop has a player preview to see how they look once they buy an item
  * User can buy hats, colours (for the balls) and trails (temporarily highlight path)
  * On large screens, trail not seen (probably due to camera angle), fine for target device
* All shop buttons init with "Locked" text (even if previously unlocked)
  * Need to check unlock state per item THEN set button text to Locked if it isn't unlocked
* Change shop prices (currently all fixed at 10), balance with amount of coins per level
* Unused assets under `_Imports` yet to be cleaned up (mostly ubused 3d assets)
* Disallow skipping holes if no coins left
  * make local copy of savestate coin amount, or simply save after each skip
* Save level unlock states (progression)
  * Add savestate int and saveManager bitwise function
  * Call unlock function when last hole of a level reached
  * Update menu buttons accordingly
* User suggestion: add Accessories shop category? (eg moustache, balloon, etc)

## Installing the included test build
* Locate build folder under "UWP Install"
* Run the .appxbundle file OR run the .ps1 file as admin

## Documentation
* Please refer to the Documentation folder for further details about...
  * How to make a build of/install the game? -> Deployment Manual
  * How to actually play the game? -> User Manual
  * What does all his game code mean? -> System Manual
  * New physio game design for dummies? -> LEMONS Poster
  * Anything else/further details -> LEMONS - A framework... (thesis)
* Keep on reading for a TL;DR of the above.

## Directory structure
```
Assets (contains Unity project assets)
|
+- _Imports (all 3d models and tools imported)
|
+- Audio (copyright music used)
|
+- Prefabs (collection of items used as templates)
|   |
|   +- Hats (all hat prefabs)
|   |
|   +- Map (obstacles/coins/course sections/endpoints/etc)
|   |
|   +- Trails (the trail prefabs)
|
+- Scenes (The Unity scenes)
|
+- Scripts (All the code used in the game)
|	|
|	+- Controllers (player/camera/preview/pointer behaviours)
|	|
|	+- Rotators  (scripts fr rotating coins/obstacles/etc)
|	|
|	+- Scenes (the behaviour scripts for scenes, and the parent LevelContent file)
|	|
|	+- Other (saving functions, tools, user input adapter class)
|
+- Visuals (all image/texture/material assets)

```


## Packaging new builds to Windows Store format
* Once built, App.cs and Package.appmanifest need to be edited:
* Update the following App.cs method:
```
private void ApplicationView_Activated(CoreApplicationView sender, IActivatedEventArgs args)
        {
            CoreWindow.GetForCurrentThread().Activate();
            if (args.Kind == ActivationKind.Protocol)
            {
                CoreWindow.GetForCurrentThread().Activate();
            }
        }
```
* In Package.appmanifest add a Protocol in Declarations and provide a name (ie: `minigolfmadness`)

## Project notes
* `UserInput` class is an adapter class waiting for the breath framework to be finalised.
* Game mechanics and behaviours are finalised so content creation should be easy.
* Recommended expansion/further creation of levels (see Game Info for details).
* Item shop fully functional, could add new items (especially hats) but not crucial.
* ProCore import has to be deleted for UWP builds! Will not run otherwise.

## Game Info
* HomePage is the parent scene and also contains the level select and shop menus.
* Levels aim to be of similar total length (except Tutorial, obviously)
  * Over 100 total par (typical airway clearance session = 10 sets of 10 huffs)
  * Amount of holes is irrelevant, though each level should have about 10 coins (1/set)
  * Aim for a mix of challenging and easy, long and short holes. Avoid blind spots.
  * Remember, users would rather continue playing than have the game cut short!
  * DON'T PUNISH THE PLAYERS FOR INACTIVITY

## Developer notes
* Before making a build:
  * Some imports mess with the Unity Build process (eg avoid TextMeshPro at all costs)
  * Make sure `ProCore` folder under `_Imports` is deleted when building for Windows Store.
  * Make sure gameObjects which are cloned/instantiated are NOT ProCore objects!
  * Remove ALL "static" flags, they mess with the Unity lightmaps and make exports super slow.
* When adding levels:
  * Existing `LevelContent` child classes (such as `Woods`) should be used as template.
  * Ensure the Unity scene has an empty LevelContent gameObject containing the child class script.
  * The LvlHUD prefab gameObjects need to be referenced in the LevelContent gameObject. 
* Adding shop items need changes in several places:
  * GameObjects need to be referenced in the SaveManager object in Unity
  * The `HomePage` script contains an array of prices, expand as necessary.
  * Colour and Trail item buttons take their colour from the SaveManager array.
  * Trails and Hats are cloned on player spawn from the SaveManager array, make sure to ref!
  
## Credits
* All music/audio used is copywrite free or with permission:
  * Menu music by Yoji Maru (https://soundcloud.com/yoji-maru/cassual-game-music)
  * Woods audio by BerlinAtmosphere (https://youtu.be/Z9IoMarxQtY)
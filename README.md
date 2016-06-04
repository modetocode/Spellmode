#Spellmode (Hero Adventures)
# 0. Getting started
This project **Spellmode (Hero Adventures)** was made with Unity 5.3.3 using C# language for scripting. After cloning of the repository the contents has to be opened as a project in Unity.  In Unity Editor, the game can be started only from the login scene located in **Scenes/Login.unity**. 

This is a demo game that was created in a 2 months time. Have fun, enjoy the game and feel free to write to me about any suggestions about the code structure or the game. 

The application was published on the Play Store. [Link to app](https://play.google.com/store/apps/details?id=com.ModeToCode.HeroAdventures)


#1. Architecture
##1.1 Overview
The game Hero Adventures consists of the following features: 
 - Login - handles login and loading the gamestate of the players
 - Level Select - displays the available levels, allowing the player to choose which level he wants to play.
 - Level Run - executes the run for the selected level and handles player input.
 - Shop - displays a list of the available heroes and upgrades with the ability to purchaise them.
   
A variation of MVC pattern was used for the Level Run feature of the game:
 - **LevelRunModel** (Model) - stores the data for the level run 
 - **LevelRunManager** (Controller) - updates the model, knows how to execute the logic flow of the level run
 - **LevelRunComponent** (Controller/View) - keeps and executes an instance of LevelRunManager, responsible for all the components that shows game objects in the scene based on the logic objects, handles input 
 - **LevelRunGUIComponent** (View) - responsible for displaying the gui in the scene, gets the data from the model, player input is passed to the controller  
 
One thing that should be noted here is that the LevelRunManager can execute the whole run without the need of the LevelRunComponent.
The manager handles the logic flow of the game by creating logic objects that can live without visual objects (Game Objects).
There is one exeption to this statement, as the collision detection of bullets with units, and units with loot items is done using Unity Colliders that are 
found inside Game Objects, which was done because of easier developent.
In any case, the point is that each run can be executed independent of any visual representation. 
This can help in creating automated bots and tests for level run, avoiding rendering of the objects in the scene.  

Following the logic from presentation separation, all of the scripts that are created for presentational objects (Game Objects) have a suffix "Component" 
in the name and they are initialized with a logic object of the appropriate type. For example, for each bullet 
a new logic object is created and then a Game Object having a BulletComponent script is initialized with that object.
When an object needs to be destroyed only the visual representation is destoyed. This was applied also for all the units and loot items in the game.

All the logic objects that have time-dependant logic are tickable objects which are executed by a ticker.
This concept really helped in stopping some objects from moving (bullets, units) when the game is paused.
The ticking of objects can also help in speeding up/slowing down the whole game making it independent from 
the Unity Update and FixedUpdate methods. The presentational objects are just following the positions of the
logic objects hence they don't need any change when the game speed is increased/decreased.

#2. Gameplay
All of the game variables that affect the gameplay are tweakable via the Unity Inspector. Everything connected to the progression and gameplay
can be tweaked by a person which is preferably a game designer, and doesn't need to be a developer.

There is a template for defining new level in the game so by just filling up the template a new level can be added to the game. Here is the actual level template for level 3 of the game
(an editor script was created to achieve this): 

![Level Template](/Development screenshots/level_design.JPG)

All of the other tweakable data can be found in the Game Mechanics Menu in Unity:

![Game Mechanics Menu](/Development screenshots/game_mechanics_menu.jpg)

For each unit the following data is specified (base unit data + weapon data):

![Unit Progression Data](/Development screenshots/unit_progression_data.JPG)
![Unit Weapon Progression Data](/Development screenshots/unit_weapon_progression_data.JPG)
 
 
The loot which each unit gives on kill can be specified in the loot table

![Unit Progression Data](/Development screenshots/loot_table.JPG)

#3. Optimizations
##3.1. Object pool
Object pools were introduced for the objects that are dynamically instantiated multiple times during a level run.
That includes units, bullets and loot items.
A pool container was defined so when new unit type or loot type is added in the game a new pool will be automatically added for them.
 
##3.2. Vieweable distance
All of the objects that need to be shown in the level run (units, loot items) are shown and instantiated only when they are in viewable range when the player really 
needs to see them. When the objects pass the screen and are no longer needed they are no longer rendered.
The instantiation of the objects (fetching from pool) is done by the spawner when the player has reached the trigger point based on his progress in the level.
The objects are destoyed (returned to pool) by a destroyer component that follows the hero and have a collider that detects when an object is out of the screen.

##3.3. UI 
All of the sprites that are used for the UI are stored in atlases in order the same material to be used and hence to reduce the number of draw calls.
There are sprites for which 9-grid is defined and smaller images are used in order to reduce the texture size.

# 4. Other 
##4.1. Player Game Data
The player game data currently is stored on the device and contains data about the list of heroes that the player owns, gold amount and information if the tutorials are completed.
The file is serialized on disk when the game is saved, and deserialized when the game needs to be loaded.

##4.2. Unit animations
For all units a base animator controller is defined that shows animations based on the state of the object. 
Then the base controller is used and for every unit a animator override controller is defined containing the specific animation of the unit.
The following packs were used for the models and animations:

Melee units: https://www.assetstore.unity3d.com/en/#!/content/18030

Archer units: https://www.assetstore.unity3d.com/en/#!/content/18748

##4.3. UI##
The UI was done using the standard Unity UI with some additional added components. 
All of the screens were designed to be mobile-friendly and to be viewable on wide-range of devices.
Layouting was used to achieve best results for different devices.

The following additional UI components were added:
- Message Popup: A popup that displays a info message for the player.
- Yes/no popup: A popup that displays a message and has two buttons: yes and no.
- Scrollable tabs addon: An addon that can display scrollable content shown in tabs and swipeable by the player.
 
The following GUI pack was used for the UI components:
https://www.assetstore.unity3d.com/en/#!/content/17387

##4.4. Events##
To reduce the dependencies in the project and to properly execute the logic flow between components, C# events were introduced in the code.
Additional attention was given for every object subscribed to an event, so it will be unsubscribed when the appropriate object is destroyed to avoid memory leaks. 

##4.5. Constants##
All the global constants that are needed in the code of the project is stored in the Constants file.
The following things are included there: name of the all of the animation parameters, input names, scene names, predefined message strings, etc.

##4.6 Parallax scrolling background##
To achieve the background effect in the level run multiple seamless textures were used, and by tweaking the tiling offset the moving effect is achieved.
Because of the different resolutions and multiple devices that this game can be played each of the textures were resized in order to keep the aspect ratio and to adapt to the screen size.

Acknowledgement for the author of the textures: Designed by Freepik (http://www.freepik.com)

# 5. Licence

The licence info is shown in the file [LICENSE.txt](/LICENSE.txt)

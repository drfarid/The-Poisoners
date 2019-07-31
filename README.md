# The-Poisoners
Repository for building the Poison King

## Creators
Thomas Farid - tfarid3@gatech.edu, tfarid3
Robert Martin - robartin@gatech.edu, robartin
Maura Gerke - mgerke@gatech.edu, mgerke

## Overview
You are Mithridates in exile and must scavenge the land for different poisons and test them on yourself. Be careful though, other characters are in the same position as you, and rather than test their poisons on themselves they might just test them on you to get your items and the cure. 

Enter different worlds and experience strange physical effects and fight mythical monsters in your hallucinatory poisoned states. Find the ultimate antidote by collecting all the rarest poisons before others do and become the Poison King.

## Gameplay Instructions
1. Play tutorial and learn how to play
2. Play forest level and mix hallucination potion
    a. Make the strength potion and knock down the tallest tree
3. Beat the boar in the dream world and collect the gem
4. Play desert level and mix hallucination potion
    a. Make the speed potion and dig in the desert
5. Beat the demon in the dream world and collect the gem
6. Mix the final gems and win the game!

### Controls
Directional keys - move character
F - Pickup item
M - Mixing menu
Ctrl - Attack enemy
R - Throw fireball
Space - Dig
T - Pause menu

## Meeting Requirements
Game Feel
    - Goal: make potions and beat monsters in dream world
    - The crow communicates your progress as well as next steps
    - The start menu gives you a history of Mithridates and two play options
    - After you die you are taken to a death scene where you can quit or retry
    - Third person camera

Precursors
    - Goals and sub goals are dynamically communicated by the crow
    - Players have options and actions have consequences
        - They can throw fireballs or fight with fists
        - They can run fast through guards
        - They can take health potions to mend damage
        - If you used your flower for a speed boost you can't use it for the hallucination
    
3D Character
    - character movement is based on root motion and a forward blend tree
    - there are digging, throwing, fighting, flying, picking up animations for the player
    - press shift key to run, release it to walk while moving

3D World with Physics
    - world is an island based on several biomes that a player interacts with at different times
    - music plays in the background and interactions are accented with sounds
    - Enemies follow you around and you can throw fireballs at them from far away
    - The second boss also throws projectiles at you
        - You can bounce off the projectiles with your own  
    - When you are a giant, you can knock down one of the trees
    - When you are in the desert, you can dig through layers to find a hidden gem

Real Time NPC's
    - AI has multiple states of behavior: guarding object, chasing player, attacking player
    - uses Root motion and a navmesh to allow for path planning.
    - enemies are dangerous if gathered around you, fight one at a time

Polish
    - Game has a start menu with a quick history lesson about Mithridates
        - Game is based off a historical character
    - Colors have been picked to communicate a clear aesthetic of being lost among the elements, on a journey
    - Original music sets the scene for each level.

### Walk-through Instructions

1. Tutorial:
    a. Make sure to follow the instructions on the screen
    b. Learn the controls and movement

2. Forest:
    a. Read the crows suggestions
    b. Learn how to take a special potion with a useful effect

3. First dream: 
    a. Run away from the monster
    b. Fire fireballs with R at it
    c. Attack it from close up with CTRL

4. Desert:
    a. Read the crows suggestions
    b. Beat the guards to get the flowers
        i. Or steal them with stealth
    c. Make a speed potion and take it near the flat sand
    d. Take the speed potion so you can dig faster than they can kill you
    e. Combine the gem the flower and rock and make the next hallucination

5. Second dream:
    a. Run towards the enemy at an angle
    b. Attack with CTRL or go far away and throw fireballs

6. Win Scene:
    a. Mix the gems
    b. Drink the potion
    c. You win!


## Known Bugs
The one thing that we were not able to address were some quirks with the NPC behaviors, minor drifting and movement tilting were persistent.
They are not too noticeable, so we kept them in given our time constraint.

## External Credits
### Models/Animation
- Wizard Character - []
- Boar Dragon - Dungeon Mason[https://assetstore.unity.com/packages/3d/characters/creatures/dragon-the-terror-bringer-and-dragon-boar-77121]
- Lowpoly Trees and Rocks - Grayroad Studios[https://assetstore.unity.com/packages/3d/vegetation/lowpoly-trees-and-rocks-88376]
- Lowpoly Foliage - Rad-Coders[https://assetstore.unity.com/packages/3d/vegetation/low-poly-foliage-66638]
- Fireball boss - []
- The skull model, the bucket model, the shovel

### Sound
- Background Audio - custom by Thomas Farid
- Fireball - [https://freesound.org/people/qubodup/sounds/442827/]
- Player hit - [https://freesound.org/people/xtrgamr/sounds/257780/]
- Enemy hit - [https://freesound.org/people/johnfolker/sounds/269232/]
- Crow squak - [https://freesound.org/people/neufv/sounds/54973/]


### Tools
- GameObject Brush - Kellojo[https://assetstore.unity.com/packages/tools/utilities/gameobject-brush-118135]
- Easy Poly Map Creator - Game&AI Programmer[https://assetstore.unity.com/packages/tools/terrain/easy-poly-map-creator-custom-your-lowpoly-world-87682]

## Work Breakdown

Thomas Farid
    - Inventory, item collection, mixing, second boss scene, winning scene, death scene, music, desert scene, npc guards, projectile firing

Robert Martin
    - Character model, animations, forest scene, tree knockdown, picking up objects, messaging system for game moments

Maura Gerke
    - The whole world model, all the trees and the details in the landscape and the first hallucination scene, the crow companion events, sound effects and background audio event system

### Scenes To Open In Unity
- Begin: open the scene MainMenu.unity or tutorial.unity
- Level 1: forest.unity and hallucination.unity
- Level 2: the-desert.unity and hallucination2.unity
- End: win-scene.unity or death-scene.unity

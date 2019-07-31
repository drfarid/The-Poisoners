Thomas Farid - tfarid3@gatech.edu, tfarid3
Robert Martin - robartin@gatech.edu, robartin
Maura Gerke - mgerke@gatech.edu, mgerke

§ Gameplay instructions

1. Play tutorial and learn how to play
2. Play forest level and mix hallucination potion
	a. Make the strength potion and knock down the tallest tree
3. Beat the boar in the dream world and collect the gem
4. Play desert level and mix hallucination potion
	a. Make the speed potion and dig in the desert
5. Beat the demon in the dream world and collect the gem
6. Mix the final gems and win the game!


§ Detailing how your game meets the requirements of the rubric

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

• Include walk-through instructions on how to observe scene

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

§ Detail any deficiencies or known bugs

The one thing that we were not able to address were some quirks with the NPC behaviors, minor drifting and movement tilting were persistent.
They are not too noticeable, so we kept them in given our time constraint.

§ External resources used (should match with in-game credits)

-Eggs, mushrooms, gems, potions, flowers, rocks, honey etc
-The wizard is an asset
-The skull model, the bucket model, the boss models, the shovel


§ Who did what

Thomas Farid
	- Inventory, item collection, mixing, second boss scene, winning scene, death scene, music, desert scene, npc guards, projectile firing

Robert Martin
	- Character model, animations, forest scene, tree knockdown, picking up objects, messaging system for game moments

Maura Gerke
	- The whole world model was built by Maura, she set all the trees and the details in the landscape also the first hallucination scene.


§ What scene(s) to open in Unity
	- 0. main_menu
	- 1. tutorial
	- 2. forest
	- 3. hallucination
	- 4. the_desert
	- 5. hallucination2
	- 6. win_scene
	- 7. death_scene
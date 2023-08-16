# Crazy Nanny

## Introduction

This is a game named “Crazy Nanny”, a single-player house cleaning and management 3D simulation game. This is an exciting and engaging 3D game. Players will experience house-keeping challenges, including cleaning the house, taking care of babies, and cooking dishes. In this game, players will embark on a solo journey and act as a nanny, improving their time management and multitasking skills, exploring diverse environments and handling different kinds of challenges. “Crazy Nanny” aims to provide a rewarding and engaging experience for players who prefer a single-player gaming experience.  

## Author

TeamBUFF

- Ameer Al-Haddi implemented Dog AI in the garden.
  Ameer implemented the assets inlcluding the dog, flowers, grassland in the garden. The script he created is "DogAI.cs".
- Bin He implemented the kitchen, laundry and their delivery systems; Time Countdown and Game Over system; Overall game scene initialization.
  - Design both delivery systems, Kitchen layout, player interaction and appliance animations in kitchen and laundry room;
  - Game timer, start countdown and game over design;
  - The scripts he created are Kitchen related ("BaseCounter.cs", "Container.cs", "CounterAnimation.cs", "EmptyCounter.cs", "InterfaceKitchenObj.cs", "KitchenObject.cs", "PlateCompleteVisual.cs", "Stove.cs", "TrashBin.cs", "ScriptableObjects.cs"), laundry related ("ClothCleanedWard.cs", "ClothManager.cs"), their delivery system ("ProgressBar.cs", "BurgerManager.cs", "GameWorker.cs") and all the other game related ("Timer.cs", "Player.cs",  "GameOver.cs", "GameStartCountDownUI.cs")
- Yinghai Yu implemented trash generation and trash cleaning interactions. 
  - setup of the street view and part of the garden view.
  - task icon counter - mainly includes the trash and the key icon counters
  - nanny's pickup animation and logic setup.
  - He implemented the asset including trash. The scripts he created are "GamePlayerController.cs", "TrashGenerator.cs", "KeyGenerator.cs", "Player.cs".
- Yuankun Zeng
  - Implementations of the baby AI and the teenager AI.
  - Setup of the baby room setup and interactions.
  - Implementation of conversation logics for the options that player can interact with the teenager AI and the setup conversation UI.
  - Scoring for the releveant interactions with the baby AI and the teenager AI.
  - He implemented the assets including the baby AI, the teenager AI, the baby room setup, Dialogu Canvas with Panel and Text. The scripts he created inlcude "BabyActions.cs", "DialogueManager.cs", "Teenager.cs".
 
- Kaidi Zhang:
  - setup of the living room, the laundry room, the game room, and the restaurant. 
  - the map + instruction menu + task bar + other adjustments on canvas.
  - furniture related prefabs, doors, the nanny + nanny actions + nanny animations, background music, etc.
  - The scripts include "DoubleDoorController.cs", "SingleDoorController.cs", "Player.cs", "DogAI.cs", and other scripts related to scoring.

## Game Instructions

### General Actions

1. Use `arrow` keys to control the movement of the character
2. Press `esc` key to open the menu. From here, you can view the map and general instructions
3. Press the `Space` key to open the door, pick up food/baby/trash/keys, talk with the teenager in living room, etc.
4. Press `D` to pet the dog, drop the baby, continue conversation with the teenage boy

### Goal of the Game

There will be many housekeeping tasks (available tasks are shown on the task bar above the screen); completing different tasks will earn different amount of coins. The player will take on the role of a nanny, and they should adjust their strategies for different tasks and aim to earn as much coin as possible in limited amount of time. 

### Find Keys and Open Doors

When game starts, the player will be first asked to find a blue key and a black key to open the blue and black doors in the game, respectively. The keys may be hidden around the street or bushes in front of the main door. Once the keys are found, press the `Space` key to pick up a key on the ground. Keys will be randomly generated on the street or next to the bushes and avoid being hidden in trees.

### Kitchen

When interacting with kitchen appliances, the player can pick up or interact with various items by pressing the the `Space` key.

1. Pick up meat from the refrigerator.
2. Pick up plates from the kitchen sink.
3. Pick up bread from the bread counter (with a bread toaster on top of it).
4. Pick up shredded lettuce from the counter (with a red cutter on top of it).
   All these counters are colored in grey to create a cohesive visual theme.

To add an element of liveliness, instead of using icons, interesting objects on the counters are used as representations for the items.

The stove is used to cook the meat. Press the space button to place the uncooked meat on the pan when the player is holding it. The meat has different states: uncooked, fried, and burned (By default, the cooking process from uncooked to fried takes 10 seconds, and from fried to burned takes 5 seconds. It should be picked up from the pan before it gets burned. Uncooked or burned meat cannot be used to make burgers.

Delivering burgers to guests on second floor will earn coins. The player should aim to earn as much coin as possible.

> Hint:
> To make a finished burger, pick up a plate first and place it on an empty blue counter. A finished burger consists of bread, shredded lettuce, and cooked meat.

Finally, once the player finishes preparing a burger, the player can deliver the burger to guests waiting in the restaurant area. 50 points will be rewarded for successfully making and delivering a burger.

### Baby Room (AI)

There is an AI kid in this room with two states: wandering around and laying on the bed. The main idea is to pick up the kid and place him/her on the bed as soon as possible. The kid will go back to wandering on the ground 100 seconds after the kid is placed on the bed. 

Actions involved in the bedroom scene:

1. Pick up the kid when the player gets close to the kid using the `Space` key.
2. Carry the kid and get close to the bed and drop the kid on the bed using the `D` key. 
3. After 100 seconds the kid is placed on the bed, the kid will go back to the ground and start wandering.
4. The baby will cry after wandering within the baby room for about 90 seconds and this will make come with baby crying sound. Player needs to pick up the baby and place on the bed so the baby will stop crying. Another way to stop the baby from crying is to interact with the teenager and ask him to take care of the baby.

Sucessfully dropping the baby will earn 5 points. If the player interacts with the teenager first and asks him to take care of the baby, all the baby dropping interactions will double the original points (2 x 5 = 10 points) afterwards.

### Master Bedroom + Laundry Room

The player will pick up laundry in a small wardrobe in the master bedroom, put the laundry into the washer in the laundry room upstairs, and then hang the clean laundry inside another wardrobe in the master bedroom. It takes five seconds for the washer to finish. The player can only take one laundry at a time. If the player is holding extra laundry, they can throw it away inside the laundry basket in the laundry room. The player will interact with everything using the `Space` key. Upon finishing this task, 15 points will be awarded.

### Living Room

There is an AI teenager in the living room with three states: walking, standing and talking, and guiding the player to baby room. Player should press the `Space` key to talk with him, press the `D` key to continue the conversation, and press number keys to select different options and earn some hidden coins. Talking with teenage boy before taking care of the baby will also allow the player to earn extra coins by babysitting.

There are also randomly generated trash on the floor. By pressing the `Space` key to pick them up, players will also earn 1 coin. The trash will be generated throughout the whole game.

### Garden

There is an AI dog in the garden with two states: walking, standing, barking. The dog will walk and stay on a random schedule; when it gets lonely, it will shift to barking state and the player needs to go pet him by pressing the `D` key; otherwise it will continue barking.

## References:

1. A very good tutorial for Unity beginner: Recommended!
   https://www.youtube.com/watch?v=AmGSEH7QcDg&t=4511s
2. CountDown: https://www.youtube.com/watch?v=JivuXdrIHK0
3. Pick up and drop objects: https://www.youtube.com/watch?v=YlB9BlRIryk&t=638s

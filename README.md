# Chest System (Clash Royale)
 - A Chest System Simulation.
 - Clone of Clash Royale's chest system.
 - **Play** : [Play](https://brickster241.itch.io/clash-royale-chest-system)
 - **Demo** : [Demo](https://www.linkedin.com/posts/ashish-verma-4552b9265_gamedevelopment-gamedesign-programming-activity-7059631116655198208-DSHn?utm_source=share&utm_medium=member_desktop)
 
![Chest-System](https://user-images.githubusercontent.com/65897987/236028240-fb411ac5-e571-4c79-9364-d6b4edb95f3b.png)

### Gameplay Features : 
 - Exploring Chests has a cost. Checks for conditions when COINS / GEMS are not enough.
 - Queue Limit : Can only queue 2 chests unlocking at a time. 
 - Unlock Now  : Chests , while unlocking can be unlocked instantly using Gems.

### Coding Features Implemented : 
 - **ScriptableObjects** : Nested Scriptable Objects for different types of Chest Configurations (Common, Mini, Rare, Legendary).
 - **MVC Pattern**       : Implemented for Chest MVC.
 - **State Pattern**     : Customized State Machine for different Chest States (LOCKED, QUEUED, UNLOCKING, OPEN)
 - **Object Pooling**    : Added ChestPool for Chest Gameobjects.
 - **Services**          : Communicate with each other to perform operations. e.g AudioService, EventService, ChestService, PopupService, etc.




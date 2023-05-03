# Chest System (Clash Royale)
 - A Chest System Simulation.
 - Clone of Clash Royale's chest system.
 

### Gameplay Features : 
 - Exploring Chests has a cost. Checks for conditions when COINS / GEMS are not enough.
 - Queue Limit : Can only queue 2 chests unlocking at a time. 
 - Unlock Now  : Chests , while unlocking can be unlocked instantly using Gems.

### Coding Features Implemented : 
 - ScriptableObjects : Nested Scriptable Objects for different types of Chest Configurations.
 - MVC Pattern       : Implemented for Chest MVC.
 - State Pattern     : Customized State Machine for different Chest States (LOCKED, QUEUED, UNLOCKING, OPEN)
 - Object Pooling    : Added ChestPool for Chest Gameobjects.
 - Services          : Communicate with each other to perform operations. e.g AudioService, EventService, ChestService, PopupService, etc.


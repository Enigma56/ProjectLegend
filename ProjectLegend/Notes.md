# Issues
1. If parameter is not passed into function, the 
    action cannot be set.

``void MainLoop(Player player){}``

``Action<Player> PrimaryGameLoop = MainLoop;``
- Implement the Command Design Pattern to solve this problem

4. No way to test methods, only functional testing which is not adequate
--> This is very important, do this next
---
**What can be made into a library?**
1. 

---
**What can be turned into a design pattern?**

### Player as a singleton

The player can be a singleton with the drawback that enemies and players derive from different objects
1. Turn the player class into a singleton. Two ways to do this:
   1. Create a static class of each legend type that gets initialized at game start
   2. Assign functions to general player singleton when a specific legend gets chosen

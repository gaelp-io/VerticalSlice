# GDIM33 Vertical Slice
## Milestone 1 Devlog
1. One visual scripting graph in my game is the player's script machine which handles the smooth movement. I used the On Input System Event Vector 2 in order to make my movement alongside the inputs of a keyboard (WASD). Orginally I used transform translate however this led to jittery/vibrating collisions with the invisible borders in my game. In order to prevent this I went with rigidbody 2D nodes instead of transform translates. At first I wasn't having trouble and actually got smoother movement without any collision issues. However, I had realized that the player wouldn't stop when the keys were released because the logic in the rigidbody 2D node was to set it's velocity. So even though the On Input System Vector 2 node was set to On Hold, the player would keep moving in whichever direction (or key) they last pressed. After some trial and error I tried a solution where I would have two On Input System Event Vector 2 nodes where one handled the movement I had alreayd and the second would solely be connected to a single node that would set the Rigidbody 2D velocity back to 0,0 which effectively would stop movement. I realized this as a solution because the On Released chocie for the On Input System Event Vector 2 would stop movement whenever a key was released which is exactly what I was looking for. Then, the actual movement nodes started at the On Hold, On Input System Event Vector 2 node where I would set the vector 2 value into a get x and get y node, then multiple those two nodes with their own get variable (for speed) and then input those outcomes into a create vector 2 node in order to be able to input that new value into the set rigidbody 2D velocity which would create the smooth movement for the player.

2. [2D Racing Game Break-Down.pdf](https://github.com/user-attachments/files/27150924/2D.Racing.Game.Break-Down.pdf)


## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- [Car Sprites](https://agusstt.itch.io/pixel-art-car-sprite)
- [Track Sprites](https://amcaricola.itch.io/car-street-and-monster-for-scrolling-game)
- [Environment Sprites](https://assetstore.unity.com/packages/2d/environments/pixel-art-top-down-basic-cainos-187605)
- [Pixel Font](https://www.fontsquirrel.com/fonts/list/classification/pixel)
- [Rain Particles](https://assetstore.unity.com/packages/vfx/particles/rain-particles-351846)

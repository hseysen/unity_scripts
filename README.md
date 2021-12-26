# Unity Scripts

These are the C# scripts that I've written for personal projects, I'm just putting them here so that it's easier to find them in the future. Below is a detailed explanation of each script.

## MoveObjects.cs
I've usually attached this [script](https://github.com/hseysen/unity_scripts/blob/master/MoveObjects.cs) to my camera or the empty game object that has the main camera as one of its children. By default, it only allows you to move the Game Object along the XZ plane and lifts the Game Object to `y=1` when you're dragging, and `y=0` when you let go. These parts should be customized. Additionally, the objects that you wish to be draggable need to be tagged as `draggable` and have a Layer Mask `draggable` (perhaps these could be changed by modifying the code).

## Swipe.cs
[The script](https://github.com/hseysen/unity_scripts/blob/master/Swipe.cs) should be attached to the Game Object that requires the swipe input. In another script, access this "Swipe" component and get the inputs. Works with both mouse clicks and mobile touch input.
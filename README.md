# Unity Scripts

These are the C# scripts that I've written for personal projects, I'm just putting them here so that it's easier to find them in the future. Below is a detailed explanation of each script.

## FirstPersonPlayerController.cs
I've usually attached this [script](https://github.com/hseysen/unity_scripts/blob/master/FirstPersonPlayerController.cs) to a GameObject that represents my player. Doesn't use Rigidbody component for handling movement. Pretty handy at times.

## GameManager.cs
This [script](https://github.com/hseysen/unity_scripts/blob/master/GameManager.cs) is a general GameManager script. The instances are singleton, and only need to be instanced in the first scene of the build settings. Won't get destroyed in scene transitions.

## InteractableObject.cs
This [script](https://github.com/hseysen/unity_scripts/blob/master/InteractableObject.cs) is just a base class for all interactable objects to derive from. The [SelectionManager](https://github.com/hseysen/unity_scripts/blob/master/SelectionManager.cs) can interact with the objects with this component, as long as they have the proper tag set up in the inspector. Derived `InteractableObject` classes should override the existing `Interact()` virtual method.

## MoveObjects.cs
I've usually attached this [script](https://github.com/hseysen/unity_scripts/blob/master/MoveObjects.cs) to my camera or the empty game object that has the main camera as one of its children. By default, it only allows you to move the Game Object along the XZ plane and lifts the Game Object to `y=1` when you're dragging, and `y=0` when you let go. These parts should be customized. Additionally, the objects that you wish to be draggable need to be tagged as `draggable` and have a Layer Mask `draggable` (perhaps these could be changed by modifying the code).

## SelectionManager.cs
Attach this [script](https://github.com/hseysen/unity_scripts/blob/master/SelectionManager.cs) to the player object. The objects which you want to interact with should have the proper tag set up in the inspector, as well as have the [InteractableObject](https://github.com/hseysen/unity_scripts/blob/master/InteractableObject.cs) component.

## Swipe.cs
[The script](https://github.com/hseysen/unity_scripts/blob/master/Swipe.cs) should be attached to the Game Object that requires the swipe input. In another script, access this "Swipe" component and get the inputs. Works with both mouse clicks and mobile touch input.
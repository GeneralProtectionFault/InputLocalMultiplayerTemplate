# InputLocalMultiplayerTemplate
Template for selection screen for using multiple (local) cursor icons using gamepads and the new input system.

*** This project has not been thoroughly as yet, this should be a good start, though ***

This is a Unity project using the New Input System package.
Many people, myself included as much as anyone, have had a difficult time with local multiplayer in the new input system.

This is a template selection screen, in which any Gamepad or Joystick input is listened for, and will spawn an appropriate cursor with a PlayerInput component.
This component is how the new input system recognizes a player has joined.

Pressing the submit button on the gamepad (adjustable in the action maps, of course) will lock the movement of the cursor and place the game object that is BEHIND
the cursor into a variable in the CursorBehavior script.  On loading the following scene, this object can either be stored in a persistent variable or instantiated,
if the next scene is the gameplay.  The PlayerInput component will be kept and attached to this object to maintain the player/object relationship.

This is all done in Screen Space (camera) in order that the game objects being selected can feasibly be placed in the selection area without absurd scaling.
Ideally, the actual prefab that will be used for the player in the gameplay scene(s) can be used.

Currently, by the same token, the selection is done with raycasting and a game object.  While it happens to be in the canvas and is a UI Image, this approach
is used because of limitations.  The current VirtualMouseInput that the new input system offers does not interface with the PlayerInput component, so
it doesn't lend itself well to having separate instances that can be manipulated by different controllers (at least as far as my expertise allows).

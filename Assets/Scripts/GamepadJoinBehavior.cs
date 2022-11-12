using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class GamepadJoinBehavior : MonoBehaviour
{
    [SerializeField] Canvas currentCanvas;
    public int numberOfActivePlayers { get; private set; } = 0;






    // Start is called before the first frame update
    void Start()
    {
        // This subscribes us to events that will fire if any button is pressed.  We'll most certainly want to throw this away 
        // when not in a selection screen (performance intensive)!
        var myAction = new InputAction(binding: "/*/<button>");
        myAction.performed += (action) => 
        {
            //UnityEngine.Debug.Log(Gamepad.current.description.deviceClass);
            //UnityEngine.Debug.Log(action.control.device.description);
            AddPlayer (action.control.device);   
        };
        myAction.Enable();
    }




 
 
    void AddPlayer(InputDevice device)
    {
        // Avoid running if the device is already paired to a player
        foreach (var player in PlayerInput.all)
        {
            foreach (var playerDevice in player.devices)
            {
                if (device == playerDevice)
                {
                    return;
                }
            }            
        }

        //UnityEngine.Debug.Log(device.device);
        

        // Don't execute if not a gamepad or joystick
        if (!device.displayName.Contains("Controller") && !device.displayName.Contains("Joystick") && !device.displayName.Contains("Gamepad"))
            return;

        var playerNumberToAdd = PlayerInput.all.Count + 1;

        string controlScheme = "";

        // The inclusion of a Gamepad & Joystick scheme here is due to the potential for cheap/whacky
        // controllers to be picked up as joysticks (D-pads detected as HATs or what-not)
        if (device.displayName.Contains("Controller") || device.displayName.Contains("Gamepad"))
            controlScheme = "Gamepad";
        else if (device.displayName.Contains("Joystick"))
            controlScheme = "Joystick";

        // *** Note this utilizes the NAME of the cursor prefabs to associate the player/player # ***
        GameObject playerCursor = Resources.Load<GameObject>($"CursorPrefabs/P{playerNumberToAdd}_Cursor");


        if (!playerCursor.activeInHierarchy)
        {
            // This creates the PlayerInput component.
            // In Unity's new input system, the creation of this component is what defines the existence of the "player"
            PlayerInput theCursor = PlayerInput.Instantiate(playerCursor, -1, controlScheme, -1, device);
            theCursor.transform.parent = currentCanvas.transform;
            theCursor.transform.localScale = new Vector3 (1f, 1f, 1f);
        }

    }



    public void OnPlayerJoin(PlayerInput input)
    {
        numberOfActivePlayers = PlayerInput.all.Count;
        UnityEngine.Debug.Log("There are currently " + numberOfActivePlayers + " players.");
    }

    public void OnPlayerLeft(PlayerInput input)
    {
        numberOfActivePlayers = PlayerInput.all.Count;
        UnityEngine.Debug.Log("There are currently " + numberOfActivePlayers + " players.");
    }
  
}

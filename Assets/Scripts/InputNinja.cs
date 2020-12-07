using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputNinja : MonoBehaviour
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
            // Debug.Log($"{action.control.device}");

            // Put logic HERE to check how many players exist (if any), and pass the controller to be bound
            // Get TYPE of controller - IMPORTANT! 
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

        // UnityEngine.Debug.Log(device.displayName);

        // Don't execute if not a gamepad or joystick
        if (!device.displayName.Contains("Controller") && !device.displayName.Contains("Joystick") && !device.displayName.Contains("Gamepad"))
            return;

        var playerNumberToAdd = numberOfActivePlayers + 1;

        string controlScheme = "";

        if (device.displayName.Contains("Controller") || device.displayName.Contains("Gamepad"))
            controlScheme = "Gamepad";
        else if (device.displayName.Contains("Joystick"))
            controlScheme = "Joystick";

        GameObject playerCursor = Resources.Load<GameObject>($"CursorPrefabs/P{playerNumberToAdd}_Cursor");


        if (!playerCursor.activeInHierarchy)
        {
            PlayerInput theCursor = PlayerInput.Instantiate(playerCursor, -1, controlScheme, -1, device);
            theCursor.transform.parent = currentCanvas.transform;
            
        }

        UnityEngine.Debug.Log("There are currently " + numberOfActivePlayers + " players.");
    }



    public void OnPlayerJoin()
    {
        numberOfActivePlayers = PlayerInput.all.Count;
    }

    public void OnPlayerLeft(PlayerInput input)
    {
        numberOfActivePlayers = PlayerInput.all.Count;
    }
  
}

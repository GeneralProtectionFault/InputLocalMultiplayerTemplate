using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerObjectHandler : MonoBehaviour
{
    // In order to preserve the player's selections AND the controllers the used, and transfer the PlayerInput component
    // from the cursor to the selection for gameplay, we'll use dictionaries to keep track of both, destroy the cursors and
    // selected objects, then reinstantiate

    public static Dictionary<int, InputDevice> playerControllers = new Dictionary<int, InputDevice>();
    public static Dictionary<int, string> playerSelectionNames = new Dictionary<int, string>();
    public static Dictionary<int, string> playerControlSchemes = new Dictionary<int, string>();

    public static bool shouldSpawnSelectedPlayers = false;

    private void OnEnable()
    {
        CursorBehavior.DoneSelectingEvent += PlayersDoneSelecting;
    }

    private void OnDisable()
    {
        CursorBehavior.DoneSelectingEvent -= PlayersDoneSelecting;
    }



    private void PlayersDoneSelecting(object sender, EventArgs e)
    {
        // UnityEngine.Debug.Log("Player done method tripped");


        // First, make sure all players have selected their objects (players/vehicles/etc...)
        GameObject[] playerCursors = GameObject.FindGameObjectsWithTag("PlayerCursor");
        foreach (var cursor in playerCursors)
        {
            if (!cursor.GetComponent<CursorBehavior>().objectSelected)
            {
                UnityEngine.Debug.Log(cursor + " object has not selected a player!");
                return;
            }
        }

        

        // If all have selected, store the object(s)
        for (int i = 0; i < playerCursors.Length; i++)
        {
            var playerInputComponent = playerCursors[i].GetComponent<PlayerInput>();
            var playerSelection = playerCursors[i].GetComponent<CursorBehavior>().playerSelection.name;

            // DontDestroyOnLoad(playerSelection);

            playerControllers.Add(playerInputComponent.playerIndex, playerInputComponent.devices[0]);
            playerSelectionNames.Add(playerInputComponent.playerIndex, playerSelection);
            playerControlSchemes.Add(playerInputComponent.playerIndex, playerInputComponent.currentControlScheme);
        }

        shouldSpawnSelectedPlayers = true;

        SceneManager.LoadScene("GameplayTestScene");
    }
}

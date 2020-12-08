using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectHandler : MonoBehaviour
{
    // These will store the prefabs/objects that the player has selected
    // The reason for using lists is so that we can have an undetermined number of objects that pertain to any given player
    // For example, a racing game, the player picks a character and a car...
    public static List<GameObject> player1Objects = new List<GameObject>();
    public static List<GameObject> player2Objects = new List<GameObject>();
    public static List<GameObject> player3Objects = new List<GameObject>();
    public static List<GameObject> player4Objects = new List<GameObject>();
    public static List<GameObject> player5Objects = new List<GameObject>();
    public static List<GameObject> player6Objects = new List<GameObject>();
    public static List<GameObject> player7Objects = new List<GameObject>();
    public static List<GameObject> player8Objects = new List<GameObject>();

    private static Dictionary<int, List<GameObject>> playerDictionary = new Dictionary<int, List<GameObject>>()
    {
        { 1, player1Objects },
        { 2, player2Objects },
        { 3, player3Objects },
        { 4, player4Objects },
        { 5, player5Objects },
        { 6, player6Objects },
        { 7, player7Objects },
        { 8, player8Objects },
    };


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


        // First, make sure all players have selected their objects
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("PlayerCursor");
        foreach (var cursor in playerObjects)
        {
            // UnityEngine.Debug.Log(cursor.name);

            if (!cursor.GetComponent<CursorBehavior>().objectSelected)
                return;
        }

        // If all have selected, store the object(s)
        foreach (var cursor in playerObjects)
        {
            //UnityEngine.Debug.Log(cursor.GetComponent<PlayerInput>().playerIndex + 1);
            //UnityEngine.Debug.Log(cursor.gameObject.name);

            int playerNumber = Convert.ToInt32((cursor.GetComponent<PlayerInput>().playerIndex + 1).ToString());

            // We're adding the object that the player SELECTED to their list
            playerDictionary[playerNumber].Add(cursor.GetComponent<CursorBehavior>().playerSelection);

            // We'll be needing to transfer the PlayerInput component from the cursor object to the selected object

        }   
    }
}

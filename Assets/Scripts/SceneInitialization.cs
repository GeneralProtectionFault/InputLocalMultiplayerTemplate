﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class SceneInitialization : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneBeginCheck;
    }


    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneBeginCheck;
    }


    private void SceneBeginCheck(Scene fromScene, Scene toScene)
    {

        //foreach (var item in PlayerObjectHandler.playerControlSchemes)
        //{
        //    UnityEngine.Debug.Log(item);
        //}


        if (PlayerObjectHandler.shouldSpawnSelectedPlayers)
        {
            SpawnSelectedPlayers();
            PlayerObjectHandler.shouldSpawnSelectedPlayers = false;
        }
    }



    private void SpawnSelectedPlayers()
    {
        foreach (var player in PlayerObjectHandler.playerControllers)
        {
            var playerController = PlayerObjectHandler.playerControllers[player.Key];
            var playerObjectName = PlayerObjectHandler.playerSelectionNames[player.Key];
            var playerControlScheme = PlayerObjectHandler.playerControlSchemes[player.Key];

            var playerObject = Resources.Load<GameObject>(playerObjectName);
            PlayerInput playerInput = PlayerInput.Instantiate(playerObject, player.Key, playerControlScheme, -1, playerController);

            // Activates the player input component on the prefab we just instantiated
            // We have the component disabled by default, otherwise it could not be a "selectable object" independent of the PlayerInput component on the cursor
            // in the selection screen
            playerObject.GetComponent<PlayerControls>().SetPlayerInputActive(true, playerInput);

            //  *** It seems...that the above Instantiation doesn't exactly work... I'm assuming, because the PlayerInput component on the prefab is starting off
            // disabled, that it...doesn't work.  This code here will force it to keep the device/scheme/etc... that we tried to assign the wretch above!
            var inputUser = playerInput.user;
            playerInput.SwitchCurrentControlScheme(playerControlScheme);
            InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
            // SpawnSelectedPlayers();
            PlayerObjectHandler.shouldSpawnSelectedPlayers = false;
        }
    }

    private void SpawnSelectedPlayers()
    {
        //UnityEngine.Debug.Log("Spawning players!");
        //UnityEngine.Debug.Log(PlayerObjectHandler.playerControllers.Count + " player controllers...");

        foreach (var player in PlayerObjectHandler.playerControllers)
        {
            var playerController = PlayerObjectHandler.playerControllers[player.Key];
            var playerObject = PlayerObjectHandler.playerSelections[player.Key];
            var playerControlScheme = PlayerObjectHandler.playerControlSchemes[player.Key];


            PlayerInput.Instantiate(playerObject, player.Key, playerControlScheme, -1, playerController);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorBehavior : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private float cursorSpeed;

    private float screenEdgeThreshold = .02f;

    public void OnTestButton()
    {
        
        foreach (var player in PlayerInput.all)
        {
            UnityEngine.Debug.Log(player);
        }
    }


    void Update()
    {
        // Don't allow the cursor past the edge of the screen!
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if ((viewportPosition.x < screenEdgeThreshold && movement.x < 0) ||
            (viewportPosition.x > 1 - screenEdgeThreshold && movement.x > 0)||
            (viewportPosition.y < screenEdgeThreshold && movement.y < 0)||
            (viewportPosition.y > 1 - screenEdgeThreshold && movement.y > 0))
            return;

        // UnityEngine.Debug.Log(movement);

        transform.Translate(new Vector3(movement.x, movement.y, 0f) * cursorSpeed);
    }



    public void OnCursorMove(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled)
            movement = context.ReadValue<Vector2>();
        else  // Released button!
            movement = Vector2.zero;
    }
    
}

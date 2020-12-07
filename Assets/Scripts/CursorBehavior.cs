using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorBehavior : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private float cursorSpeed;

    private float screenEdgeThreshold = .02f;

    private bool objectSelected = false;



    void Update()
    {
        // Don't allow the cursor past the edge of the screen!
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if ((viewportPosition.x < screenEdgeThreshold && movement.x < 0) ||
            (viewportPosition.x > 1 - screenEdgeThreshold && movement.x > 0)||
            (viewportPosition.y < screenEdgeThreshold && movement.y < 0)||
            (viewportPosition.y > 1 - screenEdgeThreshold && movement.y > 0))
            return;

        // Moves the cursor
        transform.Translate(new Vector3(movement.x, movement.y, 0f) * cursorSpeed/1000);
    }



    public void OnCursorMove(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled && !objectSelected)
            movement = context.ReadValue<Vector2>();
        else  // Released button!
            movement = Vector2.zero;
    }


    public void OnSelectButton (InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // Debug.DrawRay(transform.position, Vector3.forward, Color.red, 50f);

            if(Physics.Raycast(transform.position, Vector3.forward, 1000f, LayerMask.GetMask("PlayerObjects")))
            {
                UnityEngine.Debug.Log(objectSelected);
                if (!objectSelected)
                {
                    objectSelected = true;
                    return;
                }
            }

            if (objectSelected)
                objectSelected = false;
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler Interaction;
    private PlayerInputActions playerInputActions; //instantiate the class auto-generated

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += obj => InputInteract(obj);
    }

    private void InputInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (Interaction != null) { //see if there is any listener
            Interaction(this, EventArgs.Empty);
        }
    }

    public Vector2 InputMove() {
        //can normalized here, or in Input actions object-processors-normalized
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); 
        return inputVector;
    }

    public Vector2 InputRotate()
    {
        //can normalized here, or in Input actions object-processors-normalized
        Vector2 inputVector = playerInputActions.Player.Rotate.ReadValue<Vector2>();
        return inputVector;
    }
}

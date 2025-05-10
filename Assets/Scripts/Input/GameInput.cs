using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInput;
    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Movement.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInput.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }
}

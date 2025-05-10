using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameInput gameInput;
    public float speed = 1.8f;
    public float turnSpeed = 2f;
    public float rotationSpeed = 5f; 
    private Rigidbody rbPlayer;
    private Animator playerAnimator;

    Vector2 inputVector;
    public static event Action OnPlayerDead;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAnimator.applyRootMotion = true;
        inputVector = new Vector2(0, 0);
    }

    void Update()
    {
        inputVector = gameInput.GetMovementVectorNormalized();
    }
    private void FixedUpdate()
    {

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        playerAnimator.SetBool("isWalking", moveDirection != Vector3.zero);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnAnimatorMove()
    {
        if (playerAnimator && playerAnimator.applyRootMotion)
        {
            if (playerAnimator.GetBool("isWalking"))
            {
                rbPlayer.MovePosition(rbPlayer.position + playerAnimator.deltaPosition);
                Quaternion newRotation = playerAnimator.rootRotation;
                newRotation.x = 0;
                newRotation.z = 0;
                rbPlayer.MoveRotation(newRotation);
            }
        }
    }
}

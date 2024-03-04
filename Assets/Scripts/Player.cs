using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField] Transform[] LaneTransforms;

    Vector3 Destination;

    int CurrentLaneIndex;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
        }
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        playerInput.Gameplay.Move.performed += MovePerformed;
        for (int i = 0; i < LaneTransforms.Length; i++)
        {
            if (LaneTransforms[i].position == transform.position)
            {
                CurrentLaneIndex = i;
                Destination = LaneTransforms[i].position;
            }
        }
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        float InputValue = obj.ReadValue<float>();
        if (InputValue > 0)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
        
    }

    private void MoveLeft()
    {
        if (CurrentLaneIndex == 0)
        {
            return;
        }

        CurrentLaneIndex--;
        Destination = LaneTransforms[CurrentLaneIndex].position;
    }

    private void MoveRight()
    {
        if (CurrentLaneIndex == LaneTransforms.Length - 1)
        {
            return;
        }

        CurrentLaneIndex++;
        Destination = LaneTransforms[CurrentLaneIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Destination;
    }
}

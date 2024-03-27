using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField] Transform[] LaneTransforms;

    [SerializeField] float moveSpeed = 20f;

    [SerializeField] float jumpHeight = 2.5f;

    [SerializeField] Transform groundCheckTransform;
    [SerializeField] [Range(0, 1)] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask GroundCheckMask;
    [SerializeField] Vector3 BlockageCheckHalfExtend;
    [SerializeField] string BlockageCheckTag = "Threat";

    [Header("Audio")]
    [SerializeField] AudioClip jumpAudioClip;
    [SerializeField] AudioClip moveAudioClip;
    [SerializeField] AudioSource actionAudioSource;

    Vector3 Destination;

    [SerializeField] InGameUI playerUI;
    
    int CurrentLaneIndex;

    Animator animator;

    Camera playerCamera;
    Vector3 playerCameraOffset;

    
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
        playerInput.Gameplay.Jump.performed += JumpPerformed;
        playerInput.Menu.Pause.performed += TogglePause;
        for (int i = 0; i < LaneTransforms.Length; i++)
        {
            if (LaneTransforms[i].position == transform.position)
            {
                CurrentLaneIndex = i;
                Destination = LaneTransforms[i].position;
            }
        }

        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
        playerCameraOffset = playerCamera.transform.position - transform.position;

    }

    private void TogglePause(InputAction.CallbackContext obj)
    {
        GameMode gameMode = GamePlayStatics.GetGameMode();
        if (playerInput.Gameplay.enabled)
        {
            playerInput.Gameplay.Disable();
        }
        else
        {
            playerInput.Gameplay.Enable();
        }
        if (gameMode != null && !gameMode.IsGameOver())
        {
            gameMode.TogglePause();
            playerUI.SingnalPause(gameMode.IsGamePaused());
        }
        
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        if (!IsOnGround())
        {
            return;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            float jumpUpSpeed = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
            rb.AddForce(new Vector3(0f, jumpUpSpeed, 0f), ForceMode.VelocityChange);
            actionAudioSource.clip = jumpAudioClip;
            actionAudioSource.Play();
        }
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        if (!IsOnGround())
        {
            return;
        }

        float InputValue = obj.ReadValue<float>();
        int goalIndex = CurrentLaneIndex;
        if (InputValue > 0)
        {
            if (goalIndex == LaneTransforms.Length - 1) return;
            goalIndex++;
        }
        else
        {
            if (CurrentLaneIndex == 0) return;
            goalIndex--;
        }

        Vector3 goalPos = LaneTransforms[goalIndex].position;
        if (GamePlayStatics.isPositionOccupied(goalPos , BlockageCheckHalfExtend , BlockageCheckTag))
        {
            return;
        }
        actionAudioSource.clip = moveAudioClip;
        actionAudioSource.Play();
        CurrentLaneIndex = goalIndex;
        Destination = goalPos;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (!IsOnGround())
        {
            animator.SetBool("isOnGround", false);
        }
        else
        {
            animator.SetBool("isOnGround", true);
        }

        float TransformX = Mathf.Lerp(transform.position.x, Destination.x, Time.deltaTime*moveSpeed);
        transform.position = new Vector3(TransformX, transform.position.y, transform.position.z);
        
        
    }

    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position + playerCameraOffset;
    }

    bool IsOnGround()
    {
        return (Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius , GroundCheckMask));
       
    }
}

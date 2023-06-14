using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerJumping : MonoBehaviour
{
    /*const string PLAYER_JUMP = "Jumping";
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpPressBufferTime = 0.05f;
    [SerializeField] float jumpGroundGraceTime = .2f;

    PlayerMovement Player;

    bool tryingToJump;
    float lastJumpPressTime;
    float lastGroundedTime;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        Player = GetComponent<PlayerMovement>();
    }

    void OnEnable()
    {
        Player.OnBeforeMove += onBeforeMove;
        Player.OnGroundStateChange += OnGroundStateChange;
    }

        void OnDisable()
    {
        Player.OnBeforeMove -= onBeforeMove;
        Player.OnGroundStateChange -= OnGroundStateChange;
    }

    void OnJump()
    {
        tryingToJump = true;
        lastJumpPressTime = Time.time;
    }

    void onBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;
        bool wasGrounded = Time.time - lastGroundedTime < jumpGroundGraceTime;

        bool isOrWasTryingToJump = tryingToJump || (wasTryingToJump && Player.IsGrounded);
        bool isOrWasGrounded = Player.IsGrounded || wasGrounded;
        if(isOrWasTryingToJump && isOrWasGrounded)
        {
            Player.velocity.y += jumpSpeed;
            changeAnimationState(PLAYER_JUMP);
        }
        tryingToJump = false;

    }

    void OnGroundStateChange(bool isGrounded)
    {
        if(!isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }

    void changeAnimationState(string newState)
    {
        if (Player.currentState == newState) return;

        animator.Play(newState);

        Player.currentState = newState;
    }*/
}


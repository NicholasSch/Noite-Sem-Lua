using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class NewMonoBehaviourScript : MonoBehaviour
{
    private enum Directions {UP, DOWN, LEFT, RIGHT}; 

    //Dependencies
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //Data
    private Vector2 moveInput = Vector2.zero;
    private Directions facingDirection = Directions.RIGHT;
    private float moveSpeed = 120f;

    //Animations
    private readonly int animWalkSide = Animator.StringToHash("Anim_Player_Walk_Side");
    private readonly int animIdleSide = Animator.StringToHash("Anim_Player_Idle_Side");
    private readonly int animWalkDown = Animator.StringToHash("Anim_Player_Walk_Down");
    private readonly int animIdleDown = Animator.StringToHash("Anim_Player_Idle_Down");
    private readonly int animWalkUP = Animator.StringToHash("Anim_Player_Walk_UP");
    private readonly int animIdleUP = Animator.StringToHash("Anim_Player_Idle_UP");

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();  

    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
        CalculateFacingDirection();
        UpdateAnimation();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void CalculateFacingDirection ()
    {
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                facingDirection = Directions.RIGHT;
            }
            else if(moveInput.x < 0)
            {
                facingDirection = Directions.LEFT;
            }
        }

        else if (moveInput.y != 0)
        {
            if (moveInput.y > 0)
            {
                facingDirection = Directions.UP;
            }
            else if(moveInput.y < 0)
            {
                facingDirection = Directions.DOWN;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (facingDirection == Directions.LEFT)
        {
            spriteRenderer.flipX = true;
        }
        else if (facingDirection == Directions.RIGHT)
        {
            spriteRenderer.flipX = false;
        }

        bool isMoving = moveInput.SqrMagnitude() > 0;
        int animation = animWalkSide;

        switch(facingDirection)
        {
            case Directions.UP:
                if(isMoving && moveSpeed == 110f)
                {
                    animation = animWalkUP; 
                }
                else
                {
                    animation = animIdleUP; 
                }
                break;
            case Directions.DOWN:
                if(isMoving && moveSpeed == 110f)
                {
                    animation = animWalkDown; 
                }
                else
                {
                    animation = animIdleDown; 
                }
                break;
            case Directions.LEFT:
            case Directions.RIGHT:
            default:
                if(isMoving && moveSpeed == 110f)
                {
                    animation = animWalkSide; 
                }
                else
                {
                    animation = animIdleSide; 
                }
                break;
        }

        animator.CrossFade(animation,0);
    }

}

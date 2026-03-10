using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[SelectionBase]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameObject interactPrompt;
    private enum Directions {UP, DOWN, LEFT, RIGHT}; 

    private IInteractable currentInteractable;
    

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
        if (GameStateManager.CurrentState != GameState.Gameplay)
        {
            rigidBody.linearVelocity = Vector2.zero;
            moveInput = Vector2.zero;
            UpdateAnimation();
            return;
        }
        rigidBody.linearVelocity = moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
        CalculateFacingDirection();
        UpdateAnimation();
    }

    private void LateUpdate()
    {
        if (interactPrompt != null)
            interactPrompt.transform.position = transform.position + Vector3.up * 1.14f;
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
                if(isMoving && moveSpeed == 120f)
                {
                    animation = animWalkUP; 
                }
                else
                {
                    animation = animIdleUP; 
                }
                break;
            case Directions.DOWN:
                if(isMoving && moveSpeed == 120f)
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
                if(isMoving && moveSpeed == 120f)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentInteractable = other.GetComponent<IInteractable>();

        if (currentInteractable != null && GameStateManager.CurrentState == GameState.Gameplay)
            interactPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            currentInteractable = null;
            interactPrompt.SetActive(false);
        }
    }

    private void OnInteract(InputValue value)
    {

        if (value.isPressed && currentInteractable != null)
        {
            currentInteractable.Interact();
            interactPrompt.SetActive(false);
        }
    }
    private void OnClick(InputValue value)
    {
        if (!value.isPressed)
            return;


        switch (GameStateManager.CurrentState)
        {
            case GameState.Dialogue:
                FindFirstObjectByType<DialogueUI>().OnSubmit();
                break;

            case GameState.Letter:
                FindFirstObjectByType<JournalInteractable>().Close();
                break;

            case GameState.Gameplay:
                if (currentInteractable != null)
                    currentInteractable.Interact();
                break;
        }
    }
    private bool DialogueUIActive()
    {
        DialogueUI ui = FindFirstObjectByType<DialogueUI>();
        return ui != null && ui.gameObject.activeInHierarchy;
    }

    private bool LetterIsOpen()
    {
        JournalInteractable journal = FindFirstObjectByType<JournalInteractable>();
        return journal != null && journal.LetterIsOpen();
    }

    public void LookAtTarget(Transform target)
    {
        Vector2 dir = target.position - transform.position;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
                facingDirection = Directions.RIGHT;
            else
                facingDirection = Directions.LEFT;
        }
        else
        {
            if (dir.y > 0)
                facingDirection = Directions.UP;
            else
                facingDirection = Directions.DOWN;
        }

        UpdateAnimation();
    }

    public void ForceFaceUp()
    {
        facingDirection = Directions.UP;
        UpdateAnimation();
    }
    public void ForceFaceDown()
    {
        facingDirection = Directions.DOWN;
        UpdateAnimation();
    }

    public IEnumerator MoveTo(Vector2 targetPosition, float speed = 2f)
    {
        GameStateManager.CurrentState = GameState.Cutscene;

        while (Vector2.Distance(transform.position, targetPosition) > 0.05f)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            moveInput = direction;

            rigidBody.linearVelocity = direction * speed;

            CalculateFacingDirection();
            UpdateAnimation();

            yield return new WaitForFixedUpdate();
        }

        rigidBody.linearVelocity = Vector2.zero;
        moveInput = Vector2.zero;

        UpdateAnimation();

        GameStateManager.CurrentState = GameState.Gameplay;
    }

    
}

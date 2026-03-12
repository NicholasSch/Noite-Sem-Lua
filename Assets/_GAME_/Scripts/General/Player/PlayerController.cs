using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject interactPromptPrefab;
    [SerializeField] private float walkSpeed = 120f;
    [SerializeField] private float runSpeed = 180f;

    private enum Directions
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    //Dependencies
    private IInteractable currentInteractable;
    private GameObject currentInteractPrompt;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //Data
    private Vector2 moveInput = Vector2.zero;
    private Directions facingDirection = Directions.RIGHT;
    private bool isRunning;
    private bool isScriptedMoving;
    private float currentMoveSpeed;
    private int currentAnimation = -1;

    //Anims
    private readonly int animWalkSide = Animator.StringToHash("WalkingSide");
    private readonly int animIdleSide = Animator.StringToHash("IdleSide");
    private readonly int animWalkDown = Animator.StringToHash("WalkingDown");
    private readonly int animIdleDown = Animator.StringToHash("IdleDown");
    private readonly int animWalkUP = Animator.StringToHash("WalkingUP");
    private readonly int animIdleUP = Animator.StringToHash("IdleUP");
    private readonly int animRunSide = Animator.StringToHash("RunningSide");
    private readonly int animRunDown = Animator.StringToHash("RunningDown");
    private readonly int animRunUP = Animator.StringToHash("RunningUP");

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentMoveSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        if (GameStateManager.CurrentState == GameState.Gameplay)
        {
            bool isMoving = moveInput.sqrMagnitude > 0.0001f;
            bool isSprinting = isRunning && isMoving;

            currentMoveSpeed = isSprinting ? runSpeed : walkSpeed;
            rigidBody.linearVelocity = moveInput.normalized * currentMoveSpeed * Time.fixedDeltaTime;

            CalculateFacingDirection();
            UpdateAnimation();
            return;
        }

        if (isScriptedMoving)
        {
            CalculateFacingDirection();
            UpdateAnimation();
            return;
        }

        rigidBody.linearVelocity = Vector2.zero;
        moveInput = Vector2.zero;
        isRunning = false;
        UpdateAnimation();
    }

    private void Update()
{
    if (GameStateManager.CurrentState != GameState.Gameplay || isScriptedMoving)
    {
        isRunning = false;
        return;
    }

    if (Keyboard.current == null)
    {
        isRunning = false;
        return;
    }

    isRunning = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
}

    private void LateUpdate()
    {
        if (currentInteractPrompt != null)
        {
            currentInteractPrompt.transform.position = transform.position + Vector3.up * 1.14f;
        }
    }

    private void OnMove(InputValue value)
    {
        if (GameStateManager.CurrentState != GameState.Gameplay || isScriptedMoving)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = value.Get<Vector2>();
    }

    private void OnSprint(InputValue value)
    {
        if (GameStateManager.CurrentState != GameState.Gameplay || isScriptedMoving)
        {
            isRunning = false;
            return;
        }

        isRunning = value.isPressed;
    }

    private void CalculateFacingDirection()
    {
        if (moveInput.x != 0f)
        {
            facingDirection = moveInput.x > 0f ? Directions.RIGHT : Directions.LEFT;
        }
        else if (moveInput.y != 0f)
        {
            facingDirection = moveInput.y > 0f ? Directions.UP : Directions.DOWN;
        }
    }

    private void UpdateAnimation()
{
    if (spriteRenderer == null || animator == null)
        return;

    if (facingDirection == Directions.LEFT)
    {
        spriteRenderer.flipX = true;
    }
    else if (facingDirection == Directions.RIGHT)
    {
        spriteRenderer.flipX = false;
    }

    bool isMoving = moveInput.sqrMagnitude > 0.0001f;
    bool isSprinting = isRunning && isMoving;
    int targetAnimation = animIdleSide;

    switch (facingDirection)
    {
        case Directions.UP:
            if (!isMoving)
            {
                targetAnimation = animIdleUP;
            }
            else
            {
                targetAnimation = isSprinting ? animRunUP : animWalkUP;
            }
            break;

        case Directions.DOWN:
            if (!isMoving)
            {
                targetAnimation = animIdleDown;
            }
            else
            {
                targetAnimation = isSprinting ? animRunDown : animWalkDown;
            }
            break;

        default:
            if (!isMoving)
            {
                targetAnimation = animIdleSide;
            }
            else
            {
                targetAnimation = isSprinting ? animRunSide : animWalkSide;
            }
            break;
    }

    if (currentAnimation == targetAnimation)
        return;

    currentAnimation = targetAnimation;
    animator.CrossFade(targetAnimation, 0.08f);
}

    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;

        if (GameStateManager.CurrentState == GameState.Gameplay)
        {
            ShowInteractPrompt();
        }
    }

    public void ClearInteractable(IInteractable interactable)
    {
        if (currentInteractable != interactable)
            return;

        currentInteractable = null;
        HideInteractPrompt();
    }

    private void ShowInteractPrompt()
    {
        if (interactPromptPrefab == null || currentInteractPrompt != null)
            return;

        currentInteractPrompt = Instantiate(
            interactPromptPrefab,
            transform.position + Vector3.up * 1.14f,
            Quaternion.identity
        );
    }

    private void HideInteractPrompt()
    {
        if (currentInteractPrompt == null)
            return;

        GameObject prompt = currentInteractPrompt;
        currentInteractPrompt = null;
        Destroy(prompt);
    }

    private void OnInteract(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (GameStateManager.CurrentState != GameState.Gameplay)
            return;

        if (currentInteractable == null)
            return;

        currentInteractable.Interact();
        HideInteractPrompt();
    }

    private void OnClick(InputValue value)
    {
        if (!value.isPressed)
            return;

        switch (GameStateManager.CurrentState)
        {
            case GameState.Narration:
                FindFirstObjectByType<NarrationUI>()?.OnSubmit();
                break;

            case GameState.Letter:
                FindFirstObjectByType<LetterUI>()?.Close();
                break;

            case GameState.Thought:
                ThoughtUI.Instance?.Skip();
                break;
        }
    }

    public void LookAtTarget(Transform target)
    {
        Vector2 dir = target.position - transform.position;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            facingDirection = dir.x > 0f ? Directions.RIGHT : Directions.LEFT;
        }
        else
        {
            facingDirection = dir.y > 0f ? Directions.UP : Directions.DOWN;
        }

        UpdateAnimation();
    }

    public void ForceFaceUp()
    {
        facingDirection = Directions.UP;
        moveInput = Vector2.zero;
        isRunning = false;
        UpdateAnimation();
    }

    public void ForceFaceDown()
    {
        facingDirection = Directions.DOWN;
        moveInput = Vector2.zero;
        isRunning = false;
        UpdateAnimation();
    }

    public IEnumerator MoveTo(Vector2 targetPosition, float speed = 2f, bool restoreGameplayState = false)
    {
        GameStateManager.SetState(GameState.Cutscene);
        HideInteractPrompt();
        isScriptedMoving = true;
        isRunning = false;

        while (Vector2.Distance(rigidBody.position, targetPosition) > 0.05f)
        {
            Vector2 direction = (targetPosition - rigidBody.position).normalized;

            moveInput = direction;

            rigidBody.MovePosition(
                Vector2.MoveTowards(rigidBody.position, targetPosition, speed * Time.fixedDeltaTime)
            );

            CalculateFacingDirection();
            UpdateAnimation();

            yield return new WaitForFixedUpdate();
        }

        rigidBody.MovePosition(targetPosition);
        rigidBody.linearVelocity = Vector2.zero;
        moveInput = Vector2.zero;
        isScriptedMoving = false;
        UpdateAnimation();

        if (restoreGameplayState)
        {
            GameStateManager.SetState(GameState.Gameplay);

            if (currentInteractable != null)
            {
                ShowInteractPrompt();
            }
        }
    }
}
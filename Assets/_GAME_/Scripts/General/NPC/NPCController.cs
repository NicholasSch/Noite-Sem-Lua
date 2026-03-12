using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    //Dependencies
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 2f;

    //Anims
    [SerializeField] private string idleUpState = "Idle";
    [SerializeField] private string idleDownState = "Idle";
    [SerializeField] private string idleSideState = "Idle";
    [SerializeField] private string walkUpState = "WalkingUP";
    [SerializeField] private string walkDownState = "WalkingDown";
    [SerializeField] private string walkSideState = "WalkingSide";

    private Direction currentDirection = Direction.Down;
    private int currentAnimation = -1;
    private bool isMoving;

    private int idleUpHash;
    private int idleDownHash;
    private int idleSideHash;
    private int walkUpHash;
    private int walkDownHash;
    private int walkSideHash;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        idleUpHash = Animator.StringToHash(idleUpState);
        idleDownHash = Animator.StringToHash(idleDownState);
        idleSideHash = Animator.StringToHash(idleSideState);
        walkUpHash = Animator.StringToHash(walkUpState);
        walkDownHash = Animator.StringToHash(walkDownState);
        walkSideHash = Animator.StringToHash(walkSideState);

        PlayIdle();
    }

    public void FaceDirection(Direction direction)
    {
        currentDirection = direction;
        isMoving = false;
        UpdateAnimation();
    }

    public void LookAtTarget(Transform target)
    {
        if (target == null)
            return;

        Vector2 direction = target.position - transform.position;
        currentDirection = GetDirectionFromVector(direction);
        isMoving = false;
        UpdateAnimation();
    }

    public void PlayIdle()
    {
        isMoving = false;
        UpdateAnimation();
    }

    public void ResetToIdle()
    {
        isMoving = false;
        UpdateAnimation();
    }

    public void PlayAnimationState(string stateName)
    {
        if (animator == null || string.IsNullOrWhiteSpace(stateName))
            return;

        isMoving = false;

        int stateHash = Animator.StringToHash(stateName);

        if (currentAnimation == stateHash)
            return;

        currentAnimation = stateHash;
        animator.CrossFade(stateHash, 0.08f);
    }

    public void PlayAnimationState(string stateName, bool moving)
    {
        if (animator == null || string.IsNullOrWhiteSpace(stateName))
            return;

        isMoving = moving;

        int stateHash = Animator.StringToHash(stateName);

        if (currentAnimation == stateHash)
            return;

        currentAnimation = stateHash;
        animator.CrossFade(stateHash, 0.08f);
    }

    public IEnumerator WalkTo(Transform target)
    {
        if (target == null)
            yield break;

        yield return WalkTo(target.position);
    }

    public IEnumerator WalkTo(Vector2 targetPosition)
    {
        while (Vector2.Distance(transform.position, targetPosition) > 0.05f)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            currentDirection = GetDirectionFromVector(direction);
            isMoving = true;
            UpdateAnimation();

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        UpdateAnimation();
    }

    private Direction GetDirectionFromVector(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x >= 0f ? Direction.Right : Direction.Left;
        }

        return direction.y >= 0f ? Direction.Up : Direction.Down;
    }

    private void UpdateAnimation()
    {
        if (animator == null || spriteRenderer == null)
            return;

        if (currentDirection == Direction.Left)
        {
            spriteRenderer.flipX = true;
        }
        else if (currentDirection == Direction.Right)
        {
            spriteRenderer.flipX = false;
        }

        int targetAnimation = idleDownHash;

        switch (currentDirection)
        {
            case Direction.Up:
                targetAnimation = isMoving ? walkUpHash : idleUpHash;
                break;

            case Direction.Down:
                targetAnimation = isMoving ? walkDownHash : idleDownHash;
                break;

            case Direction.Left:
            case Direction.Right:
                targetAnimation = isMoving ? walkSideHash : idleSideHash;
                break;
        }

        if (currentAnimation == targetAnimation)
            return;

        currentAnimation = targetAnimation;
        animator.CrossFade(targetAnimation, 0.08f);
    }
}
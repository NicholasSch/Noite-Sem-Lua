using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 3f;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public IEnumerator WalkTo(Transform target)
    {
        animator.SetBool("IsWalking", true);

        while (Vector2.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        animator.SetBool("IsWalking", false);
    }
}
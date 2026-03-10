using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public Animator animator;


    public float moveSpeed = 3f;


    void Awake()
    {
        animator = GetComponent<Animator>();
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
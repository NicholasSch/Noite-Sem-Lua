using System.Collections;
using UnityEngine;

public class CorpoSecoController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem dustEffect;

    private readonly int animIdle = Animator.StringToHash("Idle");
    private readonly int animPoint = Animator.StringToHash("Point");

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
    }

    public void LookAt(Transform target)
    {
        if (target == null || spriteRenderer == null)
            return;

        spriteRenderer.flipX = target.position.x < transform.position.x;
    }

    public void PlayIdle()
    {
        if (animator != null)
        {
            animator.CrossFade(animIdle, 0f);
        }
    }

    public void PlayPoint()
    {
        if (animator != null)
        {
            animator.CrossFade(animPoint, 0f);
        }
    }

    public IEnumerator Disappear(float delay = 0.8f)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (dustEffect != null)
        {
            dustEffect.transform.parent = null;
            dustEffect.Play();
        }

        Destroy(gameObject);
    }
}
using UnityEngine;

public class MistCloud : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pushForce = 8f;
    [SerializeField] private float lifetime = 12.5f;
    [SerializeField] private AudioClip coughSFX;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            AudioManager.Instance.PlaySFX(coughSFX);
            playerController.ApplyMistHit(Vector2.right, pushForce);
        }
    }
}
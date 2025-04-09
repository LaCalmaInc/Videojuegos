using UnityEngine;

namespace TopDown.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyChasePlayer : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float chaseRange = 5f;
        [SerializeField] private float speed = 3f;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= chaseRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = direction * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}

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
        private TopDown.Movement.PlayerModeManager playerModeManager;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerModeManager = player.GetComponent<TopDown.Movement.PlayerModeManager>();
        }

        private void FixedUpdate()
        {
            if (playerModeManager != null && playerModeManager.IsImmortal)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

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

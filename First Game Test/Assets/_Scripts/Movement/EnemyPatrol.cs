using UnityEngine;

namespace TopDown.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float speed = 2f;
        private int currentPointIndex = 0;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (patrolPoints.Length == 0) return;

            Vector3 target = patrolPoints[currentPointIndex].position;
            Vector2 direction = (target - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if (Vector2.Distance(transform.position, target) < 0.2f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
        }
    }
}

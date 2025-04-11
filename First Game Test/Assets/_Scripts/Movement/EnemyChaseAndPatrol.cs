using UnityEngine;
using System.Collections;
using TopDown.Movement;

namespace TopDown.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyChaseAndPatrol : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [Header("Player")]
        [SerializeField] private Transform player;
        [SerializeField] private float chaseRange = 5f;

        [Header("Movimiento")]
        [SerializeField] private float speed = 2f;
        [SerializeField] private float waitTime = 1f;
        [SerializeField] private float patrolRadius = 3f;

        private Rigidbody2D rb;
        private Vector3 targetPosition;
        private bool isWaiting = false;
        private PlayerModeManager playerModeManager;
        private Vector3 patrolCenter;
        private Vector3 lastPosition;
        private float stuckTime = 0f;
        private float stuckThreshold = 1.5f; 
        private float movementThreshold = 0.01f; 

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            patrolCenter = transform.position;
            ChooseNewTarget();

            if (player == null)
            {
                GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
                if (foundPlayer != null)
                    player = foundPlayer.transform;
            }

            if (player != null)
                playerModeManager = player.GetComponent<PlayerModeManager>();
        }

        private void FixedUpdate()
        {
            if (player == null) return;

            // --- DETECCIÓN DE ENEMIGO PEGADO ---
            float moved = Vector2.Distance(transform.position, lastPosition);
            if (moved < movementThreshold)
            {
                stuckTime += Time.fixedDeltaTime;
                if (stuckTime >= stuckThreshold)
                {
                    ChooseNewTarget(); // se pegó, cambiar destino
                    stuckTime = 0f;
                }
            }
            else
            {
                stuckTime = 0f;
            }

            lastPosition = transform.position;
            
            // --- LÓGICA DE MOVIMIENTO NORMAL ---
            if (playerModeManager != null && playerModeManager.IsImmortal)
            {
                Patrol();
                return;
            }

            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= chaseRange)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
        }

        private void ChasePlayer()
        {
            isWaiting = false;
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if (animator != null)
                animator.SetBool("isChasing", false); // invertido

            RotateTowards(direction);
        }

        private void Patrol()
        {
            if (isWaiting) return;

            Vector2 direction = (targetPosition - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if (animator != null)
                animator.SetBool("isChasing", true); // invertido

            RotateTowards(direction);

            if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
            {
                StartCoroutine(WaitAndChooseNewTarget());
            }
        }

        private void RotateTowards(Vector2 direction)
        {
            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private IEnumerator WaitAndChooseNewTarget()
        {
            isWaiting = true;
            rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(waitTime);
            ChooseNewTarget();
            isWaiting = false;
        }

        private void ChooseNewTarget()
        {
            float x = Random.Range(-patrolRadius, patrolRadius);
            float y = Random.Range(-patrolRadius, patrolRadius);
            targetPosition = patrolCenter + new Vector3(x, y, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Enemigo colisionó con: " + collision.gameObject.name);
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(1);
                }
            }
            else
            {
                ChooseNewTarget();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, patrolRadius);
        }
    }
}

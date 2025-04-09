using UnityEngine;
using System.Collections;

namespace TopDown.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float waitTime = 1f;
        [SerializeField] private Rect movementBounds;

        private Rigidbody2D rb;
        private Vector3 targetPosition;
        private bool isWaiting = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            ChooseNewTarget();
        }

        private void FixedUpdate()
        {
            if (isWaiting) return;

            Vector2 direction = (targetPosition - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
            {
                StartCoroutine(WaitAndChooseNewTarget());
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
            float x = Random.Range(movementBounds.xMin, movementBounds.xMax);
            float y = Random.Range(movementBounds.yMin, movementBounds.yMax);
            targetPosition = new Vector3(x, y, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Opcional: podés filtrar por etiquetas
            if (collision.gameObject.CompareTag("Player")) return;

            // Redefinir destino al chocar con cualquier cosa
            ChooseNewTarget();
        }
    }
}

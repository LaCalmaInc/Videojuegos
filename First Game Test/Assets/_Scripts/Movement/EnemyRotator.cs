using UnityEngine;

namespace TopDown.Enemies
{
    public class EnemyRotator : MonoBehaviour
    {
        [SerializeField] private Transform torso;
        [SerializeField] private Rigidbody2D rb;

        [Header("Mover Reference")]
        [SerializeField] private EnemyMover enemyMover;

        private void FixedUpdate()
        {
            if (rb.linearVelocity.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}

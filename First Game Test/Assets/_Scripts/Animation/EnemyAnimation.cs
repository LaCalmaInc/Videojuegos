using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool isMoving = rb.linearVelocity.sqrMagnitude > 0.01f;
        enemyAnimator.SetBool("moving", isMoving);
    }
}

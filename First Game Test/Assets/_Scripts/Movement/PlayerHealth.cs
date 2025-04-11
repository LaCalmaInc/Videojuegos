using UnityEngine;
using TopDown.Movement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives;
    private PlayerModeManager modeManager;

    private void Start()
    {
        currentLives = maxLives;
        modeManager = GetComponent<PlayerModeManager>();
    }

    public void TakeDamage(int damage)
    {
        if (modeManager != null && !modeManager.IsImmortal)
        {
            currentLives -= damage;
            Debug.Log("Jugador recibió daño. Vidas restantes: " + currentLives);

            if (currentLives <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("¡El jugador es inmortal y no recibió daño!");
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // Aquí puedes: desactivar el jugador, reiniciar la escena, mostrar UI, etc.
        gameObject.SetActive(false);
    }
    public int CurrentLives => currentLives;

}

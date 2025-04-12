using UnityEngine;
using TopDown.Movement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    private int currentLives;
    public int MaxLives => maxLives;
    private PlayerModeManager modeManager;

    [SerializeField] private AudioClip[] damageClips;
    private AudioSource audioSource;


    private void Start()
    {
        currentLives = maxLives;
        modeManager = GetComponent<PlayerModeManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (modeManager != null && !modeManager.IsImmortal)
        {
            currentLives -= damage;
            Debug.Log("Jugador recibió daño. Vidas restantes: " + currentLives);
            if (damageClips.Length > 0)
            {
                int index = Random.Range(0, damageClips.Length);
                audioSource.PlayOneShot(damageClips[index]);
            }
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
        // Aquí puedes: reiniciar el juego, mostrar UI de muerte, etc.
        gameObject.SetActive(false);
    }
    public int CurrentLives => currentLives;

}

using UnityEngine;
using TopDown.Movement; // Asegúrate de tener esto para acceder al PlayerModeManager

public class CollectibleObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica si el jugador está en modo normal
            PlayerModeManager modeManager = other.GetComponent<PlayerModeManager>();

            if (modeManager != null && !modeManager.IsImmortal)
            {
                Debug.Log("¡Recolectado!");
                GameManager.Instance.Collect();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No se puede recolectar en modo inmortal.");
            }
        }
    }
}

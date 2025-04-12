using UnityEngine;
using TopDown.Movement; // Asegúrate de tener esto para acceder al PlayerModeManager

public class CollectibleObject : MonoBehaviour
{
    [SerializeField] private AudioClip[] collectSounds;
    [SerializeField, Range(0f, 1f)] private float collectVolume = 0.1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerModeManager modeManager = other.GetComponent<PlayerModeManager>();

            if (modeManager != null && !modeManager.IsImmortal)
            {
                Debug.Log("¡Recolectado!");

                if (collectSounds.Length > 0)
                {
                    int index = Random.Range(0, collectSounds.Length);
                    AudioSource.PlayClipAtPoint(collectSounds[index], transform.position, collectVolume);
                }


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

using UnityEngine;

public class CharacterMode : MonoBehaviour
{
    public enum Mode
    {
        MoveLimited, // Modo de movimiento limitado
        Immortal     // Modo inmortal
    }

    public Mode currentMode = Mode.MoveLimited; // Establecer el modo por defecto
    public float moveSpeed = 5f; // Velocidad de movimiento
    private Camera mainCamera;
    public GameObject playerSprite; // Para controlar la visibilidad del personaje
    public GameObject enemyDetection; // Objeto o script que controla la detección de enemigos
    private Renderer playerRenderer; // Para ocultar el jugador en modo inmortal
    private bool isMoving = false;

    private void Start()
    {
        mainCamera = Camera.main;
        playerRenderer = playerSprite.GetComponent<Renderer>(); // Suponiendo que tienes un GameObject para el sprite del jugador
    }

    private void Update()
    {
        if (MenuPausa.GameIsPaused) return;
        HandleModeSwitch();
        HandleMovement();
        HandleImmortalVisibility();
    }

    // Cambiar de modo con una tecla (por ejemplo, 'M')
    void HandleModeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            currentMode = currentMode == Mode.MoveLimited ? Mode.Immortal : Mode.MoveLimited;
        }
    }

    // Controlar el movimiento del personaje
    void HandleMovement()
    {
        if (currentMode == Mode.MoveLimited)
        {
            // Movimiento en el modo limitado
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            isMoving = direction.magnitude > 0;
        }
        else if (currentMode == Mode.Immortal)
        {
            // Movimiento en el modo inmortal
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            isMoving = direction.magnitude > 0;
        }
    }

    // Hacer que el jugador no sea visible y no pueda ser detectado por los enemigos en el modo inmortal
    void HandleImmortalVisibility()
    {
        if (currentMode == Mode.Immortal)
        {
            // Hacer al jugador invisible
            playerRenderer.enabled = false;
            // Bloquear la visión de la cámara (si quieres que no vea nada)
            mainCamera.cullingMask = 0; // Esto hará que la cámara no renderice nada
            // Detener la detección de enemigos (suponiendo que tengas un sistema de detección de enemigos)
            if (enemyDetection != null)
            {
                // Asegúrate de que los enemigos no detecten al jugador
                // Esto puede hacerse con un sistema de "detectabilidad" en los enemigos
                enemyDetection.SetActive(false); // Desactiva la detección del jugador por los enemigos
            }
        }
        else
        {
            // Restaurar visibilidad y detección cuando no estás en modo inmortal
            playerRenderer.enabled = true;
            mainCamera.cullingMask = -1; // Restaurar la visibilidad de todo lo que la cámara pueda ver
            if (enemyDetection != null)
            {
                enemyDetection.SetActive(true); // Restaurar la detección de enemigos
            }
        }
    }
}
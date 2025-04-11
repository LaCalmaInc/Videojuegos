using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vision : MonoBehaviour
{

    [Header("Configuración del FOV")]
    [Tooltip("Radio de alcance de la visión (cuánto distancia puede ver el personaje).")]
    public float viewRadius = 5f;

    [Range(0, 360)]
    [Tooltip("Ángulo total del campo de visión (por ejemplo, 90°).")]
    public float viewAngle = 90f;

    [Header("Capas (Layers)")]
    [Tooltip("Capa a la que pertenecen los objetos objetivo a detectar.")]
    public LayerMask targetMask;
    [Tooltip("Capa a la que pertenecen los obstáculos que bloquean la visión.")]
    public LayerMask obstacleMask;

    [Header("Resultados")]
    [Tooltip("Lista de targets visibles en el frame actual.")]
    public List<Transform> visibleTargets = new List<Transform>();

    // Intervalo de búsqueda de targets (para no hacer la detección cada frame)
    public float delay = 0.2f;

    private void Start()
    {
        // Lanza una corrutina para buscar los targets cada cierto intervalo
        StartCoroutine(FindTargetsWithDelay(delay));
    }

    // Corrutina para buscar targets visibles con un retardo
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // Método para detectar targets visibles
    void FindVisibleTargets()
    {
        // Vacía la lista de targets visibles
        visibleTargets.Clear();

        // Busca todos los colliders en el radio definido que pertenezcan a la capa targetMask
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        // Itera sobre cada collider encontrado
        foreach (Collider2D targetCollider in targetsInViewRadius)
        {
            Transform target = targetCollider.transform;
            // Calcula la dirección desde el personaje hasta el target
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            // Compara el ángulo entre la dirección 'forward' del personaje y la dirección al target.
            // En este ejemplo, se asume que el frente del personaje es 'transform.up'. 
            // Si usas otra orientación (por ejemplo, transform.right) ajústalo según tu configuración.
            if (Vector2.Angle(transform.up, directionToTarget) < viewAngle / 2)
            {
                // Calcula la distancia real hasta el target
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                // Realiza un raycast para saber si hay obstáculos entre el personaje y el target
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask);
                if (!hit)
                {
                    // Si no hay colisión con un obstáculo, el target es visible
                    visibleTargets.Add(target);
                    // Opcional: puedes hacer algo con el target visible, por ejemplo, marcarlo o imprimir un mensaje
                    // Debug.Log("Target detectado: " + target.name);
                }
            }
        }
    }

    // Opcional: Método para obtener un vector dirección a partir de un ángulo (útil si deseas dibujar el cono)
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            // Suma la rotación actual del objeto (en el eje Z para 2D)
            angleInDegrees += transform.eulerAngles.z;
        }
        // Calcula el vector usando seno y coseno (en radianes)
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Opcional: Dibujar en el editor el área del FOV para visualización
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Dibuja el círculo que representa el radio de visión
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Dibuja las dos líneas que delimitan el ángulo del cono
        Vector2 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector2 viewAngleB = DirFromAngle(viewAngle / 2, false);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)viewAngleB * viewRadius);

        // Dibuja líneas hasta cada target visible para comprobarlo visualmente
        Gizmos.color = Color.red;
        foreach (Transform target in visibleTargets)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
    
}
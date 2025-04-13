using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Objetivos")]
    public int totalCollectibles = 5;
    private int collected = 0;
    public int CollectedCount => collected;
    public int TotalCollectibles => totalCollectibles;


    [Header("UI")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject victoryMenu;

    [Header("Spawning")]
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private float spawnRadius = 8f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnCollectibles();
    }

    public void Collect()
    {
        collected++;

        if (collected >= totalCollectibles)
        {
            Win();
        }
    }


    private void SpawnCollectibles()
    {
        for (int i = 0; i < totalCollectibles; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, 0);
            Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);

        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
    }
    private void Win()
    {
        Debug.Log("¡Ganaste!");
        botonPausa.SetActive(false);
        victoryMenu.SetActive(true);
        Time.timeScale = 0f;
        MenuPausa.GameIsPaused= true;
        // Aquí puedes: mostrar UI, reiniciar, pasar a siguiente nivel...
    }
}

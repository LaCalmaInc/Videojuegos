using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private AudioClip victoryMusic;   // Solo en GameManager
    [SerializeField] private AudioSource musicSource;

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
    [SerializeField] private Rect spawnArea;


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
        int spawned = 0;
        int maxAttempts = 100;

        while (spawned < totalCollectibles && maxAttempts > 0)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.xMin, spawnArea.xMax),
                Random.Range(spawnArea.yMin, spawnArea.yMax)
            );

            // Verifica si está libre (puedes ajustar la LayerMask)
            Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.5f, LayerMask.GetMask("Obstacles"));
            if (hit == null)
            {
                Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
                spawned++;
            }

            maxAttempts--;
        }

        if (spawned < totalCollectibles)
            Debug.LogWarning($"Solo se generaron {spawned} de {totalCollectibles} objetos. Zona limitada.");
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
        if (musicSource != null && victoryMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = victoryMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
    }
}

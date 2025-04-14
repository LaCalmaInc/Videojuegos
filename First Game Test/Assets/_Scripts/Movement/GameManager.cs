using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioSource musicSource;

    [Header("Objetivos")]
    [SerializeField] private int requiredCollectibles = 5; // Lo necesario para ganar
    [SerializeField] private int totalSpawnedCollectibles = 8; // Las que realmente se generan
    private int collected = 0;

    public int CollectedCount => collected;
    public int TotalCollectibles => requiredCollectibles;

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
        UpdateUI();
    }

    public void Collect()
    {
        collected++;
        UpdateUI();

        if (collected >= requiredCollectibles)
        {
            Win();
        }
    }

    private void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = $"Recolectados: {collected} / {requiredCollectibles}";
        }
    }

    private void SpawnCollectibles()
    {
        int spawned = 0;
        int maxAttempts = 100;

        while (spawned < totalSpawnedCollectibles && maxAttempts > 0)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.xMin, spawnArea.xMax),
                Random.Range(spawnArea.yMin, spawnArea.yMax)
            );

            Collider2D hit = Physics2D.OverlapCircle(spawnPos, 0.5f, LayerMask.GetMask("Obstacles"));
            if (hit == null)
            {
                Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
                spawned++;
            }

            maxAttempts--;
        }

        if (spawned < totalSpawnedCollectibles)
            Debug.LogWarning($"Solo se generaron {spawned} de {totalSpawnedCollectibles} objetos. Zona limitada.");
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
        MenuPausa.GameIsPaused = true;

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
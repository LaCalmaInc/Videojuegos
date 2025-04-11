using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Objetivos")]
    public int totalCollectibles = 5;
    private int collected = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI counterText;

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
        UpdateUI();
    }

    public void Collect()
    {
        collected++;
        UpdateUI();

        if (collected >= totalCollectibles)
        {
            Win();
        }
    }

    private void UpdateUI()
    {
        counterText.text = $"Objetos: {collected}/{totalCollectibles}";
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

    private void Win()
    {
        Debug.Log("¡Ganaste!");
        // Aquí puedes: mostrar UI, reiniciar, pasar a siguiente nivel...
    }
}

using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("Intro UI")]
    [SerializeField] private GameObject introPanel;

    [Header("Música de fondo")]
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] private AudioSource audioSource;
    [SerializeField, Range(0f, 1f)] private float volume = 0.5f;

    private bool isIntroActive = true;

    private void Start()
    {
        // Mostrar intro y pausar el juego
        Time.timeScale = 0f;
        introPanel.SetActive(true);
        MenuPausa.GameIsPaused = true;
        isIntroActive = true;

        PlayRandomMusic();
    }

    private void Update()
    {
        if (!isIntroActive) return;

        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        introPanel.SetActive(false);
        Time.timeScale = 1f;
        MenuPausa.GameIsPaused = false;
        isIntroActive = false;
    }

    private void PlayRandomMusic()
    {
        if (musicTracks.Length == 0 || audioSource == null) return;

        int index = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[index];
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }
}
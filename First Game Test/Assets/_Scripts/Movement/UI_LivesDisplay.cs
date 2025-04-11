using UnityEngine;
using TMPro;

public class UI_LivesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private PlayerHealth playerHealth;

    private void Update()
    {
        if (playerHealth != null)
        {
            livesText.text = "Vidas: " + playerHealth.CurrentLives;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class UI_HeartsDisplay : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private PlayerHealth playerHealth;

    private List<Image> hearts = new List<Image>();

    private void Start()
    {
        for (int i = 0; i < playerHealth.MaxLives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Add(heart.GetComponent<Image>());
        }

        UpdateHearts();
    }
    private int lastLives = -1;

    private void Update()
    {
        if (playerHealth.CurrentLives != lastLives)
        {
            AnimateLoss();
            UpdateHearts();
            lastLives = playerHealth.CurrentLives;
        }
    }


    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < playerHealth.CurrentLives ? fullHeart : emptyHeart;
        }
    }
        private void AnimateLoss()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i >= playerHealth.CurrentLives)
            {
                StartCoroutine(PopEffect(hearts[i].rectTransform));
            }
        }
    }

    private IEnumerator PopEffect(RectTransform heart)
    {
        Vector3 original = Vector3.one;
        Vector3 popped = Vector3.one * 1.2f;
        float duration = 0.15f;
        float t = 0;

        // Escala hacia arriba
        while (t < duration)
        {
            heart.localScale = Vector3.Lerp(original, popped, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        heart.localScale = popped;

        // Escala hacia abajo
        t = 0;
        while (t < duration)
        {
            heart.localScale = Vector3.Lerp(popped, original, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        heart.localScale = original;
    }

}

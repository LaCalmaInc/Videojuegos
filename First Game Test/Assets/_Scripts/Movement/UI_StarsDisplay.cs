using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_StarsDisplay : MonoBehaviour
{
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameManager gameManager;

    private List<Image> stars = new List<Image>();

    private void Start()
    {
        for (int i = 0; i < gameManager.TotalCollectibles; i++)
        {
            GameObject star = Instantiate(starPrefab, transform);
            stars.Add(star.GetComponent<Image>());
        }

        UpdateStars();
    }

    private void Update()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].sprite = i < gameManager.CollectedCount ? fullStar : emptyStar;
        }
    }
}

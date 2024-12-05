using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGen : MonoBehaviour
{
    private int collectableCount = 0;

    public void AddCollectable(int amount)
    {
        collectableCount += amount;
        Debug.Log("Collected " + amount + "! Total Collectables: " + collectableCount);
    }

    public int GetCollectableCount()
    {
        return collectableCount;
    }
    public void SaveStars(int level, int starsEarned)
    {
        // Save the highest stars earned for the level
        string key = $"Level{level}Stars";
        int previousStars = PlayerPrefs.GetInt(key, 0);
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(key, starsEarned);
            PlayerPrefs.Save();
        }
    }
}

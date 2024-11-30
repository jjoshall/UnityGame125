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
}

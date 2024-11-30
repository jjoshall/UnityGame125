using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGen : MonoBehaviour
{
    private int collectableCount = 0;

    [Header("Settings")]
    public string collectableTag = "Collectable"; // Tag for collectable objects
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.gameObject.name}");
        if (other.CompareTag(collectableTag))
        {
            Debug.Log("Player collided with collectable!");
            CollectableScript collectable = other.GetComponent<CollectableScript>();
            if (collectable != null)
            {
                // Add to inventory and trigger collection behavior
                AddCollectable(collectable.value);
                collectable.Collect(); // Call the collect behavior from the collectable
            }
        }
    }
    */
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

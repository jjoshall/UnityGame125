using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This file spawns obstacles randomly on the slope in the game.
// Got help from: https://www.youtube.com/watch?v=gfD8S32xzYI&ab_channel=BobsiTutorials
public class PlaceObstacles : MonoBehaviour
{
     [Header("Spawn Settings")]
     public GameObject obstaclePrefab;
     public GameObject crystalPrefab;
     public float spawnChance;

     [Header("Raycast Settings")]
     public float distanceBetweenChecks;
     public float heightOfCheck = 10f, rangeOfCheck = 30f;       // Height is how high the raycast is from the ground, range is how far down the raycast goes
     public LayerMask layerMask = ~0;        // This is a mask that includes all layers
     public Vector2 positivePosition, negativePosition;          // Tracks the area where the obstacles can spawn

     private void Start()
     {
          SpawnObstacles();
     }

    /*
     private void Update()
     {
          // Press space to delete and respawn obstacles
          if (Input.GetKeyDown(KeyCode.Space))
          {
               DeleteObstacles();
               SpawnObstacles();
          }
     }
    */

     // SpawnObstacles() will spawn obstacles in the area defined by positivePosition and negativePosition
     void SpawnObstacles()
     {
          for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks)
          {
               for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks)
               {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck))
                    {
                         if (Random.Range(0, 101) < spawnChance)
                         {
                                // Determine whether to spawn an obstacle or a crystal
                                GameObject prefabToSpawn = Random.Range(0f, 1f) < 0.7f ? obstaclePrefab : crystalPrefab;

                                // Instantiate with random rotation
                                if (prefabToSpawn == crystalPrefab)
                                {
                                    Vector3 crystalPos = hit.point;
                                    crystalPos.y += 0.3f;
                                    Instantiate(prefabToSpawn, hit.point, Quaternion.Euler(-127, 0, 0), transform);
                                }
                                else
                                {
                                    Instantiate(prefabToSpawn, hit.point, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                                }
                         }
                    }
               }
          }
     }


     void DeleteObstacles()
     {
          foreach (Transform child in transform)
          {
               Destroy(child.gameObject);
          }
     }
}

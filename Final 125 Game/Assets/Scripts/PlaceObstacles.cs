using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This file spawns obstacles randomly on the slope in the game.
// Got help from: https://www.youtube.com/watch?v=gfD8S32xzYI&ab_channel=BobsiTutorials
public class PlaceObstacles : MonoBehaviour
{
     [Header("Spawn Settings")]
     public GameObject obstaclePrefab;
     public float spawnChance;

     [Header("Raycast Settings")]
     public float distanceBetweenChecks;
     public float heightOfCheck = 10f, rangeOfCheck = 30f;
     public LayerMask layerMask = ~0;
     public Vector2 positivePosition, negativePosition;

     private void Start()
     {
          SpawnObstacles();
     }

     private void Update()
     {
          if (Input.GetKeyDown(KeyCode.Space))
          {
               DeleteObstacles();
               SpawnObstacles();
          }
     }

     void SpawnObstacles()
     {
          for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenChecks)
          {
               for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenChecks)
               {
                    RaycastHit hit;
                    Debug.DrawRay(new Vector3(x, heightOfCheck, z), Vector3.down * rangeOfCheck, Color.red, 1f);
                    if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck))
                    {
                         Debug.Log("Raycast hit at: " + hit.point);
                         if (Random.Range(0, 101) < spawnChance)
                         {
                              Instantiate(obstaclePrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                              Debug.Log("Obstacle spawned at: " + hit.point);
                         }
                         else
                         {
                              Debug.Log("Spawn chance not met at: " + hit.point);
                         }
                    }
                    else
                    {
                         Debug.Log("Raycast did not hit.");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WispNavigation : MonoBehaviour
{
     // References to the endpoint, player, and the NavMeshAgent
     public Transform endPoint;
     public Transform player;
     private NavMeshAgent agent;

     public bool cutsceneEnded = true;


     // Start is called before the first frame update
     void Start()
     {
          agent = GetComponent<NavMeshAgent>();
     }

     // Update is called once per frame
     void Update()
     {
          if (!cutsceneEnded) return;
          // Make the agent's destination the endpoint
          agent.SetDestination(endPoint.position);

          // If the player gets close to the wisp, the wisp will get a little bit faster
          if (Vector3.Distance(player.position, transform.position) < 15)
          {
               agent.speed = 24;
          }
          else
          {
               agent.speed = 19;
          }
          // Debug.Log("Distance to player: " + Vector3.Distance(player.position, transform.position));
     }
}

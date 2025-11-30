using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WispNavigation : MonoBehaviour
{
    public Transform endPoint;
    public Transform player;
    private NavMeshAgent agent;

    public bool cutsceneEnded = false;

    // Logic Variables
    private float baseSpeed = 21f;
    private float baseAccel = 60f;
    private float slowSpeed = 6f; // Speed when waiting for player
    private float burstSpeed = 30f; // Speed when fleeing
    private float burstAccel = 200f; // Snap acceleration when fleeing

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // --- HARDCODED SETTINGS AS REQUESTED ---
        agent.speed = baseSpeed;
        agent.angularSpeed = 300f;
        agent.acceleration = baseAccel;
        agent.stoppingDistance = 0f;

    }

    void Update()
    {
        if (cutsceneEnded)
        {
            // Update destination
            // (Optimization: Only update if the target endpoint moves significantly)
            if (Vector3.Distance(agent.destination, endPoint.position) > 1.0f)
            {
                agent.SetDestination(endPoint.position);
            }

            float distToPlayer = Vector3.Distance(player.position, transform.position);

            // LOGIC 1: TOO FAR (Wait for Player)
            if (distToPlayer > 30f)
            {
                agent.speed = slowSpeed;
                agent.acceleration = baseAccel;
            }
            // LOGIC 2: TOO CLOSE (Burst Away)
            else if (distToPlayer < 10f)
            {
                // High acceleration allows it to reach top speed instantly
                agent.acceleration = burstAccel;
                agent.speed = burstSpeed;
            }
            // LOGIC 3: NORMAL (Cruising)
            else
            {
                agent.speed = baseSpeed;
                agent.acceleration = baseAccel;
            }
        }
    }

    public void CutsceneEnded()
    {
        cutsceneEnded = true;
    }
}
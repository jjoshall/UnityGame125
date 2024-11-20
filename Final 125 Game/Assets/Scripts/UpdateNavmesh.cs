using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

/// <summary>
/// NavMeshSurface that updates only once per frame upon request
/// </summary>
[RequireComponent(typeof(NavMeshSurface))]
public class UpdateNavmesh : MonoBehaviour
{
     static bool s_NeedsNavMeshUpdate;

     NavMeshSurface m_Surface;

     public static void RequestNavMeshUpdate()
     {
          s_NeedsNavMeshUpdate = true;
     }

     void Start()
     {
          m_Surface = GetComponent<NavMeshSurface>();
          m_Surface.BuildNavMesh();
     }

     void Update()
     {
          if (s_NeedsNavMeshUpdate)
          {
               m_Surface.UpdateNavMesh(m_Surface.navMeshData);
               s_NeedsNavMeshUpdate = false;
          }
     }
}

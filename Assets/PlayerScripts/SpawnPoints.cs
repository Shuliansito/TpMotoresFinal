using System.Collections.Generic;
using UnityEngine;
public class SpawnPoints
{
    public List<Vector3> spawnPoint = new List<Vector3>();

    public SpawnPoints()
    {
        spawnPoint.Add(new Vector3(707, 20, 173));
        spawnPoint.Add(new Vector3(194, 31, 213));
        spawnPoint.Add(new Vector3(228, 45, 597));
    }
}
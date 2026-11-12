using System.Collections.Generic;
using UnityEngine;
public class SpawnPoints
{
    public List<Vector3> spawnPoint = new List<Vector3>();

    public SpawnPoints()
    {
        spawnPoint.Add(new Vector3(478, 12, 810));
        spawnPoint.Add(new Vector3(608, 10, 718));
        spawnPoint.Add(new Vector3(208, 33, 214));
    }
}
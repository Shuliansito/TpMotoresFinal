using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTool : MonoBehaviour
{
    SpawnPoints _spawnPoints = new SpawnPoints();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            transform.position = _spawnPoints.spawnPoint[0];
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            transform.position = _spawnPoints.spawnPoint[1];
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            transform.position = _spawnPoints.spawnPoint[2];
        }
    }
}

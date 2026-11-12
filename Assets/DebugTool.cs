using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTool : MonoBehaviour
{
    SpawnPoints _spawnPoints = new SpawnPoints();

    void Update()
    {
        if (Keyboard.current.f4Key.wasPressedThisFrame)
        {
            transform.position = _spawnPoints.spawnPoint[0];
        }
        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            transform.position = _spawnPoints.spawnPoint[1];
        }
        if (Keyboard.current.f6Key.wasPressedThisFrame)
        {
            transform.position = _spawnPoints.spawnPoint[2];
        }
    }
}

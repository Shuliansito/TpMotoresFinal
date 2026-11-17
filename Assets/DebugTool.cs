using Cryfine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTool : MonoBehaviour
{
    SpawnPoints _spawnPoints = new SpawnPoints();
    ZombieSpawner[] _spawners;

    [SerializeField] private float _debugRange = 10f;
    [SerializeField] private bool _showDistances;

    void Start()
    {
        RefreshSpawners();
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) { return; }

        if (keyboard.f3Key.wasPressedThisFrame) { Teleport(3); }
        if (keyboard.f4Key.wasPressedThisFrame) { Teleport(0); }
        if (keyboard.f5Key.wasPressedThisFrame) { Teleport(1); }
        if (keyboard.f6Key.wasPressedThisFrame) { Teleport(2); }
        if (keyboard.f7Key.wasPressedThisFrame) { ToggleDayNight(); }
        if (keyboard.f8Key.wasPressedThisFrame) { SpawnAllNow(); }
        if (keyboard.f9Key.wasPressedThisFrame) { KillAllZombies(); }
        if (keyboard.f10Key.wasPressedThisFrame) { ToggleDistances(); }
        if (keyboard.f11Key.wasPressedThisFrame) { LogNearestSpawner(); }

        if (_showDistances) { DrawZombieLines(); }
    }
    void ToggleDayNight()
    {
        DayNightCycle cycle = FindObjectOfType<DayNightCycle>();

        if (cycle == null) { return; }

        cycle.transform.Rotate(180f, 0f, 0f);
        cycle._totalRotation += 180f;
    }

    void OnDrawGizmos()
    {
        if (!_showDistances || _spawners == null) { return; }

        foreach (ZombieSpawner spawner in _spawners)
        {
            DebugCF.DrawDistanceLabel(transform.position, spawner.transform.position, Color.red);
        }
    }

    void RefreshSpawners()
    {
        _spawners = FindObjectsOfType<ZombieSpawner>();
    }

    void Teleport(int index)
    {
        transform.position = _spawnPoints.spawnPoint[index];
    }

    void SpawnAllNow()
    {
        RefreshSpawners();

        foreach (ZombieSpawner spawner in _spawners)
        {
            spawner.SpawnZombie();
        }
    }

    void KillAllZombies()
    {
        RefreshSpawners();

        foreach (ZombieSpawner spawner in _spawners)
        {
            spawner.KillAll();
        }
    }

    void ToggleDistances()
    {
        RefreshSpawners();
        _showDistances = !_showDistances;
    }

    void DrawZombieLines()
    {
        foreach (ZombieSpawner spawner in _spawners)
        {
            foreach (Transform zombie in spawner.transform)
            {
                DebugCF.DrawDistance(transform.position, zombie.position, Color.yellow);
            }
        }
    }

    void LogNearestSpawner()
    {
        RefreshSpawners();

        ZombieSpawner nearest = null;
        float nearestSqrDistance = float.MaxValue;
        Vector3 a = transform.position;

        foreach (ZombieSpawner spawner in _spawners)
        {
            Vector3 b = spawner.transform.position;
            float sqrDistance = MathCF.DisSqr3D(a.x, a.y, a.z, b.x, b.y, b.z);

            if (sqrDistance < nearestSqrDistance)
            {
                nearestSqrDistance = sqrDistance;
                nearest = spawner;
            }
        }

        if (nearest == null) { return; }

        DebugCF.Distance2GameObjects(gameObject, nearest.gameObject);
        DebugCF.IsAtDistance(gameObject, nearest.gameObject, _debugRange);
    }
}
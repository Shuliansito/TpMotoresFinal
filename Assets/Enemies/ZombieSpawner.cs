using Cryfine;
using CryFine;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static bool IsNight;

    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _spawnDistance = 15f;
    [SerializeField] private int _maxZombies = 5;
    [SerializeField] private float _spawnRadius = 2f;

    private float _timer;
    private bool playerIsNear;
    public int ZombieCount => transform.childCount;
    private void OnDrawGizmos()
    {
        UtilityCF.DrawRangeSphere(UtilityCF.GetCenter(transform), _spawnDistance, Color.cyan);
    }
    private void FixedUpdate()
    {
        Vector3 a = transform.position;
        Vector3 b = _player.transform.position;
        playerIsNear = MathCF.DisSqr3D(a.x, a.y, a.z, b.x, b.y, b.z) <= _spawnDistance * _spawnDistance;
    }
    private void Update()
    {


        if (!IsNight)
        {
            _timer = 0f;
            return;
        }

        if (ZombieCount >= _maxZombies) { return; }

        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval&&playerIsNear)
        {
            _timer = 0f;
            SpawnZombie();
        }
    }

    public void SpawnZombie()
    {
        Vector2 offset = Random.insideUnitCircle * _spawnRadius;
        Vector3 position = transform.position + new Vector3(offset.x, 0f, offset.y);
        Instantiate(_zombiePrefab, position, Quaternion.identity, transform);
    }

    public void KillAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
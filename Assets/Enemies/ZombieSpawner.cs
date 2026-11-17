using Cryfine;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static bool IsNight;

    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private int _maxZombies = 5;
    [SerializeField] private float _spawnRadius = 2f;
    [SerializeField] private float _activeRange = 30f;

    private PlayerMovement _player;
    private float _timer;

    public int ZombieCount => transform.childCount;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (_player == null || !PlayerIsNear())
        {
            _timer = 0f;
            return;
        }

        if (ZombieCount >= _maxZombies) { return; }

        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            SpawnZombie();
        }
    }

    private bool PlayerIsNear()
    {
        Vector3 a = transform.position;
        Vector3 b = _player.transform.position;
        float sqrDistance = MathCF.DisSqr3D(a.x, a.y, a.z, b.x, b.y, b.z);

        return sqrDistance <= _activeRange * _activeRange;
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
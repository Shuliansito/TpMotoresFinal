using Cryfine;
using CryFine;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 3.5f;
    [SerializeField] private float _chaseRange = 15f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackCooldown = 1.5f;

    [SerializeField] private bool Debugging=true;
    private NavMeshAgent _agent;
    private PlayerMovement _player;
    private float _attackTimer;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _chaseSpeed;
    }
    private void OnDrawGizmos()
    {

        if (Debugging)
        {
            UtilityCF.DrawRangeSphere(UtilityCF.GetCenter(transform), _chaseRange, Color.red);
        }
    }

    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (_player == null) { return; }


        Vector3 a = transform.position;
        Vector3 b = _player.transform.position;
        float sqrDistance = MathCF.DisSqr3D(a.x, a.y, a.z, b.x, b.y, b.z);
        bool seesPlayer = sqrDistance <= _chaseRange * _chaseRange;
        bool inRange = sqrDistance <= _attackRange * _attackRange;

        if (!seesPlayer)
        {
            _agent.ResetPath();
            return;
        }

        _agent.isStopped = inRange;

        if (!inRange)
        {
            _agent.SetDestination(b);
            return;
        }

        transform.LookAt(new Vector3(b.x, a.y, b.z));

        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0f)
        {
            _player.playerClass.Hurt(_attackDamage);
            _attackTimer = _attackCooldown;
        }
    }
}
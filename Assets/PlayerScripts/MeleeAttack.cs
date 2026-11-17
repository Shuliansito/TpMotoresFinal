using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    [SerializeField] private InputActionReference Shoot;
    [SerializeField] private float _damage = 50f;
    [SerializeField] private float _cooldown = 0.6f;
    [SerializeField] private Vector3 _hitboxSize = new Vector3(1f, 1.5f, 1.5f);
    public Camera cam;

    private Animator _animator;
    private float _nextAttackTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        _nextAttackTime = Time.time + _cooldown;
    }

    private void OnDisable()
    {
        transform.localPosition = _startPosition;
        transform.localRotation = _startRotation;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Shoot.action.WasPressedThisFrame() && Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + _cooldown;
            _animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        AudioManager.instancia.ReproduceSound("melee_sfx");
        Collider[] hits = Physics.OverlapBox(HitboxCenter(), _hitboxSize / 2f, cam.transform.rotation);
        List<ZombieHealth> damaged = new List<ZombieHealth>();

        foreach (Collider hit in hits)
        {
            ZombieHealth zombie = hit.GetComponentInParent<ZombieHealth>();

            if (zombie != null && !damaged.Contains(zombie))
            {
                damaged.Add(zombie);
                zombie.TakeDamage(_damage);
            }
        }
    }

    private Vector3 HitboxCenter()
    {
        return cam.transform.position + cam.transform.forward * (_hitboxSize.z / 2f);
    }

    private void OnDrawGizmosSelected()
    {
        if (cam == null) { return; }

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(HitboxCenter(), cam.transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _hitboxSize);
    }
}
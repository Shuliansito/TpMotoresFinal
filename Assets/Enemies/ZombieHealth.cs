using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    public void TakeDamage(float amount)
    {
        _health -= amount;

        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

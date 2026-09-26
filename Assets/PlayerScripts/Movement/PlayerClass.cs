using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass
{
    private float speed;
    private float health;
    private float damage;
    private float _stamina = 100.0f;
    public PlayerClass(float speed, float health)
    {
        this.speed = speed;
        this.health = health;
    }

    public float PlayerGetSpeed()
    {
        return speed;
    }
    public float PlayerGetHealth()
    {
        return health;
    }
    public float PlayerGetDamage()
    {
        return damage;
    }

    public void healing(float heal)
    {
        if (health > 0) { health += heal; }
    }
    public void Hurt(float dm)
    {
        if (dm > 0)
        {
            health -= dm;
        }
        health= Mathf.Clamp(health, 0.0f, 100.0f);
    }
    public void HealthLimit()
    {
        
        health = Mathf.Clamp(health, 0.0f, 100.0f);
    }


    public float GetStamina()
    {
        return _stamina;
    }
    public void SetStamina(float stam)
    {
        _stamina=stam;
    }
    public void UpdateStamina(float num)
    {
        _stamina += num;
        _stamina = Mathf.Clamp(_stamina, 0.0f, 100.0f);
    }

    //TODO: Mejorar el sistema de muerte a medida avanza el proyecto
    public void DetectDeath()
    {
        
    }

    public void ResetStats()
    {
        health = 100f;
        _stamina = 100f;

    }
}

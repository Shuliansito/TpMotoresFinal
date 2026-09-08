using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
    private float speed;
    private float health;
    private float damage;

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

    public void PlayerHurt(float dm)
    {
        if (dm > 0)
        {
            health-=dm;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealthPickUp : MonoBehaviour,IInteracrable
{

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _health;
    [SerializeField] private int _timer;
    

    public void WasInteracted()
    {
        HealingLogic(_health);
        gameObject.SetActive(false);

    }

    private void HealingLogic(float health)
    {
        if (health < 0) {
            Debug.LogError(
            "Healing value is lower than 0: "+health+" < 0"
            );
            return; 
        }

        _player.player.healing(health);


    }
}

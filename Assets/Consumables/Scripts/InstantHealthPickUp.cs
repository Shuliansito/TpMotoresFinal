using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealthPickUp : MonoBehaviour,IInteracrable
{

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private GameObject GO_Player;
    [SerializeField] private float _health;
    [SerializeField] private int _timer;

    private void Awake()
    {
        GO_Player = GameObject.Find("player");
        _player = GO_Player.GetComponent<PlayerMovement>();
    }
    public void WasInteracted()
    {


        HealingLogic(_health);
        Destroy(gameObject);

    }

    private void HealingLogic(float health)
    {
        if (health < 0) {
            Debug.LogError(
            "Healing value is lower than 0: "+health+" < 0"
            );
            return; 
        }

        _player.playerClass.healing(health);


    }
}

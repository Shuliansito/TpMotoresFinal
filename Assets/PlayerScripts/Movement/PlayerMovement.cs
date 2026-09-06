using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerClass player;
    void Start()
    {
        player = new PlayerClass(1f, 100f);
    }

    
    void FixedUpdate()
    {
        
    }
}

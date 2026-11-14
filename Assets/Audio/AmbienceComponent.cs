using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbienceComponent : MonoBehaviour
{
    [Tooltip("Area of the sound to be in")]
    public Collider Area;
    [Tooltip("Character to track")]
    public GameObject Player;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Time.timeScale == 0) 
            audioSource.mute = true;
        else 
            audioSource.mute = false;

        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        transform.position = closestPoint;
    }
}
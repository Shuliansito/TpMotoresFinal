using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    private GameObject m_player;
    private GameObject m_GameManager;
    private Rigidbody m_rigidbody;
    public float rapidez = 5;

    private void Awake()
    {
        m_GameManager = GameObject.Find("Game Manager");
        m_player = GameObject.Find("player");
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.LookAt(m_player.transform);
        transform.Translate(rapidez * Vector3.forward * Time.deltaTime);
    }

}

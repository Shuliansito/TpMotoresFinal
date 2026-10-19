using System.Collections;
using System.Collections.Generic;
using Cryfine;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    private GameObject m_player;
    private GameObject m_GameManager;
    private Rigidbody m_rigidbody;
    public float rapidez = 5;
    [SerializeField] private int detectionRange;
    [SerializeField] private bool playerIsOnRange = false;

    private void Awake()
    {
        detectionRange = Random.Range(5, 15);
        m_GameManager = GameObject.Find("Game Manager");
        m_player = GameObject.Find("player");
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerIsOnRange = (
            MathCF.DisSqr3D(
            transform.position.x, transform.position.y, transform.position.z,
            m_player.transform.position.x, m_player.transform.position.y, m_player.transform.position.z)
            < detectionRange * detectionRange);

        if (playerIsOnRange)
        {
            transform.LookAt(m_player.transform);
            transform.Translate(rapidez * Time.deltaTime * Vector3.forward);
        }

    }

}

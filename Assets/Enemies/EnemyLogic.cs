
using Cryfine;
using CryFine;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField]private GameObject m_player;
    private GameObject m_GameManager;
    private Rigidbody m_rigidbody;
    public float rapidez = 5;
    [SerializeField] private int detectionRange=5;
    [SerializeField] private bool playerIsOnRange = false;

    private void Awake()
    {
        detectionRange = Random.Range(5, 15);
        m_GameManager = GameObject.Find("Game Manager");
        if(m_player==null) m_player = GameObject.Find("player");
        m_rigidbody = GetComponent<Rigidbody>();
    }
    private void OnDrawGizmos()
    {
        UtilityCF.DrawLabel(UtilityCF.GetCenter(transform) + new Vector3(0f, 1.5f, 0f), gameObject.name + "\n" + transform.position, Color.white);
        
        UtilityCF.DrawRangeSphere(UtilityCF.GetCenter(transform), detectionRange, Color.red,false);
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

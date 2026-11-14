using UnityEngine;

public class WorldEvents : MonoBehaviour
{
    [SerializeField] private GameObject _forestAmbience;
    private GameObject _forestInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Forest"))
        {
            EnterForest();
            return;
        }

        // acá van las próximas zonas
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Forest"))
        {
            ExitForest();
            return;
        }
    }

    #region Forest
    private void EnterForest()
    {
        Debug.Log("Player entered forest");

        if (_forestInstance == null)
            _forestInstance = Instantiate(_forestAmbience);

        _forestInstance.transform.SetParent(transform);
        _forestInstance.transform.localPosition = Vector3.zero;
    }

    private void ExitForest()
    {
        if (_forestInstance == null) return;
        _forestInstance.transform.SetParent(null, true); // conserva la posición en el mundo
    }
    #endregion
}
using UnityEngine;

public class HintLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] _hints;
    [SerializeField] private float _hintDuration = 5f;
    [SerializeField] private float timer;
    [SerializeField] private int _currentHint;

    private void Start()
    {
        foreach (GameObject hint in _hints)
        {
            hint.SetActive(false);
        }

        if (_hints.Length > 0)
        {
            _hints[0].SetActive(true);
        }
    }

    private void Update()
    {
        if (_currentHint >= _hints.Length) { return; }

        timer += Time.deltaTime;

        if (timer >= _hintDuration)
        {
            _hints[_currentHint].SetActive(false);
            _currentHint++;
            timer = 0f;

            if (_currentHint < _hints.Length)
            {
                _hints[_currentHint].SetActive(true);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainLogic : MonoBehaviour
{
    private DayNightCycle dnCycle;
    [SerializeField] private GameObject _lluviaGO;

    private void Start()
    {
        dnCycle=GetComponent<DayNightCycle>();
    }

    private void Update()
    {
        if (dnCycle._currentDay == 2)
        {
            _lluviaGO.SetActive(true);
            dnCycle._dayFogDensity = 0.03f;
        }
        else { _lluviaGO.SetActive(false); dnCycle._dayFogDensity = 0.005f; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float _rotationScale = 10f;
    void Update()
    {
        transform.Rotate(_rotationScale * Time.deltaTime, 0, 0);
    }
}

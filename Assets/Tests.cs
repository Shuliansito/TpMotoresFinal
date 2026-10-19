using UnityEngine;
using Cryfine;

public class Tests : MonoBehaviour
{
    public Transform cubeLerp, cubeSmooth, cubeDecay;
    public float distance = 10f, duration = 2f, decay = 3f;

    float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / duration;

        SetX(cubeLerp, MathC.Lerp(0f, distance, t));
        SetX(cubeSmooth, MathC.Lerp(0f, distance, MathC.SmootherStep(t)));
        SetX(cubeDecay, MathC.ExpDecay(cubeDecay.position.x, distance, decay, Time.deltaTime));
    }

    void SetX(Transform c, float x)
    {
        Vector3 p = c.position; p.x = x; c.position = p;
    }
}
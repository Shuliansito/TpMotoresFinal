using Cryfine;
using CryFine;
using UnityEngine;

public class Tests : MonoBehaviour
{
    public Transform cubeLerp, cubeSmooth, cubeDecay, m_player;
    public float distance = 10f, duration = 2f, decay = 3f;

    float elapsed;
    Vector3 CentroDe(Transform t)
    {
        Renderer r = t.GetComponentInChildren<Renderer>();
        if (r != null)
        {
            return r.bounds.center;
        }
        return t.position;
    }
    void Update()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / duration;

        SetX(cubeLerp, MathCF.Lerp(0f, distance, t));
        SetX(cubeSmooth, MathCF.Lerp(0f, distance, MathCF.SmootherStep(t)));
        SetX(cubeDecay, MathCF.ExpDecay(cubeDecay.position.x, distance, decay, Time.deltaTime));


    }


    void OnDrawGizmos()
    {
        if (cubeLerp != null)
        {
            DebugCF.DrawDistanceLabel(CentroDe(m_player), CentroDe(cubeLerp), Color.yellow);
            string text = "Pos: " + cubeLerp.transform.position.ToString("F1") + "\nName: " + cubeLerp.name;
            UtilityCF.DrawLabel(cubeLerp.transform.position + Vector3.up * 2f, text, Color.white);
        }
        if (cubeSmooth != null)
        {
            DebugCF.DrawDistanceLabel(CentroDe(m_player), CentroDe(cubeSmooth), Color.red);
            string text = "Pos: " + cubeSmooth.transform.position.ToString("F1") + "\nName: " + cubeSmooth.name;
            UtilityCF.DrawLabel(cubeSmooth.transform.position + Vector3.up * 2f, text, Color.white);
        }
        if (cubeDecay != null)
        {
            DebugCF.DrawDistanceLabel(CentroDe(m_player), CentroDe(cubeDecay), Color.cyan);
            string text = "Pos: " + cubeDecay.transform.position.ToString("F1") + "\nName: " + cubeDecay.name;
            UtilityCF.DrawLabel(cubeDecay.transform.position + Vector3.up * 2f, text, Color.white);
        }
    }

    void SetX(Transform c, float x)
    {
        Vector3 p = c.position; p.x = x; c.position = p;
    }
}
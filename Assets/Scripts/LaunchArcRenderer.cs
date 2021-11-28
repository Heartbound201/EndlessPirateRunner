using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer _lr;

    public int resolution;

    private float _g;
    private float _velocity;
    private float _angle;
    private float _radianAngle;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _g = Mathf.Abs(Physics2D.gravity.y);
    }

    public void RenderArc(float velocity, float angle)
    {
        _velocity = velocity;
        _angle = angle;
        _lr.positionCount = resolution + 1;
        _lr.SetPositions(CalculateArcArray());
    }
    
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        _radianAngle = Mathf.Deg2Rad * _angle;
        float maxDistance = (_velocity * _velocity * Mathf.Sin(2 * _radianAngle)) / _g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(_radianAngle) - ((_g * x * x) / (2 * _velocity * _velocity * Mathf.Cos(_radianAngle) * Mathf.Cos(_radianAngle)));
        return new Vector3(x, y);
    }

}
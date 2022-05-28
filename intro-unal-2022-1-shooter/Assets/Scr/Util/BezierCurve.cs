/*
 * Base on http://www.theappguruz.com/blog/bezier-curve-in-games
 */

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private int numPoint = 50;

    private Vector3[] point;

    public void SetColor(Color color) {
        if (this.line == null)
            return;
        this.line.endColor = color; 
        this.line.startColor = color; 
    }

    private void Awake()
    {
        this.line = this.GetComponent<LineRenderer>();
        this.point = new Vector3[this.numPoint];
    }


    public void Draw(Vector3 p0, Vector3 p1)
    {
        if (this.numPoint != this.point.Length)
            this.point = new Vector3[this.numPoint];
        float t = 0;
        for (int i = 0; i < numPoint - 1; i++) {
            t = i / (float)this.numPoint;
            this.point[i] = this.GetLineaBezierPoint(t, p0, p1);
        }
        this.point[numPoint - 1] = p1;
        this.line.positionCount = this.numPoint;
        this.line.SetPositions(this.point);
    }


    public void Draw(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        if(this.numPoint != this.point.Length)
            this.point = new Vector3[this.numPoint];
        float t = 0;
        for (int i = 0; i < numPoint - 1; i++) {
            t = i / (float)this.numPoint;
            this.point[i] = this.GetQuadraticBezierPoint(t, p0, p1, p2);
        }
        this.point[numPoint - 1] = p2;
        this.line.positionCount = this.numPoint;
        this.line.SetPositions(this.point);
    }

    Vector3 GetLineaBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    Vector3 GetQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}

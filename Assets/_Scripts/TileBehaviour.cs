using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public int tileValue;
    public List<GameObject> wedgeList = new List<GameObject>();

    public Transform pillar;

    public void ArrangeTile()
    {
        float angle = wedgeList[0].GetComponent<WedgeData>().leftAngle + wedgeList[wedgeList.Count-1].GetComponent<WedgeData>().rightAngle;
        angle /= 2;
        transform.position = CalculateArcPosition(angle);

        transform.LookAt(pillar);
    }

    Vector3 CalculateArcPosition(float angle)
    {
        float rad = Mathf.Deg2Rad * angle;

        Vector3 pos = new Vector3
        {
            x = 10 * Mathf.Cos(rad),
            y = transform.position.y,
            z = 10 * Mathf.Sin(rad)
        };

        return pos;
    }
}

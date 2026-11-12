using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public int tileValue;
    [SerializeField] MeshRenderer meshRenderer;
    public List<GameObject> wedgeList = new List<GameObject>();
    public float radius = 15;

    public void SetValue(int value)
    {
        tileValue = value;

        meshRenderer.material.mainTextureOffset = new Vector2(value * 0.04f, 0);
    }

    public void ArrangeTile()
    {
        transform.position = CalculateArcPosition();

        Quaternion look = Quaternion.LookRotation(Vector3.zero - transform.position);
        transform.rotation = look;
        transform.position = new Vector3
        (
            transform.position.x,
            0.15f,
            transform.position.z
        );
    }

    Vector3 CalculateArcPosition()
    {
        Vector3 p1 = CircleCalc.PositionFromAngle(radius, wedgeList[0].GetComponent<WedgeData>().leftAngle);
        Vector3 p2 = CircleCalc.PositionFromAngle(radius, wedgeList[wedgeList.Count - 1].GetComponent<WedgeData>().rightAngle);

        Vector3 midP = new Vector3
        {
            x = (p1.x + p2.x) * 0.5f,
            y = p1.y,
            z = (p1.z + p2.z) * 0.5f
        };

        Vector3 dir = new Vector3
        {
            x = midP.x - Vector3.zero.x,
            y = p1.y,
            z = midP.z - Vector3.zero.z
        };

        float magnitude = Mathf.Sqrt((dir.x * dir.x) + (dir.z * dir.z));

        if(magnitude > 11)
        {
            midP.x *= 0.8f;
            midP.z *= 0.8f;
        }
        else if (magnitude > 9)
        {
            midP.x *= 1.2f;
            midP.z *= 1.2f;
        }
        else if(magnitude < 5)
        {
            midP.x *= 3.0f;
            midP.z *= 3.0f;
        }

        return midP;
    }
}

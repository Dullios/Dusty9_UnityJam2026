using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Header("Wedge Information")]
    public GameObject[] wedgeArray;
    public Material[] materialArray;
    List<int> matIndexList = new List<int>();

    [Header("Switch Information")]
    public GameObject numberTile;
    List<GameObject> tileList = new List<GameObject>();

    const int wedgeCount = 24;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonTest(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonTest(int n)
    {
        AssignTiles(n);
        AssignWedges(n);
    }

    public void AssignWedges(int division)
    {
        int range = wedgeCount / division;
        int offset = Random.Range(0, 24);
        int step = 0;

        int tileIndex = 0;

        matIndexList.Clear();

        for (int i = 0; i < materialArray.Length; i++)
        {
            matIndexList.Add(i);
        }
        int matIndex = GetMaterialIndex();


        for (int i = 0; i < wedgeCount; i++)
        {
            int index = i + offset;
            index = index >= 24 ? index % 24 : index;
            wedgeArray[index].GetComponent<MeshRenderer>().material = materialArray[matIndex];
            tileList[tileIndex].GetComponent<TileBehaviour>().wedgeList.Add(wedgeArray[index]);
            
            step++;
            if (step == range)
            {
                step = 0;

                tileList[tileIndex].GetComponent<TileBehaviour>().ArrangeTile();
                tileIndex++;

                if (i < 23)
                    matIndex = GetMaterialIndex();
            }
        }
    }

    int GetMaterialIndex()
    {
        int randomIndex = Random.Range(0, matIndexList.Count);
        int matIndex = matIndexList[randomIndex];
        matIndexList.Remove(matIndex);

        return matIndex;
    }

    public void AssignTiles(int division)
    {
        foreach (GameObject tile in tileList)
            Destroy(tile);
        tileList.Clear();

        for (int i = 0; i < division; i++)
        {
            GameObject tempTile = Instantiate(numberTile, transform);
            tileList.Add(tempTile);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Header("Wedge Information")]
    public GameObject[] wedgeArray;
    public Material[] materialArray;
    List<int> matIndexList = new List<int>();

    [Header("Tile Information")]
    public GameObject numberTile;
    List<GameObject> tileList = new List<GameObject>();
    List<int> tileValues = new List<int>();

    const int wedgeCount = 24;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 1; i <= 9; i++)
        {
            tileValues.Add(i);
        }

        ButtonTest(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonTest(int n)
    {
        GenerateTiles(n);
        AssignWedges(n);
    }

    public void GenerateTiles(int division)
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

    int AssignTileValue()
    {
        List<int> tileValTemp = new List<int>();
        foreach (int val in tileValues)
            tileValTemp.Add(val);

        int value = tileValTemp[Random.Range(0, tileValTemp.Count)];
        tileValTemp.Remove(value);

        return value;
    }

    public void AssignWedges(int division)
    {
        int range = wedgeCount / division;
        int offset = Random.Range(0, 24);
        int step = 0;

        int tileIndex = 0;
        TileBehaviour _tileBehaviour = tileList[tileIndex].GetComponent<TileBehaviour>();

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
            _tileBehaviour.wedgeList.Add(wedgeArray[index]);
            
            step++;
            if (step == range)
            {
                step = 0;

                _tileBehaviour.SetValue(AssignTileValue());
                _tileBehaviour.ArrangeTile();
                tileIndex++;
                _tileBehaviour = tileList[tileIndex].GetComponent<TileBehaviour>();

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
}

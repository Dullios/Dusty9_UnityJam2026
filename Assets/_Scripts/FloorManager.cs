using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorManager : MonoBehaviour
{
    [Header("Wedge Data")]
    public GameObject[] wedgeArray;
    const int wedgeCount = 24;
    int wedgeGroupSize;
    public Material[] materialArray;
    List<int> matIndexList = new List<int>();

    [Header("Tile Data")]
    public GameObject numberTile;
    List<GameObject> tileList = new List<GameObject>();
    List<int> tileValues = new List<int>();
    List<int> tileValTemp = new List<int>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 1; i <= 24; i++)
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
        //GenerateTiles(n);
        //AssignWedges(n);

        PrepareTiles(n);
        GroupWedges();
    }

    void PrepareTiles(int division)
    {
        foreach (GameObject tile in tileList)
            Destroy(tile);
        tileList.Clear();

        wedgeGroupSize = wedgeCount / division;

        SetTempValueList();

        bool targetMatched = false;

        for (int i = 0; i < division; i++)
        {
            GameObject tempTile = Instantiate(numberTile, transform);
            tempTile.GetComponent<TileBehaviour>().SetValue(GetNumberForTile());

            if (GameManager.Instance.CheckLuckyNumber(tempTile.GetComponent<TileBehaviour>().tileValue))
                targetMatched = true;

            tileList.Add(tempTile);
        }

        if (!targetMatched)
        {
            foreach (GameObject tile in tileList)
            {
                tile.GetComponent<TileBehaviour>().wedgeCount = wedgeGroupSize;
            }

            GameManager.Instance.SetTargetNumber(tileList[Random.Range(0, tileList.Count)].GetComponent<TileBehaviour>().tileValue);
        }
        else
        {
            GameManager.Instance.SetTargetNumber();

            if (division != 24)
            {
                foreach (GameObject tile in tileList)
                {
                    TileBehaviour tb = tile.GetComponent<TileBehaviour>();

                    switch (division)
                    {
                        case 2:
                        case 3:
                            tile.GetComponent<TileBehaviour>().wedgeCount = wedgeGroupSize;
                            break;

                        case 4:
                            tile.GetComponent<TileBehaviour>().wedgeCount = GameManager.Instance.CheckLuckyNumber(tb.tileValue) ? 12 : 4;
                            break;

                        case 6:
                            tile.GetComponent<TileBehaviour>().wedgeCount = GameManager.Instance.CheckLuckyNumber(tb.tileValue) ? 9 : 3;
                            break;

                        case 8:
                            tile.GetComponent<TileBehaviour>().wedgeCount = GameManager.Instance.CheckLuckyNumber(tb.tileValue) ? 10 : 2;
                            break;

                        case 12:
                            tile.GetComponent<TileBehaviour>().wedgeCount = GameManager.Instance.CheckLuckyNumber(tb.tileValue) ? 12 : 1;
                            break;
                    }
                }
            }
            else
            {
                if (!GameManager.Instance.CheckLuckyNumber(tileList[0].GetComponent<TileBehaviour>().tileValue) &&
                    !GameManager.Instance.CheckLuckyNumber(tileList[1].GetComponent<TileBehaviour>().tileValue))
                {
                    tileList.Remove(tileList[1]);
                    tileList.Remove(tileList[0]);
                }
                else
                {
                    tileList.Remove(tileList[4]);
                    tileList.Remove(tileList[3]);
                }

                foreach (GameObject tile in tileList)
                {
                    TileBehaviour tb = tile.GetComponent<TileBehaviour>();
                    tile.GetComponent<TileBehaviour>().wedgeCount = GameManager.Instance.CheckLuckyNumber(tb.tileValue) ? 3 : 1;
                }
            }
        }
    }

    public void GroupWedges()
    {
        matIndexList.Clear();
        for (int i = 0; i < materialArray.Length; i++)
        {
            matIndexList.Add(i);
        }

        int wedgeCount = 0;
        int offset = Random.Range(0, 24);

        foreach (GameObject tile in tileList)
        {
            TileBehaviour tb = tile.GetComponent<TileBehaviour>();
            
            int matIndex = GetMaterialIndex();

            for (int i = 0; i < tb.wedgeCount; i++, wedgeCount++)
            {
                int wedgeIndex = wedgeCount + offset;
                wedgeIndex = wedgeIndex >= 24 ? wedgeIndex % 24 : wedgeIndex;

                wedgeArray[wedgeIndex].GetComponent<MeshRenderer>().material = materialArray[matIndex];
                tb.wedgeList.Add(wedgeArray[wedgeIndex]);
            }

            tb.ArrangeTile();
        }
    }

    //public void GenerateTiles(int division)
    //{
    //    foreach (GameObject tile in tileList)
    //        Destroy(tile);
    //    tileList.Clear();

    //    for (int i = 0; i < division; i++)
    //    {
    //        GameObject tempTile = Instantiate(numberTile, transform);
    //        tileList.Add(tempTile);
    //    }
    //}

    void SetTempValueList()
    {
        tileValTemp.Clear();
        foreach (int val in tileValues)
            tileValTemp.Add(val);
    }

    int GetNumberForTile()
    {
        int value = tileValTemp[Random.Range(0, tileValTemp.Count)];
        tileValTemp.Remove(value);

        return value;
    }

    //public void AssignWedges(int division)
    //{
    //    int range = wedgeCount / division;
    //    int offset = Random.Range(0, 24);
    //    int step = 0;

    //    int tileIndex = 0;
    //    SetTempValueList();
    //    TileBehaviour _tileBehaviour = tileList[tileIndex].GetComponent<TileBehaviour>();

    //    matIndexList.Clear();

    //    for (int i = 0; i < materialArray.Length; i++)
    //    {
    //        matIndexList.Add(i);
    //    }
    //    int matIndex = GetMaterialIndex();


    //    for (int i = 0; i < wedgeCount; i++)
    //    {
    //        int index = i + offset;
    //        index = index >= 24 ? index % 24 : index;
    //        wedgeArray[index].GetComponent<MeshRenderer>().material = materialArray[matIndex];
    //        _tileBehaviour.wedgeList.Add(wedgeArray[index]);
            
    //        step++;
    //        if (step == range)
    //        {
    //            step = 0;

    //            _tileBehaviour.SetValue(GetNumberForTile());
    //            _tileBehaviour.ArrangeTile();
    //            tileIndex++;
                
    //            if (i < 23)
    //            {
    //                _tileBehaviour = tileList[tileIndex].GetComponent<TileBehaviour>();
    //                matIndex = GetMaterialIndex();
    //            }
    //        }
    //    }

    //    GameManager.Instance.SetTargetNumber(tileList[Random.Range(0, tileList.Count)].GetComponent<TileBehaviour>().tileValue);
    //}

    int GetMaterialIndex()
    {
        int randomIndex = Random.Range(0, matIndexList.Count);
        int matIndex = matIndexList[randomIndex];
        matIndexList.Remove(matIndex);

        return matIndex;
    }
}

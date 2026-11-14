using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    _instance = obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] int targetNumber;
    [SerializeField] GameObject numberDisplay;

    [SerializeField] int luckyNumber;

    // Start is called before the first frame update
    void Start()
    {
        luckyNumber = Random.Range(1, 25);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTargetNumber(int value)
    {
        targetNumber = value;
        numberDisplay.GetComponent<TextMeshProUGUI>().text = targetNumber.ToString();
    }

    public void SetTargetNumber()
    {
        targetNumber = luckyNumber;
        numberDisplay.GetComponent<TextMeshProUGUI>().text = targetNumber.ToString();
    }

    public bool CheckNumber(int number)
    {
        return number == targetNumber;
    }

    public bool CheckLuckyNumber(int num)
    {
        return num == luckyNumber;
    }
}
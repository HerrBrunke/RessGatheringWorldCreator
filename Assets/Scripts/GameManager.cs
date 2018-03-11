using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject TownCenterController;
    public bool TownCenterisSelected;
    /*
        bool isSelecting = false;
        Vector3 mousePosition1;

        public GameObject selectionCirclePrefab;
     */
    //Properties
    #region Properties
    public float Food
    {
        get
        {
            return food;
        }
        set
        {
            food = value;
            foodText.text = food.ToString();
        }
    }
    private float food;

    public float Wood
    {
        get
        {
            return wood;
        }
        set
        {
            wood = value;
            woodText.text = wood.ToString();
        }
    }
    private float wood;

    public float Stone
    {
        get
        {
            return stone;
        }
        set
        {
            stone = value;
            stoneText.text = stone.ToString();
        }
    }
    private float stone;

    public float Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldText.text = gold.ToString();
        }
    }
    private float gold;

    #endregion
    //Textfields
    public Text foodText;
    public Text woodText;
    public Text goldText;
    public Text stoneText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        Food = 500;
        Wood = 200;
        Gold = 100;
        Stone = 50;
    }

    // Update is called once per frame
    void Update()
    {
        /*
    //EventSystem WorkingStates
    public enum WorkingStates
    {
        idle, 
        gatherRessources,
        bringBackRessources,
       */
    }

}
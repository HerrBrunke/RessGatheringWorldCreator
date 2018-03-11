using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownCenterController : MonoBehaviour {

    public bool isSelected = false;
    //OnClick open TownCenterUI
    public Button button;
    public GameObject uiTownCenter;
    public GameObject Villager;
    public float villagerBuildTimer = 60f;
    public Camera cam;
    public GameManager gameManager;
    public float villagerConstructionCost = 50f;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
       cam = Camera.main;
       isSelected = false;
    }

    private void Update()
    {
       // if (Input.GetMouseButtonDown(0)&&Input.mousePosition. )
        //{



        //}

        //if TownCenter isSelected show TC-UI;
            if (isSelected)
        {
            uiTownCenter.SetActive(true);
        }
        else
        {
            uiTownCenter.SetActive(false);
        }
        
    }


     private void OnMouseDown()
     {
        isSelected = true;
        /*if (hit.collider.name == "TownCenter")
        {
            isSelected = true;
            Debug.Log("Hit.collider.name == TownCenter");
        }
        else
        {
            isSelected = false;
            Debug.Log("Hit.collider.name != TownCenter");
        }*/
    } 

    public void CreateVillager()
    {
        if(gameManager.Food >= villagerConstructionCost) { 
        //  Villager.Instantiate<(Villager)>
        Debug.Log("CreateVillager");
        Invoke("CreateVillagerAfterDelay", villagerBuildTimer);
        //onCreation take 50 food
        gameManager.Food -= (int)50f;
        }
    }
    public void CreateVillagerAfterDelay()
    {
        Instantiate(Villager, transform.position + Vector3.forward * 4f, Quaternion.identity, GameObject.Find("Villagers").transform);
    }
}

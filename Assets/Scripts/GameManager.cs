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
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0))
        {
           
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponents>())
            {
                if (selectableObject.selectionCircle != null)
                {
                    Destroy(selectableObject.selectionCircle.gameObject);
                    selectableObject.selectionCircle = null;
                }
            }
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            var selectedObjects = new List<SelectableUnitComponents>();
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponents>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectedObjects.Add(selectableObject);
                    Debug.Log("Selected Objects Count: " + selectedObjects.Count);

                    VillagerController villagerController = selectableObject.GetComponent<VillagerController>();
                    if (villagerController != null)
                    {
                        villagerController.isSelected = true;
                    }
                }
            }



            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
            foreach (var selectedObject in selectedObjects)
                sb.AppendLine("-> " + selectedObject.gameObject.name);
            Debug.Log(sb.ToString());

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if (isSelecting)
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponents>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
                else
                {
                    if (selectableObject.selectionCircle != null)
                    {
                        Destroy(selectableObject.selectionCircle.gameObject);
                        selectableObject.selectionCircle = null;
                    }
                }
            }
        }
    }
    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    //EventSystem WorkingStates
    public enum WorkingStates
    {
        idle, 
        gatherRessources,
        bringBackRessources,
       */
    }

}
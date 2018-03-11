using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Text;

public class VillagerController : MonoBehaviour {

    public bool isSelected;
    NavMeshAgent agent;
    public float distanceLeft = 0.5f;
    public Camera cam;
    public float inventory;
    public float maxInventory = 15f;
    public bool inventoryIsFull = false;
    bool isSelecting = false;
    Vector3 mousePosition1;
    public GameObject selectionCirclePrefab;

    // Use this for initialization
    void Start(){

        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        if(cam == null)
        {
            Debug.Log("Scheiße!");
        }
	}

    // Update is called once per frame
    void Update()
    {
        //LeftMouseButton takes array of targets(villagers) with rightclick move on position 
        //if (Input.GetMouseButtonDown(0))
        if (agent.remainingDistance <= distanceLeft)
        {
            //Debug.Log("Remaining Distance");
            agent.isStopped = true;
        }
        if (isSelected == true)
        {


            if (Input.GetMouseButtonDown(1))
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    MoveToPoint(hit.point);

                   
                    isSelected = false;
                    Debug.Log("We hit: " + hit.collider.name + " " + hit.point);
                    //Move target to what we hit

                }

            }

        }
        if (inventory <= maxInventory)
        {
            inventoryIsFull = true;
        }
        else
        {
            inventoryIsFull = false;
        }

        #region UnitSelection
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0))
        {

            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            //check if List is empty , otherwise empty it
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
            //create a new List "selectedObjects" and add "selectableObjects" within 'selection bounds'
            var selectedObjects = new List<SelectableUnitComponents>();
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponents>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectedObjects.Add(selectableObject);
                    Debug.Log("Selected Objects Count: " + selectedObjects.Count);

                    //activate movement on selectedThings //überrest aus mech?!
                    VillagerController villagerController = selectableObject.GetComponent<VillagerController>();
                    if (villagerController != null)
                    {
                        villagerController.isSelected = true;
                    }
                }
            }


            //string builder [nochmal anschauen]
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
            foreach (var selectedObject in selectedObjects)
                sb.AppendLine("-> " + selectedObject.gameObject.name);
            Debug.Log(sb.ToString());

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        //buggy :b
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

    /// <summary>
    /// Checks weather Objects is in selectionBounds
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
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

    #endregion
    /*
    //EventSystem WorkingStates
    public enum WorkingStates
    {
        idle,
        gatherRessources,
        bringBackRessources,

    }

    WorkingStates.idle
    {

    }
    */
    /// <summary>
    /// /Changes WorkingState after input.rightklick.hit
    /// </summary>
    /// <param name="point"></param>
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);

        agent.isStopped = false;

        //StateMachine which object was hitted (Ressource = gather, TC = bringBack, Ground goTo -> idle)
        //if()
    }

    private void OnCollisionEnter(Collision collision)
    {
       // if(hit.collider.name )
    }

    private void OnMouseDown()
    {
        isSelected = true;
    }

    private void Idle()
    {

    }
    private void GatherRessources()
    {

    }
    private void BringBackRessources()
    {

    }
}

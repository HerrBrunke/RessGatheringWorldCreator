using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraZoneMin = 0.02f;
    public float cameraZoneMax = 0.98f;
    public float cameraSpeed = 10f;
    public float zoomSpeed = 20f;
    public float maxZoom = 50f;
    public float minZoom = 5f;

	// Use this for initialization
	void Start () {
        
        //Output the current screen window width in the console
        //Debug.Log("Screen Width : " + Screen.width);
        //Output the current screen window height in the console
        //Debug.Log("Screen Height : " + Screen.height);
    }
	
	// Update is called once per frame
	void Update () {

        //Edit: Added ArrowButtons to CameraControlle
        //Transforms position with InputValues
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime + Vector3.forward * Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;

        //Set InputValues
        //Mouse Left -> Camera Left;
        if (Input.mousePosition.x <= Screen.width * cameraZoneMin)
        {
            //Debug.Log("Links");
            transform.position += Vector3.left * cameraSpeed * Time.deltaTime;
        }
        //Mouse Right -> Camera Right
        if(Input.mousePosition.x >= Screen.width * cameraZoneMax) 
        {
            //Debug.Log("Rechts");
            transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
        }
        //Mouse Down -> Camera Down
        if(Input.mousePosition.y <= Screen.height * cameraZoneMin)
        {
            //Debug.Log("Down");
            transform.position += Vector3.back * cameraSpeed * Time.deltaTime;
        }
        //Mouse Top -> Camera Top
        if (Input.mousePosition.y >= Screen.height * cameraZoneMax)
        {
            //Debug.Log("Top");
            transform.position += Vector3.forward * cameraSpeed * Time.deltaTime;
        }
       
        //Zoom in/out ScrollMouse
        //ZoomOut
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            if (transform.position.y <= maxZoom)
            {
                //Debug.Log("ZoomOut");
                transform.position += Vector3.up * zoomSpeed * Time.deltaTime;
            }
        }
        //ZoomIn
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (transform.position.y >= minZoom)
            {
                //Debug.Log("ZoomIn");
                transform.position += Vector3.down * zoomSpeed * Time.deltaTime;
            }
        }
    }
}

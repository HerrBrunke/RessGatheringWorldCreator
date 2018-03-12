using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCreation : MonoBehaviour {

    public Planet planet;

    private void Start()
    {
        Debug.LogFormat("Planet: \ntileSize: {0} \nradius: {1}\nCircumference: {2}\nTilesInCircle:{3}", planet.tileSize, planet.radius, planet.Circumference, planet.TilesInCircle);
        Generate();
    }

    void Generate()
    {
        GameObject newPlane;
        float stepAngle = 360f / planet.TilesInCircle;
        for (int x = 0; x < planet.TilesInCircle; x++)
        {
            for(int y = 0; y < planet.TilesInCircle; y++)
            {
                newPlane = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newPlane.transform.localScale = Vector3.one * planet.tileSize*0.5f;// * 0.1f;
                newPlane.transform.position = transform.position;
                //newPlane.transform.rotation = Quaternion.AngleAxis(stepAngle *(float) x, transform.position);
                newPlane.transform.Rotate(stepAngle * (float)x, stepAngle * (float)y, 0);
                newPlane.transform.position += newPlane.transform.forward.normalized * planet.radius; //(planet.tileSize/ Mathf.PI);
                Debug.Log(stepAngle * x);
                newPlane.transform.LookAt(transform.position);
                newPlane.transform.Rotate(Vector3.right, -90f);
                //newPlane.transform.RotateAround(newPlane.transform.position, Vector3.right, 90);
            }
        }
    }

    void Integrate()
    {
       // Vector3  planet.transform.position + planet.radius;
    }
}

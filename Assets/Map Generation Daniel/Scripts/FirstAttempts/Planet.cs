using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public float tileSize = 1f;
    public float radius = 128f;
    public float Diameter 
    {
        get
        {
            return radius*2;
        }
    }
    public float Circumference //Umfang
    {
        get
        {
            return 2 * Mathf.PI * radius;
        }
    }

    public float TilesInCircle
    {
        get
        {
            return Mathf.Floor(Circumference / tileSize);
        }
    }


}

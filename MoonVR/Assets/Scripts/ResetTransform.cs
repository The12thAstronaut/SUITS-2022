using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEditor;

public class ResetTransform : MonoBehaviour
{
    public GameObject Waypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void resetTransform()
    {
        Waypoint.transform.localPosition = new Vector3(0,0,0);
        Waypoint.transform.eulerAngles = new Vector3(0f,0f,0f);
    } 
}

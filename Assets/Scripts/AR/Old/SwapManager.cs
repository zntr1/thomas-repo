using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class SwapManager : MonoBehaviour
{
    private GameObject doorObject;
    private ARSessionOrigin origin;
    private GameObject[] prefabArray;

    private static SwapManager instance;

    private void Awake()
    {
        instance = this;

       



      
    }

    public static SwapManager GetInstance()
    {
        return instance;
    }

    public void SetDoor(GameObject door)
    {
        doorObject = door;
    }

    public void NextDoor()
    {
        if(doorObject == null)
        {
            Debug.Log("Door is not placed yet, cant swap! Returning!");
            return;
        }

        //origin = GetComponent<ARSessionOrigin>();
        //origin.
    }
}

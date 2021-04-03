using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public partial class RotateAround2 : MonoBehaviour
{

    public Transform rotierer;
    public Transform rotierachseBezug;
    public float rotationZeit = 1.0f;
    public float rotationWinkel = 90.0f;
    private bool rotating = false;
    private bool tuerAuf = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !rotating && tuerAuf)
        {
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, -rotationWinkel, rotationZeit));
            tuerAuf = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rotating && !tuerAuf)
        {
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, rotationWinkel, rotationZeit));
            tuerAuf = true;
        }
    }

    private IEnumerator Rotate(Transform rotierer, Transform rotierachseBezug, Vector3 rotateAxis, float degrees, float totalTime)
    {
        if (rotating)
        {
            yield return null;
        }
        else
        {
            rotating = true;
        }


        float rate = degrees / totalTime;

        //Start Rotate
        for (float i = 0.0f; Mathf.Abs(i) < Mathf.Abs(degrees); i += Time.deltaTime * rate)
        {
            rotierer.RotateAround(rotierachseBezug.position, rotateAxis, Time.deltaTime * rate);
            yield return null;
        }

        rotating = false;
    }

}

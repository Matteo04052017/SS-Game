using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchGrab : MonoBehaviour
{
    private GameObject MoveThisObj;
    private GameObject otherhand;

    // Use this for initialization
    void Start()
    {
        if(gameObject.name=="lefthand")
            otherhand = GameObject.Find("righthand");
        else
            otherhand = GameObject.Find("lefthand");
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveThisObj != null)
        {
            if (MoveThisObj.GetComponent<Rigidbody>() == null)
                MoveThisObj = null;
            else
                MoveThisObj.transform.position = gameObject.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag != "kinect")
            return;
        
        //if (MoveThisObj != null)
        //    return;

        if (otherhand.GetComponent<OnTouchGrab>().MoveThisObj == collision.gameObject)
            return;

        if (collision.gameObject.transform.parent != null)
            MoveThisObj = collision.gameObject.transform.parent.gameObject;
        else
            MoveThisObj = collision.gameObject;

        if (otherhand.GetComponent<OnTouchGrab>().MoveThisObj == MoveThisObj)
            MoveThisObj = null;
    }
}
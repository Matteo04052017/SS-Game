using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbitCollision : MonoBehaviour
{
    public AudioSource Failure;
    public AudioSource Success;
    public GameObject Victory;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    string lock_str = "lock_str";
    void OnTriggerEnter(Collider otherCollider)
    {
        lock (lock_str)
        {
            if (otherCollider.name == "GrabVolumeBig" || otherCollider.name == "GrabVolumeSmall" || otherCollider.name == "GrabVolumeCone")
                return;

            print("OnTriggerEnter.otherCollider.name=" + otherCollider.name);
            if (otherCollider.name == this.gameObject.name)
            {
                Success.Play();
                gameObject.SetActive(false);
                (otherCollider.gameObject.GetComponent("DistanceGrabbable") as DistanceGrabbable).SnapToOriginAfterRelease = true;

                CheckSuccess();
            }
            else
            {
                Failure.Play();
            }
        }
    }

    void CheckSuccess()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Orbita");
        foreach (var item in objs)
        {
            if (item.activeInHierarchy)
                return;
        }
        if(Victory!=null)
            Victory.SetActive(true);
    }

    void OnTriggerExit(Collider otherCollider)
    {
        gameObject.SetActive(true);
    }
}

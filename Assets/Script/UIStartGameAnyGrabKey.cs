using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartGameAnyGrabKey : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
            SceneManager.LoadScene("SolarSystem --version demo 2");
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
            SceneManager.LoadScene("SolarSystem --version demo 2");
    }
}

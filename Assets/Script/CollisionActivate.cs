using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionActivate : MonoBehaviour
{
    MeshRenderer meshRenderer = null;

    public GameObject[] AudioObjs;
    public bool _DisableGlassEffect = true;

    private bool showAudio = false;
    private bool showAudioOnlyOnce = false;

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        foreach (GameObject item in AudioObjs)
            item.SetActive(false);
        if (!_DisableGlassEffect)
            meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (showAudio && !showAudioOnlyOnce)
        {
            foreach (GameObject item in AudioObjs)
                item.SetActive(true);

            showAudioOnlyOnce = true;
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (!_DisableGlassEffect)
            meshRenderer.enabled = true;
        showAudio = true;
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (!_DisableGlassEffect)
            meshRenderer.enabled = false;
    }
}

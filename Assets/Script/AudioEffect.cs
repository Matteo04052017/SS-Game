using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public float speed = 1.0f;
    public float height = 1.0f;
    public bool DestroyAfterExecution = false;
    private bool started = false;
    private bool DoNotDestroy = false;
    // Use this for initialization
    void Start()
    {
        //transform.Find("Particles").GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(transform.parent.position.x,
            transform.parent.position.y + height, transform.parent.position.z), Vector3.up, Time.deltaTime * speed);

        if (!GetComponent<AudioSource>().isPlaying)
            transform.Find("Particles").GetComponent<ParticleSystem>().Stop();

        if (started && DestroyAfterExecution && !DoNotDestroy && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void RequestStop()
    {
        GetComponent<AudioSource>().Stop();
        DoNotDestroy = true;
    }

    string lock_str = "lock_str";
    void OnTriggerEnter(Collider otherCollider)
    {
        lock (lock_str)
        {
            if (otherCollider.name == "DetectGrabRange")
                return;

            foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
                if (this != item && item.GetComponent<AudioSource>().isPlaying)
                    item.GetComponent<AudioEffect>().RequestStop();
            DoNotDestroy = false;
            GetComponent<AudioSource>().Play();
            transform.Find("Particles").GetComponent<ParticleSystem>().Play();
            started = true;
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        lock (lock_str)
        {
            if (!GetComponent<AudioSource>().isPlaying)
                transform.Find("Particles").GetComponent<ParticleSystem>().Stop();
        }
    }
}

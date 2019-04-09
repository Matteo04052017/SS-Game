using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeReaction : MonoBehaviour
{
    public bool Simulate = false;
    public GameObject RaggioGamma;
    public GameObject Composite;
    public GameObject He4;
    public GameObject Be8;
    public GameObject C12;

    public float Be8ReactionTime = 10;

    private bool onCommand = false;
    private float Be8Rection_startTime = 0;
    private MyParticleScript MyParticleScript;

    // Use this for initialization
    void Start()
    {
        if (Simulate)
            MyParticleScript = GameObject.Find("MyPersonalParticleSystem").GetComponent<MyParticleScript>();
        onCommand = false;
        if (tag == "Be8")
            Be8Rection_startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Be8" && ((Time.time - Be8Rection_startTime) > Be8ReactionTime))
        {
            Be8InverseRection();
        }

        Rigidbody r = gameObject.GetComponent<Rigidbody>();
        if (r != null && !Simulate)
        {
            r.position = new Vector3(r.position.x, r.position.y, -2.69f);
        }
    }

    private static int numberCollisions = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider>() != null && Simulate)
        {
            Vector3 toReflect = gameObject.GetComponent<Rigidbody>().velocity;
            Vector3 normal = collision.gameObject.transform.position;

            if (collision.gameObject.name == "sx")
                normal = Vector3.right;
            if (collision.gameObject.name == "dx")
                normal = Vector3.left;
            if (collision.gameObject.name == "up")
                normal = Vector3.down;
            if (collision.gameObject.name == "down")
                normal = Vector3.up;
            if (collision.gameObject.name == "front")
                normal = Vector3.back;
            if (collision.gameObject.name == "back")
                normal = Vector3.forward;

            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(toReflect, normal) * 50);
            return;
        }

        if (collision.gameObject.GetComponent<BoxCollider>() != null)
            return;

        numberCollisions++;
        if (Simulate && (numberCollisions % UnityEngine.Random.Range(1, MyParticleScript.SimulationRandomity) != 0))
            return;

        if (gameObject.tag == "kinect" || collision.gameObject.tag == "kinect")
            return;

        if (gameObject.tag == "Be8" && collision.gameObject.tag == "He4")
            C12Reaction(collision);

        if (gameObject.tag == "He4" && collision.gameObject.tag == "He4")
            Be8Rection(collision);

        if (gameObject.tag == "D" && collision.gameObject.transform.childCount == 0)
            Elio3Reaction(collision);

        // reazione che coinvolge solo due atomi di elio3
        if (gameObject.tag == "He3" && collision.gameObject.tag == "He3")
            Elio4Reaction(collision);
    }

    private void C12Reaction(Collision collision)
    {
        if (gameObject.tag != "Be8")
            return;

        GameObject c12 = Instantiate(C12, gameObject.transform.position, Quaternion.identity);
        //if (Simulate)
        //    c12.transform.localScale = MyParticleScript.SimulationScale;
        c12.GetComponent<CompositeReaction>().Simulate = Simulate;
        c12.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-200, 200));
        CreateGammaRay();

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void CreateGammaRay()
    {
        //raggio gamma
        GameObject raggiogamma = Instantiate(RaggioGamma);
        raggiogamma.transform.position = gameObject.transform.position;
        VolumetricLines.VolumetricLineBehavior behavior = raggiogamma.GetComponent<VolumetricLines.VolumetricLineBehavior>();
        behavior.StartPos = gameObject.transform.position;
        Vector3 force = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), behavior.StartPos.z);
        behavior.EndPos = force;
        //raggiogamma.transform.rotation = Quaternion.LookRotation(behavior.EndPos - behavior.StartPos);// raggiogamma.transform.forward);
        raggiogamma.GetComponent<Rigidbody>().AddForce((behavior.EndPos - behavior.StartPos) * 50);
        Destroy(raggiogamma, 15);
    }

    private void Be8InverseRection()
    {
        float scaleFactor = 1.9f;
        GameObject he4 = Instantiate(He4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (He4.gameObject.transform.localScale.y / scaleFactor), 0), Quaternion.identity);
        //if (Simulate)
        //    he4.transform.localScale = MyParticleScript.SimulationScale;
        he4.GetComponent<CompositeReaction>().Simulate = Simulate;
        he4.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-200, 200));
        he4 = Instantiate(He4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (He4.gameObject.transform.localScale.y / scaleFactor), 0), Quaternion.identity);
        //if (Simulate)
        //    he4.transform.localScale = MyParticleScript.SimulationScale;
        he4.GetComponent<CompositeReaction>().Simulate = Simulate;
        he4.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-200, 200));
        Destroy(gameObject);
    }

    private void Be8Rection(Collision collision)
    {
        if (collision.gameObject.GetComponent<CompositeReaction>().onCommand)
            return;

        onCommand = true;

        GameObject be8 = Instantiate(Be8, gameObject.transform.position, gameObject.transform.rotation);
        if (Simulate)
            be8.transform.localScale = MyParticleScript.SimulationScale;
        be8.GetComponent<CompositeReaction>().Simulate = Simulate;
        be8.tag = "Be8";
        be8.GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-200, 200));

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void Elio3Reaction(Collision collision)
    {
        collision.gameObject.transform.SetParent(gameObject.transform);
        collision.gameObject.transform.localPosition = collision.gameObject.transform.position = new Vector3(0, 0, .2f);

        gameObject.tag = "He3";

        CreateGammaRay();
    }

    private void Elio4Reaction(Collision collision)
    {
        if (collision.gameObject.GetComponent<CompositeReaction>().onCommand)
            return;

        onCommand = true;

        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        int i = 1;
        foreach (var item in children)
        {
            if (item.gameObject.tag == "N")
                item.localPosition = item.position = new Vector3(0, 0, -.2f);

            if (item.gameObject.tag == "H")
            {
                item.localPosition = item.position = new Vector3(0, 0.2f * i, 0);
                i = i * -1;
            }
        }

        bool first = true;
        children = collision.gameObject.GetComponentsInChildren<Transform>();
        foreach (var item in children)
        {
            if (item.gameObject.tag == "N")
            {
                item.SetParent(gameObject.transform);
                item.localPosition = item.position = new Vector3(0, 0, .2f);
            }
            if (item.gameObject.tag == "H")
            {
                float ForceZ = 0;
                if (Simulate)
                    ForceZ = UnityEngine.Random.Range(0, MyParticleScript.MaxZ);

                item.SetParent(null);
                item.gameObject.AddComponent<Rigidbody>();
                item.gameObject.GetComponent<Rigidbody>().useGravity = false;
                if (first)
                {
                    item.localPosition = item.position = new Vector3(item.position.x + 1, item.position.y, item.position.z);

                    item.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, UnityEngine.Random.Range(-200, 200), ForceZ));
                }
                else
                {
                    item.localPosition = item.position = new Vector3(item.position.x - 1, item.position.y, item.position.z);
                    item.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-200, 200), 0, ForceZ));
                }



                first = false;
            }
        }
        gameObject.tag = "He4";

        //if (Simulate)
        //    gameObject.transform.localScale = MyParticleScript.SimulationScale;

        Destroy(collision.gameObject);

        onCommand = false;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleScript : MonoBehaviour
{
    public int SimulationRandomity = 2000;
    public Vector3 SimulationScale = new Vector3(1, 1, 1);
    public GameObject Hidrogen;
    public GameObject He3;
    private float MinX = 0, MaxX = 0;
    private float MinY = 0, MaxY = 0;
    public float MinZ = -13.68f, MaxZ = 252.66f;
    public int MaxParticle;

    private List<GameObject> lGameObject;
    private int LastCreationTime;
    private Vector2 screenSize;
    private int indexCreation = 0;
    // Use this for initialization
    void Start()
    {
        lGameObject = new List<GameObject>();
        //for (int i = 0; i < MaxParticle; i++)
        //{
        //    CreateObject();
        //}
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        MinX = cameraPos.x - screenSize.x;
        MaxX = cameraPos.x + screenSize.x;
        MinY = cameraPos.y - screenSize.y;
        MaxY = cameraPos.y + screenSize.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hidrogen == null || He3 == null)
            return;

        if (lGameObject.Count >= MaxParticle)
            return;

        CreateObject();
    }

    private void CreateObject()
    {
        GameObject obj;
        if (indexCreation >= 3)
        {
            obj = Instantiate(He3, new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ)), Quaternion.identity);
            obj.GetComponent<CompositeReaction>().Simulate = true;
            indexCreation = 0;
        }
        else
        {
            obj = Instantiate(Hidrogen, new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ)), Quaternion.identity);
            obj.GetComponent<DeuterioReaction>().Simulate = true;
        }
        lGameObject.Add(obj);
        obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), Random.Range(-200, 200)));
        indexCreation++;
    }
}

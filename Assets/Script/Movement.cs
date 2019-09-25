using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public const float DistanzaRiferimentoSpazio = 1.879157f;
    public const float ReduceVel = 0.1f;
    //default per terra
    [SerializeField]
    public float PeriodoRivoluzione = 1;
    [SerializeField]
    public float PeriodoRotazione = 1;
    [SerializeField]
    public bool OnlySelfRotation = false;
    [SerializeField]
    public bool RandomInitialPosition = true;

    Quaternion rotation;
    float currentRotation = 0.0f;
    float deltaRotation = 0.0f;
    Vector3 radius = new Vector3(5, 0, 0);
    [SerializeField]
    public GameObject TargetObj = null;

    // Use this for initialization
    void Start()
    {
        radius = new Vector3(transform.position.z - TargetObj.transform.position.z, 0, 0);
        if (PeriodoRivoluzione > 0)
            deltaRotation = ((2 * Mathf.PI * radius.x) / PeriodoRivoluzione) / ReduceVel;
        else
            deltaRotation = 0;

        if (RandomInitialPosition && !OnlySelfRotation && !name.Equals("Moon") && TargetObj == GameObject.Find("Sole"))
        {
            currentRotation = (Random.value * 10000000000000000000) * deltaRotation;
            transform.RotateAround(TargetObj.transform.position, Vector3.up, currentRotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (!OnlySelfRotation)
        {
            currentRotation = Time.deltaTime * deltaRotation;
            transform.RotateAround(TargetObj.transform.position, Vector3.up, currentRotation);
        }
        if (PeriodoRotazione != 0)
        {
            float VelRotazione = (Time.deltaTime * ((2 * Mathf.PI) / PeriodoRotazione));
            transform.Rotate(0, VelRotazione, 0);
        }
    }
}

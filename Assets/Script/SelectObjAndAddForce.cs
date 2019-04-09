using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjAndAddForce : MonoBehaviour
{
    private GameObject _Nearest;
    private Vector3 _InitialMousePosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _Nearest = GetNearestGameObject();
            _InitialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_Nearest == null || _Nearest.GetComponent<Rigidbody>() == null)
                return;

            Vector3 direction = Input.mousePosition - _InitialMousePosition;
            direction.z = 0;
            _Nearest.GetComponent<Rigidbody>().AddForce(direction);
        }
    }

    private GameObject GetNearestGameObject()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        GameObject[] allGameObjs = gameObject.scene.GetRootGameObjects();
        foreach (var item in allGameObjs)
        {
            if (item.GetComponent<Rigidbody>() == null)
                continue;

            float dist = Mathf.Abs(item.transform.position.z - Camera.main.transform.position.z);
            Vector3 v3Pos = new Vector3(_InitialMousePosition.x, _InitialMousePosition.y, dist);
            v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            float dSqrToTarget = Vector3.Distance(v3Pos, item.transform.position);
            //Vector3 directionToTarget = item.transform.localPosition - _InitialMousePosition;
            //float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = item;
            }
        }
        return bestTarget;
    }
}

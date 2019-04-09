using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenColliders : MonoBehaviour
{
    private Vector2 screenSize;

    void Start()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        UpdateColliders(cameraPos);
    }

    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        float x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        float y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        if (x != screenSize.x || y != screenSize.y)
            UpdateColliders(cameraPos);
    }

    private void UpdateColliders(Vector3 cameraPos)
    {
        GameObject sx = GameObject.Find("sx");
        sx.transform.position = new Vector3(cameraPos.x - screenSize.x, sx.transform.position.y, sx.transform.position.z);
        GameObject dx = GameObject.Find("dx");
        dx.transform.position = new Vector3(cameraPos.x + screenSize.x, dx.transform.position.y, dx.transform.position.z);
        GameObject up = GameObject.Find("up");
        up.transform.position = new Vector3(up.transform.position.x, cameraPos.y + screenSize.y, up.transform.position.z);
        GameObject down = GameObject.Find("down");
        down.transform.position = new Vector3(down.transform.position.x, cameraPos.y - screenSize.y, down.transform.position.z);
    }
}
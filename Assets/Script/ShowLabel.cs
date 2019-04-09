using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLabel : MonoBehaviour {
    public float offset = .5f;
    private Text text;
    private GameObject textGO;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
            return;

        if (gameObject.transform.parent != null)
        {
            Destroy(textGO);
            return;
        }

        // Offset position above object bbox (in world space)
        float offsetPosY = gameObject.transform.position.y + (gameObject.transform.localScale.y / 2) + offset;
        
        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(gameObject.transform.position.x, offsetPosY, gameObject.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);
        
        if (text == null)
        {
            // Create the Text GameObject.
            textGO = new GameObject();
            textGO.transform.parent = canvas.transform;
            text = textGO.AddComponent<Text>();
            text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
        }

        text.text = gameObject.tag;
        text.rectTransform.position = screenPoint;
    }

    private void OnDestroy()
    {
        Destroy(textGO);
    }
}

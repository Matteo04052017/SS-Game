using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Show off all the Debug UI components.
public class UIStartGame : MonoBehaviour
{
    bool inMenu;

	void Start ()
    {
        DebugUIBuilder.instance.AddLabel("Riscostruisci il sistema solare");
        DebugUIBuilder.instance.AddLabel("Attraverso i controller puoi muoverti nella scena, afferrare gli oggetti e posizionarli nell'obita corretta");
        DebugUIBuilder.instance.AddLabel("Scopri le orbite dei pianeti attraverso i pannelli informativi presenti nella scena");
        DebugUIBuilder.instance.AddDivider();
        DebugUIBuilder.instance.AddButton("Vai al gioco", StartGame);
        
        DebugUIBuilder.instance.Show();
        inMenu = true;
	}
    
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("SolarSystem --version demo 2");
    }
}

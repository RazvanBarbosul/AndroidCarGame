using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Canvas WelcomeScreen;

    [SerializeField]
    private Canvas MainMenuScreen;

    [SerializeField]
    private Canvas RaceSelectionScreen;

    [SerializeField]
    private Canvas CarSelectionScreen;


    public void OnClickStartButton()
    {
        MainMenuScreen.gameObject.SetActive(true);
        WelcomeScreen.gameObject.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        MainMenuScreen.gameObject.SetActive(false);
        WelcomeScreen.gameObject.SetActive(true);
        RaceSelectionScreen.gameObject.SetActive(false);
        CarSelectionScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRaceButton()
    {
        RaceSelectionScreen.gameObject.SetActive(false);
        CarSelectionScreen.gameObject.SetActive(true);
    }

    public void OnClickCampaignButton()
    {
        MainMenuScreen.gameObject.SetActive(false);
        RaceSelectionScreen.gameObject.SetActive(true);
    }
}

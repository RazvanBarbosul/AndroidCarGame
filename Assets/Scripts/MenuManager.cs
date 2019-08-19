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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

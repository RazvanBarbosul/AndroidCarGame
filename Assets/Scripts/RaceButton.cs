using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RaceButton : MonoBehaviour
{
    public int[] ScenesToLoad;
    public Button Btn;
    public Sprite ButtonSprite;
    public Image ButtonImage;
    // Start is called before the first frame update
    void Start()
    {
        ButtonImage.overrideSprite = ButtonSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

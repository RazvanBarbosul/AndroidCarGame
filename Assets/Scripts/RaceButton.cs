using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RaceButton : MonoBehaviour
{
    [System.Serializable]
    public struct Oponent
    {
        public int TopSpeed;
        public float Acceleration;
        public int Handling;
        public string Drive;
        public string Tyres;
        public Sprite CarImage;
    };


    public int[] ScenesToLoad;
    public Oponent[] Oponents;
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

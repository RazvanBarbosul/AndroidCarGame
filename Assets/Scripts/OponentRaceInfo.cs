using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OponentRaceInfo : MonoBehaviour
{
    [System.Serializable]
    public struct CarStats
    {
        public int TopSpeed;
        public float Acceleration;
        public int Handling;
        public string Drive;
        public string Tyres;
        public Sprite CarImage;
    };

    public CarStats CarInfo;

    [SerializeField]
    TextMeshProUGUI CarSpeed;

    [SerializeField]
    TextMeshProUGUI CarAcceleration;

    [SerializeField]
    TextMeshProUGUI CarHandling;

    [SerializeField]
    TextMeshProUGUI CarDrive;

    [SerializeField]
    TextMeshProUGUI CarTyres;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().overrideSprite = CarInfo.CarImage;
        this.CarSpeed.SetText(CarInfo.TopSpeed.ToString());
        this.CarAcceleration.SetText(CarInfo.Acceleration.ToString());
        this.CarHandling.SetText(CarInfo.Handling.ToString());
        this.CarDrive.SetText(CarInfo.Drive);
        this.CarTyres.SetText(CarInfo.Tyres);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        this.GetComponent<Image>().overrideSprite = CarInfo.CarImage;
        this.CarSpeed.SetText(CarInfo.TopSpeed.ToString());
        this.CarAcceleration.SetText(CarInfo.Acceleration.ToString());
        this.CarHandling.SetText(CarInfo.Handling.ToString());
        this.CarDrive.SetText(CarInfo.Drive);
        this.CarTyres.SetText(CarInfo.Tyres);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    private int CurrentLoadedSceneIndex;

    public GameObject OponentsContainer;

    public static GameManager Get()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("GameManager Instance: " + (Instance == this));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(OponentsContainer == null)
        {
            OponentsContainer = GameObject.FindGameObjectWithTag("OponentsContainer");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupOponetInfo(RaceButton Button)
    {
        if (Button)
        {
            if (OponentsContainer == null)
            {
                OponentsContainer = GameObject.FindGameObjectWithTag("OponentsContainer");
            }

            if(OponentsContainer)
            {
                for (int i = 0; i < OponentsContainer.transform.childCount - 1; i++)
                {
                    GameObject OponentRace = OponentsContainer.transform.GetChild(i).gameObject;

                    OponentRaceInfo RaceInfo = OponentRace.GetComponent<OponentRaceInfo>();

                    RaceInfo.CarInfo.Acceleration = Button.Oponents[i].Acceleration;
                    RaceInfo.CarInfo.TopSpeed = Button.Oponents[i].TopSpeed;
                    RaceInfo.CarInfo.Handling = Button.Oponents[i].Handling;
                    RaceInfo.CarInfo.Drive = Button.Oponents[i].Drive;
                    RaceInfo.CarInfo.Tyres = Button.Oponents[i].Tyres;
                    RaceInfo.CarInfo.CarImage = Button.Oponents[i].CarImage;
                }
            }
            
        }
    }
}

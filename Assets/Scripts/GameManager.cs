using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Vehicles.Car;

public class GameManager : MonoBehaviour
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


    private static GameManager Instance;
    private int CurrentLoadedSceneIndex;
    public OponentRaceInfo[] RaceInformations;
    public CarButton[] PlayerCarInformations;
    public List<GameObject> DropPoints;
    public Button StartRacesButton;
    public int PlayerScore = 0;
    public int OponentScore = 0;
    public float PlayerRaceDuration = 0;
    public float OponentRaceDuration = 0;
    public bool bRaceStarted = false;
    public int[] ScenesToLoad;

    public GameObject OponentsContainer;
    public GameObject PlayerCarContainer;

    public CarStats[] PlayerCarsStats;
    public CarStats[] OponentCarStats;

    [SerializeField]
    private int CurrentRaceIndex = 0;

    [SerializeField]
    TextMeshProUGUI PlayerScoreText;

    [SerializeField]
    TextMeshProUGUI OponentScoreText;

    [SerializeField]
    TextMeshProUGUI RaceResult;

    [SerializeField]
    private Canvas WaitScreen;

    [SerializeField]
    private Canvas InGameScreen;

    [SerializeField]
    TextMeshProUGUI PlayerSpeedText;

    [SerializeField]
    TextMeshProUGUI OponentSpeedText;

    public OponentRaceInfo PlayerRaceInfo;
    public OponentRaceInfo OponentRaceInfo;

    public OponentRaceInfo InGamePlayerRaceInfo;
    public OponentRaceInfo InGameOponentRaceInfo;
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
            RaceInformations = new OponentRaceInfo[5];
            PlayerCarInformations = new CarButton[5];
            PlayerCarsStats = new CarStats[5];
            OponentCarStats = new CarStats[5];
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
        FinishLine FL = GameObject.FindObjectOfType<FinishLine>();

        if (FL)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && bRaceStarted && FL.bRaceEnded)
            {
                OnClickStartBtn();
            }
        }

        if (bRaceStarted)
        {
            RaceManager RaceGuru = GameObject.FindObjectOfType<RaceManager>();

            if (RaceGuru)
            {
                int PlayerSp = (int)(RaceGuru.PlayerCar.GetComponent<CarController>().CurrentSpeed);
                int OponentSp = (int)(RaceGuru.OponentCar.GetComponent<CarController>().CurrentSpeed);
                PlayerSpeedText.SetText(PlayerSp.ToString() + "MPH");
                OponentSpeedText.SetText(OponentSp.ToString() + "MPH");
            }
        }
    }

    public void SetupOponetInfo(RaceButton Button)
    {
        if (Button)
        {
            if (OponentsContainer == null)
            {
                OponentsContainer = GameObject.FindGameObjectWithTag("OponentsContainer");
            }

            ScenesToLoad = Button.ScenesToLoad;

            if (OponentsContainer)
            {
                for (int i = 0; i < OponentsContainer.transform.childCount ; i++)
                {
                    GameObject OponentRace = OponentsContainer.transform.GetChild(i).gameObject;

                    OponentRaceInfo RaceInfo = OponentRace.GetComponent<OponentRaceInfo>();

                    RaceInfo.CarInfo.Acceleration = Button.Oponents[i].Acceleration;
                    RaceInfo.CarInfo.TopSpeed = Button.Oponents[i].TopSpeed;
                    RaceInfo.CarInfo.Handling = Button.Oponents[i].Handling;
                    RaceInfo.CarInfo.Drive = Button.Oponents[i].Drive;
                    RaceInfo.CarInfo.Tyres = Button.Oponents[i].Tyres;
                    RaceInfo.CarInfo.CarImage = Button.Oponents[i].CarImage;
                    RaceInformations[i] = RaceInfo;

                    OponentCarStats[i].Acceleration = Button.Oponents[i].Acceleration;
                    OponentCarStats[i].TopSpeed = Button.Oponents[i].TopSpeed;
                    OponentCarStats[i].Handling = Button.Oponents[i].Handling;
                    OponentCarStats[i].Drive = Button.Oponents[i].Drive;
                    OponentCarStats[i].Tyres = Button.Oponents[i].Tyres;
                    OponentCarStats[i].CarImage = Button.Oponents[i].CarImage;
                }
            }
            
        }
    }

    public void CheckCars()
    {
        for(int i =0; i < PlayerCarInformations.Length; i++)
        {
            PlayerCarInformations[i] = null;
        }

        int StatsIndex = 0;

        if (!PlayerCarContainer)
        {
            PlayerCarContainer = GameObject.FindGameObjectWithTag("PlayerTagContainer");
        }

        for(int i = 0; i < PlayerCarContainer.transform.childCount; i++)
        {
            GameObject Temp = PlayerCarContainer.transform.GetChild(i).gameObject;
            CarButton CarToCheck = Temp.GetComponent<CarButton>();

            if(CarToCheck && CarToCheck.AssignedDropPoint)
            {
                PlayerCarInformations[DropPoints.IndexOf(CarToCheck.AssignedDropPoint)] = CarToCheck;
                PlayerCarsStats[StatsIndex].Acceleration = CarToCheck.CarInfo.Acceleration;
                PlayerCarsStats[StatsIndex].CarImage = CarToCheck.CarInfo.CarImage;
                PlayerCarsStats[StatsIndex].Drive = CarToCheck.CarInfo.Drive;
                PlayerCarsStats[StatsIndex].Handling = CarToCheck.CarInfo.Handling;
                PlayerCarsStats[StatsIndex].TopSpeed = CarToCheck.CarInfo.TopSpeed;
                PlayerCarsStats[StatsIndex].Tyres = CarToCheck.CarInfo.Tyres;
                StatsIndex ++;

            }
        
        }

        
        bool bShowStartButton = false;

        for(int i =0; i < PlayerCarInformations.Length; i++)
        {
            if(PlayerCarInformations[i] != null)
            {
                bShowStartButton = true;
            }
            else
            {
                bShowStartButton = false;
                break;
            }
        }

        if (bShowStartButton)
        {
            StartRacesButton.gameObject.SetActive(true);
        }
        else
        {
            StartRacesButton.gameObject.SetActive(false);
        }
    }

    public void ShowWaitScreen(bool bPlayerWon)
    {
        WaitScreen.gameObject.SetActive(true);
        InGameScreen.gameObject.SetActive(false);
        
        PlayerScoreText.SetText(PlayerScore.ToString());
        OponentScoreText.SetText(OponentScore.ToString());

        //PlayerRaceInfo.CarInfo.Acceleration = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Acceleration;
        //PlayerRaceInfo.CarInfo.CarImage = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.CarImage;
        //PlayerRaceInfo.CarInfo.Drive = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Drive;
        //PlayerRaceInfo.CarInfo.Handling = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Handling;
        //PlayerRaceInfo.CarInfo.TopSpeed = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.TopSpeed;
        //PlayerRaceInfo.CarInfo.Tyres = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Tyres;

        PlayerRaceInfo.CarInfo.Acceleration = PlayerCarsStats[CurrentLoadedSceneIndex].Acceleration;
        PlayerRaceInfo.CarInfo.CarImage = PlayerCarsStats[CurrentLoadedSceneIndex].CarImage;
        PlayerRaceInfo.CarInfo.Drive = PlayerCarsStats[CurrentLoadedSceneIndex].Drive;
        PlayerRaceInfo.CarInfo.Handling = PlayerCarsStats[CurrentLoadedSceneIndex].Handling;
        PlayerRaceInfo.CarInfo.TopSpeed = PlayerCarsStats[CurrentLoadedSceneIndex].TopSpeed;
        PlayerRaceInfo.CarInfo.Tyres = PlayerCarsStats[CurrentLoadedSceneIndex].Tyres;

        PlayerRaceInfo.Setup();
       

        OponentRaceInfo.CarInfo.Acceleration = OponentCarStats[CurrentLoadedSceneIndex].Acceleration;//RaceInformations[CurrentLoadedSceneIndex];
        OponentRaceInfo.CarInfo.CarImage = OponentCarStats[CurrentLoadedSceneIndex].CarImage;
        OponentRaceInfo.CarInfo.Drive = OponentCarStats[CurrentLoadedSceneIndex].Drive;
        OponentRaceInfo.CarInfo.Handling = OponentCarStats[CurrentLoadedSceneIndex].Handling;
        OponentRaceInfo.CarInfo.TopSpeed = OponentCarStats[CurrentLoadedSceneIndex].TopSpeed;
        OponentRaceInfo.CarInfo.Tyres = OponentCarStats[CurrentLoadedSceneIndex].Tyres;
        OponentRaceInfo.Setup();
       

        if(bPlayerWon)
        {
            RaceResult.SetText("RACE WON!");
        }
        else
        {
            RaceResult.SetText("RACE LOST!");
        }

        RaceManager RM = GameObject.FindObjectOfType<RaceManager>();

        if (RM)
        {
            RM.PlayerCar.GetComponent<CarController>().m_Topspeed = 0;
            RM.OponentCar.GetComponent<CarController>().m_Topspeed = 0;
        }
    }

    public void HideWaitScreen()
    {
        WaitScreen.gameObject.SetActive(false);
        InGameScreen.gameObject.SetActive(true);
    }

    public void StartRace(int RaceNum)
    {
        if(RaceNum < ScenesToLoad.Length - 1)
        {
            InGameOponentRaceInfo.gameObject.SetActive(false);
            InGamePlayerRaceInfo.gameObject.SetActive(false);
            PlayerSpeedText.gameObject.SetActive(false);
            OponentSpeedText.gameObject.SetActive(false);
            SceneManager.LoadScene(ScenesToLoad[RaceNum]);
            //RaceManager RaceGuru = GameObject.FindObjectOfType<RaceManager>();

            //if (RaceGuru)
            //{
            //    RaceGuru.SetupCars(PlayerCarInformations[CurrentLoadedSceneIndex], RaceInformations[CurrentLoadedSceneIndex]);
            //}
            //CurrentLoadedSceneIndex++;
            StartCoroutine(SetupCars());
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }

    IEnumerator SetupCars()
    {
        yield return new WaitForSeconds(3);

        FinishCarSetup();
    }

    public void FinishCarSetup()
    {
        RaceManager RaceGuru = GameObject.FindObjectOfType<RaceManager>();

        if (RaceGuru)
        {
            //CarButton Player = PlayerCarInformations[CurrentLoadedSceneIndex];
            //OponentRaceInfo Oponent = RaceInformations[CurrentLoadedSceneIndex];
            CarStats Player = PlayerCarsStats[CurrentLoadedSceneIndex];
            CarStats Oponent = OponentCarStats[CurrentLoadedSceneIndex];
            RaceGuru.SetupCars(Player, Oponent);
            bRaceStarted = true;

            InGamePlayerRaceInfo.gameObject.SetActive(true);
            InGamePlayerRaceInfo.CarInfo.CarImage = Player.CarImage;
            InGamePlayerRaceInfo.CarInfo.Acceleration = Player.Acceleration;
            InGamePlayerRaceInfo.CarInfo.Drive = Player.Drive;
            InGamePlayerRaceInfo.CarInfo.Handling = Player.Handling;
            InGamePlayerRaceInfo.CarInfo.TopSpeed = Player.TopSpeed;
            InGamePlayerRaceInfo.CarInfo.Tyres = Player.Tyres;

            InGamePlayerRaceInfo.Setup();

            InGameOponentRaceInfo.gameObject.SetActive(true);
            InGameOponentRaceInfo.CarInfo.Acceleration = Oponent.Acceleration;//RaceInformations[CurrentLoadedSceneIndex];
            InGameOponentRaceInfo.CarInfo.CarImage = Oponent.CarImage;
            InGameOponentRaceInfo.CarInfo.Drive = Oponent.Drive;
            InGameOponentRaceInfo.CarInfo.Handling = Oponent.Handling;
            InGameOponentRaceInfo.CarInfo.TopSpeed = Oponent.TopSpeed;
            InGameOponentRaceInfo.CarInfo.Tyres = Oponent.Tyres;
            InGameOponentRaceInfo.Setup();
            PlayerSpeedText.gameObject.SetActive(true);
            OponentSpeedText.gameObject.SetActive(true);
            InGameScreen.gameObject.SetActive(true);
        }
        CurrentLoadedSceneIndex++;
    }
     public void OnClickStartBtn()
    {
        bRaceStarted = false;
        HideWaitScreen();
        StartRace(CurrentLoadedSceneIndex);
    }

    public void UpdateInGameUI()
    {
        RaceManager RaceGuru = GameObject.FindObjectOfType<RaceManager>();

        if (RaceGuru)
        {
            CarStats Player = PlayerCarsStats[CurrentLoadedSceneIndex];
            CarStats Oponent = OponentCarStats[CurrentLoadedSceneIndex];

           
        }
       }
}

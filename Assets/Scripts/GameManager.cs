using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    [SerializeField]
    private int CurrentLoadedSceneIndex;
    [SerializeField]
    public OponentRaceInfo[] RaceInformations;
    [SerializeField]
    public CarButton[] PlayerCarInformations;
    [SerializeField]
    public List<GameObject> DropPoints;
    [SerializeField]
    public Button StartRacesButton;
    [SerializeField]
    public int PlayerScore = 0;
    [SerializeField]
    public int OponentScore = 0;
    [SerializeField]
    public float PlayerRaceDuration = 0;
    [SerializeField]
    public float OponentRaceDuration = 0;
    [SerializeField]
    public bool bRaceStarted = false;
    [SerializeField]
    public int[] ScenesToLoad;

    [SerializeField]
    public GameObject OponentsContainer;
    [SerializeField]
    public GameObject PlayerCarContainer;

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
    public OponentRaceInfo PlayerRaceInfo;
    [SerializeField]
    public OponentRaceInfo OponentRaceInfo;

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

        for(int i = 0; i < PlayerCarContainer.transform.childCount; i++)
        {
            GameObject Temp = PlayerCarContainer.transform.GetChild(i).gameObject;
            CarButton CarToCheck = Temp.GetComponent<CarButton>();

            if(CarToCheck && CarToCheck.AssignedDropPoint)
            {
                PlayerCarInformations[DropPoints.IndexOf(CarToCheck.AssignedDropPoint)] = CarToCheck;
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
        PlayerScoreText.SetText(PlayerScore.ToString());
        OponentScoreText.SetText(OponentScore.ToString());

        PlayerRaceInfo.CarInfo.Acceleration = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Acceleration;
        PlayerRaceInfo.CarInfo.CarImage = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.CarImage;
        PlayerRaceInfo.CarInfo.Drive = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Drive;
        PlayerRaceInfo.CarInfo.Handling = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Handling;
        PlayerRaceInfo.CarInfo.TopSpeed = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.TopSpeed;
        PlayerRaceInfo.CarInfo.Tyres = PlayerCarInformations[CurrentLoadedSceneIndex].CarInfo.Tyres;

        PlayerRaceInfo.Setup();
        OponentRaceInfo = RaceInformations[CurrentLoadedSceneIndex];
        OponentRaceInfo.Setup();

        if(bPlayerWon)
        {
            RaceResult.SetText("RACE WON!");
        }
        else
        {
            RaceResult.SetText("RACE LOST!");
        }
    }

    public void HideWaitScreen()
    {
        WaitScreen.gameObject.SetActive(false);
    }

    public void StartRace(int RaceNum)
    {
        SceneManager.LoadScene(ScenesToLoad[RaceNum]);
        //RaceManager RaceGuru = GameObject.FindObjectOfType<RaceManager>();

        //if (RaceGuru)
        //{
        //    RaceGuru.SetupCars(PlayerCarInformations[CurrentLoadedSceneIndex], RaceInformations[CurrentLoadedSceneIndex]);
        //}
        //CurrentLoadedSceneIndex++;
        StartCoroutine(SetupCars());

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
            CarButton Player = PlayerCarInformations[CurrentLoadedSceneIndex];
            OponentRaceInfo Oponent = RaceInformations[CurrentLoadedSceneIndex];
            RaceGuru.SetupCars(Player, Oponent);
        }
        CurrentLoadedSceneIndex++;
    }
     public void OnClickStartBtn()
    {
        StartRace(CurrentLoadedSceneIndex);
    }
}

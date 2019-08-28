using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class RaceManager : MonoBehaviour
{

    public GameObject PlayerCar;
    public GameObject OponentCar;
    public Transform RaceEnd;
    private GameManager GM;
    public GameObject FinishLine;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetupCars(GameManager.CarStats PlayerCarInfo, GameManager.CarStats OponentCarInfo)//(CarButton PlayerCarInfo, OponentRaceInfo OponentCarInfo)
    {
        //if(PlayerCarInfo && OponentCarInfo)
        //{
            #region PlayerCarSetup
            //Acceleration
            float Diff = (10.0f - PlayerCarInfo.Acceleration) / 0.7f * 100.0f;
            PlayerCar.GetComponent<CarController>().m_FullTorqueOverAllWheels = 3000 + Diff;

            //Drive
            if(PlayerCarInfo.Drive == "FWD")
            {
                PlayerCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FrontWheelDrive;
            }
            else if(PlayerCarInfo.Drive == "RWD")
            {
                PlayerCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.RearWheelDrive;
            }
            else
            {
                PlayerCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FourWheelDrive;
            }

            //Handling
           // PlayerCar.GetComponent<CarController>().m_SteerHelper = PlayerCarInfo.Handling / 10;
            #endregion

            #region OponentCarSetup
            //Acceleration
            Diff = (10.0f - OponentCarInfo.Acceleration) / 0.7f * 100.0f;
            OponentCar.GetComponent<CarController>().m_FullTorqueOverAllWheels = 3000 + Diff;

            //Drive
            if (OponentCarInfo.Drive == "FWD")
            {
                OponentCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FrontWheelDrive;
            }
            else if (OponentCarInfo.Drive == "RWD")
            {
                OponentCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.RearWheelDrive;
            }
            else
            {
                OponentCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FourWheelDrive;
            }

        //Handling
        // OponentCar.GetComponent<CarController>().m_SteerHelper = OponentCarInfo.Handling / 10;
        #endregion
        // }

        if (!GM)
        {
            GM = GameObject.FindObjectOfType<GameManager>();
        }

        if (GM)
        {
            GM.UpdateInGameUI();
        }
        StartRace(PlayerCarInfo, OponentCarInfo);
    }

    void StartRace(GameManager.CarStats PlayerCarInfo, GameManager.CarStats OponentCarInfo)
    {
        //if(PlayerCarInfo && OponentCarInfo)
        //{
            PlayerCar.GetComponent<CarController>().m_Topspeed = PlayerCarInfo.TopSpeed;
            OponentCar.GetComponent<CarController>().m_Topspeed = OponentCarInfo.TopSpeed;
            StartCoroutine(AssignEndPoint());
       // }
    }

    IEnumerator AssignEndPoint()
    {
        yield return new WaitForSeconds(5);
        //PlayerCar.GetComponent<CarAIControl>().m_Target = RaceEnd;
        //PlayerCar.GetComponent<CarAIControl>().m_StopWhenTargetReached = true;


        //OponentCar.GetComponent<CarAIControl>().m_Target = RaceEnd;
        //OponentCar.GetComponent<CarAIControl>().m_StopWhenTargetReached = true;
        FinishLine.gameObject.SetActive(true);
       // FinishLine.GetComponent<FinishLine>().FinishColider.gameObject.SetActive(true);
    }

   
}

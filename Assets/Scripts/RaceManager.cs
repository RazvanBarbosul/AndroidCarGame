using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class RaceManager : MonoBehaviour
{

    public GameObject PlayerCar;
    public GameObject OponentCar;
    public Transform RaceEnd;
    public GameObject FinishLine;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetupCars(CarButton PlayerCarInfo, OponentRaceInfo OponentCarInfo)
    {
        if(PlayerCarInfo && OponentCarInfo)
        {
            #region PlayerCarSetup
            //Acceleration
            float Diff = (10.0f - PlayerCarInfo.CarInfo.Acceleration) / 0.7f * 100.0f;
            PlayerCar.GetComponent<CarController>().m_FullTorqueOverAllWheels = 3000 + Diff;

            //Drive
            if(PlayerCarInfo.CarInfo.Drive == "FWD")
            {
                PlayerCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FrontWheelDrive;
            }
            else if(PlayerCarInfo.CarInfo.Drive == "RWD")
            {
                PlayerCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.RearWheelDrive;
            }
            else
            {
                PlayerCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FourWheelDrive;
            }

            //Handling
            PlayerCar.GetComponent<CarController>().m_SteerHelper = PlayerCarInfo.CarInfo.Handling / 10;
            #endregion

            #region OponentCarSetup
            //Acceleration
            Diff = (10.0f - OponentCarInfo.CarInfo.Acceleration) / 0.7f * 100.0f;
            OponentCar.GetComponent<CarController>().m_FullTorqueOverAllWheels = 3000 + Diff;

            //Drive
            if (OponentCarInfo.CarInfo.Drive == "FWD")
            {
                OponentCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FrontWheelDrive;
            }
            else if (OponentCarInfo.CarInfo.Drive == "RWD")
            {
                OponentCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.RearWheelDrive;
            }
            else
            {
                OponentCar.GetComponent<CarController>().m_CarDriveType = CarDriveType.FourWheelDrive;
            }

            //Handling
            OponentCar.GetComponent<CarController>().m_SteerHelper = OponentCarInfo.CarInfo.Handling / 10;
            #endregion
        }

        StartRace(PlayerCarInfo, OponentCarInfo);
    }

    void StartRace(CarButton PlayerCarInfo, OponentRaceInfo OponentCarInfo)
    {
        if(PlayerCarInfo && OponentCarInfo)
        {
            PlayerCar.GetComponent<CarController>().m_Topspeed = PlayerCarInfo.CarInfo.TopSpeed;
            OponentCar.GetComponent<CarController>().m_Topspeed = OponentCarInfo.CarInfo.TopSpeed;
            StartCoroutine(AssignEndPoint());
        }
    }

    IEnumerator AssignEndPoint()
    {
        yield return new WaitForSeconds(5);
        PlayerCar.GetComponent<CarAIControl>().m_Target = RaceEnd;
        PlayerCar.GetComponent<CarAIControl>().m_StopWhenTargetReached = true;
        

        OponentCar.GetComponent<CarAIControl>().m_Target = RaceEnd;
        OponentCar.GetComponent<CarAIControl>().m_StopWhenTargetReached = true;

        FinishLine.GetComponent<FinishLine>().FinishColider.gameObject.SetActive(true);
    }

   
}

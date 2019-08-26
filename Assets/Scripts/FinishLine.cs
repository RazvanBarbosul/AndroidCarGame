using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public BoxCollider FinishColider;
    private GameManager GM;
    private bool bPlayerWon = false;
    private bool bOponentWon = false;
    public bool bRaceEnded = false;
    private int RacersPassed = 0;
    private string PreviousRacer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        if (!GM)
        {
            GM = GameObject.FindObjectOfType<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bPlayerWon && !bRaceEnded && !bOponentWon)
        {
            GM.PlayerScore += (int)Time.deltaTime * 100;
        }
        else if(bOponentWon && !bRaceEnded && !bPlayerWon)
        {
            GM.OponentScore += (int)Time.deltaTime * 100;
        }

        if(!bPlayerWon && GM.bRaceStarted)
        {
            GM.PlayerRaceDuration += Time.deltaTime;
        }

        if(!bOponentWon && GM.bRaceStarted)
        {
            GM.OponentRaceDuration += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GM)
        {
            GM = GameObject.FindObjectOfType<GameManager>();
        }

        if((other.tag == "Player" || other.tag == "Oponent") && PreviousRacer != other.tag)
        {
            PreviousRacer = other.tag;
            RacersPassed++;
        }

        if (RacersPassed == 2 && !bRaceEnded)
        {
            bRaceEnded = true;
            GM.ShowWaitScreen(bPlayerWon);
        }

        if (other.tag == "Player" && !bOponentWon &&!bRaceEnded &&!bPlayerWon)
        {
            GM.PlayerScore += 50;
            bPlayerWon = true;
            //other.gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_Topspeed = 0;
        }
        else if(other.tag == "Oponent" && !bPlayerWon && !bRaceEnded && !bOponentWon)
        {
            GM.OponentScore += 50;
            bOponentWon = true;
            //other.gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_Topspeed = 0;
        }

        
    }
}

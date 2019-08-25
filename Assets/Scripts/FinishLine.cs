using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public BoxCollider FinishColider;
    private GameManager GM;
    private bool bPlayerWon = false;
    private bool bOponentWon = false;
    private bool bRaceEnded = false;
    private int RacersPassed = 0;
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
            GM.PlayerScore += (int)Time.deltaTime;
        }
        else if(bOponentWon && !bRaceEnded && !bPlayerWon)
        {
            GM.OponentScore += (int)Time.deltaTime;
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

        if(other.tag == "Player" || other.tag == "Oponent")
        {
            RacersPassed++;
        }

        if (RacersPassed == 2)
        {
            bRaceEnded = true;
            GM.ShowWaitScreen(bPlayerWon);
        }

        if (other.tag == "Player" && !bOponentWon &&!bRaceEnded)
        {
            GM.PlayerScore += 50;
            bPlayerWon = true;
        }
        else if(other.tag == "Oponent" && !bPlayerWon && !bRaceEnded)
        {
            GM.OponentScore += 50;
            bOponentWon = true;
        }

        
    }
}

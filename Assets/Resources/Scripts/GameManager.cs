using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject pitcher;
    public GameObject batter;

    public GameObject scoreBoard;

    public KYUSYU kyusyu;

    public Text textScore;
    public Text textCall;
    public Text textInfo;

    public Score score;

    public string kekka = "out";

    public string mode = "batting";



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void printCall()
    {
        textCall.text = this.kekka;
    }

    public void kekkaSyori()
    {
        bool isChange = false;
        int runningScore = 0;
        switch (kekka)
        {
            case ("strike"):
                score.addStrikeCount();
                if (score.strikeCount >= 3)
                {
                    isChange = score.addOutCount();
                    kekka = "三振";
                }
                break;
            case ("ball"):
                score.addBallCount();
                score.addBallCount();
                if (score.ballCount >= 4)
                {
                    runningScore = score.addFourBall();
                    kekka = "フォアボール";
                }
                break;
            case ("OUT"):
                isChange = score.addOutCount();
                break;
            case ("1BH"):
                runningScore = score.addOneBase();
                break;
            case ("2BH"):
                runningScore = score.addTwoBase();
                break;
            case ("3BH"):
                runningScore = score.addThreeBase();
                break;
            case ("HR"):
                runningScore = score.addHomeRun();
                break;
            default:
                new System.Exception("変な結果");
                return;
        }
        score.addPoint(runningScore);

        //if (isChange && mode == "battle")
        if (isChange && mode == "battle")
        {
            score.resetOutCount();
            Debug.Log("hogege");
            BattingStrategy battingStrategy = batter.GetComponentInChildren<Batter>().strategy;
            PitchingStrategy pittingStrategy = pitcher.GetComponentInChildren<Pitcher>().strategy;
            if (battingStrategy.isCPU())
            {
                pitcher.GetComponentInChildren<Pitcher>().strategy = new PitchingCPUStrategy();
                batter.GetComponentInChildren<Batter>().strategy = new BattingHumanStrategy();
            }
            else
            {
                pitcher.GetComponentInChildren<Pitcher>().strategy = new PitchingPlayerStrategy();
                batter.GetComponentInChildren<Batter>().strategy = new BattingCPUStrategy();
            }
        }
        else if (isChange && mode != "battle")
        {
            score.resetOutCount();
            score.resetOutCount();
        }

        printCall();
    }

}

public enum KYUSYU
{
    NONE,
    FAST,
    CURB,
    SLOW,
    FORK,
    FASTEST,

}


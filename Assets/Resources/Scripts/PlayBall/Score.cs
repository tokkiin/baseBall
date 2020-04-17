using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text ballCountText;
    public GameObject first;
    public GameObject second;
    public GameObject third;

    private int kai = 1;
    private int[] senkoPoint = new int[18];
    private int[] kokopoint = new int[18];

    public int strikeCount;
    public int ballCount;
    public int outCount;

    public string senkoTeamName = "先攻";
    public string kokoTeamName = "後攻";

    //1111のように表示
    //1000の位生還
    //100の位3塁
    //10の位2塁
    //1の位1塁
    public int runner = 0;

    private Omoteura omoteura = Omoteura.表;
    private string senkoTeam = "先攻";
    private string kokoTeam = "後攻";

    public void init(string senkoTeam1, string kokoTeam1)
    {
        updateScore();
        ballCountText.text = "S: " + strikeCount + "\nB: " + ballCount + "\nO: " + outCount;
    }

    private void updateScore()
    {
        int senkoPointSum = sumHairetu(senkoPoint);
        int kokoPointSum = sumHairetu(kokopoint);
        scoreText.text = "" + kai + " " + omoteura + "\n" + senkoTeam + senkoPointSum + "\n" + kokoTeam + kokoPointSum;
    }

    private int sumHairetu(int[] senkoPoint)
    {
        int sum = 0;
        for (int i = 0; i < senkoPoint.Length; i++)
        {
            sum += senkoPoint[i];
        }
        return sum;
    }

    public void resetStrikeCount()
    {
        strikeCount = 0;
        ballCount = 0;
        ballCountText.text = "S: " + strikeCount + "\nB: " + ballCount + "\nO: " + outCount;
    }

    public void resetOutCount()
    {
        if (omoteura == Omoteura.表)
        {
            omoteura = Omoteura.裏;
        }
        else
        {
            omoteura = Omoteura.表;
            kai += 1;
        }
        strikeCount = 0;
        ballCount = 0;
        outCount = 0;
        runner = 0;
        ballCountText.text = "S: " + strikeCount + "\nB: " + ballCount + "\nO: " + outCount;
        updateRunnerDisplay();
        updateScore();
    }

    public void addStrikeCount()
    {
        strikeCount += 1;
        ballCountText.text = "S: " + strikeCount + "\nB: " + ballCount + "\nO: " + outCount;
    }

    public bool addOutCount()
    {
        outCount += 1;
        strikeCount = 0;
        ballCount = 0;
        ballCountText.text = "S: " + strikeCount + "\nB: " + ballCount + "\nO: " + outCount;
        if (outCount >= 3)
        {
            return true;
        }
        return false;
    }

    public void addBallCount()
    {

        ballCount += 1;
        ballCountText.text = "S: " + strikeCount + "\nB: " + ballCount + "\nO: " + outCount;
    }

    public int addOneBase()
    {
        resetStrikeCount();
        runner = runner * 10 + 1;
        updateRunnerDisplay();
        return calcRunningScore();
    }

    public int addTwoBase()
    {
        resetStrikeCount();
        runner = runner * 100 + 10;
        updateRunnerDisplay();
        return calcRunningScore();
    }

    public int addThreeBase()
    {
        resetStrikeCount();
        runner = runner * 1000 + 100;
        updateRunnerDisplay();
        return calcRunningScore();
    }

    public int addHomeRun()
    {
        resetStrikeCount();
        runner = runner * 10000 + 1000;
        updateRunnerDisplay();
        return calcRunningScore();
    }

    public int addFourBall()
    {
        resetStrikeCount();
        if (runner % 10 == 0)
        {
            runner = runner + 1;
        }
        else if (runner % 100 == 1)
        {
            runner = runner + 10;
        }
        else if (runner % 1000 == 11)
        {
            runner = runner + 100;
        }
        else if (runner % 10000 == 111)
        {
            runner = runner + 1000;
        }
        updateRunnerDisplay();
        return calcRunningScore();
    }

    public void addPoint(int point)
    {
        if (omoteura == Omoteura.表)
        {
            senkoPoint[kai - 1] += point;
        }
        else
        {
            kokopoint[kai - 1] += point;
        }
        updateScore();
    }


    private void updateRunnerDisplay()
    {
        if (runner % 10 == 1)
        {
            first.SetActive(true);
        }
        else
        {
            first.SetActive(false);
        }
        if ((runner / 10) % 10 == 1)
        {
            second.SetActive(true);
        }
        else
        {
            second.SetActive(false);
        }

        if ((runner / 100) % 10 == 1)
        {
            third.SetActive(true);
        }
        else
        {
            third.SetActive(false);
        }
    }

    private int calcRunningScore()
    {
        int runningScore = 0;
        if (runner >= 1000000)
        {
            runningScore++;
            runner -= 1000000;
        }
        if (runner >= 100000)
        {
            runningScore++;
            runner -= 100000;
        }
        if (runner >= 10000)
        {
            runningScore++;
            runner -= 10000;
        }
        if (runner >= 1000)
        {
            runningScore++;
            runner -= 1000;
        }
        return runningScore;
    }
    private enum Omoteura
    {
        表,
        裏,
    }
}




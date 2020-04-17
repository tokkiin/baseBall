using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchingCPUStrategy : PitchingStrategy
{
    public void pitch(Pitcher pitcher)
    {
        int randVal = (int)(Random.value * 100);
        Debug.Log(randVal);
        if (randVal < 50)
        {
            pitcher.gameManager.kyusyu = KYUSYU.FAST;
        }
        else if (randVal < 70)
        {
            pitcher.gameManager.kyusyu = KYUSYU.SLOW;
        }
        else if (randVal < 90)
        {
            pitcher.gameManager.kyusyu = KYUSYU.CURB;
        }
        else
        {
            pitcher.gameManager.kyusyu = KYUSYU.FORK;
        }

        Debug.Log(pitcher.gameManager.kyusyu);
        pitcher.state = new PitchingState();
    }
}

public class PitchingCPUStrategyHard : PitchingStrategy
{
    public void pitch(Pitcher pitcher)
    {
        int randVal = (int)(Random.value * 100);
        Debug.Log(randVal);
        if (randVal < 50)
        {
            pitcher.gameManager.kyusyu = KYUSYU.FASTEST;
        }
        else if (randVal < 70)
        {
            pitcher.gameManager.kyusyu = KYUSYU.FAST;
        }
        else if (randVal < 90)
        {
            pitcher.gameManager.kyusyu = KYUSYU.CURB;
        }
        else
        {
            pitcher.gameManager.kyusyu = KYUSYU.FORK;
        }

        Debug.Log(pitcher.gameManager.kyusyu);
        pitcher.state = new PitchingState();
    }
}

public class PitchingPlayerStrategy : PitchingStrategy
{
    public void pitch(Pitcher pitcher)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Debug.Log("user pitch");
            pitcher.gameManager.kyusyu = KYUSYU.FAST;
            pitcher.state = new PitchingState();
        }
        if (Input.GetKey(KeyCode.X))
        {
            pitcher.gameManager.kyusyu = KYUSYU.SLOW;
            pitcher.state = new PitchingState();
        }

        if (Input.GetKey(KeyCode.C))
        {
            pitcher.gameManager.kyusyu = KYUSYU.CURB;
            pitcher.state = new PitchingState();
        }

        if (Input.GetKey(KeyCode.V))
        {
            pitcher.gameManager.kyusyu = KYUSYU.FORK;
            pitcher.state = new PitchingState();
        }
    }
}

public interface PitchingStrategy
{
    void pitch(Pitcher pitcher);
}
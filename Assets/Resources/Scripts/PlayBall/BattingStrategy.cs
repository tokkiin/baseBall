using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattingStrategy
{
    bool isCPU();
    bool swing(GameObject ball, Ball ballObj);
}

public class BattingHumanStrategy : BattingStrategy
{
    public bool isCPU()
    {
        return false;
    }
    public bool swing(GameObject ball, Ball ballObj)
    {
        return Input.GetKey(KeyCode.Space);
    }
}

public class BattingCPUStrategy : BattingStrategy
{
    public bool swing(GameObject ball, Ball ballObj)
    {
        if (ballObj.kyusyu == KYUSYU.CURB)
        {
            return ball.transform.position.x < 5;
        }
        if (ballObj.kyusyu == KYUSYU.FORK)
        {
            if (ball.transform.position.x > 5.85 && ball.transform.position.x < 6)
            {
                int randVal = (int)(Random.value * 100);
                Debug.Log(randVal);
                return randVal < 25;
            }
            return false;
        }
        if (ballObj.kyusyu == KYUSYU.FAST)
        {
            return (ball.transform.position.x < 7);
        }
        return (ball.transform.position.x < 5);
    }

    public bool isCPU()
    {
        return true;
    }
}

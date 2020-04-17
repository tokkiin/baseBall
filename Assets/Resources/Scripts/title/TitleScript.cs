using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public PlayBallScene nextPage;
    public GameManager maneger;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        //SoundManager.SingletonInstance.StopSound();
        gameObject.SetActive(false);
        nextPage.Open();
    }
    public void OnClickStartCPUBating()
    {
        Close();
    }

    public void OnClickStartCPUBatingHard()
    {
        GameObject pitObj = maneger.pitcher;
        Pitcher pitcher = pitObj.GetComponent<Pitcher>();
        pitcher.strategy = new PitchingCPUStrategyHard();
        Close();
    }
    public void OnClickStartCPUPitch()
    {
        Close();
    }

    public void OnClickStartHuman()
    {
        GameObject pitObj = maneger.pitcher;
        Pitcher pitcher = pitObj.GetComponent<Pitcher>();
        pitcher.strategy = new PitchingPlayerStrategy();
        maneger.mode = "battle";
        Close();
    }

    public void OnClickCPUPitching()
    {
        GameObject pitObj = maneger.pitcher;
        Pitcher pitcher = pitObj.GetComponent<Pitcher>();
        pitcher.strategy = new PitchingPlayerStrategy();

        GameObject batObj = maneger.batter;
        Batter batter = batObj.GetComponent<Batter>();
        batter.strategy = new BattingCPUStrategy();

        maneger.mode = "battle";
        Close();
    }
}

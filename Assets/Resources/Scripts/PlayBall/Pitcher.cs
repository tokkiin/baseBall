using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    public PitcherState state = new WaitingState();

    public GameManager gameManager;

    public PitchingStrategy strategy = new PitchingCPUStrategy();

    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        state.moving(this);
    }
}

public interface PitcherState
{
    void init(Pitcher pitcher);

    void moving(Pitcher pitcher);
}

public class WaitingState : PitcherState
{
    public void init(Pitcher pitcher)
    {
        pitcher.gameManager.kekka = "";
    }
    public void moving(Pitcher pitcher)
    {
        pitcher.strategy.pitch(pitcher);
        pitcher.state.init(pitcher);
    }
}

public class PitchingState : PitcherState
{
    public void init(Pitcher pitcher)
    {
        pitcher.GetComponentInParent<Animator>().Play("nageru_vmd");
    }
    public void moving(Pitcher pitcher)
    {
        if (pitcher.gameManager.kekka.Equals("wait"))
        {
            pitcher.gameManager.kekka = "";
            Debug.Log("kekkaget");
            pitcher.state = new WaitingState();
            pitcher.state.init(pitcher);
        }
    }
}
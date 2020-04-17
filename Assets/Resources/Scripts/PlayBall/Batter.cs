using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batter : MonoBehaviour
{
    public GameObject ballObj;

    public GameObject ballInstance;
    public Ball ballObjBall;
    public GameObject hitballPrefab;
    public GameObject hitballObj;
    public BatterState state = new WaitingBattingState();

    public GameManager gameManager;
    public BattingStrategy strategy = new BattingHumanStrategy();
    public string kekka = "aaa";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        state.batting(this);
    }

}

public interface BatterState
{
    void init(Batter batter);

    void batting(Batter batter);
}

public class WaitingBattingState : BatterState
{
    float waittime = 3.7f;
    public void init(Batter batter)
    {
        Debug.Log("WaitingBattingStateinit");
        batter.gameManager.kyusyu = KYUSYU.NONE;
    }
    public void batting(Batter batter)
    {

        if (batter.gameManager.kyusyu != KYUSYU.NONE)
        {
            if (waittime < 0)
            {
                batter.state = new BattingState();
                batter.state.init(batter);
            }
            else
            {
                waittime -= Time.deltaTime;
            }
        }
    }
}

public class BattingState : MonoBehaviour, BatterState
{

    bool didSwing = false;
    Animator m_Animator;
    float waittime = 0.5f;
    public void init(Batter batter)
    {
        batter.gameManager.kekka = "";
        batter.gameManager.printCall();

        Debug.Log("BattingStateinit");
        batter.ballInstance = Instantiate(batter.ballObj, new Vector3(13.0f, 1.0f, 13.0f), Quaternion.identity);
        batter.ballObjBall = batter.ballInstance.GetComponent<Ball>();
        batter.ballObjBall.kyusyu = batter.gameManager.kyusyu;

        m_Animator = batter.GetComponentInParent<Animator>();
    }
    public void batting(Batter batter)
    {
        GameObject ball = batter.ballInstance;


        if (didSwing == false && batter.strategy.swing(ball, batter.ballObjBall))
        {
            m_Animator.speed = 4;
            m_Animator.Play("凛打撃モーション01_vmd");
            didSwing = true;
        }
        if (didSwing)
        {
            waittime -= Time.deltaTime;
            if (waittime < 0)
            {
                if (canHit(batter.ballInstance, batter.gameManager.kyusyu))
                {
                    float pos = batter.ballInstance.transform.position.x;

                    int Basespeed = 180;
                    int Basetatekakudo = (int)(Random.value * 110 - 30);
                    if (pos * 1.41 < 0.5 && (Basetatekakudo - 45) * (Basetatekakudo - 45) < 400)
                    {
                        Basetatekakudo = (int)(Random.value * 110 - 30);
                    }
                    // if (pos * 1.41 < 0.17 && (Basetatekakudo - 45) * (Basetatekakudo - 45) < 400)
                    // {
                    //     Basetatekakudo = (int)(Random.value * 40 + 22.5);
                    // }
                    int Baseyokokakudo = (int)(Random.value * 45f);
                    if (pos > 0)
                    {
                        Baseyokokakudo = Baseyokokakudo + 45;
                    }
                    batter.state = new HittingState(Basespeed, Basetatekakudo, Baseyokokakudo);
                    //batter.state = new HittingState(180, -9, 18);
                    //speed180kakudo - 9yoko18->hit
                    batter.state.init(batter);
                }
                else
                {
                    waittime = 5f;
                }
            }
        }

        if (!batter.ballObjBall.kekka.Equals(""))
        {
            if (batter.ballObjBall.kekka == "ball" && didSwing)
            {
                batter.gameManager.kekka = "strike";
            }
            else
            {
                batter.gameManager.kekka = batter.ballObjBall.kekka;
            }
            Destroy(batter.ballInstance);
            batter.state = new AfterState();
            batter.state.init(batter);
        }
    }

    private bool canHit(GameObject ball, KYUSYU kyusyu)
    {
        didSwing = true;
        if (kyusyu == KYUSYU.FORK)
        {
            return false;
        }
        Debug.Log("batting Timing:" + ball.transform.position.x * 1.41);
        return (ball.transform.position.x * 1.41 < 1.7) && (ball.transform.position.x * 1.41 > -2.0);
    }
}

public class HittingState : MonoBehaviour, BatterState
{
    float basespeed; //0-150 150でぎりぎりフェンス、200でホームラン180くらいがちょうどいい
    float basetatekakudo; //-30~70
    float baseyokokakudo; //Random.value * 90f; //0～90

    public HittingState(int basespeed, int basetatekakudo, int baseyokokakudo)
    {
        Debug.Log("speed" + basespeed + "kakudo" + basetatekakudo + "yoko" + baseyokokakudo);
        this.basespeed = basespeed;
        this.basetatekakudo = basetatekakudo;
        this.baseyokokakudo = baseyokokakudo;
    }
    public void init(Batter batter)
    {
        Destroy(batter.ballInstance);


        batter.hitballObj = Instantiate(batter.hitballPrefab, new Vector3(0f, 1.0f, 0f), Quaternion.identity);

        batter.hitballObj.SetActive(true);
        Rigidbody rb = batter.hitballObj.transform.GetComponent<Rigidbody>();

        float maxBattingSpeedKmH = basespeed * 0.277f;
        float kakudo = basetatekakudo * Mathf.PI / 180;

        float speedY = Mathf.Sin(kakudo) * maxBattingSpeedKmH / 1.4f;
        float speedMae = Mathf.Cos(kakudo) * maxBattingSpeedKmH / 1.4f;
        Debug.Log("speedY: " + speedY + "speedMae: " + speedMae);

        float yokoKakudo = baseyokokakudo * Mathf.PI / 180; //-30～150
        float speedX = Mathf.Sin(yokoKakudo) * speedMae;
        float speedZ = Mathf.Cos(yokoKakudo) * speedMae;

        Debug.Log("x:" + speedX + "y:" + speedY + "z:" + speedZ);

        rb.velocity = new Vector3(speedX, speedY, speedZ);
    }

    float waittime = 20;
    float waittime2 = 40;

    /*
    バッティング結果を集計
    1. ボールが停止する
    2. 一定時間経過する
    3. 壁に当たる
    */
    public void batting(Batter batter)
    {

        if (isFinished(batter))
        {


            batter.state = new AfterState();
            batter.state.init(batter);

        }
        else
        {
            waittime -= Time.deltaTime;
            waittime2 -= Time.deltaTime;
        }
        //Debug.Log(batter.hitballObj.transform.GetComponent<Rigidbody>().velocity.magnitude);
    }
    private bool isFinished(Batter batter)
    {
        if (waittime2 < 0)
        {
            return true;
        }

        if (batter.hitballObj.GetComponentInChildren<Ball>().kekka != "")
        {
            string kekka = batter.hitballObj.GetComponentInChildren<Ball>().kekka;
            if (kekka.Equals("HR") && batter.hitballObj.GetComponentInChildren<Ball>().isBound)
            {
                kekka = "2BH";
            }
            Debug.Log(kekka);
            batter.gameManager.kekka = kekka;
            return true;
        }
        if (batter.hitballObj.transform.GetComponent<Rigidbody>().velocity.magnitude <= 0.2 && waittime < 0)
        {
            Debug.Log("ballstop");
            batter.gameManager.kekka = "OUT";
            return true;
        }

        return false;
    }
}

public class AfterState : MonoBehaviour, BatterState
{

    public void init(Batter batter)
    {

        batter.gameManager.printCall();
        batter.gameManager.kekkaSyori();
    }

    float waittime = 3;
    public void batting(Batter batter)
    {
        if (waittime < 0)
        {
            batter.state = new WaitingBattingState();
            batter.state.init(batter);
            batter.gameManager.kekka = "wait";

            if (batter.hitballObj != null)
            {
                Destroy(batter.hitballObj);
            }
        }
        waittime -= Time.deltaTime;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    public GameObject ballObj;
    private int speed = 100; //100km/h=27m/s,150km/h=40
    public KYUSYU kyusyu;

    public string kekka;

    public bool isBound = false;

    // Use this for initialization
    void Start()
    {
        kekka = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (kyusyu != KYUSYU.NONE)
        {
            moving();
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "catcher")
        {
            Debug.Log("catch");
            if (kyusyu == KYUSYU.FORK)
            {
                kekka = "ball";
            }
            else
            {
                kekka = "strike";
            }
        }

        if (other.gameObject.tag == "mato")
        {
            //            Debug.Log("catch" + other.gameObject.GetComponentInChildren<TextMesh>().text);
            kekka = other.gameObject.GetComponentInChildren<TextMesh>().text;
        }

        if (other.gameObject.tag == "yuka")
        {

            isBound = true;
        }
    }

    public void moving()
    {
        if (kyusyu == KYUSYU.FAST)
        {
            speed = 130;
        }
        if (kyusyu == KYUSYU.SLOW)
        {
            speed = 120;
        }
        if (kyusyu == KYUSYU.CURB)
        {
            speed = 150;
            if (ballObj.transform.position.x + ballObj.transform.position.z <= 20)
            {
                speed = 80;
            }
        }
        if (kyusyu == KYUSYU.FORK)
        {
            speed = 140;
            if (ballObj.transform.position.x + ballObj.transform.position.z <= 20)
            {
                Vector3 yy = ballObj.transform.position;
                yy.y = 0.2f;
                ballObj.transform.position = yy;
                speed = 80;
            }
        }
        if (kyusyu == KYUSYU.FASTEST)
        {
            speed = 140;
        }
        ballObj.transform.position += new Vector3(-1 / 1.41f, 0, -1 / 1.41f) * Time.deltaTime * (speed * 0.277f / 2);
        // int v = (int)(ballObject.speed * (1 - (0.000001 * Time.deltaTime)));
        // ballObject.speed = v;
    }
}
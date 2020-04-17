using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBallScene : MonoBehaviour
{

    // Use this for initialization
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        //SoundManager.SingletonInstance.StopSound();
        gameObject.SetActive(false);
        //nextPage.Open();
    }
}

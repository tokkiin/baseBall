using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        EventTrigger currentTrigger = gameObject.AddComponent<EventTrigger>();
        currentTrigger.triggers = new List<EventTrigger.Entry>();

        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter; //PointerClickの部分は追加したいEventによって変更してね
            entry.callback.AddListener(x => SelectSelf());  //ラムダ式の右側は追加するメソッドです。

            currentTrigger.triggers.Add(entry);
        }
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select; //PointerClickの部分は追加したいEventによって変更してね
            entry.callback.AddListener(x => onSelect());  //ラムダ式の右側は追加するメソッドです。

            currentTrigger.triggers.Add(entry);
        }

        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Deselect; //PointerClickの部分は追加したいEventによって変更してね
            entry.callback.AddListener(x => onNonSelect());  //ラムダ式の右側は追加するメソッドです。

            currentTrigger.triggers.Add(entry);
        }

        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit; //PointerClickの部分は追加したいEventによって変更してね
            entry.callback.AddListener(x => onNonSelect());  //ラムダ式の右側は追加するメソッドです。

            currentTrigger.triggers.Add(entry);
        }

        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Submit; //PointerClickの部分は追加したいEventによって変更してね
            entry.callback.AddListener(x => Onclick());  //ラムダ式の右側は追加するメソッドです。

            currentTrigger.triggers.Add(entry);
        }

        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick; //PointerClickの部分は追加したいEventによって変更してね
            entry.callback.AddListener(x => Onclick());  //ラムダ式の右側は追加するメソッドです。

            currentTrigger.triggers.Add(entry);
        }
    }


    public virtual void onPointerUp()
    {

    }

    public void SelectSelf()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        onSelect();
    }

    public virtual void onSelect()
    {
        if (Time.time > 1)
        {
            SoundManager.SingletonInstance.PlaySE(SELabel.choise);
        }
        string text = "";
        Transform aaa = gameObject.transform.Find("Explain");
        if (aaa != null)
        {
            text = aaa.GetComponent<Text>().text;
        }

        GameObject console = GameObject.Find("Console");
        if (console != null)
        {
            console.transform
                .Find("Text")
                .GetComponent<Text>().text = text;
        }

    }

    void NonSelectSelf()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        onNonSelect();
    }

    public virtual void onNonSelect()
    {

    }

    public void Onclick()
    {
        SoundManager.SingletonInstance.PlaySE(SELabel.yes);
        //Debug.Log("clicked ");
    }
}

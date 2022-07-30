using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();

    public UnityEvent<string> OnShotFired; // For Editor Event
    public Action OnGunReload; // Event within the scripts
    public static event ClickAction OnClicked;

    public void Attack()
    {
        if (OnClicked != null)
            OnClicked();
    }

    public void DebugAString(string s)
    {
        Debug.Log(s);
    }
}

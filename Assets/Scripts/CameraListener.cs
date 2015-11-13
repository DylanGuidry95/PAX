﻿/*
    Setup:
        1) Listening For: [the message to listen for]
        2) Start Cam: If this camera is the active camera on level start.
        2) Activator: The trigger(s) that this camera is activated with. 
                      -Currently supports two activators. 
                      1. Drag the activator in the first empty slot. If an 
                         activator slot(s) is not used, leave it empty.
*/

using UnityEngine;

public class CameraListener : MonoBehaviour
{
    public string listeningForSetCam;
    string listeningForReTarget = "playerdied";
    public bool startCam;
    public GameObject activator1, activator2;

    private CameraManager cm;

	// Use this for initialization
	void Awake()
    {
        cm = GetComponentInParent<CameraManager>();

        if (activator1 != null)
        {
            GameObject temp = activator1;
            if (activator1.transform.childCount > 0)
            {
                Collider[] _activator1 = activator1.gameObject.GetComponentsInChildren<Collider>();
                activator1 = null;
                
                foreach (Collider c in _activator1)
                {
                    if (c.isTrigger == true)
                    {
                        activator1 = c.gameObject;
                    }
                }

                if (activator1 == null)
                    activator1 = temp;

            }
        }

        if (activator2 != null)
        {
            GameObject temp = activator2;
            if (activator2.transform.childCount > 0)
            {
                Collider[] _activator2 = activator2.gameObject.GetComponentsInChildren<Collider>();
                activator2 = null;

                foreach (Collider c in _activator2)
                {
                    if (c.isTrigger == true)
                        activator2 = c.gameObject;
                }

                if (activator2 == null)
                    activator2 = temp;

            }
        }

        Messenger.AddListener<GameObject,string>(listeningForSetCam, SetCam);
        Messenger.AddListener<string>(listeningForReTarget, SetTarget);
        Messenger.MarkAsPermanent(listeningForSetCam);
        Messenger.MarkAsPermanent(listeningForReTarget);
        if (!startCam)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the active camera.
    /// </summary>
    /// <param name="s">Name of the gameobject that triggered the event.</param>
    /// <param name="broadcaster">Name of the gameobject that broadcasts the message.</param>
    void SetCam(GameObject o, string broadcaster)
    {
        if (o.tag == "Player")
        {
            if (gameObject.activeSelf == false)
            {
                if(activator2 == null)
                {
                    if (broadcaster == activator1.name)
                        gameObject.SetActive(true);
                }
                else
                {
                    if(broadcaster == activator1.name || broadcaster == activator2.name)
                        gameObject.SetActive(true);
                }
            }
            else
                gameObject.SetActive(false);
        }
    }

    void SetTarget(string s)
    {
        if(s == "Player")
            cm.SetTargets();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<GameObject, string>(listeningForSetCam, SetCam);
        Messenger.RemoveListener<string>(listeningForReTarget, SetTarget);
    }
}

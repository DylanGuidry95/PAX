/*
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
    bool AutoFindActivators = false;

    public GameObject activator1, activator2;

    private CameraManager cm;    

	// Use this for initialization
	void Awake()
    {
        cm = GetComponentInParent<CameraManager>();

        //listening for "checkpoint" to swap cameras
        Messenger.AddListener<GameObject, Transform>(listeningForSetCam, SetCam);
        //listening for "playerdied" to switch the follow targets to the alive player
        Messenger.AddListener<string>(listeningForReTarget, SetTarget);
        Messenger.MarkAsPermanent(listeningForSetCam);
        Messenger.MarkAsPermanent(listeningForReTarget);

        //  If AutoFindActivators is checked, this will search for all game objects with the
        // cameraBroadcast script and set the activators to the closest
        if (AutoFindActivators)
        {

        }
        else
        {
            if (activator1 != null)
            {
                //store activator gameObject in a temporary variable for when null we can retrieve later
                GameObject temp = activator1;
                //  Checks for a trigger collider; If the game object in the activator slot
                // is not trigger, but has a child that is a trigger, the child becomes the activator
                if (activator1.transform.childCount > 0)
                {
                    Collider[] _activator1 = activator1.gameObject.GetComponentsInChildren<Collider>();
                    activator1 = null;

                    foreach (Collider c in _activator1)
                    {
                        if (c.isTrigger == true)
                            activator1 = c.gameObject;
                    }

                    if (activator1 == null)
                        activator1 = temp;

                }
            }
            //do the same thing if a second activator exists
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
        }

        if (!startCam)
            gameObject.SetActive(false);

}

    /// <summary>
    /// Sets the active camera.
    /// </summary>
    /// <param name="s">Name of the gameobject that triggered the event.</param>
    /// <param name="broadcaster">Name of the gameobject that broadcasts the message.</param>
    void SetCam(GameObject o, Transform broadcaster)
    {
        if (o.tag == "Player")
        {
            if (gameObject.activeSelf == false)
            {
                if(activator2 == null)
                {
                    if (broadcaster == activator1.transform && activator1.activeSelf)
                        gameObject.SetActive(true);
                }
                else
                {
                    if(broadcaster == activator1.transform || broadcaster == activator2.transform && activator2.activeSelf)
                        gameObject.SetActive(true);
                }
            }
            else
                gameObject.SetActive(false);
        }
    }

    void SetTarget(string s)
    {
        if (s == "Player")
        {
            Debug.Log("set target");
            cm.SetTargets();
        }
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<GameObject, Transform>(listeningForSetCam, SetCam);
        Messenger.RemoveListener<string>(listeningForReTarget, SetTarget);
    }
}

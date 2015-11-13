using UnityEngine;
using System.Collections;

public class TriggerActivateLimit : MonoBehaviour
{
    // number of times the trigger can be activated
    public int activateLimit;
    int numActivations = 0;

    void OnTriggerEnter()
    {
        numActivations++;

        if (numActivations >= activateLimit && activateLimit != 0)
            gameObject.SetActive(false);

    }
    
}

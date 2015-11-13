using UnityEngine;
using System.Collections;

public class CameraBroadcast : BroadcastOnTrigger
{    
    protected override void OnTriggerEnter(Collider col)
    {
        Messenger.Broadcast<GameObject, Transform>(message, col.gameObject, this.transform);
    }
}

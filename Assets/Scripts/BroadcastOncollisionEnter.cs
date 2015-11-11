﻿using UnityEngine;
using System.Collections;

public class BroadcastOncollisionEnter : MonoBehaviour
{
	[SerializeField] string message;

	protected virtual void OnCollisionEnter(Collision c)
	{
		Messenger.Broadcast<string>(message,c.gameObject.GetInstanceID().ToString());

	}

}
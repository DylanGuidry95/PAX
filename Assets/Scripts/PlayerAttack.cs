using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class PlayerAttack : MonoBehaviour
{
	void Awake()
	{
		GetComponent<Rigidbody>().useGravity  = false;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Messenger.Broadcast("modstat", other.gameObject.GetInstanceID().ToString(), "health", -damage);
		}
	}

	[SerializeField] float damage;
}

/// Eric Mouledoux
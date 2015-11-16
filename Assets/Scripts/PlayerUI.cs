using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	public GameObject player;

	void Start () 
	{
		MenuSystem.AddPrefab ("start->play", player);
		MenuSystem.ListenerToTransition ("gamestatechanged");
	}
}

using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour 
{


	void Start () 
	{
		MenuSystem.AddPrefab ("play->pause", gameObject);
		MenuSystem.ListenerToTransition("gamestatechanged");
	}

	public void resume()
	{
		gameObject.SetActive (false);
		GameStateManager.ToPlay ();
	}

	public void exitPause()
	{
		gameObject.SetActive (false);
		GameStateManager.ToGameover ();
	}
}

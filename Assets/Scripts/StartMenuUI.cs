using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuUI : MonoBehaviour 
{
	void Start()
	{
		MenuSystem.AddPrefab("init->start", gameObject);
		MenuSystem.AddPrefab ("gameover->start", gameObject);
		MenuSystem.ListenerToTransition("gamestatechanged");
	}

	public void playButton()
	{
		gameObject.SetActive(false);
		MenuSystem.printMenus ();
		GameStateManager.ToPlay();
	}

	public void quitButton()
	{
		gameObject.SetActive(false);
		GameStateManager.ToEnd();
	}
}

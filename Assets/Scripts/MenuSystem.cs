using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

static public class MenuSystem
{
	static Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
	
	static public void AddPrefab(string argument, GameObject prefab)
	{
		menus.Add(argument, prefab);
	}
	
	static public void ListenerToTransition(string message)
	{
		Messenger.AddListener<string>(message, selfActivate);
	}
	
	static private void selfActivate(string argument)
	{
		menus [argument].SetActive(true);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreditMenuUI : MonoBehaviour {

	 
	public Text End;
	public Text Credits;
	public Text Programmers;
	Vector3 textPos;

	void Start () 
	{
		textPos = Programmers.transform.position;
		StartCoroutine ("credits");

		MenuSystem.AddPrefab ("start->end", gameObject);	
		MenuSystem.ListenerToTransition ("gamestatechanged");
	}
	

	IEnumerator credits() 
	{
		yield return new WaitForSeconds (3f);
		End.gameObject.SetActive(false);

		yield return new WaitForSeconds (1f);
		Credits.gameObject.SetActive (true);

		yield return new WaitForSeconds (2f);
		Credits.gameObject.SetActive (false);

		yield return new WaitForSeconds (1f);
		Programmers.gameObject.SetActive (true);

		for (int i = 0; i < 120; ++i) 
		{
			Programmers.transform.position = new Vector3 (Programmers.transform.position.x,
			                                              Programmers.transform.position.y + 200 * Time.deltaTime,
			                                              Programmers.transform.position.z);
			yield return new WaitForSeconds (0.06f);
		}

		yield return new WaitForSeconds (2f);
		Programmers.gameObject.SetActive (false);
		Programmers.transform.position = textPos;
		gameObject.SetActive (false);
	}

}

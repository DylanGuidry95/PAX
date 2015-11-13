using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerChain : MonoBehaviour 
{
	[SerializeField]
	float ChainLength;

	[SerializeField]
	float ChainSlack;

	Vector3 Seperation;

	public GameObject ChainAnchor;
	public GameObject ChainTarget;
	public GameObject LinkPrefab;
	public List<GameObject>Links = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		ChainAnchor = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>().Player1;
		ChainTarget = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>().Player2;

		ChainLength = (CalcDis(ChainAnchor.transform.position, ChainTarget.transform.position) + ChainSlack);
		CreateChain();
	}

	void CreateChain()
	{
		Vector3 guide = Vector3.zero;
		guide.x = ChainAnchor.transform.position.x + 0.3f;
		Vector3 stop = Vector3.zero;
		stop.x = ChainTarget.transform.position.x + 0.3f;
		float numLinks = (ChainLength - ChainSlack) / LinkPrefab.transform.lossyScale.x;
		for(int i = 0; i <= numLinks; i++)
		{
			if(CalcDis(guide, stop) > .5f)
			{
				guide.y = .2f;
				Links.Add(Instantiate(LinkPrefab, guide, Quaternion.identity) as GameObject);
				guide.x += LinkPrefab.transform.lossyScale.x;
			}
			else
				break;
		}
	}

	float CalcDis(Vector3 pos1, Vector3 pos2)
	{
		float dis;
		dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
		return Mathf.Sqrt(dis);
	}

	// Update is called once per frame
	void Update () 
	{


	}
}

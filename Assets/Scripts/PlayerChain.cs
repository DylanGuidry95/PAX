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
				if(i-1 >= 0)
				{
					Links[i].GetComponent<ChainLink>().prevLink = Links[i-1];
					Links[i-1].GetComponent<ChainLink>().nextLink = Links[i];
                }
				if(i == 0)
				{
					Links[i].GetComponent<ChainLink>().prevLink = ChainAnchor;
				}
			}
		}
		Links[Links.Count - 1].GetComponent<ChainLink>().nextLink = ChainTarget;
	}

	float CalcDis(Vector3 pos1, Vector3 pos2)
	{
		float dis;
		dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
		return Mathf.Sqrt(dis);
	}

	Vector3 Cohesion(GameObject a)
	{
		Vector3 pc = Vector3.zero;
		foreach(GameObject link in Links)
		{
			if(link != a && (link == a.GetComponent<ChainLink>().nextLink || link == a.GetComponent<ChainLink>().prevLink))
			{
				pc = pc + link.GetComponent<ChainLink>().Position;
			}
		}
		pc = pc / (Links.Count - 2);
		return (pc - a.GetComponent<ChainLink>().Position )/ 100;
	}

	// Update is called once per frame
	void Update () 
	{
//			Vector3 cohesion = Vector3.zero;
//			GameObject l;
//			foreach(GameObject link in Links)
//			{
//				cohesion = Cohesion(l);
//				l.GetComponent<ChainLink>().Velocity = l.GetComponent<ChainLink>().Velocity + Cohesion;
//				l.GetComponent<ChainLink>().Position = l.GetComponent<ChainLink>().Position + l.GetComponent<ChainLink>().Velocity;
//			}
	}
}

using UnityEngine;
using System.Collections;

public class ChainLink : MonoBehaviour 
{
	public Vector3 Position;
	public GameObject nextLink;
	public GameObject prevLink;
	public Vector3 Velocity;

    public float tension;

    void Start()
    {
        
    }

    void CheckSeperation()
    {
        if(CalcDis(Position, nextLink.GetComponent<ChainLink>().Position) > .2f )
        {

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

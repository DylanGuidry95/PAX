using UnityEngine;
using System.Collections.Generic;

public class SetPlayerPosition : MonoBehaviour
{
    public string listeningFor;
    public float maxDistance;
    Transform trigger;
    List<Transform> players = new List<Transform>();

    void Awake()
    {
        Messenger.AddListener<GameObject, Transform>(listeningFor, SetPlayerPos);

        //keep track of players
        var playerTemp = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < playerTemp.Length; ++i)
            players.Add(playerTemp[i].transform);

        trigger = GetComponentInChildren<CameraBroadcast>().gameObject.transform;

    }

    void SetPlayerPos(GameObject o, Transform broadcaster)
    {
        if (players.Count == 2 && o.tag == "Player" && broadcaster == trigger.transform)
        {
            //if (Physics.Linecast(players[0].position, players[1].position) || 
            //    Vector3.Distance(players[0].position, 
            //    players[1].position) >= maxDistance)
            //{
            //if gameObject that triggered the event is player1, move player2
            if (o == players[0].gameObject)
            {
                players[1].position = transform.position;
            }
            else
            {
                players[0].position = transform.position;
            }
            //}
        }

    }

    void OnDestroy()
    {
        Messenger.RemoveListener<GameObject, Transform>(listeningFor, SetPlayerPos);
    }
}

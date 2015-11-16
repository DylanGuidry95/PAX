using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
    PlayerStates player1States, player2States;

    public GameObject Player1
    {
        get;
        private set;
    }
    public GameObject Player2
    {
        get;
        private set;
    }

    public int PlayerCount
    {
        get { return players.Count; }

        set { PlayerCount = value; }
    }

    [SerializeField]
    List<GameObject> players = new List<GameObject>();
    protected override void Awake()
    {
        //players are initially 0
        //PlayerCount = 0;

        List<PlayerCharacterController> pcc = new List<PlayerCharacterController>(FindObjectsOfType<PlayerCharacterController>());

        foreach (PlayerCharacterController p in pcc)
        {
            players.Add(p.gameObject);
        }

        if (players[0] != null)
        {
            Player1 = players[0];
			Player1.GetComponent<PlayerCharacterController>().PlayerNumber = 1;
        }

        if (players[1] != null)
        {
            Player2 = players[1];
			Player1.GetComponent<PlayerCharacterController>().PlayerNumber = 2;
        }

        player1States = Player1.GetComponent<PlayerStates>();
        player2States = Player2.GetComponent<PlayerStates>();
    }

    void Update()
    {
        if (Player1.activeSelf == false && 
            Player2.activeSelf == false
            )
            GameStateManager.ToGameover();
    }
}

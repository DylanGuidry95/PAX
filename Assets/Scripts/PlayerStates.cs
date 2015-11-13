using UnityEngine;
using System.Collections;

public class PlayerStates : HealthStats
{
    HealthStats healthStats;
    int intState;
    public enum STATES
    {
        ALIVE,
        DEAD
    }

    private _FSM<STATES> _fsm = new _FSM<STATES>();
    Callback handleDead;
    protected void Awake()
    {
        //   healthStats = GetComponent<HealthStats>();
        //Messenger.AddListener<STATES>("maketransition", CallTransition);
        //Messenger.AddListener<string>("win", RestartLevel);
        //Messenger.AddListener<string>("entitydied", ChangeState);

        //Messenger.MarkAsPermanent("maketransition");
        //Messenger.MarkAsPermanent("win");
        //Messenger.MarkAsPermanent("entitydied");

        // EnterDeadState = OnDead;

        _fsm.AddState(STATES.ALIVE);
        _fsm.AddState(STATES.DEAD);

        _fsm.m_currentState = STATES.ALIVE;

        handleDead = HandleDead;
        _fsm.AddTransition(STATES.ALIVE, STATES.DEAD, handleDead); 
    }
    
 
    // Callback EnterDeadState;

    //void DoSomething(string transition)
    //{
    //    Messenger.Broadcast("transition", transition);
    //}

    //void RestartLevel(string s)
    //{
    //    Messenger.RemoveListener<string>("win", RestartLevel);
    //    Messenger.RemoveListener<STATES>("maketransition", CallTransition);
    //    Application.LoadLevel(Application.loadedLevel);
    //}

    //void CallTransition(STATES transition)
    //{
    //    _fsm.MakeTransitionTo(transition);
    //}

    //void Update()
    //{
    //    if (healthStats.health <= 0)
    //        _fsm.MakeTransitionTo(STATES.DEAD);

    //    intState = (int)_fsm.m_currentState;

    //    Messenger.Broadcast<string, int>("playerstate", gameObject.tag, intState);
    //}

    public void HandleDead()
    {
        //do death animations
        //do sounds
        //do everything associated with this object being "dead"
        Debug.Log("FUHHH... FUH.. ");
        gameObject.SetActive(false);
        Messenger.Broadcast<string>("playerdied", gameObject.tag);

    }
    protected override void OnDead()
    {
        _fsm.MakeTransitionTo(STATES.DEAD);
    }
}

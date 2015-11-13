using UnityEngine;
using System.Collections.Generic;
public enum STATES
    {
        INIT,
        START,
        PLAY,
        PAUSE,
        GAMEOVER,
        END,
        TERM,
    }
public class GameStates : Singleton<MonoBehaviour>
{
    protected override void Awake()
    {
        base.Awake();

		Messenger.AddListener<STATES>("changegamestate", CallTransition);

		Messenger.MarkAsPermanent("changegamestate");
      

        callback = DoSomething;

        _fsm.AddState(STATES.INIT); 
        _fsm.AddState(STATES.START); 
        _fsm.AddState(STATES.PLAY);
        _fsm.AddState(STATES.PAUSE);
        _fsm.AddState(STATES.GAMEOVER);
        _fsm.AddState(STATES.END);
        _fsm.AddState(STATES.TERM);

        _fsm.m_currentState = STATES.INIT;
                                                                        	 //||   transition   ||
        _fsm.AddTransition(STATES.INIT,     STATES.START,  	 callback);    	 //||init->start     ||
        _fsm.AddTransition(STATES.START,    STATES.PLAY,   	 callback);      //||start->play     ||
        _fsm.AddTransition(STATES.PLAY,     STATES.PAUSE,  	 callback);   	 //||play->pause     ||
        _fsm.AddTransition(STATES.PAUSE,    STATES.PLAY,   	 callback);      //||pause->play     ||
        _fsm.AddTransition(STATES.PLAY,     STATES.GAMEOVER, callback); 	 //||play->gameover  ||
        _fsm.AddTransition(STATES.PAUSE,    STATES.GAMEOVER, callback); 	 //||pause->gameover ||
        _fsm.AddTransition(STATES.GAMEOVER, STATES.START, 	 callback);    	 //||gameover->start ||
        _fsm.AddTransition(STATES.START,    STATES.END, 	 callback);      //||start->end      ||
        _fsm.AddTransition(STATES.END,      STATES.TERM, 	 callback);      //||end->term       ||

        _fsm.AddTransition(STATES.GAMEOVER, STATES.PLAY, callback);     //||gameover->play  ||
        CallTransition(STATES.START);
    }   

    void DoSomething(string transition)
    {
        /// Will broadcast transition and as a param the previous state to the new state
        /// string param as "oldstate->currentstate"
        Messenger.Broadcast("gamestatechanged", transition.ToLower());
		//Listeneres would be Whoever needs to know when to play a state when a gamestate has been changed.
		//They would receivea a stringof one of the strings listed both on Trello and in the above comments.
		//Each string is what the transition was and desided what to do based on that transition.
    }

    void CallTransition(STATES transition)
    {
        _fsm.MakeTransitionTo(transition);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<STATES>("changegamestate", CallTransition);
    }



    Callback<string> callback;
    private _FSM<STATES> _fsm = new _FSM<STATES>();
    
}

using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generic template for finite state machines.
/// Will store possible states and transitions,
/// as well as check for valid transitions
/// </summary>
/// <typeparam name="T"></typeparam>
public class _FSM<T>
{
    /// <summary>
    /// Adds a staate of generic type to the list of possible states.
    /// </summary>
    /// <param name="a_state"> State to add </param>
    public void AddState(T a_state)
    {
        if (!m_states.Contains(a_state))    // makes sure the state does not already exixt
        {                                   // if so...
            m_states.Add(a_state);              // add it to the list of states
        }
    }

    /// <summary>
    /// Adds a valid transition from state A to state B to the list of valid transitions.
    /// </summary>
    /// <param name="a_state"> State the transistion is from</param>
    /// <param name="b_state"> State the transistion is to</param>
    /// <param name="a_handler"> Delegate to excuite upon success</param>
    public void AddTransition(T a_state, T b_state, Delegate a_handler)
    {
        if (!CheckTransition(a_state, b_state))                             // makes sure the key does not already exist
        {                                                                   // if so...
            string trans = a_state.ToString() + "->" + b_state.ToString();      // creates a key from the transition states
            trans = trans.ToLower();                                            // makes every letter lowercase
            m_transitions.Add(trans, a_handler);                                // add it to the list of valid transitions
        }
    }

    /// <summary>
    /// Removes a state from the list of states as well as all valid transitions associated with it.
    /// </summary>
    /// <param name="a_state"> The state to be removed</param>
    public void RemoveState(T a_state)
    {
        List<string> keys = new List<string>();     // new, empty list to store keys

        if (m_states.Contains(a_state))                                 // checks to make sure the state exist
        {                                                               // if so...
            m_states.Remove(a_state);                                       // remove it from the list of possible states

            foreach (KeyValuePair<string, Delegate> s in m_transitions)     // and for every possible transition
            {                                                               //  
                keys.Add(s.Key);                                                // add the key to the new list
            }

            foreach (string s in keys)                                      // and for every key in the list
            {                                                               //
                if (s.Contains(a_state.ToString().ToLower()))                   // check if the state is part of it
                {                                                                   // and if it is...
                    m_transitions.Remove(s);                                            // remove it from the possible transitions
                }
            }
        }
    }

    /// <summary>
    /// Returns TRUE or FALSE if the transition from state A to state B is valid or not.
    /// </summary>
    /// <param name="a_state"> State the transistion is from</param>
    /// <param name="b_state"> State the transistion is to</param>
    /// <returns></returns>
    public bool CheckTransition(T a_state, T b_state)
    {
        string trans = a_state.ToString() + "->" + b_state.ToString();      // creates a key from the transition states
        trans = trans.ToLower();                                            // makes every letter lowercase
        return m_transitions.ContainsKey(trans);                            // checks to see if the key exist as a valid transition
    }

    /// <summary>
    /// Chacks if transition form current state to state b is valid.
    /// If so, excuite delegate associated with thats transition.
    /// </summary>
    /// <param name="b_state"> State the transistion is to</param>
    public void MakeTransitionTo(T b_state)
    {
        string trans = m_currentState.ToString() + "->" + b_state.ToString();   // creates a key from the transition states
        trans = trans.ToLower();                                                // makes every letter lowercase

        if (m_transitions.ContainsKey(trans))           // checks to see if the key exist as a valid transition
        {                                               // if so...
            m_currentState = b_state;                       // set the current state to the new state
           // Callback d = m_transitions[trans] as Callback; //make a new callback and assign it the 
            //function that is associated with this transition
            //d();//call it
            m_transitions[trans].DynamicInvoke(trans);      // and call the functions associated with the transition
        }
    }

    /// <summary>
    /// The current state of the object.
    /// </summary>
    public T m_currentState;
    /// <summary>
    /// The list of possible states for the object.
    /// </summary>
    public List<T> m_states = new List<T>();
    /// <summary>
    /// The list of possible transitions for the object and function to excuite upon success.
    /// </summary>
    public Dictionary<string, Delegate> m_transitions = new Dictionary<string, Delegate>();
}

//Eric Mouledoux
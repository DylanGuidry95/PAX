using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

/// <summary>
/// Dylan Guidry
/// InputHandler.cs
/// 10/26/15
/// 
/// Handles all inputs by the user and if they are a input that is assigned to an action
/// it will publish it and what ever is listening for the event will perform the action 
/// </summary>

public class InputHandler : Singleton<InputHandler>
{
    void Start()
    {
        //Adds all the controls to the list
        //They are modified in the SetControls Functions for each player
        P1Controls.Add(Attack + ":Attack");
        P1Controls.Add(Jump + ":Jump");
        P1Controls.Add(Special + ":Special");
        P1Controls.Add(Sprint + ":Sprint");
        P2Controls.Add(Attack + ":Attack");
        P2Controls.Add(Jump + ":Jump");
        P2Controls.Add(Special + ":Special");
        P2Controls.Add(Sprint + ":Sprint");

        SetP1Controls();
        SetP2Controls();
    }

    void Update()
    {
        IdleTime += Time.deltaTime;
        if(IdleTime > 20)
        {
            Debug.Log("Thanks for playing");
        }

        if (Input.anyKey || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 ||
            Input.GetAxis("p2Vertical") != 0 || Input.GetAxis("p2Horizontal") != 0)
        {
            IdleTime = 0;
        }

        //Sets the vertical and horizontal axis Player1 should move on based on if
        //KeyBoad Controls are active or not
        if(!keyboard)
        {
            Messenger.Broadcast<string>("Player1:v", "Vertical");
            Messenger.Broadcast<string>("Player1:h", "Horizontal");
        }
        else
        {
            Messenger.Broadcast<string>("Player1:v", "KeyBoardVertical");
            Messenger.Broadcast<string>("Player1:h", "KeyBoardHorizontal");
        }

        //Sets the Axis Player2 moves on regardless of control scheme. It will allways
        //move with the controller and not the keybard
        Messenger.Broadcast<string>("Player2:h", "p2Horizontal");
        Messenger.Broadcast<string>("Player2:v", "p2Vertical");

        //Checks for inputs that are in the List of controls for player1
        foreach (string s in P1Controls)
        {
            string[] temp = s.Split(':');
            if (!keyboard)
            {
                if (Input.GetKeyDown(temp[1]))
                {
                    Messenger.Broadcast<string>("Player1:" + temp[0], temp[0]);
                }
            }
            else
            {
                if(Input.GetKeyDown(temp[0]))
                {
                    Messenger.Broadcast<string>("Player1:" + temp[1], temp[1]);
                }
            }
        }

        //Checks for inputs that are in the List of controls for player2
        foreach (string s in P2Controls)
        {
            string[] temp = s.Split(':');
            if (Input.GetKeyDown(temp[1]))
            {
                Messenger.Broadcast<string>("Player2:" + temp[0], temp[0]);
            }
        }
    }

    /// <summary>
    /// Will be used to tell how many players are active in the game and will enable 
    /// the controls only necessary for that many players
    /// </summary>
    private void NumberOfPlayers()
    {
        numPlayers++;
    }

    /// <summary>
    /// Sets the controls the user defines in to the list of controls for there selected character
    /// to utilize and react to when they are pressed
    /// </summary>
    private void SetP1Controls()
    {
        if (!keyboard)
        {
            for (int i = 0; i < P1Controls.Capacity; i++)
            {
                string[] temp = P1Controls[i].Split(':');
                switch (temp[0])
                {
                    case "A":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 0";
                            break;
                        }
                    case "B":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 1";
                            break;
                        }
                    case "X":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 2";
                            break;
                        }
                    case "Y":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 3";
                            break;
                        }
                    case "RightBumper":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 4";
                            break;
                        }
                    case "LeftBumper":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 5";
                            break;
                        }
                    case "BackButton":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 6";
                            break;
                        }
                    case "StartButton":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 7";
                            break;
                        }
                    case "LeftStick":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 8";
                            break;
                        }
                    case "RightStick":
                        {
                            P1Controls[i] = temp[1] + ":joystick 1 button 9";
                            break;
                        }
                }
            }
        }
    }

    /// <summary>
    /// Sets the controls the user defines in to the list of controls for there selected character
    /// to utilize and react to when they are pressed
    /// </summary>
    private void SetP2Controls()
    {
        if (!keyboard)
        {
            //Checks the button the user assigns to the selected action and assigns it to the appropriate
            //string value for unity to recognize inputs of that type
            for (int i = 0; i < P2Controls.Capacity; i++)
            {
                string[] temp = P2Controls[i].Split(':');
                switch (temp[0])
                {
                    case "A":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 0";
                            break;
                        }
                    case "B":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 1";
                            break;
                        }
                    case "X":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 2";
                            break;
                        }
                    case "Y":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 3";
                            break;
                        }
                    case "RightBumper":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 4";
                            break;
                        }
                    case "LeftBumper":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 5";
                            break;
                        }
                    case "BackButton":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 6";
                            break;
                        }
                    case "StartButton":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 7";
                            break;
                        }
                    case "LeftStick":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 8";
                            break;
                        }
                    case "RightStick":
                        {
                            P2Controls[i] = temp[1] + ":joystick 2 button 9";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        //Default controls for when keyboard is active
        else
        {
            P2Controls[0] = "Attack" + ":joystick 2 button 1";
            P2Controls[1] = "Jump" + ":joystick 2 button 0";
            P2Controls[2] = "Special" + ":joystick 2 button 2";
            P2Controls[3] = "Sprint" + ":joystick 2 button 4";
        }
    }

    /// <summary>
    /// All variables of the InputHandler class
    /// </summary>
    #region Varaibles
    public string Attack; //Control assigned to the attack action
    public string Jump; //Control assigned to the Jump Action
    public string Special; //Control assigned to the Specail Action
    public string Sprint; //Control assigned to the Sprint Action
    public bool keyboard; //A boolean to determine if the controls are configured for keyboard or Controler

    private int numPlayers; //Keeps track of number of players active in the game

    [SerializeField]
    protected List<string> P1Controls = new List<string>(); //List that stores all controls for player 1
    [SerializeField]
    protected List<string> P2Controls = new List<string>(); //List that stores all controls for player 2

    private float IdleTime; //Will be used to turn return to main menu if no input for a certain time.
    #endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Static messenger getter.
/// 
/// Developed by: Quinton "Kiro" Baudoin
/// 
/// 
/// Purpose : To fetch information from Messenger for the use of EditorMessengerWindow.cs.
/// 
/// Use:  The Use comes from EditorMessengerWindow.cs and the information comes from Messenger.cs
/// 	
/// 	if definition of LOG_ADD_LISTENER_EDITORWINDOW is commented out or EditorWindowInfo is commented out this script will only display the event messages
/// 		not the target or the methods of those messages.
/// 
/// 
/// 	Usage Note: If information is application is NOT in play this script will not fetch the information from Messenger.
/// 
/// </summary>



public class StaticMessengerGetter
{

	static public List<string> MessengerListenerList()
	{
		List<string> Listener = new List<string>();
		if(Application.isPlaying)
		{
			if(Messenger.EditorWindowInfo.Count > 0)
				return Messenger.EditorWindowInfo;

		Listener.Add("<<Method Info Missing>>");
		Listener.Add("Messenger.cs: LOG_ADD_LISTENER_EDITORWINDOW or EditorWindowInfo is commented out or missing");
		foreach(KeyValuePair<string,Delegate> message in Messenger.eventTable)
		{
					Listener.Add(message.Key);
		}
		}
		return Listener;
	}




}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;

namespace Debug_Cheat_Console
{
	public class CheatConsoleManager : MonoBehaviour
	{

		#region Singleton

		private static CheatConsoleManager _instance;

		public static CheatConsoleManager Instance
		{
			get
			{
				if (_instance == null)
				{
					Instance = Instantiate(new GameObject("CheatConsoleManager Object", new[] {typeof(CheatConsoleManager)}).GetComponent<CheatConsoleManager>());
				}
				
				return _instance;
			}

			set
			{
				if (_instance == null)
				{
					_instance = value;
				}
				else
				{
					Destroy(value);
				}
			}
		}
		

		#endregion
		
		#region Variables

		private bool showConsole = false;

		private string input;
		
		private PlayerInput _playerInput;

		private bool _initialize = false;
		
		#endregion

		#region Getter/Setter

		public bool Initialize => _initialize;

		#endregion

		#region Commands

		public static DebugCommand<string> DEBUG_MESSAGE;

		[HideInInspector]
		public List<object> commandList; 
		
		#endregion

		#region Initialization

		private void Awake()
		{
			Instance = this;
			InitializeCommands();
			
			_initialize = true;
		}
	

		private void InitializeCommands()
		{
			DEBUG_MESSAGE = new DebugCommand<string>("debug_message","Debug a message in the console","debug_message [your message]",Debug.Log);
			
			commandList = new List<object>
			{
				DEBUG_MESSAGE
			};
		}

		#endregion
		
		#region ConsoleCheat

		#region Input

		public void OnToggleDebug(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;
			
			showConsole = !showConsole;
		}

		public void OnReturn(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed || !showConsole) return;
			
			HandleInput();
			input = "";
		}

		#endregion

		private void OnGUI()
		{
			if (!showConsole) return;

			float y = 0f;
			
			GUI.Box(new Rect(0,y, Screen.width,30), "" );
			GUI.backgroundColor = new Color(0f,0f,0f,0f);
			input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
			
		}

		private void HandleInput()
		{
			string[] properties = input.Split(' ');
			
			foreach (object commandObject in commandList)
			{
				if (commandObject is DebugCommandBase commandBase && properties[0].Contains(commandBase.CommandID))
				{
					//Iterate througt everyType
					if (commandBase is DebugCommand command)
					{
						command.Invoke();
					}
					else if(commandBase is DebugCommand<string> commandString)
					{
						string message = string.Join(" ", properties, 1, properties.Length - 1);
						commandString.Invoke(message);
					}
					else
					{
						throw new ArgumentException("The command \"" + commandBase.CommandID + "\" is not iterate on the if.");
					}
				}
			}
			
		}

		#endregion

		
		#region Sub/Unsub

		public bool Subscribe(DebugCommandBase command)
		{
			if (!_initialize || commandList.Contains(command)) return false;
			
			commandList.Add(command);
			return true;
		}

		public bool Unsubscribe(DebugCommandBase command)
		{
			if (!_initialize || !commandList.Contains(command)) return false;

			commandList.Remove(command);
			return true;
		}

		#endregion
	}
}
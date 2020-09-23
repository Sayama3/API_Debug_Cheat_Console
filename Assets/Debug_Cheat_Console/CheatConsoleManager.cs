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

		[SerializeField]
		private GUIStyle buttonStyle = new GUIStyle();
		
		private bool _showConsole;

		private bool _showHelp;

		private string _input;
		
		private PlayerInput _playerInput;

		private bool _initialize = false;
		
		#endregion

		#region Getter/Setter

		public bool Initialize => _initialize;

		#endregion

		#region Commands
		
		/// <summary>
		/// Command Listing every other command.
		/// </summary>
		public static DebugCommand HELP { get; private set; }

		
		/// <summary>
		/// Dictionary containing the commands, and the action to launch the command with parameters :<br />
		///  - The associate commands <br />
		///  - The array of properties include at index 0 the command.
		/// </summary>
		private Dictionary<DebugCommandBase, Action<DebugCommandBase,string[]>> _dicoCommandsActivation;
		
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
			HELP = new DebugCommand("help", "Show a list of command.", "help",
				() => _showHelp = !_showHelp);
			
			if(_dicoCommandsActivation == null)
				_dicoCommandsActivation = new Dictionary<DebugCommandBase, Action<DebugCommandBase, string[]>>();

			if (!_dicoCommandsActivation.ContainsKey(HELP))
				_dicoCommandsActivation.Add(HELP, (command, properties) => ((DebugCommand) command).Invoke());

		}

		#endregion
		
		#region ConsoleCheat

		#region Input

		public void OnToggleDebug(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;
			
			_showConsole = !_showConsole;
		}

		public void OnReturn(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed || !_showConsole) return;
			
			HandleInput();
			_input = "";
		}

		#endregion

		private Vector2 _scroll;
		private void OnGUI()
		{
			if (!_showConsole) return;

			float y = 0f;
			
			if (_showHelp)
			{
				GUI.Box(new Rect(0,y,Screen.width,100),"" );
				
				Rect viewport = new Rect(0,0,Screen.width-30,30*_dicoCommandsActivation.Count);
				_scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), _scroll, viewport);

				int realIndex = 0;
				for (int i = 0; i < _dicoCommandsActivation.Count; i++)
				{
					DebugCommandBase command = _dicoCommandsActivation.Keys.ToArray()[i];
					if(_input != null && !command.CommandFormat.Contains(_input.Split(' ')[0])) continue;
					string label = $"{command.CommandFormat} - {command.CommandDescription}";
					
					Rect labelRect = new Rect(5,30*realIndex,viewport.width-100,30);

					if (GUI.Button(labelRect, label,buttonStyle))
						_input = command.CommandID;
					
					realIndex++;
				}
				
				GUI.EndScrollView();
				
				y += 100;
			}
			
			GUI.Box(new Rect(0,y, Screen.width,30), "" );
			GUI.backgroundColor = new Color(0f,0f,0f,0f);
			_input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
			
		}

		private void HandleInput()
		{
			string[] properties = _input.Split(' ');

			foreach (DebugCommandBase commandBase in _dicoCommandsActivation.Keys)
			{
				if (properties[0].Contains(commandBase.CommandID))
				{
					_dicoCommandsActivation[commandBase].Invoke(commandBase,properties);
				}
			}
			
		}

		#endregion

		
		#region Sub/Unsub

		/// <summary>
		/// Add a command to the possible command
		/// </summary>
		/// <param name="command">The command we want to add</param>
		/// <param name="activation">
		///	The action to launch the command taking as parameter<br />
		/// 	- The associate command (previous parameters)<br />
		/// 	- An array of string containing the command and every associate parameters
		/// </param>
		/// <returns></returns>
		public bool Subscribe(DebugCommandBase command,Action<DebugCommandBase,string[]> activation)
		{
			if (_dicoCommandsActivation.ContainsKey(command)) return false;
			
			_dicoCommandsActivation.Add(command,activation);
			return true;
		}

		/// <summary>
		/// Remove the command from the possible Command
		/// </summary>
		/// <param name="command">The command we want to remove</param>
		/// <returns></returns>
		public bool Unsubscribe(DebugCommandBase command)
		{
			if (!_dicoCommandsActivation.ContainsKey(command) || command == HELP) return false;

			_dicoCommandsActivation.Remove(command);
			return true;
		}

		#endregion
	}
}
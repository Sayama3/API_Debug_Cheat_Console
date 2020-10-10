using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Debug_Cheat_Console
{
	[AddComponentMenu("Debug_Cheat_Console/CheatConsoleManager")]
	public class CheatConsoleManager : MonoBehaviour
	{

		#region Singleton

		private static CheatConsoleManager _instance;

		/// <summary>
		/// Singleton of the CheatConsoleManager
		/// </summary>
		public static CheatConsoleManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameObject("CheatConsoleManager Object", typeof(CheatConsoleManager)).GetComponent<CheatConsoleManager>();
				}
				
				return _instance;
			}

			set => _instance = value;
		}

		#endregion
		
		#region Variables

		[Header("Button Style Parameters")]
		[SerializeField]
		private GUIStyle buttonStyle = new GUIStyle();

		[Header("User Input Parameters")]
		[SerializeField] private char beginningChar = '>';
		
		private bool _showConsole;

		private bool _showHelp;

		private string _input;

		private bool _initialize = false;
		
		#endregion

		#region Getter/Setter

		/// <summary>
		/// Is the game initialize
		/// </summary>
		public bool Initialize => _initialize;

		public GUIStyle ButtonStyle
		{
			get => buttonStyle;
			set => buttonStyle = value;
		}

		#endregion

		#region Editor

#if UNITY_EDITOR
		
		[UnityEditor.MenuItem("Tools/Debug_Cheat_Console/Create CheatConsoleManager",false)]
		public static void CreateDebugCheatManagerObject()
		{
			CheatConsoleManager[] list = FindObjectsOfType<CheatConsoleManager>();
			foreach (var obj in list)
				DestroyImmediate(obj.gameObject,false);
			
			var instance = new GameObject("CheatConsoleManager Object", typeof(CheatConsoleManager));
			
			UnityEditor.Presets.Preset preset = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.Presets.Preset>("Packages/com.sayama.debugcheatconsole/Runtime/Preset/Default_CheatConsoleManager.preset");
			preset?.ApplyTo(instance.GetComponent<CheatConsoleManager>());
		}
#endif

		#endregion

		#region Commands
		
		/// <summary>
		/// Command Listing every other command.
		/// </summary>
		public static DebugCommandBase HELP { get; private set; }

		
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
			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
			InitializeCommands();
			DontDestroyOnLoad(gameObject);
			_initialize = true;
		}

		private void InitializeCommands()
		{
			HELP = new DebugCommandBase("help", "Show/Hide the list of the actual commands.", "help");
			
			if(_dicoCommandsActivation == null)
				_dicoCommandsActivation = new Dictionary<DebugCommandBase, Action<DebugCommandBase, string[]>>();

			if (!_dicoCommandsActivation.ContainsKey(HELP))
				_dicoCommandsActivation.Add(HELP, (command, properties) => _showHelp = !_showHelp);

		}

		#endregion
		
		#region ConsoleCheat

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
			GUI.TextArea(new Rect(10f, y + 5f, 20f, 20f), beginningChar.ToString()); //BeginningChar
			_input = GUI.TextField(new Rect(22.5f, y + 5f, Screen.width - 32.5f, 20f), _input); //The input
			
		}

		private void HandleInput()
		{
			if (_input == null || _input == "") return;
			
			string[] properties = _input.Split(' ');

			foreach (DebugCommandBase commandBase in _dicoCommandsActivation.Keys)
			{
				if (properties[0]?.Contains(commandBase.CommandID)??false)
				{
					_dicoCommandsActivation[commandBase].Invoke(commandBase,properties);
				}
			}
			
		}

		#endregion

		
		#region Input
		
		
		[Header("Input Event Parameters",order = 3)]
		public UnityEvent<bool> onToggleDebug;
		public UnityEvent<string> onInputReturn;
		

#if ENABLE_INPUT_SYSTEM


		public void OnToggleDebug(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;
			
			_showConsole = !_showConsole;
			onToggleDebug?.Invoke(_showConsole);
			if (!_showConsole)
				_input = "";
		}

		public void OnReturn(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed || !_showConsole) return;
			
			HandleInput();
			onInputReturn?.Invoke(_input);
			_input = "";
		}
		
		

#else

		[Header("Input Parameters",order = 2)]
		[SerializeField]
		private KeyCode _onToggleDebug = KeyCode.Quote;
		[SerializeField] 
		private KeyCode _onReturn = KeyCode.Return;

		[SerializeField] 
		private bool _debugKeyPress = false;

		private void Update()
		{
			if (Input.GetKeyDown(_onToggleDebug))
			{
				_showConsole = !_showConsole;
				onToggleDebug?.Invoke(_showConsole);
				if (!_showConsole)
					_input = "";
			}

			if (Input.GetKeyDown(_onReturn) && _showConsole)
			{
				HandleInput();
				onInputReturn?.Invoke(_input);
				_input = "";
			}

			if (_debugKeyPress)
			{
				foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
				{
					if(Input.GetKeyDown(key))
						Debug.Log("Key press is " + key);
				}
			}
		}

#endif


		#endregion

		
		#region Sub/Unsub

		/// <summary>
		/// Add a command to the possible command
		/// </summary>
		/// <param name="command">The command we want to add</param>
		/// <param name="actionCommand">
		///	The action associate to the command taking as parameter<br />
		/// 	- The associate command (previous parameters)<br />
		/// 	- An array of string containing the command and every associate parameters
		/// </param>
		/// <returns></returns>
		public bool Subscribe(DebugCommandBase command,Action<DebugCommandBase,string[]> actionCommand)
		{
			if (_dicoCommandsActivation.ContainsKey(command)) return false;
			
			_dicoCommandsActivation.Add(command,actionCommand);
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
	
	
	/// <summary>
	/// A Class with all information needed from a command
	/// </summary>
	public class DebugCommandBase
	{
		#region Variable

		private string _commandID;
		private string _commandDescription;
		private string _commandFormat;

		#endregion

		#region Getters/Setters

		/// <summary>
		/// The Id of the command
		/// </summary>
		public string CommandID => _commandID;
		/// <summary>
		/// The description of the command. What does he do
		/// </summary>
		public string CommandDescription => _commandDescription;
		/// <summary>
		/// The format of the command, (name + every parameters) <br />
		/// Useful in the HELP command for exemple
		/// </summary>
		public string CommandFormat => _commandFormat;

		#endregion


		public DebugCommandBase(string id, string description, string format)
		{
			_commandID = id;
			_commandDescription = description;
			_commandFormat = format;
		}

		public DebugCommandBase(){}

	}
}
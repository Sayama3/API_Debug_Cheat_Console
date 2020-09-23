using System;

namespace Debug_Cheat_Console
{
    public class DebugCommandBase
    {
        #region Variable

        private string _commandID;
        private string _commandDescription;
        private string _commandFormat;

        #endregion

        #region Getters/Setters

        public string CommandID => _commandID;
        public string CommandDescription => _commandDescription;
        public string CommandFormat => _commandFormat;

        #endregion


        public DebugCommandBase(string id, string description, string format)
        {
            _commandID = id;
            _commandDescription = description;
            _commandFormat = format;
        }
        
    }

    public class DebugCommand : DebugCommandBase
    {
        private Action _command;

        public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
        {
            _command = command;
        }

        public void Invoke()
        {
            _command.Invoke();
        }
    }

    public class DebugCommand<T1> : DebugCommandBase
    {
        private Action<T1> _command;

        public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
        {
            _command = command;
        }

        public void Invoke(T1 value)
        {
            _command.Invoke(value);
        }
    }
    
    public class DebugCommand<T1,T2> : DebugCommandBase
    {
        private Action<T1,T2> _command;

        public DebugCommand(string id, string description, string format, Action<T1,T2> command) : base(id, description, format)
        {
            _command = command;
        }

        public void Invoke(T1 value1,T2 value2)
        {
            _command.Invoke(value1,value2);
        }
    }
    
    public class DebugCommand<T1,T2,T3> : DebugCommandBase
    {
        private Action<T1,T2,T3> _command;

        public DebugCommand(string id, string description, string format, Action<T1,T2,T3> command) : base(id, description, format)
        {
            _command = command;
        }

        public void Invoke(T1 value1,T2 value2,T3 value3)
        {
            _command.Invoke(value1,value2,value3);
        }
    }
}
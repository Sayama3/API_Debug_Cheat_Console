using System;

namespace Debug_Cheat_Console
{
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
﻿using QueryConverter.Core.Enums;
using System.Diagnostics;

namespace QueryConverter.Core.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ArgumentAttribute : Attribute
    {
        private string _shortName;
        private string _longName;
        private string _helpText;
        private object _defaultValue;
        private ArgumentType _type;

        /// <summary>
        /// Allows control of command line parsing.
        /// </summary>
        /// <param name="type"> Specifies the error checking to be done on the argument. </param>
        public ArgumentAttribute(ArgumentType type)
        {
            _type = type;
        }

        /// <summary>
        /// The error checking to be done on the argument.
        /// </summary>
        public ArgumentType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Returns true if the argument did not have an explicit short name specified.
        /// </summary>
        public bool DefaultShortName { get { return null == _shortName; } }

        /// <summary>
        /// The short name of the argument.
        /// Set to null means use the default short name if it does not
        /// conflict with any other parameter name.
        /// Set to String.Empty for no short name.
        /// This property should not be set for DefaultArgumentAttributes.
        /// </summary>
        public string ShortName
        {
            get { return _shortName; }
            set { Debug.Assert(value == null || !(this is DefaultArgumentAttribute)); _shortName = value; }
        }

        /// <summary>
        /// Returns true if the argument did not have an explicit long name specified.
        /// </summary>
        public bool DefaultLongName { get { return null == _longName; } }

        /// <summary>
        /// The long name of the argument.
        /// Set to null means use the default long name.
        /// The long name for every argument must be unique.
        /// It is an error to specify a long name of String.Empty.
        /// </summary>
        public string LongName
        {
            get { Debug.Assert(!this.DefaultLongName); return _longName; }
            set { Debug.Assert(value != ""); _longName = value; }
        }

        /// <summary>
        /// The default value of the argument.
        /// </summary>
        public object DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        /// <summary>
        /// Returns true if the argument has a default value.
        /// </summary>
        public bool HasDefaultValue { get { return null != _defaultValue; } }

        /// <summary>
        /// Returns true if the argument has help text specified.
        /// </summary>
        public bool HasHelpText { get { return null != _helpText; } }

        /// <summary>
        /// The help text for the argument.
        /// </summary>
        public string HelpText
        {
            get { return _helpText; }
            set { _helpText = value; }
        }
    }
}

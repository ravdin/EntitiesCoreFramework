using System;

namespace EntitiesCoreFramework.Utilities
{
    /// <summary>
    /// Assign a string to enum values.
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}

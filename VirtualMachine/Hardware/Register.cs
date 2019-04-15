using System;

namespace VirtualMachine
{
    public class Register
    {
        private int m_Value;

        public string Name { get; private set; }

        public int Value {
            get { return m_Value; }
            set {
                if (ReadOnly)
                {
                    throw new Exception($"Failed to write to register {Name}, read only.");
                }

                m_Value = value;
            }
        }

        public bool ReadOnly { get; private set; }



        public Register(string name)
        {
            Name = name;
        }

        public Register(string name, int value, bool readOnly = true)
        {
            Name = name;
            Value = value;
            ReadOnly = readOnly;
        }
    }
}
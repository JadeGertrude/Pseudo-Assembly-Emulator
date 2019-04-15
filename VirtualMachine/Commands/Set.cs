using System;

namespace VirtualMachine.Commands
{
    public class SetCommand : ICommand
    {
        private Register Register { get; set; }
        private int Value { get; set; }


        public SetCommand(Register register, int value)
        {
            Register = register;
            Value = value;
        }

        public void Execute(ProgramFile program)
        {
            Register.Value = Value;
        }
    }
}
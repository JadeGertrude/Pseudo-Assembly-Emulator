using System;

namespace VirtualMachine.Commands
{
    public class SetCommand : ICommand
    {
        private Register Register { get; set; }
        private byte Value { get; set; }


        public SetCommand(Register register, byte value)
        {
            Register = register;
            Value = value;
        }

        public void Execute(ProgramFile program)
        {
            Register.Value = Value;
        }

        public void Dump()
        {
            Console.WriteLine($"SET {Register.Name} {Value:X}");
        }
    }
}
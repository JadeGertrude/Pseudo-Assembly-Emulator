using System;

namespace VirtualMachine.Commands
{
    public class OutCommand : ICommand
    {
        private Register Register { get; set; }



        public OutCommand(Register register)
        {
            Register = register;
        }

        public void Execute(ProgramFile program)
        {
            Console.WriteLine(Register.Value);
        }

        public void Dump()
        {
            Console.WriteLine($"OUT {Register.Name}");
        }
    }
}
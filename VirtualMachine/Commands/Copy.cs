using System;

namespace VirtualMachine.Commands
{
    public class CopyCommand : ICommand
    {
        private Register RegisterOne { get; set; }
        private Register RegisterTwo { get; set; }


        public CopyCommand(Register registerOne, Register registerTwo)
        {
            RegisterOne = registerOne;
            RegisterTwo = registerTwo;
        }

        public void Execute(ProgramFile program)
        {
            RegisterTwo.Value = RegisterOne.Value;
        }

        public void Dump()
        {
            Console.WriteLine($"CPY {RegisterOne.Name} {RegisterTwo.Name}");
        }
    }
}
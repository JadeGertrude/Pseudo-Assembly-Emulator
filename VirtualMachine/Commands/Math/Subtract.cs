using System;

namespace VirtualMachine.Commands
{
    public class SubtractCommand : ICommand
    {
        private Register RegisterOne { get; set; }
        private Register RegisterTwo { get; set; }
        private Register RegisterOut { get; set; }


        public SubtractCommand(Register registerOne, Register registerTwo, Register registerOut)
        {
            RegisterOne = registerOne;
            RegisterTwo = registerTwo;
            RegisterOut = registerOut;
        }

        public void Execute(ProgramFile program)
        {
            RegisterOut.Value = RegisterOne.Value - RegisterTwo.Value;
        }
    }
}
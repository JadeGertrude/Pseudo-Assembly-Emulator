﻿using System;

namespace VirtualMachine.Commands
{
    public class AddCommand : ICommand
    {
        private Register RegisterOne { get; set; }
        private Register RegisterTwo { get; set; }
        private Register RegisterOut { get; set; }


        public AddCommand(Register registerOne, Register registerTwo, Register registerOut)
        {
            RegisterOne = registerOne;
            RegisterTwo = registerTwo;
            RegisterOut = registerOut;
        }

        public void Execute(ProgramFile program)
        {
            RegisterOut.Value = (byte)(RegisterOne.Value + RegisterTwo.Value);
        }

        public void Dump()
        {
            Console.WriteLine($"ADD {RegisterOne.Name} {RegisterTwo.Name} {RegisterOut.Name}");
        }
    }
}
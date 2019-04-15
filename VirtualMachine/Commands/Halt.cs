using System;

namespace VirtualMachine.Commands
{
    public class HaltCommand : ICommand
    {
        public HaltCommand()
        {

        }

        public void Execute(ProgramFile program)
        {
            Console.WriteLine("PAUSED");
            Console.ReadKey(true);
        }
    }
}
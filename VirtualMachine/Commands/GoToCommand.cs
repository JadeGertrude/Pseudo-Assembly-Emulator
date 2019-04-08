using System;

namespace VirtualMachine.Commands
{
    public class JumpCommand : ICommand
    {
        private string Label { get; set; }


        public JumpCommand(string label)
        {
            Label = label;
        }

        public void Execute(ProgramFile program)
        {
            program.Jump(Label);
        }

        public void Dump()
        {
            Console.WriteLine($"JMP {Label}");
        }
    }
}
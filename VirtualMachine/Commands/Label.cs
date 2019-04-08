using System;

namespace VirtualMachine.Commands
{
    public class LabelCommand : ICommand
    {
        private string Label { get; set; }


        public LabelCommand(string label)
        {
            Label = label;
        }

        public void Execute(ProgramFile program)
        {
            
        }

        public void Dump()
        {
            Console.WriteLine($"LBL {Label}");
        }
    }
}
using System.Collections.Generic;
using VirtualMachine.Commands;

namespace VirtualMachine
{
    public class ProgramFile
    {
        public int Counter { get; private set; }

        public List<ICommand> Commands { get; private set; }
        public Dictionary<string, int> Labels { get; private set; }



        public ProgramFile()
        {
            Counter = 0;
            Commands = new List<ICommand>();
            Labels = new Dictionary<string, int>();
        }

        public void Run()
        {
            for (Counter = 0; Counter < Commands.Count; Counter++)
            {
                Commands[Counter].Execute(this);
            }
        }

        public void Clear()
        {
            Commands.Clear();
            Labels.Clear();
        }

        public void MakeLabel(string label)
        {
            if (!Labels.ContainsKey(label))
            {
                Labels.Add(label, Counter);
            }
        }

        public void Jump(string label)
        {
            if (Labels.ContainsKey(label))
            {
                Counter = Labels[label];
            }
        }

        public void AddCommand(ICommand command)
        {
            Commands.Add(command);

            Counter++;
        }
    }
}
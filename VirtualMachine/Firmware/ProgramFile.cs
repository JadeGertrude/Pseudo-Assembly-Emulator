using System.Collections.Generic;
using VirtualMachine.Commands;

namespace VirtualMachine
{
    public class ProgramFile
    {
        public int Counter { get; private set; }

        public List<ICommand> Commands { get; private set; }
        public List<Label> Labels { get; private set; }
        public Dictionary<string, string> Strings { get; private set; }



        public ProgramFile()
        {
            Counter = 0;
            Commands = new List<ICommand>();
            Labels = new List<Label>();
            Strings = new Dictionary<string, string>();
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

        public void MakeLabel(string name)
        {
            if (Labels.Find(x => x.Name == name) == null)
            {
                Labels.Add(
                    new Label()
                    {
                        Name = name,
                        Index = Counter - 1
                    }
                );
            }
        }

        public void Jump(string name)
        {
            Label label = Labels.Find(x => x.Name == name);

            if (label != null)
            {
                Counter = label.Index;
            }
        }

        public void RegisterString(string key, string value)
        {
            Strings.Add(key, value);
        }

        public void AddCommand(ICommand command)
        {
            Commands.Add(command);

            Counter++;
        }
    }
}
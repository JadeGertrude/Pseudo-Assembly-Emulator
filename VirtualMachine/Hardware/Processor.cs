using System;
using System.Collections.Generic;
using System.IO;
using VirtualMachine.Commands;
using ICommand = VirtualMachine.Commands.ICommand;

namespace VirtualMachine
{
    public class Processor
    {
        private Dictionary<string, Register> Registers;
        private ProgramFile Program;


        public Processor()
        {
            Registers = new Dictionary<string, Register>();
            Program = new ProgramFile();
        }

        public void UseDefaultRegisters()
        {
            AddRegister("A");
            AddRegister("B");
            AddRegister("C");
            AddRegister("D");
            AddRegister("I");
            AddRegister("N");
            AddRegister("X0");
            AddRegister("X1");
            AddRegister("X2");
            AddRegister("X3");
            AddRegister("X4");
            AddRegister("X5");
            AddRegister("X6");
            AddRegister("X7");
            AddRegister("K0", 0);
            AddRegister("K1", 1);
        }

        public void AddRegister(string name, int value = 0, bool readOnly = false)
        {
            Registers.Add(name, new Register(name, value, readOnly));
        }

        public void Compile(string text)
        {
            Program.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Compilation started {DateTime.Now.TimeOfDay}...");
            Console.ForegroundColor = ConsoleColor.Gray;

            string[] lines = text.Split(new char[] { '\n' });

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // Split the line into separate parts.
                string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 0)
                {
                    continue;
                }

                // Retrieve the name of the command and any arguments that follow.
                string name = words[0];
                string[] arguments = new string[words.Length - 1];
                Array.Copy(words, 1, arguments, 0, arguments.Length);

                // Generate a command from the string information.
                ICommand command = CommandFromString(name, arguments);
                
                if (command != null)
                {
                    Program.AddCommand(command);
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Compilation finished {DateTime.Now.TimeOfDay}!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private ICommand CommandFromString(string name, string[] arguments)
        {
            ICommand command = null;

            switch (name)
            {
                case "SET":
                    {
                        Register register = Registers[arguments[0]];
                        int value = int.Parse(arguments[1]);

                        command = new SetCommand(register, value);
                        break;
                    }
                case "ADD":
                    {
                        Register registerOne = Registers[arguments[0]];
                        Register registerTwo = Registers[arguments[1]];
                        Register registerOut = Registers[arguments[2]];

                        command = new AddCommand(registerOne, registerTwo, registerOut);
                        break;
                    }
                case "SUB":
                    {
                        Register registerOne = Registers[arguments[0]];
                        Register registerTwo = Registers[arguments[1]];
                        Register registerOut = Registers[arguments[2]];

                        command = new SubtractCommand(registerOne, registerTwo, registerOut);
                        break;
                    }
                case "MUL":
                    {
                        Register registerOne = Registers[arguments[0]];
                        Register registerTwo = Registers[arguments[1]];
                        Register registerOut = Registers[arguments[2]];

                        command = new MultiplyCommand(registerOne, registerTwo, registerOut);
                        break;
                    }
                case "DIV":
                    {
                        Register registerOne = Registers[arguments[0]];
                        Register registerTwo = Registers[arguments[1]];
                        Register registerOut = Registers[arguments[2]];

                        command = new DivideCommand(registerOne, registerTwo, registerOut);
                        break;
                    }
                case "OUT":
                    {
                        if (Registers.ContainsKey(arguments[0]))
                        {
                            Register register = Registers[arguments[0]];
                            command = new OutCommand(register);
                        }
                        else
                        {
                            command = new OutCommand(arguments[0]);
                        }

                        break;
                    }
                case "HLT":
                    {
                        command = new HaltCommand();
                        break;
                    }
                case "JMP":
                    {
                        string label = arguments[0];

                        command = new JumpCommand(label);
                        break;
                    }
                case "JIF":
                    {
                        Register registerOne = Registers[arguments[0]];
                        Register registerTwo = Registers[arguments[2]];
                        string conditionString = arguments[1];
                        JumpCondition jumpCondition = JumpCondition.Undefined;
                        switch (conditionString)
                        {
                            case "==":
                                jumpCondition = JumpCondition.Equal;
                                break;
                            case "<":
                                jumpCondition = JumpCondition.LessThan;
                                break;
                            case "<=":
                                jumpCondition = JumpCondition.LessThanOrEqual;
                                break;
                            case ">":
                                jumpCondition = JumpCondition.GreaterThan;
                                break;
                            case ">=":
                                jumpCondition = JumpCondition.GreaterThanOrEqual;
                                break;
                            case "!=":
                                jumpCondition = JumpCondition.NotEqual;
                                break;
                        }

                        string label = arguments[3];

                        command = new JumpIfCommand(registerOne, registerTwo, label, jumpCondition);
                        break;
                    }
                case "CPY":
                    {
                        Register registerOne = Registers[arguments[0]];
                        Register registerTwo = Registers[arguments[1]];

                        command = new CopyCommand(registerOne, registerTwo);
                        break;
                    }
                default:
                    if (name == "str:")
                    {
                        string stringKey = arguments[0];
                        string stringValue = string.Empty;
                        for (int i = 1; i < arguments.Length; i++)
                        {
                            stringValue += arguments[i];
                            if (i < arguments.Length)
                            {
                                stringValue += " ";
                            }
                        }

                        Program.RegisterString(stringKey, stringValue);
                    }
                    else if (name.Length > 1 && name.EndsWith(":"))
                    {
                        Program.MakeLabel(name.Substring(0, name.Length - 1));
                        break;
                    }
                    break;
            }

            return command;
        }

        public void DoFile(string path)
        {
            string text = string.Join("\n", File.ReadAllLines(path));

            Compile(text);
            Run();
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Executing program...");
            Console.ForegroundColor = ConsoleColor.Gray;

            Program.Run();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Finished!");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadKey(true);
        }
    }
}
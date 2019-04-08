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

        public void AddRegister(string name, byte value = 0, bool readOnly = false)
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
                string[] words = line.Split(new char[] { ' ' });

                string commandName = words[0];
                string[] arguments = new string[words.Length - 1];
                Array.Copy(words, 1, arguments, 0, arguments.Length);

                ICommand command = null;

                Console.Write($"{Program.Commands.Count, -2} | ");

                switch (commandName)
                {
                    case "SET":
                        {
                            Register register = Registers[arguments[0]];
                            byte value = byte.Parse(arguments[1]);

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
                    case "OUT":
                        {
                            Register register = Registers[arguments[0]];
                        
                            command = new OutCommand(register);
                            break;
                        }
                    case "HLT":
                        {
                            command = new HaltCommand();
                            break;
                        }
                    case "LBL":
                        {
                            string label = arguments[0];

                            command = new LabelCommand(label);
                            Program.MakeLabel(label);
                            break;
                        }
                    case "JMP":
                        {
                            string label = arguments[0];

                            command = new JumpCommand(label);
                            break;
                        }
                    case "JET":
                        {
                            Register registerOne = Registers[arguments[0]];
                            Register registerTwo = Registers[arguments[1]];
                            string label = arguments[2];

                            command = new JumpIfCommand(registerOne, registerTwo, label, JumpCondition.Equal);
                            break;
                        }
                    case "JLT":
                        {
                            Register registerOne = Registers[arguments[0]];
                            Register registerTwo = Registers[arguments[1]];
                            string label = arguments[2];

                            command = new JumpIfCommand(registerOne, registerTwo, label, JumpCondition.LessThan);
                            break;
                        }
                    case "JLE":
                        {
                            Register registerOne = Registers[arguments[0]];
                            Register registerTwo = Registers[arguments[1]];
                            string label = arguments[2];

                            command = new JumpIfCommand(registerOne, registerTwo, label, JumpCondition.LessThanOrEqual);
                            break;
                        }
                    case "JGT":
                        {
                            Register registerOne = Registers[arguments[0]];
                            Register registerTwo = Registers[arguments[1]];
                            string label = arguments[2];

                            command = new JumpIfCommand(registerOne, registerTwo, label, JumpCondition.GreaterThan);
                            break;
                        }
                    case "JGE":
                        {
                            Register registerOne = Registers[arguments[0]];
                            Register registerTwo = Registers[arguments[1]];
                            string label = arguments[2];

                            command = new JumpIfCommand(registerOne, registerTwo, label, JumpCondition.GreaterThanOrEqual);
                            break;
                        }
                    case "JNE":
                        {
                            Register registerOne = Registers[arguments[0]];
                            Register registerTwo = Registers[arguments[1]];
                            string label = arguments[2];

                            command = new JumpIfCommand(registerOne, registerTwo, label, JumpCondition.NotEqual);
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
                        break;
                }

                if (command != null)
                {
                    Program.AddCommand(command);
                    command.Dump();
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    Console.CursorLeft = 0;
                    Console.WriteLine("   | ");
                }
                else if (line.StartsWith("#"))
                {
                    Console.CursorLeft = 0;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("   | " + line);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.CursorLeft = 0;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"   | ERROR LINE {i}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Compilation finished {DateTime.Now.TimeOfDay}!");
            Console.ForegroundColor = ConsoleColor.Gray;
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
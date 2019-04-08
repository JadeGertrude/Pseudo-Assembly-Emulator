using System;

namespace VirtualMachine.Commands
{
    public enum JumpCondition
    {
        LessThan,
        LessThanOrEqual,
        Equal,
        GreaterThanOrEqual,
        GreaterThan,
        NotEqual
    }

    public class JumpIfCommand : ICommand
    {
        private Register RegisterOne { get; set; }
        private Register RegisterTwo { get; set; }
        private string Label { get; set; }

        private JumpCondition Condition { get; set; }


        public JumpIfCommand(Register registerOne, Register registerTwo, string label, JumpCondition condition)
        {
            RegisterOne = registerOne;
            RegisterTwo = registerTwo;
            Condition = condition;
            Label = label;
        }

        public void Execute(ProgramFile program)
        {
            switch (Condition)
            {
                case JumpCondition.LessThan:
                    if (RegisterOne.Value < RegisterTwo.Value)
                    {
                        program.Jump(Label);
                    }

                    break;
                case JumpCondition.LessThanOrEqual:
                    if (RegisterOne.Value <= RegisterTwo.Value)
                    {
                        program.Jump(Label);
                    }

                    break;
                case JumpCondition.Equal:
                    if (RegisterOne.Value == RegisterTwo.Value)
                    {
                        program.Jump(Label);
                    }

                    break;
                case JumpCondition.GreaterThanOrEqual:
                    if (RegisterOne.Value >= RegisterTwo.Value)
                    {
                        program.Jump(Label);
                    }

                    break;
                case JumpCondition.GreaterThan:
                    if (RegisterOne.Value > RegisterTwo.Value)
                    {
                        program.Jump(Label);
                    }

                    break;
                case JumpCondition.NotEqual:
                    if (RegisterOne.Value != RegisterTwo.Value)
                    {
                        program.Jump(Label);
                    }

                    break;
            }
        }

        public void Dump()
        {
            switch (Condition)
            {
                case JumpCondition.LessThan:
                    Console.WriteLine($"JLT {RegisterOne.Name} {RegisterTwo.Name} {Label}");

                    break;
                case JumpCondition.LessThanOrEqual:
                    Console.WriteLine($"JLE {RegisterOne.Name} {RegisterTwo.Name} {Label}");

                    break;
                case JumpCondition.Equal:
                    Console.WriteLine($"JEQ {RegisterOne.Name} {RegisterTwo.Name} {Label}");

                    break;
                case JumpCondition.GreaterThanOrEqual:
                    Console.WriteLine($"JGE {RegisterOne.Name} {RegisterTwo.Name} {Label}");

                    break;
                case JumpCondition.GreaterThan:
                    Console.WriteLine($"JGT {RegisterOne.Name} {RegisterTwo.Name} {Label}");

                    break;
                case JumpCondition.NotEqual:
                    Console.WriteLine($"JNE {RegisterOne.Name} {RegisterTwo.Name} {Label}");

                    break;
                default:
                    break;
            }
        }
    }
}
using System;

namespace VirtualMachine.Commands
{
    public class OutCommand : ICommand
    {
        private Register Register { get; set; }
        private string StringKey { get; set; }

        private bool UsesRegister { get; set; }



        public OutCommand(Register register)
        {
            Register = register;
            UsesRegister = true;
        }

        public OutCommand(string stringKey)
        {
            StringKey = stringKey;
            UsesRegister = false;
        }

        public void Execute(ProgramFile program)
        {
            if (UsesRegister)
            {
                Console.WriteLine(Register.Value);
            }
            else
            {
                Console.WriteLine(program.Strings[StringKey]);
            }
        }
    }
}
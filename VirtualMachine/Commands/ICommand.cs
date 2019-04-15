namespace VirtualMachine.Commands
{
    public interface ICommand
    {
        void Execute(ProgramFile program);
    }
}
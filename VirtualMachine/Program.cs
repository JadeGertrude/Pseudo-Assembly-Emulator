namespace VirtualMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Processor processor = new Processor();
            processor.UseDefaultRegisters();
            processor.DoFile("program.vmc");
        }
    }
}
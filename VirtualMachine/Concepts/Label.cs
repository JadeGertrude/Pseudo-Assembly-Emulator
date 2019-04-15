namespace VirtualMachine
{
    public class Label
    {
        public string Name { get; set; }

        public int Index { get; set; }



        public Label()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
namespace PIF1006_tp1
{
    public class Transition
    {
        public char Input { get; set; }
        public State TransiteTo { get; set; }

        public Transition(char input, State transiteTo)
        {
            Input = input;
            TransiteTo = transiteTo;
        }

        // Au besoin, vous pouvez ajouter du code ici, au min. de redéfinir ToString()
        public override string ToString()
        {
            string temp = this.TransiteTo.Name + " | " + (this.Input - 48);
            return temp;
        }    
    }
}

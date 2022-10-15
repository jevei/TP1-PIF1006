using System.Collections.Generic;

namespace PIF1006_tp1
{
    public class State
    {
        public bool IsFinal {get; set;}
        public string Name { get; private set; }
        public List<Transition> Transitions { get; private set; }

        public State (string name, bool isFinal)
        {
            Name = name;
            IsFinal = isFinal;
            Transitions = new List<Transition>();
        }
        public State()
        { 

        }

        // Au besoin, vous pouvez ajouter du code ici, au min. de redéfinir ToString()
        public override string ToString()
        {
            string temp = this.Name + " | ";
            if (this.IsFinal == true)
            {
                temp += 1 + " | ";
            }
            else
            {
                temp += 0 + " | ";
            }
            return temp;
        }    
    }
}

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PIF1006_tp1
{
    public class Automate
    {
        private ObservableCollection<State> StateList { get; set; }
        private ObservableCollection<Transition> TransitionList { get; set; }
        public State InitialState { get; set; }
        public State CurrentState { get; set; }

        public Automate(State initialState)
        {
            InitialState = initialState;
            Reset();
        }
        public Automate()
        {
            StateList = new ObservableCollection<State>();
            TransitionList = new ObservableCollection<Transition>();
        }

        public void LoadFromFile(string filePath)
        {
            // Vous devez pouvoir charger à partir d'un fichier quelconque.  Cela peut être un fichier XML, JSON, texte, binaire, ...
            // P.ex. avec un fichier texte, vous pouvoir balayer ligne par ligne et interprété en séparant chaque ligne en un tableau de strings
            // dont le premier représente l'action, et la suite les arguments. L'équivalent de l'automate décrit manuellement dans la classe
            // Program pourrait être:
            //  state s0 0
            //  state s1 0
            //  state s2 1
            //  state s3 0
            //  transition s0 0 s1
            //  transition s1 0 s0
            //  transition s1 1 s2
            //  transition s2 1 s2
            //  transition s2 0 s3
            //  transition s3 1 s1
            // Dans une boucle, on prend les lignes une par une et si le 1er terme est "state", on prend les arguments et on crée un état du même nom
            // et on l'ajoute à une liste d'état
            // Si c'est "transition" on cherche dans la liste d'état l'état qui a le nom en 1er argument et on ajoute la transition avec les 2 autres
            // arguments à sa liste
            // Etc.
            //
            // Considérez que:
            //   - S'il y a d'autres termes, les lignes pourraient être ignorées;
            //   - Si l'état n'est pas trouvé dans la liste (p.ex. l'état est référencé mais n'existe pas (encore)), la transition est ignorée
            
            //structure des données dans le fichier doit être
            //pour state
            //state name isFinal
            //pour transition
            //transition state input transiteTo
            //comme dans l'exemple plus haut
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                string word = "";
                bool found = false;
                for (int i = 0; found != true; i++)
                {
                    if (line.ElementAt<char>(i) != ' ')
                    {
                        word += line.ElementAt<char>(i);
                    }
                    else
                    {
                        if (word == "state")
                        {
                            StateList.Add(EvaluateState(line));
                            found = true;
                        }
                        else if (word == "transition")
                        {
                            if (EvaluateTransition(line) != null)
                            {
                                Transition returnTransition = EvaluateTransition(line);
                                TransitionList.Add(returnTransition);
                                string tState = "";
                                for (int j = 11; j != line.Length; j++)
                                {
                                    if (line.ElementAt<char>(j) != ' ')
                                    {
                                        tState += line.ElementAt<char>(j);
                                    }
                                    else
                                    {
                                        j = line.Length - 1;
                                    }
                                }
                                StateList.ElementAt(StateList.IndexOf(StateList.Where(a => a.Name == tState).ToArray()[0])).Transitions.Add(returnTransition);
                            }
                            found = true;
                        }
                    }
                }
            }
        }

        private Transition EvaluateTransition(string line)
        {
            string tState = "";
            char valeur = ' ';
            string transit = "";
            for (int i = 11; i != line.Length; i++)
            {
                if (line.ElementAt<char>(i) != ' ' && valeur == ' ')
                {
                    tState += line.ElementAt<char>(i);
                }
                else if (valeur != ' ')
                {
                    transit += line.ElementAt<char>(i);
                }
                else
                {
                    i++;
                    valeur = line.ElementAt<char>(i);
                    i++;
                }
            }
            if (StateList.Count != 0)
            {
                if (StateList.Where(a => a.Name == transit) != null && StateList.Where(a => a.Name == tState) != null)
                {
                    State temp = StateList.ElementAt(StateList.IndexOf(StateList.Where(a => a.Name == transit).ToArray()[0]));
                    Transition returnTransition = new Transition(valeur, temp);
                    return returnTransition;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private State EvaluateState(string line)
        {
            string nom = "";
            bool final = false;
            for (int i = 6; i != line.Length; i++)
            {
                if (line.ElementAt<char>(i) != ' ')
                {
                    nom += line.ElementAt<char>(i);
                }
                else
                {
                    i++;
                    if (line.ElementAt<char>(i) - 48 == 1)
                    {
                        final = true;
                    }
                    else
                    {
                        final = false;
                    }
                }
            }
            return new State(nom, final);
        }

        public bool Validate(string input)
        {
            bool isValid = true;
            Reset();
            // Vous devez transformer l'input en une liste / un tableau de caractères (char) et les lire un par un;
            // L'automate doit maintenant à jour son "CurrentState" en suivant les transitions et en respectant l'input.
            // Considérez que l'automate est déterministe et que même si dans les faits on aurait pu mettre plusieurs
            // transitions possibles pour un état et un input donné, le 1er trouvé dans la liste est le chemin emprunté.
            // Si aucune transition n'est trouvé pour un état courant et l'input donné, cela doit retourner faux;
            // Si tous les caractères ont été pris en compte, on vérifie si l'état courant est final ou non et on retourne
            // vrai ou faux selon.
            char[] zeroOneList = input.ToCharArray();

            return isValid;
        }

        public override string ToString()
        {
            // Vous devez modifier cette partie de sorte à retourner un équivalent string qui décrit tous les états et
            // la table de transitions de l'automate.
            string temp = "Table de transition\nCurrentState | IsFinal | NextState | Input\n";
            for (int i = 0; i != StateList.Count; i++)
            {
                for (int j = 0; j != StateList.ElementAt(i).Transitions.Count; j++)
                {
                    temp += StateList.ElementAt(i).Name + " | ";
                    if (StateList.ElementAt(i).IsFinal == true)
                    {
                        temp += 1 + " | ";
                    }
                    else
                    {
                        temp += 0 + " | ";
                    }
                    temp += StateList.ElementAt(i).Transitions.ElementAt(j).TransiteTo.Name + " | " + (StateList.ElementAt(i).Transitions.ElementAt(j).Input - 48) + "\n";
                }
            }
            return temp; 
        }

        public void Reset() => CurrentState = InitialState;
    }
}

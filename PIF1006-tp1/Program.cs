using System;
using System.Diagnostics;
using System.IO;

namespace PIF1006_tp1
{
    public class Program
    {
        static void Main(string[] args)
        {
 
            //-- Ceci est un exemple manuel de ce qui devrait fonctionner --
            State s0 = new State("s0", false);
            State s1 = new State("s1", false);
            State s2 = new State("s2", true);
            State s3 = new State("s3", false);
            s0.Transitions.Add(new Transition('0', s1));
            s1.Transitions.Add(new Transition('0', s0));
            s1.Transitions.Add(new Transition('1', s2));
            s2.Transitions.Add(new Transition('1', s2));
            s2.Transitions.Add(new Transition('0', s3));
            s3.Transitions.Add(new Transition('1', s1));

            // Dans cet exemple uniquement, on permet au constructuer d'accueilir un état initial
            // (qui par référence "transporte" tout l'automate en soi)
            Automate automate = new Automate(s0);

            // On doit pouvoir ensuite appeler une méthode qui permet de valider un input ou non
            bool isValid = automate.Validate("011000");

            // Et ainsi de suite...

            //---------------------------------------------------------------------------------------------------------------------------
            // Ci-haut est un exemple.  Vous devez plutôt faire un menu avec des options/interactions utilisateurs pour:
            //      (1) Charger un fichier en spécifiant le chemin (relatif) du fichier.  Vous pouvez charger un fichier par défaut au démarrage
            //      (2) La liste des états et la liste des transitions doivent pouvoir être affichées proprement;
            //      (3) Soumettre un input en tant que chaîne de 0 ou de 1 -> Assurez-vous que la chaine passée ne contient QUE ces caractères
            //          avant d'envoyer n'est pas obligatoire, mais cela ne doit pas faire planter de l'autre coté;  un message doit indiquer si
            //          c'est accepté ou rejeté;
            //      (4) Quitter l'application.
            string input;
            string defaultFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\default.txt\";
            do
            {
                Console.WriteLine("Bonjour,\nVeuillez choisir parmi les choix suivant\n1-Charger un fichier en spécifiant le chemin\n2-Afficher la liste des états et la liste des transitions\n3-Soumettre un input en tant que chaîne de 0 ou de 1\n4-Quitter");
                input = Console.ReadLine();
                Console.WriteLine(input);
            } while (input != "4");
        }
    }
}

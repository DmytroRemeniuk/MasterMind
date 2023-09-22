/*ETML
Auteur     : Dmytro Remeniuk
Date       : 08.09.2023
Description: un programme qui permets jouer au Mastermind.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //définition des valeurs (couleurs possibles, random pour générer des symboles aléatoires, valeur, entrée par l'utilisateur,
            //nombre d'essais souhaité, essais, la longueur d'une combinaison souhaitée et nombre des couleurs
            //l'idée de random a été prise là:https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
            var colours = "GYWRBMC";
            var random = new Random();
            string comb;
            int attempts;
            int tries;
            int combLength;
            int numColours;

            //boucle qui demande l'utilisateur s'il veux continuer
            do
            {
                //effacer tout ce qui est écrit pour la vue plus agréable
                Console.Clear();

                //affichage du texte bienvenue
                Console.WriteLine("Bienvenue sur MasterMind!");

                //entrer le nombre d'essais souhaité et le vérifier si c'est entre 1 et 30
                Console.WriteLine("Combien d'essais avez-vous besoin (1-30)?");
                tries = Convert.ToInt32(Console.ReadLine());
                while (tries <= 0 || tries > 30)
                {
                    Console.WriteLine("Entrez le nombre de 1 à 30:");
                    tries = Convert.ToInt32(Console.ReadLine());
                }

                //entrer la longueur d'une combinaison souhaitée et la vérifier si c'est entre 2 et 6
                Console.WriteLine("Quelle longueur d'une combinaison voulez-vous (2-6)?");
                combLength = Convert.ToInt32(Console.ReadLine());
                while (combLength < 2 || combLength > 6)
                {
                    Console.WriteLine("Entrez le nombre de 2 à 6:");
                    combLength = Convert.ToInt32(Console.ReadLine());
                }

                //
                Console.WriteLine("Combien de couleurs possibles souhaitez-vous avoir (2-7)?");
                numColours = Convert.ToInt32(Console.ReadLine());
                while (numColours < 2 || numColours > 7)
                {
                    Console.WriteLine("Entrez le nombre de 2 à 7");
                    numColours = Convert.ToInt32(Console.ReadLine());
                }


                if (numColours == 2)
                {
                    colours = "GY";
                }
                else if (numColours == 3)
                {
                    colours = "GYW";
                }
                else if (numColours == 4)
                {
                    colours = "GYWR";
                }
                else if (numColours == 5)
                {
                    colours = "GYWRB";
                }
                else if (numColours == 6)
                {
                    colours = "GYWRBM";
                }

                //effacer tout ce qui est écrit pour la vue plus agréable
                Console.Clear();

                Console.WriteLine("Devinez le code en " + combLength + " couleurs parmi " + colours + ". La répétition possible.");
                //tableau pour stocker la combinaison
                var stringColours = new char[combLength];

                //boucle qui se répète 4 fois pour que la variable "stringColours" prenne des valeurs de "colours"
                for (int i = 0; i < stringColours.Length; i++)
                {
                    stringColours[i] = colours[random.Next(colours.Length)];
                }

                //pour la vérification simple
                Console.WriteLine(stringColours);

                //boucle qui donne que 10 essais
                for (attempts = 1; attempts <= tries; attempts++)
                {

                    //des variables pour verifier bonne/mauvaise position
                    int correctColors = 0;
                    int correctPositions = 0;

                    Console.WriteLine("Essai " + attempts);
                    //début du jeu
                    Console.WriteLine("Entrez la combinaison de " + combLength + " couleurs parmi " + colours + ":");
                    comb = Console.ReadLine().ToUpper();

                    //vérification si la quantité des couleurs est bonne et si les couleurs sont corrects
                    while (comb.Length != combLength || comb.Any(c => !colours.Contains(c)))//Any - chatGPT
                    {
                        Console.WriteLine("Entrez la combinaison de " + combLength + " couleurs parmi " + colours + ":");
                        comb = Console.ReadLine().ToUpper();
                    }

                    //créer une liste avec des couleurs utilisées (l'idée: chatGPT)
                    var usedColours = new List<char>(stringColours);

                    //verifier chaque élément du tableau si c'est une bonne position
                    for (int i = 0; i < comb.Length; i++)
                    {
                        if (comb[i] == stringColours[i])
                        {
                            correctPositions++;
                            usedColours[i] = ' ';//marquer la couleur utilisée (l'idée: chatGPT)
                        }
                    }

                    //verifier chaque élément du tableau si c'est une mauvaise position
                    for (int i = 0; i < comb.Length; i++)
                    {
                        if (comb[i] != stringColours[i] && usedColours.Contains(comb[i]))
                        {
                            correctColors++;
                            usedColours[usedColours.IndexOf(comb[i])] = ' ';//marquer la couleur utilisée (l'idée: chatGPT)
                        }
                    }

                    //vérification d'une réponse et donner à la variable une autre valeur
                    if (correctPositions == combLength)
                    {
                        attempts = 100;
                    }

                    //afficher le resultat d'une réponse
                    Console.WriteLine("Bonnes positions: " + correctPositions);
                    Console.WriteLine("Mauvaises positions: " + correctColors);
                }
                //affichage des messages "gagné/perdu"
                if (attempts == 101)
                {
                    Console.WriteLine("Vous avez gagné, voulez-vous réessayer?(tapez \"y\" si vous êtes d'accord)");
                }
                else
                {
                    Console.WriteLine("Vous avez perdu, la bonne combinaison était " + new string(stringColours) + ". Voulez-vous réessayer?(tapez \"y\" si vous êtes d'accord, tous les autres caractères pour quiter)");
                }
            } while (Console.ReadLine().ToLower() == "y");
        }
    }
}
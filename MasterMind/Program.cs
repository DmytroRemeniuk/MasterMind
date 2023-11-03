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
            //l'idée de random a été prise là:https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
            var colours = "GYWRBMC";
            var random = new Random();
            string guess;
            //nombre d'essais souhaité, essais, la longueur d'une combinaison souhaitée, nombre des couleurs et un booléen pour vérifier si les couleurs entrées sont justes
            int attempts = 1;
            int tries;
            int combLength;
            int numColours;
            const int MAX_TRIES = 30;
            const int MAX_COMB_LENGTH = 6;
            const int MAX_NUM_COLOURS = 7;
            const int MIN_TRIES = 2;
            const int MIN_COMB_LENGTH = 2;
            const int MIN_NUM_COLOURS = 2;
            //choix du mode
            string mode;
            bool ok = true;
            //continuer le jeu
            string affirmation;
            //combinaison créée
            char[] stringColours = new char[1];

            //fonction pour les même partie de code pour deux modes
            void Combination()
            {
                //pour différencier parties différentes
                Console.WriteLine("***********************");

                //entrer le nombre d'essais souhaité et le vérifier si c'est agréable
                Console.WriteLine("Combien d'essais avez-vous besoin (" + MIN_TRIES + "-" + MAX_TRIES + ")?");
                tries = Convert.ToInt32(Console.ReadLine());
                while (tries < MIN_TRIES || tries > MAX_TRIES)
                {
                    Console.WriteLine("Entrez le nombre de " + MIN_TRIES + " à " + MAX_TRIES + ":");
                    tries = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("***********************");

                //entrer la longueur d'une combinaison souhaitée et la vérifier si c'est agréable
                Console.WriteLine("Quelle longueur d'une combinaison voulez-vous (" + MIN_COMB_LENGTH + "-" + MAX_COMB_LENGTH + ")?");
                combLength = Convert.ToInt32(Console.ReadLine());
                while (combLength < MIN_COMB_LENGTH || combLength > MAX_COMB_LENGTH)
                {
                    Console.WriteLine("Entrez le nombre de " + MIN_COMB_LENGTH + " à " + MAX_COMB_LENGTH + ":");
                    combLength = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("***********************");

                //entrer le nombre des couleurs souhaité et le vérifier si c'est agréable
                Console.WriteLine("Combien de couleurs possibles souhaitez-vous avoir (" + MIN_NUM_COLOURS + "-" + MAX_NUM_COLOURS + ")?");
                numColours = Convert.ToInt32(Console.ReadLine());
                while (numColours < MIN_NUM_COLOURS || numColours > MAX_NUM_COLOURS)
                {
                    Console.WriteLine("Entrez le nombre de " + MIN_NUM_COLOURS + " à " + MAX_NUM_COLOURS + ":");
                    numColours = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("***********************");

                //changer le tableau pour le nombre des couleurs choisi
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
                stringColours = new char[combLength];

                //boucle qui se répète 4 fois pour que la variable "stringColours" prenne des valeurs de "colours"
                for (int i = 0; i < stringColours.Length; i++)
                {
                    stringColours[i] = colours[random.Next(colours.Length)];
                }
            }
            //boucle qui demande l'utilisateur s'il veux continuer
            do
            {
                //effacer tout ce qui est écrit pour la vue plus agréable
                Console.Clear();

                //affichage du texte bienvenue
                Console.WriteLine("Bienvenue sur MasterMind!");

                //demander de choisir le mode 
                Console.WriteLine("Choisissez l'option:\n" +
                    "[f] pour le mode facile \n" +
                    "[d] pour le mode difficile\n" +
                    "[r] pour affichez les règles\n" +
                    "[q] pour quitter");
                mode = Console.ReadLine().ToLower();
                while (mode != "f" && mode != "d" && mode != "r" && mode != "q")
                {
                    Console.WriteLine("Entrez \"f\", \"d\", \"r\" ou \"q\"");
                    mode = Console.ReadLine();
                }

                //vérifications de la réponse
                if (mode == "d")
                {
                    //début du jeu
                    Combination();

                    //boucle qui donne que 10 essais
                    for (attempts = 1; attempts <= tries; attempts++)
                    {

                        //des variables pour verifier bonne/mauvaise position
                        int correctColors = 0;
                        int correctPositions = 0;

                        Console.WriteLine("Essai " + attempts);
                        //début du jeu
                        Console.WriteLine("Entrez la combinaison de " + combLength + " couleurs parmi " + colours + ":");
                        guess = Console.ReadLine().ToUpper();

                        //vérification si la quantité des couleurs est bonne et si les couleurs sont corrects
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (!colours.Contains(guess[i]))
                            {
                                ok = false;
                            }
                            else
                            {
                                ok = true;
                            }
                        }
                        while (guess.Length != combLength || ok == false)
                        {
                            Console.WriteLine("Entrez la combinaison de " + combLength + " couleurs parmi " + colours + ":");
                            guess = Console.ReadLine().ToUpper();
                            for (int i = 0; i < guess.Length; i++)
                            {
                                if (!colours.Contains(guess[i]))
                                {
                                    ok = false;
                                }
                                else
                                {
                                    ok = true;
                                }
                            }
                        }

                        //créer une liste avec des couleurs utilisées (l'idée: chatGPT)
                        var usedColours = new List<char>(stringColours);

                        //verifier chaque élément du tableau si c'est une bonne position
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (guess[i] == stringColours[i])
                            {
                                correctPositions++;
                                usedColours[i] = ' ';//marquer la couleur utilisée (l'idée: chatGPT)
                            }
                        }

                        //verifier chaque élément du tableau si c'est une mauvaise position
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (guess[i] != stringColours[i] && usedColours.Contains(guess[i]))
                            {
                                correctColors++;
                                usedColours[usedColours.IndexOf(guess[i])] = ' ';//marquer la couleur utilisée (l'idée: chatGPT)
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
                        Console.WriteLine("*************************");
                    }
                    //affichage des messages "gagné/perdu"
                    if (attempts == 101)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("*********************************************************************************************\n" +
                                          "Vous avez gagné, voulez-vous réessayer?(tapez \"y\" si vous êtes d'accord, \"q\" pour quitter)\n" +
                                          "*********************************************************************************************");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("*********************************************************************************************\n" +
                                          "Vous avez perdu, la bonne combinaison était " + new string(stringColours) + ". Voulez-vous réessayer?(tapez \"y\" si vous êtes d'accord, \"q\" pour quitter)\n" +
                                          "*********************************************************************************************");
                        Console.ResetColor();
                    }
                }
                else if (mode == "f")
                {
                    //début du jeu
                    Combination();

                    //tableau pour éviter des doublons
                    var noRep = new List<string>();

                    //boucle qui donne que 10 essais
                    for (attempts = 1; attempts <= tries; attempts++)
                    {

                        //des variables pour verifier bonne position
                        int correctPositions = 0;

                        Console.WriteLine("Essai " + attempts);
                        //début du jeu
                        Console.WriteLine("Entrez la combinaison de " + combLength + " couleurs parmi " + colours + ":");
                        guess = Console.ReadLine().ToUpper();

                        //vérification si la quantité des couleurs est bonne et si les couleurs sont corrects
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (!colours.Contains(guess[i]))
                            {
                                ok = false;
                            }
                            else
                            {
                                ok = true;
                            }
                        }
                        while (guess.Length != combLength || ok == false)
                        {
                            Console.WriteLine("Entrez la combinaison de " + combLength + " couleurs parmi " + colours + ":");
                            guess = Console.ReadLine().ToUpper();
                            for (int i = 0; i < guess.Length; i++)
                            {
                                if (!colours.Contains(guess[i]))
                                {
                                    ok = false;
                                }
                                else
                                {
                                    ok = true;
                                }
                            }
                        }

                        for (int i = 0; i < noRep.Count(); i++)
                        {
                            if (noRep[i].Contains(guess))
                            {
                                Console.WriteLine("Vous avez déja utilisé cette combinaison, réessayez:");
                                guess = Console.ReadLine().ToUpper();
                                while (noRep[i].Contains(guess))
                                {
                                    Console.WriteLine("Vous avez déja utilisé cette combinaison, réessayez:");
                                    guess = Console.ReadLine().ToUpper();
                                }
                                break;
                            }
                        }

                        noRep.Add(guess);//l'idée - Sébastien

                        //créer une liste avec des couleurs utilisées (l'idée: chatGPT)
                        var usedColours = new List<char>(stringColours);

                        //créer le tableau pour stocker les bonnes réponses
                        char[] result = new char[combLength];

                        //remplir ce tableau
                        for (int i = 0; i < result.Length; i++)
                        {
                            result[i] = '_';
                        }
                        //verifier chaque élément du tableau si c'est une bonne position
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (guess[i] == stringColours[i])
                            {
                                correctPositions++;
                                usedColours[i] = ' ';//marquer la couleur utilisée (l'idée: chatGPT)
                                result[i] = guess[i];
                            }
                        }

                        //verifier chaque élément du tableau si c'est une mauvaise position
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (usedColours.Contains(guess[i]) && result[i] == '_')
                            {
                                result[i] = '$';
                                usedColours[usedColours.IndexOf(guess[i])] = ' ';//marquer la couleur utilisée (l'idée: chatGPT)
                            }
                        }
                        //vérification d'une réponse et donner à la variable une autre valeur
                        if (correctPositions == combLength)
                        {
                            attempts = 100;
                        }


                        //afficher le resultat d'une réponse
                        Console.Write("Resultat: ");
                        for (int i = 0; i < result.Length; i++)
                        {
                            if (result[i] == 'G' || result[i] == '$' && guess[i] == 'G')
                            {
                                Console.ForegroundColor = ConsoleColor.Green; //librairie - https://stackoverflow.com/questions/2743260/is-it-possible-to-write-to-the-console-in-colour-in-net
                                Console.Write(result[i]);
                                Console.ResetColor();
                            }
                            else if (result[i] == 'Y' || result[i] == '$' && guess[i] == 'Y')
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(result[i]);
                                Console.ResetColor();
                            }
                            else if (result[i] == 'R' || result[i] == '$' && guess[i] == 'R')
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(result[i]);
                                Console.ResetColor();
                            }
                            else if (result[i] == 'B' || result[i] == '$' && guess[i] == 'B')
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(result[i]);
                                Console.ResetColor();
                            }
                            else if (result[i] == 'M' || result[i] == '$' && guess[i] == 'M')
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(result[i]);
                                Console.ResetColor();
                            }
                            else if (result[i] == 'C' || result[i] == '$' && guess[i] == 'C')
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write(result[i]);
                                Console.ResetColor();
                            }
                            else if (result[i] == 'W' || result[i] == '$' && guess[i] == 'W')
                            {
                                Console.Write(result[i]);
                            }
                            else if (result[i] == '_')
                            {
                                Console.Write(result[i]);
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("***********************");
                    }
                    //affichage des messages "gagné/perdu"
                    if (attempts == 101)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("***********************************************************************************************\n" +
                                          "Vous avez gagné, voulez-vous réessayer?(tapez \"y\" si vous êtes d'accord, \"q\" pour quitter)\n" +
                                          "***********************************************************************************************");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("*******************************************************************************************************\n" +
                                          "Vous avez perdu, la bonne combinaison était " + new string(stringColours) + ". Voulez-vous réessayer?(tapez \"y\" si vous êtes d'accord, \"q\" pour quitter)\n" +
                                          "*******************************************************************************************************");
                        Console.ResetColor();
                    }
                }
                //affichage des règles
                else if (mode == "r")
                {
                    Console.WriteLine("************************************************************************************************************\n" +
                                      "Règles: dans ce jeu vous allez deviner la combinaison des couleurs qui a été créée par le programme. \nLes nombres des bonnes et mauvaises positions" +
                                      "vont être affichés. Choisissez le mode du jeu et puis \nla difficulté (qui change le nombre d'essais, longueur de la combinaison et nombre des couleurs possibles.)\n" +
                                      "La différence entre deux modes est l'affichage (\"Bonnes positions: 1; Mauvaises positions : 2\" pour le mode difficile \net \"_G$_\" pour le mode facile)\n" +
                                      "************************************************************************************************************");
                    Console.WriteLine();
                    Console.WriteLine("Tapez \"y\" pour continuer, \"q\" pour quitter:");
                }
                //quitter
                else if (mode == "q")
                {
                    break;
                }
                //entrer l'affirmation de continuer
                affirmation = Console.ReadLine().ToLower();
                while (affirmation != "y" && affirmation != "q")
                {
                    Console.WriteLine("Tapez \"y\" pour continuer, \"q\" pour quitter:");
                    affirmation = Console.ReadLine().ToLower();
                }
            } while (affirmation == "y");
        }
    }
}
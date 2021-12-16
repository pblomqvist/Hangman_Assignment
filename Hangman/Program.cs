using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                WriteLine("\nWhat would you like to do?" +
                    "\n0: Exit" +
                    "\n1: Play Hangman");
                int action = GetIntFromUser();

                switch (action)
                {
                    case 0:
                        running = false;
                        break;

                    case 1:
                        Console.WriteLine("Let's play Hangman!");
                        Hangman();
                        break;

                    default:
                        WriteLine("Something went wrong");
                        break;
                }

            }

            static int GetIntFromUser()
            {
                int number = 0;
                bool success = false;

                do
                {

                    try
                    {

                        number = int.Parse(ReadLine());
                        success = true;

                    }
                    catch (OverflowException)
                    {
                        WriteLine("Your value is too big");
                    }
                    catch (ArgumentNullException)
                    {
                        WriteLine("Could not parse, value was null");
                    }
                    catch (FormatException error)
                    {
                        WriteLine(error.Message);
                        WriteLine("Wrong format");
                    }

                } while (!success);
                {
                    return number;
                }

            }

            static string GetRandomWord()
            {
                var random = new Random();
                string[] randomWords = { 
                    "showman", "boat", "decide", "stumble", "endure", "log", "cheese", "egg", 
                    "mango", "banana", "bees", "hello", "world" 
                };
                int index = random.Next(0, randomWords.Length);
                string result = randomWords[index];
                return result;
            }

            static string GetUserInput()
            {
                
                string playerGuess = Console.ReadLine();
                bool guessTest = playerGuess.All(Char.IsLetter);
                string errorMsg = "Cannot contain number, try single letter or entire word";


                while (guessTest == false)
                {
                    WriteLine(errorMsg);
                    playerGuess = ReadLine();
                }

                if (playerGuess.Length != 1)
                {
                    playerGuess = playerGuess.ToUpper();

                } else
                {
                  
                    playerGuess = playerGuess.ToUpper();
                }
                
                return playerGuess;

            }

            static void Hangman()
            {
                string secretWord = GetRandomWord();
                secretWord = secretWord.ToUpper();
                
                WriteLine("Secret word is " + secretWord);
                
                StringBuilder incorrectGuess = new StringBuilder("Wrong guesses: ");
                
                int lives = 10;
                int counter = -1;
                int wordLength = secretWord.Length;
                char[] secretArray = secretWord.ToCharArray();
                char[] printArray = new char[wordLength];
                char[] guessedLetters = new char[26];
                int numberStore = 0;
                bool victory = false;
                bool gameOn = true;

                foreach (char letter in printArray)
                {
                    counter++;
                    printArray[counter] = '_';
                }

                while (gameOn)
                {
                    counter = -1;
                    string printProgress = String.Concat(printArray);
                    bool letterFound = false;
                    char playerChar = ' ';

                    if (lives == 1)
                    {
                        gameOn = false;
                    }

                    if (printProgress == secretWord)
                    {
                        victory = true;
                        gameOn = false;
                        letterFound = true;
                    }


                    Console.WriteLine("current progress: " + printProgress);
                    Console.Write("\n\n");

                    Console.Write("Guess one letter or entire word: ");
                    string playerGuess = GetUserInput();

                    
                    if (playerGuess.Length == 1)
                    {
                       playerChar = Convert.ToChar(playerGuess);
                    } else
                    {
                        if (playerGuess == secretWord)
                        {
                            victory = true;
                            gameOn = false;
                            letterFound = true;
                        }

                    }

                    if (!guessedLetters.Contains(playerChar))
                    {

                        guessedLetters[numberStore] = playerChar;
                        numberStore++;

                        foreach (char letter in secretArray)
                        {
                            counter++;
                            if (letter == playerChar)
                            {
                                printArray[counter] = playerChar;
                                letterFound = true;
                            }
                            
                        }


                        if (letterFound)
                        {
                            Console.WriteLine("Found {0}!", playerGuess);
                              
                        }
                        else
                        {
                            Console.WriteLine("{0} is not right, try again.", playerGuess);
                            incorrectGuess.AppendFormat("{0:c}, ", playerGuess);
                            lives--;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You already guessed {0}", playerGuess);
                    }

                    WriteLine("\n" + incorrectGuess);
                    WriteLine("Lives remaining: {0}\n", lives);


                }

                if (victory)
                {
                    Console.WriteLine("The word was: {0}", secretWord);
                    Console.WriteLine("\n\nCongrats, you won!");
                }
                else
                {
                    Console.WriteLine("The word was: {0}", secretWord);
                    Console.WriteLine("\n\nSorry, you lost!");
                    
                }
            }


        }
    }
}

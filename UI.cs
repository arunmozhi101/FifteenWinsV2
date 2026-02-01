namespace FifteenWinsV2;

public class UI
{
    public static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to FifteenWins Version2 Game!");
        Console.Clear();
    }
    public static string GetGridDimensions()
    {
        string unparsedGridDimension;
        
        Console.WriteLine("Do you want to play FifteenWins using a 3x3 grid? (Y/N) : ");
        string response = Console.ReadLine().ToLower();
        if (response == "y")
        {
            Console.WriteLine("Okay then, we play with a 3x3 grid.");
            Console.Clear();
            return "3,3";
        }

        while (true)
        {
            Console.WriteLine($"Please enter the grid dimension in either of these 2 formats -> rows x columns or rows, columns : ");
            unparsedGridDimension = Console.ReadLine();

            if (unparsedGridDimension is not null)
            {
                break;
            }
            else
            {
                Console.WriteLine("Error: Incorrect grid dimension. Please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        return unparsedGridDimension;
    }
    
    public static int GetTargetNumber()
    {
        int targetNumber = 15;
        
        Console.WriteLine("Do you want to play FifteenWins with Target Number as 15? (Y/N) : ");
        string response = Console.ReadLine().ToLower();
        if (response == "y")
        {
            Console.WriteLine("Okay then, we play with Target Number as 15.");
            Console.Clear();
            return targetNumber;
        }
        while (true)
        {
            Console.WriteLine($"Please enter the Target Number : ");
            string unparsedTargetNumber = Console.ReadLine();

            if (int.TryParse(unparsedTargetNumber, out targetNumber))
            {
                Console.WriteLine($"Okay then, we play with Target Number as {targetNumber}).");
                Console.Clear();
                return targetNumber;
            }
            else
            {
                Console.WriteLine("Error: Incorrect grid dimension. Please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
    
    public static int GetNumberOfPlayers()
    {
        int numberOfPlayers;

        while (true)
        {
            Console.WriteLine("How many Players are playing?");
            string unparsedNumberOfPlayers = Console.ReadLine();

            if (int.TryParse(unparsedNumberOfPlayers, out numberOfPlayers))
            {
                if (numberOfPlayers <= 1 || numberOfPlayers > Constants.MAX_PLAYERS)
                {
                    Console.WriteLine("Error: Incorrect input for number of players. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                else
                {
                    Console.WriteLine("Excellent!");
                    Console.WriteLine("Let's play!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Error: Incorrect input for number of players. Please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        
        Console.WriteLine($"This game is played by {numberOfPlayers} players. Each player gets a chance to enter his number and position when his turn comes.");
        return numberOfPlayers;
    }
    private static void DisplayGrid(int[,] grid, int numberOfRows, int numberOfColumns)
    {
        string roofPattern = "";
        
        for (int i = 0; i < numberOfColumns; i++)
        {
            roofPattern += "+---";
        }
        roofPattern += "+";

        for (int i = 0; i < numberOfRows; i++)
        {
            Console.WriteLine(roofPattern);
            for (int j = 0; j < numberOfColumns; j++)
            {
                Console.Write($"| {grid[i, j]} ");
            }
            Console.Write($"|");
            Console.WriteLine();
        }
        Console.WriteLine(roofPattern);
    }
    
    public static (string unparsedNumberEntered, string unparsedGridPosition) GetPlayerInput(int[,] grid, int numberOfRows, int numberOfColumns, string playerName)
    {
        string unparsedNumberEntered;
        string unparsedGridPosition;
        
        while (true)
        {
            DisplayGrid(grid, numberOfRows, numberOfColumns);
            Console.Write($"{playerName}'s turn : ");
            Console.WriteLine($"Please enter a number between 1 and 9 : ");
            unparsedNumberEntered = Console.ReadLine();
            Console.WriteLine($"Please enter the position in either of these 2 formats -> rows x columns or rows, columns : ");
            unparsedGridPosition = Console.ReadLine();

            if (unparsedNumberEntered is not null && unparsedGridPosition is not null)
            {
                break;
            }
        }
        
        return (unparsedNumberEntered: unparsedNumberEntered, unparsedGridPosition: unparsedGridPosition);
    }
    
        public static void PrintIncorrectDimensionsEnteredError()
    {
        Console.WriteLine("Error: Incorrect dimensions input. Please try again.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }
    public static void PrintIncorrectNumberEnteredError()
    {
        Console.WriteLine("Error: Incorrect Number entered. Please try again.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }
    public static void PrintNumberAlreadyEnteredError()
    {
        Console.WriteLine("Error: Number already entered, please enter another number. Try again.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void PrintIncorrectIntError()
    {
        Console.WriteLine("Error: Incorrect input. Expecting a integer number. Please try again.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void PrintGridPositionUsedAlreadyError()
    {
        Console.WriteLine("Error: Position used already, please use another position. Try again.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    public static void DisplayWinner(int player)
    {
        Console.Clear();
        Console.WriteLine($"Congratulations! Player{player} wins.");
    }
    public static void DisplayNoWinner()
    {
        Console.Clear();
        Console.WriteLine("Unfortunately, the total turns is exhausted. No one wins.");
    }

    public static bool DoYouWantToPlayAgain()
    {
        Console.WriteLine("Do you want to continue playing(Y/N)?");
        string response = Console.ReadLine().ToLower();
        if (response == "n")
        {
            Console.WriteLine("Thanks for playing FifteenWins Game!");
            Console.Clear();
            return false;
        }
        return true;
    }
    
}
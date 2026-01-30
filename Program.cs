namespace FifteenWinsV2
{
    class FifteenWinsProgram
    {
        static void Main()
        {
            while (true)
            {
                List<int> usedNumbersList = new List<int>();
                List<string> filledGridPositions = new List<string>();

                (int numberOfRows, int numberOfColumns) = UI.GetGridDimensions();
                int numberOfPlayersPlaying = UI.GetNumberOfPlayers();
                int targetNumber = UI.GetTargetNumber();
                
                int numberEntered;
                bool result = true;
                int totalTurns = 0;

                int[,] grid = new int[numberOfRows, numberOfColumns];

                while (true)
                {
                    bool gameOver = false;
                    for (int player = 1; player <= numberOfPlayersPlaying; player++)
                    {
                        while (true)
                        {
                            bool isNumberAlreadyUsed = true;
                            bool isNumberInValidRange = false;
                            
                            (string unparsedNumberEntered, string unparsedGridPosition) = UI.GetPlayerInput(grid, numberOfRows, numberOfColumns, $"Player{player}");
                            
                            var (enteredRowPosition, enteredColumnPosition, error) = Logic.ValidateAndParseGridPosition(unparsedGridPosition, numberOfRows, numberOfColumns, filledGridPositions);
                            if (error.HasValue)
                            {
                                switch(error.Value)
                                {
                                    case GridPositionError.AlreadyFilled:
                                        UI.PrintGridPositionUsedAlreadyError();
                                        break;
                                    case GridPositionError.OutOfBounds:
                                        UI.PrintIncorrectDimensionsEnteredError();
                                        break;
                                    case GridPositionError.InvalidFormat:
                                        UI.PrintIncorrectDimensionsEnteredError();
                                        break;
                                }
                                continue;
                            }
                            
                            (numberEntered, isNumberAlreadyUsed, isNumberInValidRange) = Logic.ValidateAndParseNumberEntered(unparsedNumberEntered, usedNumbersList);
                            if (numberEntered == -1)
                            {
                                UI.PrintIncorrectIntError();
                                continue;
                            }
                            if (isNumberAlreadyUsed)
                            {
                                UI.PrintNumberAlreadyEnteredError();
                                continue;
                            }
                            if (!isNumberInValidRange)
                            {
                                UI.PrintIncorrectNumberEnteredError();
                                continue;
                            }
                            
                            usedNumbersList.Add(numberEntered);
                            filledGridPositions.Add($"{enteredRowPosition}{enteredColumnPosition}");
                            grid[enteredRowPosition, enteredColumnPosition] = numberEntered;
                            break;
                        }//End Of Inside while loop
                        
                        //Check if game is over
                        result = Logic.HasPlayerWon(numberOfRows, numberOfColumns, targetNumber, grid);
                        
                        if (result)
                        {
                            UI.DisplayWinner(player);
                            gameOver =  true;
                            break;
                        }

                        totalTurns++;
                        if (totalTurns >= grid.Length && !result)
                        {
                            UI.DisplayNoWinner();
                            gameOver =  true;
                            break;
                        }
                    }
                    if (gameOver)
                    {
                        break;
                    }
                }
                
                if (!UI.DoYouWantToPlayAgain())
                {
                    break;
                }
            }//End of First while loop
        }
    }
};


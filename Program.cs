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
                            bool isGridPositionAlreadyFilled = true;
                            
                            (string unparsedNumberEntered, string unparsedGridPosition) = UI.GetPlayerInput(grid, numberOfRows, numberOfColumns, $"Player{player}");
                            
                            (int enteredRowPosition, int enteredColumnPosition, isGridPositionAlreadyFilled) = Logic.ParseGridPositions(unparsedGridPosition, numberOfRows, numberOfColumns, filledGridPositions);
                            if(isGridPositionAlreadyFilled)
                            {
                                UI.PrintGridPositionUsedAlreadyError();
                                continue;
                            }
                            if (enteredRowPosition == -1 || enteredColumnPosition == -1)
                            {
                                UI.PrintIncorrectDimensionsEnteredError();
                                continue;
                            }
                            
                            (numberEntered, isNumberAlreadyUsed, isNumberInValidRange) = Logic.ParseNumberEntered(unparsedNumberEntered, usedNumbersList);
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
                        bool gridRowWin = Logic.IsGridRowWin(numberOfRows, numberOfColumns, targetNumber, grid);
                        bool gridColumnWin = Logic.IsGridColumnWin(numberOfRows, numberOfColumns, targetNumber, grid);
                        bool gridDiagonalWin = Logic.IsDiagonalWin(numberOfRows, targetNumber, grid);
                        result = gridRowWin || gridColumnWin || gridDiagonalWin;
                        
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


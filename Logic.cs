namespace FifteenWinsV2;

public class Logic
{
    public static (int numberOfRows, int numberOfColumns, GridDimensionsError? error) ValidateAndParseGridDimensions(string unparsedGridDimensions)
    {
        string[] splitGridDimensions = unparsedGridDimensions.Split(Constants.delimiters,StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries | StringSplitOptions.None);

        if (splitGridDimensions.Length == Constants.TWO_DIMENSION)
        {
            if (int.TryParse(splitGridDimensions[0], out int numberOfRows) && int.TryParse(splitGridDimensions[1], out int numberOfColumns))
            {
                if (numberOfRows >= 3 && numberOfRows <= 9 && numberOfColumns >= 3 && numberOfColumns <= 9)
                {
                    return (numberOfRows: numberOfRows, numberOfColumns: numberOfColumns, null);
                }
            }
        }
        return (numberOfRows: Constants.INVALID_DIMENSION, numberOfColumns: Constants.INVALID_DIMENSION, GridDimensionsError.InvalidFormat);
    }
    public static (int enteredRowPosition, int enteredColumnPosition, GridPositionError? error) ValidateAndParseGridPosition(string unparsedGridPosition, int  numberOfRows, int numberOfColumns, List<string> filledGridPositions)
    {
        string[] splitGridPosition = unparsedGridPosition.Split(Constants.delimiters,StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries | StringSplitOptions.None);

        if (splitGridPosition.Length == Constants.TWO_DIMENSION)
        {
            if (int.TryParse(splitGridPosition[0], out int enteredRowPosition) && int.TryParse(splitGridPosition[1], out int enteredColumnPosition))
            {
                if (enteredRowPosition >= 0 && enteredRowPosition < numberOfRows && enteredColumnPosition >= 0 && enteredColumnPosition < numberOfColumns)
                {
                    if (!filledGridPositions.Contains($"{enteredRowPosition}{enteredColumnPosition}"))
                    {
                        return (enteredRowPosition, enteredColumnPosition, null);
                    }
                    else
                    {
                        return (enteredRowPosition: Constants.INVALID_POSITION, enteredColumnPosition: Constants.INVALID_POSITION, GridPositionError.AlreadyFilled);
                    }
                }
                else
                {
                    return (enteredRowPosition: Constants.INVALID_POSITION, enteredColumnPosition: Constants.INVALID_POSITION, GridPositionError.OutOfBounds);
                }
            }
        }
        return (enteredRowPosition: Constants.INVALID_POSITION, enteredColumnPosition: Constants.INVALID_POSITION, GridPositionError.InvalidFormat);
    }
    
    public static (int numberEntered, bool isNumberAlreadyUsed, bool isNumberInValidRange) ValidateAndParseNumberEntered(string unparsedNumberEntered, List<int> usedNumbersList)
    {
        bool isNumberAlreadyUsed = true;
        bool isNumberInValidRange = false;
        //Check if the data entered by the player is a number and not entered already.
        //And check if the position entered is also an integer and not entered already. 
        if (int.TryParse(unparsedNumberEntered, out int numberEntered))
        {
            //Check if the number is already entered. If not then fill the usedNumbersList[].
            if (usedNumbersList.Contains(numberEntered))
            {
                return (numberEntered: Constants.INVALID_NUMBER, isNumberAlreadyUsed: true,  isNumberInValidRange: true);
            }
            else
            {
                if (numberEntered < 1 | numberEntered > 9)
                {
                    return (numberEntered: Constants.INVALID_NUMBER, isNumberAlreadyUsed: false,  isNumberInValidRange: false);
                }

                return (numberEntered, isNumberAlreadyUsed: false, isNumberInValidRange: true);
            }
        }
        else
        {
            return (numberEntered: Constants.INVALID_NUMBER, isNumberAlreadyUsed: isNumberAlreadyUsed,  isNumberInValidRange: isNumberInValidRange);
        }
    }
    
    private static bool IsGridRowWin(int numberOfRows, int numberOfColumns, int targetNumber, int[,] grid)
    {
        //Check if the Grid rows add up to the target value
        for (int i = 0; i < numberOfRows; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < numberOfColumns; j++)
            {
                if (grid[i, j] != 0)
                {
                    rowSum += grid[i, j];
                }
                else
                {
                    // if one of the values in the row is still 0 then skip that row
                    rowSum = 0;
                    break;
                }
            }

            if (rowSum == targetNumber)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsGridColumnWin(int numberOfRows, int numberOfColumns, int targetNumber, int[,] grid)
    {
        //Check if the Grid Columns add up to the target value
        for (int j = 0; j < numberOfColumns; j++)
        {
            int columnSum = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                if (grid[i, j] != 0)
                {
                    columnSum += grid[i, j];
                }
                else
                {   
                    // if one of the values in the column is still 0 then skip that column
                    columnSum = 0;
                    break;
                }
                
            }

            if (columnSum == targetNumber)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsDiagonalWin(int numberOfRows, int targetNumber, int[,] grid)
    {
        //Diagonal Check
        int backwardDiagonalSum = 0;
        int forwardDiagonalSum = 0;
        for (int i = 0; i < numberOfRows; i++)
        {
            if (grid[i, i] != 0)
            {
                backwardDiagonalSum += grid[i, i];
            }
            else
            {
                // None of the values in the diagonal can be a 0
                backwardDiagonalSum = 0;
                break;
            }
        }
        
        for (int i = 0; i < numberOfRows; i++)
        {
            if (grid[i, (numberOfRows - 1) - i] != 0)
            {
                forwardDiagonalSum += grid[i, (numberOfRows - 1) - i];
            }
            else
            {
                // None of the values in the diagonal can be a 0
                forwardDiagonalSum = 0;
                break;
            }
        }

        if (backwardDiagonalSum == targetNumber || forwardDiagonalSum == targetNumber)
        {
            return true;
        }

        return false;
    }
    
    public static bool HasPlayerWon(int numberOfRows, int numberOfColumns, int targetNumber, int[,] grid)
    {
        return IsGridRowWin(numberOfRows, numberOfColumns, targetNumber, grid) ||
               IsGridColumnWin(numberOfRows, numberOfColumns, targetNumber, grid) ||
               IsDiagonalWin(numberOfRows, targetNumber, grid);
    }
}
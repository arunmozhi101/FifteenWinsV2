namespace FifteenWinsV2;

public class Logic
{
    public static (int enteredRowPosition, int enteredColumnPosition, bool isGridPositionAlreadyFilled) ParseGridPositions(string unparsedGridPosition, int  numberOfRows, int numberOfColumns, List<string> filledGridPositions)
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
                        return (enteredRowPosition, enteredColumnPosition, isGridPositionAlreadyFilled: false);
                    }
                    else
                    {
                        return (enteredRowPosition: -1, enteredColumnPosition: -1, isGridPositionAlreadyFilled: true);
                    }
                }
            }
        }
        return (enteredRowPosition: -1, enteredColumnPosition: -1, isGridPositionAlreadyFilled: false);
    }
    
    public static (int numberEntered, bool isNumberAlreadyUsed, bool isNumberInValidRange) ParseNumberEntered(string unparsedNumberEntered, List<int> usedNumbersList)
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
                return (numberEntered: -1, isNumberAlreadyUsed: true,  isNumberInValidRange: true);
            }
            else
            {
                if (numberEntered < 1 | numberEntered > 9)
                {
                    return (numberEntered: -1, isNumberAlreadyUsed: false,  isNumberInValidRange: false);
                }

                return (numberEntered, isNumberAlreadyUsed: false, isNumberInValidRange: true);
            }
        }
        else
        {
            return (numberEntered: -1, isNumberAlreadyUsed: isNumberAlreadyUsed,  isNumberInValidRange: isNumberInValidRange);
        }
    }
    
        public static bool IsGridRowWin(int numberOfRows, int numberOfColumns, int targetNumber, int[,] grid)
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

    public static bool IsGridColumnWin(int numberOfRows, int numberOfColumns, int targetNumber, int[,] grid)
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

    public static bool IsDiagonalWin(int numberOfRows, int targetNumber, int[,] grid)
    {
        //Diagonal Check
        int backwardDiagonalSum = 0;
        int forwardDiagonalSum = 0;
        for (int i = 0; i < numberOfRows; i++)
        {
            if (grid[i, i] != 0)
            {
                backwardDiagonalSum += grid[i, i];
                forwardDiagonalSum += grid[i, (numberOfRows - 1) - i];
            }
            else
            {
                // None of the values in the diagonal can be a 0
                backwardDiagonalSum = 0;
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
}
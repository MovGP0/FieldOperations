namespace FieldOperations;

/// <summary>
/// Provides methods to compute the gradient, divergence, and curl of fields represented by matrices.
/// </summary>
public static partial class FieldOperations
{
    /// <summary>
    /// Computes the gradient of a scalar field.
    /// </summary>
    /// <param name="scalarField">A 2D array representing the scalar field.</param>
    /// <returns>
    /// A tuple containing two 2D arrays representing the partial derivatives of the scalar field in the x and y directions.
    /// </returns>
    public static (double[,] gradX, double[,] gradY) Gradient(double[,] scalarField)
    {
        int rows = scalarField.GetLength(0);
        int cols = scalarField.GetLength(1);
        double[,] gradX = new double[rows, cols];
        double[,] gradY = new double[rows, cols];

        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                gradX[i, j] = (scalarField[i + 1, j] - scalarField[i - 1, j]) / 2;
                gradY[i, j] = (scalarField[i, j + 1] - scalarField[i, j - 1]) / 2;
            }
        }

        return (gradX, gradY);
    }

    /// <summary>
    /// Computes the divergence of a vector field.
    /// </summary>
    /// <param name="fieldX">A 2D array representing the x-components of the vector field.</param>
    /// <param name="fieldY">A 2D array representing the y-components of the vector field.</param>
    /// <returns>
    /// A 2D array representing the divergence of the vector field.
    /// </returns>
    public static double[,] Divergence(double[,] fieldX, double[,] fieldY)
    {
        int rows = fieldX.GetLength(0);
        int cols = fieldX.GetLength(1);
        double[,] div = new double[rows, cols];

        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                double dFx_dx = (fieldX[i + 1, j] - fieldX[i - 1, j]) / 2;
                double dFy_dy = (fieldY[i, j + 1] - fieldY[i, j - 1]) / 2;
                div[i, j] = dFx_dx + dFy_dy;
            }
        }

        return div;
    }

    /// <summary>
    /// Computes the curl of a vector field.
    /// </summary>
    /// <param name="fieldX">A 2D array representing the x-components of the vector field.</param>
    /// <param name="fieldY">A 2D array representing the y-components of the vector field.</param>
    /// <returns>
    /// A 2D array representing the curl of the vector field.
    /// </returns>
    public static double[,] Curl(double[,] fieldX, double[,] fieldY)
    {
        int rows = fieldX.GetLength(0);
        int cols = fieldX.GetLength(1);
        double[,] curl = new double[rows, cols];

        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                double dFy_dx = (fieldY[i + 1, j] - fieldY[i - 1, j]) / 2;
                double dFx_dy = (fieldX[i, j + 1] - fieldX[i, j - 1]) / 2;
                curl[i, j] = dFy_dx - dFx_dy;
            }
        }

        return curl;
    }
}

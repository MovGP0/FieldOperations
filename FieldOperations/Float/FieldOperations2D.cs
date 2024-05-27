namespace FieldOperations;

public static partial class FieldOperations
{
    public static (float[,] gradientX, float[,] gradientY) Gradient(float[,] field)
    {
        int rows = field.GetLength(0);
        int cols = field.GetLength(1);
        float[,] gradX = new float[rows, cols];
        float[,] gradY = new float[rows, cols];

        // Handle interior points with central differences
        for (int i = 1; i < rows - 1; i++)
        for (int j = 1; j < cols - 1; j++)
        {
            gradX[i, j] = (field[i + 1, j] - field[i - 1, j]) / 2;
            gradY[i, j] = (field[i, j + 1] - field[i, j - 1]) / 2;
        }

        // Handle boundary points with one-sided differences
        for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
        {
            if (i == 0 || i == rows - 1 || j == 0 || j == cols - 1)
            {
                gradX[i, j] = (i < rows - 1 ? field[i + 1, j] - field[i, j] : field[i, j] - field[i - 1, j]);
                gradY[i, j] = (j < cols - 1 ? field[i, j + 1] - field[i, j] : field[i, j] - field[i, j - 1]);
            }
        }

        return (gradX, gradY);
    }

    [Obsolete("undefined", true)]
    public static float[,] Divergence(float[,] field) => throw new NotImplementedException();

    [Obsolete("undefined", true)]
    public static float[,] Curl(float[,] field) => throw new NotImplementedException();
}

namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector2[,] gradientX, Vector2[,] gradientY) Gradient(Vector2[,] scalarField)
    {
        int dimX = scalarField.GetLength(0);
        int dimY = scalarField.GetLength(1);
        Vector2[,] gradX = new Vector2[dimX, dimY];
        Vector2[,] gradY = new Vector2[dimX, dimY];

        for (int i = 1; i < dimX - 1; i++)
        for (int j = 1; j < dimY - 1; j++)
        {
            gradX[i, j] = (scalarField[i + 1, j] - scalarField[i - 1, j]) / 2;
            gradY[i, j] = (scalarField[i, j + 1] - scalarField[i, j - 1]) / 2;
        }

        return (gradX, gradY);
    }

    public static float[,] Divergence(Vector2[,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);

        float[,] divergence = new float[xSize, ySize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        {
            float dFx_dx = (field[x + 1, y].X - field[x - 1, y].X) / 2.0f;
            float dFy_dy = (field[x, y + 1].Y - field[x, y - 1].Y) / 2.0f;

            divergence[x, y] = dFx_dx + dFy_dy;
        }

        // Handle boundaries using one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFx_dx = (x < xSize - 1 ? field[x + 1, y].X : field[x, y].X) - (x > 0 ? field[x - 1, y].X : field[x, y].X);
                float dFy_dy = (y < ySize - 1 ? field[x, y + 1].Y : field[x, y].Y) - (y > 0 ? field[x, y - 1].Y : field[x, y].Y);

                divergence[x, y] = dFx_dx + dFy_dy;
            }
        }

        return divergence;
    }

    public static float[,] Curl(Vector2[,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);

        float[,] curl = new float[xSize, ySize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        {
            float dFy_dx = (field[x + 1, y].Y - field[x - 1, y].Y) / 2.0f;
            float dFx_dy = (field[x, y + 1].X - field[x, y - 1].X) / 2.0f;

            curl[x, y] = dFy_dx - dFx_dy;
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFy_dx = x < xSize - 1 ? (field[x + 1, y].Y - field[x, y].Y) : (field[x, y].Y - field[x - 1, y].Y);
                float dFx_dy = y < ySize - 1 ? (field[x, y + 1].X - field[x, y].X) : (field[x, y].X - field[x, y - 1].X);

                curl[x, y] = dFy_dx - dFx_dy;
            }
        }

        return curl;
    }
}

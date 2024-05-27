namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector3[,] gradientX, Vector3[,] gradientY) Gradient(Vector3[,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);

        Vector3[,] gradientX = new Vector3[xSize, ySize];
        Vector3[,] gradientY = new Vector3[xSize, ySize];

        // Calculate central differences for the interior points
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        {
            float dFx_dx = (field[x + 1, y].X - field[x - 1, y].X) / 2.0f;
            float dFx_dy = (field[x, y + 1].X - field[x, y - 1].X) / 2.0f;

            float dFy_dx = (field[x + 1, y].Y - field[x - 1, y].Y) / 2.0f;
            float dFy_dy = (field[x, y + 1].Y - field[x, y - 1].Y) / 2.0f;

            float dFz_dx = (field[x + 1, y].Z - field[x - 1, y].Z) / 2.0f;
            float dFz_dy = (field[x, y + 1].Z - field[x, y - 1].Z) / 2.0f;

            gradientX[x, y] = new Vector3(dFx_dx, dFy_dx, dFz_dx);
            gradientY[x, y] = new Vector3(dFx_dy, dFy_dy, dFz_dy);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFx_dx = x < xSize - 1 ? (field[x + 1, y].X - field[x, y].X) : (field[x, y].X - field[x - 1, y].X);
                float dFx_dy = y < ySize - 1 ? (field[x, y + 1].X - field[x, y].X) : (field[x, y].X - field[x, y - 1].X);

                float dFy_dx = x < xSize - 1 ? (field[x + 1, y].Y - field[x, y].Y) : (field[x, y].Y - field[x - 1, y].Y);
                float dFy_dy = y < ySize - 1 ? (field[x, y + 1].Y - field[x, y].Y) : (field[x, y].Y - field[x, y - 1].Y);

                float dFz_dx = x < xSize - 1 ? (field[x + 1, y].Z - field[x, y].Z) : (field[x, y].Z - field[x - 1, y].Z);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1].Z - field[x, y].Z) : (field[x, y].Z - field[x, y - 1].Z);

                gradientX[x, y] = new Vector3(dFx_dx, dFy_dx, dFz_dx);
                gradientY[x, y] = new Vector3(dFx_dy, dFy_dy, dFz_dy);
            }
        }

        return (gradientX, gradientY);
    }

    /// <summary>
    /// Computes the divergence of a 2D vector field.
    /// </summary>
    public static float[,] Divergence(Vector3[,] field)
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

    public static Vector3[,] Curl(Vector3[,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);

        Vector3[,] curl = new Vector3[xSize, ySize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        {
            float dFz_dy = (field[x, y + 1].Z - field[x, y - 1].Z) / 2.0f;
            float dFy_dz = (field[x, y + 1].Y - field[x, y - 1].Y) / 2.0f;

            float dFx_dy = (field[x, y + 1].X - field[x, y - 1].X) / 2.0f;
            float dFz_dx = (field[x + 1, y].Z - field[x - 1, y].Z) / 2.0f;

            float dFy_dx = (field[x + 1, y].Y - field[x - 1, y].Y) / 2.0f;
            float dFx_dz = (field[x + 1, y].X - field[x - 1, y].X) / 2.0f;

            curl[x, y] = new Vector3(dFy_dz - dFz_dy, dFz_dx - dFx_dz, dFx_dy - dFy_dx);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1].Z - field[x, y].Z) : (field[x, y].Z - field[x, y - 1].Z);
                float dFy_dz = y < ySize - 1 ? (field[x, y + 1].Y - field[x, y].Y) : (field[x, y].Y - field[x, y - 1].Y);

                float dFx_dy = y < ySize - 1 ? (field[x, y + 1].X - field[x, y].X) : (field[x, y].X - field[x, y - 1].X);
                float dFz_dx = x < xSize - 1 ? (field[x + 1, y].Z - field[x, y].Z) : (field[x, y].Z - field[x - 1, y].Z);

                float dFy_dx = x < xSize - 1 ? (field[x + 1, y].Y - field[x, y].Y) : (field[x, y].Y - field[x - 1, y].Y);
                float dFx_dz = x < xSize - 1 ? (field[x + 1, y].X - field[x, y].X) : (field[x, y].X - field[x - 1, y].X);

                curl[x, y] = new Vector3(dFy_dz - dFz_dy, dFz_dx - dFx_dz, dFx_dy - dFy_dx);
            }
        }

        return curl;
    }
}
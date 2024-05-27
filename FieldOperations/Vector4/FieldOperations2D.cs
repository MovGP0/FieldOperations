namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector4[,] gradientX, Vector4[,] gradientY) Gradient(Vector4[,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);

        Vector4[,] gradientX = new Vector4[xSize, ySize];
        Vector4[,] gradientY = new Vector4[xSize, ySize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        {
            float dFx_dx = (field[x + 1, y].X - field[x - 1, y].X) / 2.0f;
            float dFx_dy = (field[x, y + 1].X - field[x, y - 1].X) / 2.0f;

            float dFy_dx = (field[x + 1, y].Y - field[x - 1, y].Y) / 2.0f;
            float dFy_dy = (field[x, y + 1].Y - field[x, y - 1].Y) / 2.0f;

            float dFz_dx = (field[x + 1, y].Z - field[x - 1, y].Z) / 2.0f;
            float dFz_dy = (field[x, y + 1].Z - field[x, y - 1].Z) / 2.0f;

            float dFw_dx = (field[x + 1, y].W - field[x - 1, y].W) / 2.0f;
            float dFw_dy = (field[x, y + 1].W - field[x, y - 1].W) / 2.0f;

            gradientX[x, y] = new Vector4(dFx_dx, dFy_dx, dFz_dx, dFw_dx);
            gradientY[x, y] = new Vector4(dFx_dy, dFy_dy, dFz_dy, dFw_dy);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFx_dx = x < xSize - 1 ? (field[x + 1, y].X - field[x, y].X) : (field[x, y].X - field[x - 1, y].X);
                float dFy_dx = x < xSize - 1 ? (field[x + 1, y].Y - field[x, y].Y) : (field[x, y].Y - field[x - 1, y].Y);
                float dFz_dx = x < xSize - 1 ? (field[x + 1, y].Z - field[x, y].Z) : (field[x, y].Z - field[x - 1, y].Z);
                float dFw_dx = x < xSize - 1 ? (field[x + 1, y].W - field[x, y].W) : (field[x, y].W - field[x - 1, y].W);

                float dFx_dy = y < ySize - 1 ? (field[x, y + 1].X - field[x, y].X) : (field[x, y].X - field[x, y - 1].X);
                float dFy_dy = y < ySize - 1 ? (field[x, y + 1].Y - field[x, y].Y) : (field[x, y].Y - field[x, y - 1].Y);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1].Z - field[x, y].Z) : (field[x, y].Z - field[x, y - 1].Z);
                float dFw_dy = y < ySize - 1 ? (field[x, y + 1].W - field[x, y].W) : (field[x, y].W - field[x, y - 1].W);

                gradientX[x, y] = new Vector4(dFx_dx, dFy_dx, dFz_dx, dFw_dx);
                gradientY[x, y] = new Vector4(dFx_dy, dFy_dy, dFz_dy, dFw_dy);
            }
        }

        return (gradientX, gradientY);
    }

    public static float[,] Divergence(Vector4[,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);

        float[,] divergence = new float[xSize, ySize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        {
            float dFx_dx = (field[x + 1, y].X - field[x - 1, y].X) / 2.0f;
            float dFy_dy = (field[x, y + 1].Y - field[x, y - 1].Y) / 2.0f;
            float dFz_dx = (field[x + 1, y].Z - field[x - 1, y].Z) / 2.0f;
            float dFw_dy = (field[x, y + 1].W - field[x, y - 1].W) / 2.0f;

            divergence[x, y] = dFx_dx + dFy_dy + dFz_dx + dFw_dy;
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFx_dx = x < xSize - 1 ? field[x + 1, y].X - field[x, y].X : field[x, y].X - field[x - 1, y].X;
                float dFy_dy = y < ySize - 1 ? field[x, y + 1].Y - field[x, y].Y : field[x, y].Y - field[x, y - 1].Y;
                float dFz_dx = x < xSize - 1 ? field[x + 1, y].Z - field[x, y].Z : field[x, y].Z - field[x - 1, y].Z;
                float dFw_dy = y < ySize - 1 ? field[x, y + 1].W - field[x, y].W : field[x, y].W - field[x, y - 1].W;

                divergence[x, y] = dFx_dx + dFy_dy + dFz_dx + dFw_dy;
            }
        }

        return divergence;
    }

    public static float[,] Curl(Vector4[,] field)
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
namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector2[,,,] gradX, Vector2[,,,] gradY, Vector2[,,,] gradZ, Vector2[,,,] gradW) Gradient(Vector2[,,,] field)
    {
        int dimX = field.GetLength(0);
        int dimY = field.GetLength(1);
        int dimZ = field.GetLength(2);
        int dimW = field.GetLength(3);
        Vector2[,,,] gradX = new Vector2[dimX, dimY, dimZ, dimW];
        Vector2[,,,] gradY = new Vector2[dimX, dimY, dimZ, dimW];
        Vector2[,,,] gradZ = new Vector2[dimX, dimY, dimZ, dimW];
        Vector2[,,,] gradW = new Vector2[dimX, dimY, dimZ, dimW];

        for (int i = 1; i < dimX - 1; i++)
        for (int j = 1; j < dimY - 1; j++)
        for (int k = 1; k < dimZ - 1; k++)
        {
            for (int l = 1; l < dimW - 1; l++)
            {
                gradX[i, j, k, l] = (field[i + 1, j, k, l] - field[i - 1, j, k, l]) / 2;
                gradY[i, j, k, l] = (field[i, j + 1, k, l] - field[i, j - 1, k, l]) / 2;
                gradZ[i, j, k, l] = (field[i, j, k + 1, l] - field[i, j, k - 1, l]) / 2;
                gradW[i, j, k, l] = (field[i, j, k, l + 1] - field[i, j, k, l - 1]) / 2;
            }
        }

        return (gradX, gradY, gradZ, gradW);
    }

    public static float[,,,] Divergence(Vector2[,,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);
        int wSize = field.GetLength(3);

        float[,,,] divergence = new float[xSize, ySize, zSize, wSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 0; z < zSize; z++)
        {
            for (int w = 0; w < wSize; w++)
            {
                float dFx_dx = (field[x + 1, y, z, w].X - field[x - 1, y, z, w].X) / 2.0f;
                float dFy_dy = (field[x, y + 1, z, w].Y - field[x, y - 1, z, w].Y) / 2.0f;

                divergence[x, y, z, w] = dFx_dx + dFy_dy;
            }
        }

        // Handle boundaries using one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        for (int w = 0; w < wSize; w++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFx_dx = (x < xSize - 1 ? field[x + 1, y, z, w].X : field[x, y, z, w].X) - (x > 0 ? field[x - 1, y, z, w].X : field[x, y, z, w].X);
                float dFy_dy = (y < ySize - 1 ? field[x, y + 1, z, w].Y : field[x, y, z, w].Y) - (y > 0 ? field[x, y - 1, z, w].Y : field[x, y, z, w].Y);

                divergence[x, y, z, w] = dFx_dx + dFy_dy;
            }
        }

        return divergence;
    }

    public static float[,,,] Curl(Vector2[,,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);
        int wSize = field.GetLength(3);

        float[,,,] curl = new float[xSize, ySize, zSize, wSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 0; z < zSize; z++)
        for (int w = 0; w < wSize; w++)
        {
            float dFy_dx = (field[x + 1, y, z, w].Y - field[x - 1, y, z, w].Y) / 2.0f;
            float dFx_dy = (field[x, y + 1, z, w].X - field[x, y - 1, z, w].X) / 2.0f;

            curl[x, y, z, w] = dFy_dx - dFx_dy;
        }

        // Handle boundaries using one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        for (int w = 0; w < wSize; w++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
            {
                float dFy_dx = (x < xSize - 1 ? field[x + 1, y, z, w].Y : field[x, y, z, w].Y) - (x > 0 ? field[x - 1, y, z, w].Y : field[x, y, z, w].Y);
                float dFx_dy = (y < ySize - 1 ? field[x, y + 1, z, w].X : field[x, y, z, w].X) - (y > 0 ? field[x, y - 1, z, w].X : field[x, y, z, w].X);

                curl[x, y, z, w] = dFy_dx - dFx_dy;
            }
        }

        return curl;
    }
}
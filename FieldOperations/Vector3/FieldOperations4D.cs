namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector3[,,,] gradientX, Vector3[,,,] gradientY, Vector3[,,,] gradientZ, Vector3[,,,] gradientW) Gradient(Vector3[,,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);
        int wSize = field.GetLength(3);

        Vector3[,,,] gradientX = new Vector3[xSize, ySize, zSize, wSize];
        Vector3[,,,] gradientY = new Vector3[xSize, ySize, zSize, wSize];
        Vector3[,,,] gradientZ = new Vector3[xSize, ySize, zSize, wSize];
        Vector3[,,,] gradientW = new Vector3[xSize, ySize, zSize, wSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        for (int w = 1; w < wSize - 1; w++)
        {
            float dFx_dx = (field[x + 1, y, z, w].X - field[x - 1, y, z, w].X) / 2.0f;
            float dFx_dy = (field[x, y + 1, z, w].X - field[x, y - 1, z, w].X) / 2.0f;
            float dFx_dz = (field[x, y, z + 1, w].X - field[x, y, z - 1, w].X) / 2.0f;
            float dFx_dw = (field[x, y, z, w + 1].X - field[x, y, z, w - 1].X) / 2.0f;

            float dFy_dx = (field[x + 1, y, z, w].Y - field[x - 1, y, z, w].Y) / 2.0f;
            float dFy_dy = (field[x, y + 1, z, w].Y - field[x, y - 1, z, w].Y) / 2.0f;
            float dFy_dz = (field[x, y, z + 1, w].Y - field[x, y, z - 1, w].Y) / 2.0f;
            float dFy_dw = (field[x, y, z, w + 1].Y - field[x, y, z, w - 1].Y) / 2.0f;

            float dFz_dx = (field[x + 1, y, z, w].Z - field[x - 1, y, z, w].Z) / 2.0f;
            float dFz_dy = (field[x, y + 1, z, w].Z - field[x, y - 1, z, w].Z) / 2.0f;
            float dFz_dz = (field[x, y, z + 1, w].Z - field[x, y, z - 1, w].Z) / 2.0f;
            float dFz_dw = (field[x, y, z, w + 1].Z - field[x, y, z, w - 1].Z) / 2.0f;

            gradientX[x, y, z, w] = new Vector3(dFx_dx, dFx_dy, dFx_dz);
            gradientY[x, y, z, w] = new Vector3(dFy_dx, dFy_dy, dFy_dz);
            gradientZ[x, y, z, w] = new Vector3(dFz_dx, dFz_dy, dFz_dz);
            gradientW[x, y, z, w] = new Vector3(dFx_dw, dFy_dw, dFz_dw);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        for (int w = 0; w < wSize; w++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1 || w == 0 ||
                w == wSize - 1)
            {
                float dFx_dx = x < xSize - 1 ? (field[x + 1, y, z, w].X - field[x, y, z, w].X) : (field[x, y, z, w].X - field[x - 1, y, z, w].X);
                float dFx_dy = y < ySize - 1 ? (field[x, y + 1, z, w].X - field[x, y, z, w].X) : (field[x, y, z, w].X - field[x, y - 1, z, w].X);
                float dFx_dz = z < zSize - 1 ? (field[x, y, z + 1, w].X - field[x, y, z, w].X) : (field[x, y, z, w].X - field[x, y, z - 1, w].X);
                float dFx_dw = w < wSize - 1 ? (field[x, y, z, w + 1].X - field[x, y, z, w].X) : (field[x, y, z, w].X - field[x, y, z, w - 1].X);

                float dFy_dx = x < xSize - 1 ? (field[x + 1, y, z, w].Y - field[x, y, z, w].Y) : (field[x, y, z, w].Y - field[x - 1, y, z, w].Y);
                float dFy_dy = y < ySize - 1 ? (field[x, y + 1, z, w].Y - field[x, y, z, w].Y) : (field[x, y, z, w].Y - field[x, y - 1, z, w].Y);
                float dFy_dz = z < zSize - 1 ? (field[x, y, z + 1, w].Y - field[x, y, z, w].Y) : (field[x, y, z, w].Y - field[x, y, z - 1, w].Y);
                float dFy_dw = w < wSize - 1 ? (field[x, y, z, w + 1].Y - field[x, y, z, w].Y) : (field[x, y, z, w].Y - field[x, y, z, w - 1].Y);

                float dFz_dx = x < xSize - 1 ? (field[x + 1, y, z, w].Z - field[x, y, z, w].Z) : (field[x, y, z, w].Z - field[x - 1, y, z, w].Z);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1, z, w].Z - field[x, y, z, w].Z) : (field[x, y, z, w].Z - field[x, y - 1, z, w].Z);
                float dFz_dz = z < zSize - 1 ? (field[x, y, z + 1, w].Z - field[x, y, z, w].Z) : (field[x, y, z, w].Z - field[x, y, z - 1, w].Z);
                float dFz_dw = w < wSize - 1 ? (field[x, y, z, w + 1].Z - field[x, y, z, w].Z) : (field[x, y, z, w].Z - field[x, y, z, w - 1].Z);

                gradientX[x, y, z, w] = new Vector3(dFx_dx, dFx_dy, dFx_dz);
                gradientY[x, y, z, w] = new Vector3(dFy_dx, dFy_dy, dFy_dz);
                gradientZ[x, y, z, w] = new Vector3(dFz_dx, dFz_dy, dFz_dz);
                gradientW[x, y, z, w] = new Vector3(dFx_dw, dFy_dw, dFz_dw);
            }
        }

        return (gradientX, gradientY, gradientZ, gradientW);
    }

    public static float[,,,] Divergence(Vector3[,,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);
        int wSize = field.GetLength(3);

        float[,,,] divergence = new float[xSize, ySize, zSize, wSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        for (int w = 1; w < wSize - 1; w++)
        {
            float dFx_dx = (field[x + 1, y, z, w].X - field[x - 1, y, z, w].X) / 2.0f;
            float dFy_dy = (field[x, y + 1, z, w].Y - field[x, y - 1, z, w].Y) / 2.0f;
            float dFz_dz = (field[x, y, z + 1, w].Z - field[x, y, z - 1, w].Z) / 2.0f;
            float dFw_dw = (field[x, y, z, w + 1].X - field[x, y, z, w - 1].X) / 2.0f;

            divergence[x, y, z, w] = dFx_dx + dFy_dy + dFz_dz + dFw_dw;
        }

        // Handle boundaries using one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        for (int w = 0; w < wSize; w++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1 || w == 0 || w == wSize - 1)
            {
                float dFx_dx = (x < xSize - 1 ? field[x + 1, y, z, w].X : field[x, y, z, w].X) - (x > 0 ? field[x - 1, y, z, w].X : field[x, y, z, w].X);
                float dFy_dy = (y < ySize - 1 ? field[x, y + 1, z, w].Y : field[x, y, z, w].Y) - (y > 0 ? field[x, y - 1, z, w].Y : field[x, y, z, w].Y);
                float dFz_dz = (z < zSize - 1 ? field[x, y, z + 1, w].Z : field[x, y, z, w].Z) - (z > 0 ? field[x, y, z - 1, w].Z : field[x, y, z, w].Z);
                float dFw_dw = (w < wSize - 1 ? field[x, y, z, w + 1].X : field[x, y, z, w].X) - (w > 0 ? field[x, y, z, w - 1].X : field[x, y, z, w].X);

                divergence[x, y, z, w] = dFx_dx + dFy_dy + dFz_dz + dFw_dw;
            }
        }

        return divergence;
    }
}
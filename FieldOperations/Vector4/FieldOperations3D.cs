namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector4[,,] gradientX, Vector4[,,] gradientY, Vector4[,,] gradientZ) Gradient(Vector4[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        Vector4[,,] gradientX = new Vector4[xSize, ySize, zSize];
        Vector4[,,] gradientY = new Vector4[xSize, ySize, zSize];
        Vector4[,,] gradientZ = new Vector4[xSize, ySize, zSize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        {
            float dFx_dx = (field[x + 1, y, z].X - field[x - 1, y, z].X) / 2.0f;
            float dFx_dy = (field[x, y + 1, z].X - field[x, y - 1, z].X) / 2.0f;
            float dFx_dz = (field[x, y, z + 1].X - field[x, y, z - 1].X) / 2.0f;

            float dFy_dx = (field[x + 1, y, z].Y - field[x - 1, y, z].Y) / 2.0f;
            float dFy_dy = (field[x, y + 1, z].Y - field[x, y - 1, z].Y) / 2.0f;
            float dFy_dz = (field[x, y, z + 1].Y - field[x, y, z - 1].Y) / 2.0f;

            float dFz_dx = (field[x + 1, y, z].Z - field[x - 1, y, z].Z) / 2.0f;
            float dFz_dy = (field[x, y + 1, z].Z - field[x, y - 1, z].Z) / 2.0f;
            float dFz_dz = (field[x, y, z + 1].Z - field[x, y, z - 1].Z) / 2.0f;

            gradientX[x, y, z] = new Vector4(dFx_dx, dFx_dy, dFx_dz, 0);
            gradientY[x, y, z] = new Vector4(dFy_dx, dFy_dy, dFy_dz, 0);
            gradientZ[x, y, z] = new Vector4(dFz_dx, dFz_dy, dFz_dz, 0);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1)
            {
                float dFx_dx = x < xSize - 1 ? (field[x + 1, y, z].X - field[x, y, z].X) : (field[x, y, z].X - field[x - 1, y, z].X);
                float dFy_dx = x < xSize - 1 ? (field[x + 1, y, z].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x - 1, y, z].Y);
                float dFz_dx = x < xSize - 1 ? (field[x + 1, y, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x - 1, y, z].Z);

                float dFx_dy = y < ySize - 1 ? (field[x, y + 1, z].X - field[x, y, z].X) : (field[x, y, z].X - field[x, y - 1, z].X);
                float dFy_dy = y < ySize - 1 ? (field[x, y + 1, z].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x, y - 1, z].Y);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x, y - 1, z].Z);

                float dFx_dz = z < zSize - 1 ? (field[x, y, z + 1].X - field[x, y, z].X) : (field[x, y, z].X - field[x, y, z - 1].X);
                float dFy_dz = z < zSize - 1 ? (field[x, y, z + 1].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x, y, z - 1].Y);
                float dFz_dz = z < zSize - 1 ? (field[x, y, z + 1].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x, y, z - 1].Z);

                gradientX[x, y, z] = new Vector4(dFx_dx, dFx_dy, dFx_dz, 0);
                gradientY[x, y, z] = new Vector4(dFy_dx, dFy_dy, dFy_dz, 0);
                gradientZ[x, y, z] = new Vector4(dFz_dx, dFz_dy, dFz_dz, 0);
            }
        }

        return (gradientX, gradientY, gradientZ);
    }

    public static float[,,] Divergence(Vector4[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        float[,,] divergence = new float[xSize, ySize, zSize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        {
            float dFx_dx = (field[x + 1, y, z].X - field[x - 1, y, z].X) / 2.0f;
            float dFy_dy = (field[x, y + 1, z].Y - field[x, y - 1, z].Y) / 2.0f;
            float dFz_dz = (field[x, y, z + 1].Z - field[x, y, z - 1].Z) / 2.0f;

            divergence[x, y, z] = dFx_dx + dFy_dy + dFz_dz;
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1)
            {
                float dFx_dx = x < xSize - 1 ? field[x + 1, y, z].X - field[x, y, z].X : field[x, y, z].X - field[x - 1, y, z].X;
                float dFy_dy = y < ySize - 1 ? field[x, y + 1, z].Y - field[x, y, z].Y : field[x, y, z].Y - field[x, y - 1, z].Y;
                float dFz_dz = z < zSize - 1 ? field[x, y, z + 1].Z - field[x, y, z].Z : field[x, y, z].Z - field[x, y, z - 1].Z;

                divergence[x, y, z] = dFx_dx + dFy_dy + dFz_dz;
            }
        }

        return divergence;
    }

    public static (Vector4[,,] curlYZ, Vector4[,,] curlZX, Vector4[,,] curlXW, Vector4[,,] curlYW) Curl(Vector4[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        Vector4[,,] curlYZ = new Vector4[xSize, ySize, zSize];
        Vector4[,,] curlZX = new Vector4[xSize, ySize, zSize];
        Vector4[,,] curlXW = new Vector4[xSize, ySize, zSize];
        Vector4[,,] curlYW = new Vector4[xSize, ySize, zSize];

        // Handle interior points with central differences
        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        {
            float dFx_dy = (field[x, y + 1, z].X - field[x, y - 1, z].X) / 2.0f;
            float dFx_dz = (field[x, y, z + 1].X - field[x, y, z - 1].X) / 2.0f;
            float dFx_dw = 0f;

            float dFy_dx = (field[x + 1, y, z].Y - field[x - 1, y, z].Y) / 2.0f;
            float dFy_dz = (field[x, y, z + 1].Y - field[x, y, z - 1].Y) / 2.0f;
            float dFy_dw = 0f;

            float dFz_dx = (field[x + 1, y, z].Z - field[x - 1, y, z].Z) / 2.0f;
            float dFz_dy = (field[x, y + 1, z].Z - field[x, y - 1, z].Z) / 2.0f;
            float dFz_dw = 0f;

            float dFw_dx = (field[x + 1, y, z].W - field[x - 1, y, z].W) / 2.0f;
            float dFw_dy = (field[x, y + 1, z].W - field[x, y - 1, z].W) / 2.0f;
            float dFw_dz = (field[x, y, z + 1].W - field[x, y, z - 1].W) / 2.0f;

            curlYZ[x, y, z] = new Vector4(0, dFz_dy - dFy_dz, dFw_dz - dFz_dw, dFy_dw - dFw_dy);
            curlZX[x, y, z] = new Vector4(dFz_dx - dFx_dz, 0, dFx_dw - dFw_dx, dFw_dx - dFz_dw);
            curlXW[x, y, z] = new Vector4(dFw_dx - dFx_dw, dFy_dw - dFw_dy, 0, dFx_dy - dFy_dx);
            curlYW[x, y, z] = new Vector4(dFw_dy - dFy_dw, dFz_dw - dFw_dz, dFy_dz - dFz_dy, 0);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1)
            {
                float dFx_dy = y < ySize - 1 ? (field[x, y + 1, z].X - field[x, y, z].X) : (field[x, y, z].X - field[x, y - 1, z].X);
                float dFx_dz = z < zSize - 1 ? (field[x, y, z + 1].X - field[x, y, z].X) : (field[x, y, z].X - field[x, y, z - 1].X);
                float dFx_dw = 0f;

                float dFy_dx = x < xSize - 1 ? (field[x + 1, y, z].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x - 1, y, z].Y);
                float dFy_dz = z < zSize - 1 ? (field[x, y, z + 1].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x, y, z - 1].Y);
                float dFy_dw = 0f;

                float dFz_dx = x < xSize - 1 ? (field[x + 1, y, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x - 1, y, z].Z);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x, y - 1, z].Z);
                float dFz_dw = 0f;

                float dFw_dx = x < xSize - 1 ? (field[x + 1, y, z].W - field[x, y, z].W) : (field[x, y, z].W - field[x - 1, y, z].W);
                float dFw_dy = y < ySize - 1 ? (field[x, y + 1, z].W - field[x, y, z].W) : (field[x, y, z].W - field[x, y - 1, z].W);
                float dFw_dz = z < zSize - 1 ? (field[x, y, z + 1].W - field[x, y, z].W) : (field[x, y, z].W - field[x, y, z - 1].W);

                curlYZ[x, y, z] = new Vector4(0, dFz_dy - dFy_dz, dFw_dz - dFz_dw, dFy_dw - dFw_dy);
                curlZX[x, y, z] = new Vector4(dFz_dx - dFx_dz, 0, dFx_dw - dFw_dx, dFw_dx - dFz_dw);
                curlXW[x, y, z] = new Vector4(dFw_dx - dFx_dw, dFy_dw - dFw_dy, 0, dFx_dy - dFy_dx);
                curlYW[x, y, z] = new Vector4(dFw_dy - dFy_dw, dFz_dw - dFw_dz, dFy_dz - dFz_dy, 0);
            }
        }

        return (curlYZ, curlZX, curlXW, curlYW);
    }
}
namespace FieldOperations;

public static partial class FieldOperations
{
    public static (Vector3[,,] gradientX, Vector3[,,] gradientY, Vector3[,,] gradientZ) Gradient(Vector3[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        Vector3[,,] gradientX = new Vector3[xSize, ySize, zSize];
        Vector3[,,] gradientY = new Vector3[xSize, ySize, zSize];
        Vector3[,,] gradientZ = new Vector3[xSize, ySize, zSize];

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

            gradientX[x, y, z] = new Vector3(dFx_dx, dFx_dy, dFx_dz);
            gradientY[x, y, z] = new Vector3(dFy_dx, dFy_dy, dFy_dz);
            gradientZ[x, y, z] = new Vector3(dFz_dx, dFz_dy, dFz_dz);
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1)
            {
                float dFx_dx = x < xSize - 1 ? (field[x + 1, y, z].X - field[x, y, z].X) : (field[x, y, z].X - field[x - 1, y, z].X);
                float dFx_dy = y < ySize - 1 ? (field[x, y + 1, z].X - field[x, y, z].X) : (field[x, y, z].X - field[x, y - 1, z].X);
                float dFx_dz = z < zSize - 1 ? (field[x, y, z + 1].X - field[x, y, z].X) : (field[x, y, z].X - field[x, y, z - 1].X);

                float dFy_dx = x < xSize - 1 ? (field[x + 1, y, z].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x - 1, y, z].Y);
                float dFy_dy = y < ySize - 1 ? (field[x, y + 1, z].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x, y - 1, z].Y);
                float dFy_dz = z < zSize - 1 ? (field[x, y, z + 1].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x, y, z - 1].Y);

                float dFz_dx = x < xSize - 1 ? (field[x + 1, y, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x - 1, y, z].Z);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x, y - 1, z].Z);
                float dFz_dz = z < zSize - 1 ? (field[x, y, z + 1].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x, y, z - 1].Z);

                gradientX[x, y, z] = new Vector3(dFx_dx, dFx_dy, dFx_dz);
                gradientY[x, y, z] = new Vector3(dFy_dx, dFy_dy, dFy_dz);
                gradientZ[x, y, z] = new Vector3(dFz_dx, dFz_dy, dFz_dz);
            }
        }

        return (gradientX, gradientY, gradientZ);
    }

    public static float[,,] Divergence(Vector3[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        float[,,] divergence = new float[xSize, ySize, zSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        {
            float dFx_dx = (field[x + 1, y, z].X - field[x - 1, y, z].X) / 2.0f;
            float dFy_dy = (field[x, y + 1, z].Y - field[x, y - 1, z].Y) / 2.0f;
            float dFz_dz = (field[x, y, z + 1].Z - field[x, y, z - 1].Z) / 2.0f;

            divergence[x, y, z] = dFx_dx + dFy_dy + dFz_dz;
        }

        // Handle boundaries (if necessary) using one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1)
            {
                float dFx_dx = (x < xSize - 1 ? field[x + 1, y, z].X : field[x, y, z].X) - (x > 0 ? field[x - 1, y, z].X : field[x, y, z].X);
                float dFy_dy = (y < ySize - 1 ? field[x, y + 1, z].Y : field[x, y, z].Y) - (y > 0 ? field[x, y - 1, z].Y : field[x, y, z].Y);
                float dFz_dz = (z < zSize - 1 ? field[x, y, z + 1].Z : field[x, y, z].Z) - (z > 0 ? field[x, y, z - 1].Z : field[x, y, z].Z);

                divergence[x, y, z] = dFx_dx + dFy_dy + dFz_dz;
            }
        }

        return divergence;
    }

    public static Vector3[,,] Curl(Vector3[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        Vector3[,,] curl = new Vector3[xSize, ySize, zSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        {
            float dFx_dy = (field[x, y + 1, z].X - field[x, y - 1, z].X) / 2.0f;
            float dFx_dz = (field[x, y, z + 1].X - field[x, y, z - 1].X) / 2.0f;

            float dFz_dx = (field[x + 1, y, z].Z - field[x - 1, y, z].Z) / 2.0f;
            float dFz_dy = (field[x, y + 1, z].Z - field[x, y - 1, z].Z) / 2.0f;

            float dFy_dx = (field[x + 1, y, z].Y - field[x - 1, y, z].Y) / 2.0f;
            float dFy_dz = (field[x, y, z + 1].Y - field[x, y, z - 1].Y) / 2.0f;

            curl[x, y, z] = new Vector3(dFz_dy - dFy_dz, dFx_dz - dFz_dx, dFy_dx - dFx_dy);
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

                float dFy_dx = x < xSize - 1 ? (field[x + 1, y, z].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x - 1, y, z].Y);
                float dFy_dz = z < zSize - 1 ? (field[x, y, z + 1].Y - field[x, y, z].Y) : (field[x, y, z].Y - field[x, y, z - 1].Y);

                float dFz_dx = x < xSize - 1 ? (field[x + 1, y, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x - 1, y, z].Z);
                float dFz_dy = y < ySize - 1 ? (field[x, y + 1, z].Z - field[x, y, z].Z) : (field[x, y, z].Z - field[x, y - 1, z].Z);

                curl[x, y, z] = new Vector3(dFz_dy - dFy_dz, dFx_dz - dFz_dx, dFy_dx - dFx_dy);
            }
        }

        return curl;
    }
}

namespace FieldOperations;

public static partial class FieldOperations
{
    public static (float[,,] gradientX, float[,,] gradientY, float[,,] gradientZ) Gradient(float[,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);

        float[,,] gradientX = new float[xSize, ySize, zSize];
        float[,,] gradientY = new float[xSize, ySize, zSize];
        float[,,] gradientZ = new float[xSize, ySize, zSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        {
            gradientX[x, y, z] = (field[x + 1, y, z] - field[x - 1, y, z]) / 2.0f;
            gradientY[x, y, z] = (field[x, y + 1, z] - field[x, y - 1, z]) / 2.0f;
            gradientZ[x, y, z] = (field[x, y, z + 1] - field[x, y, z - 1]) / 2.0f;
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1)
            {
                gradientX[x, y, z] = x < xSize - 1 ? (field[x + 1, y, z] - field[x, y, z]) : (field[x, y, z] - field[x - 1, y, z]);
                gradientY[x, y, z] = y < ySize - 1 ? (field[x, y + 1, z] - field[x, y, z]) : (field[x, y, z] - field[x, y - 1, z]);
                gradientZ[x, y, z] = z < zSize - 1 ? (field[x, y, z + 1] - field[x, y, z]) : (field[x, y, z] - field[x, y, z - 1]);
            }
        }

        return (gradientX, gradientY, gradientZ);
    }

    [Obsolete("undefined", true)]
    public static void Divergence(float[,,] field) => throw new NotImplementedException();

    [Obsolete("undefined", true)]
    public static void Curl(float[,,] field) => throw new NotImplementedException();
}

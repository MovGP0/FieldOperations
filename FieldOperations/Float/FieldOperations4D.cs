namespace FieldOperations;

public static partial class FieldOperations
{
    public static (float[,,,] gradientX, float[,,,] gradientY, float[,,,] gradientZ, float[,,,] gradientW) Gradient(float[,,,] field)
    {
        int xSize = field.GetLength(0);
        int ySize = field.GetLength(1);
        int zSize = field.GetLength(2);
        int wSize = field.GetLength(3);

        float[,,,] gradientX = new float[xSize, ySize, zSize, wSize];
        float[,,,] gradientY = new float[xSize, ySize, zSize, wSize];
        float[,,,] gradientZ = new float[xSize, ySize, zSize, wSize];
        float[,,,] gradientW = new float[xSize, ySize, zSize, wSize];

        for (int x = 1; x < xSize - 1; x++)
        for (int y = 1; y < ySize - 1; y++)
        for (int z = 1; z < zSize - 1; z++)
        for (int w = 1; w < wSize - 1; w++)
        {
            float dF_dx = (field[x + 1, y, z, w] - field[x - 1, y, z, w]) / 2.0f;
            float dF_dy = (field[x, y + 1, z, w] - field[x, y - 1, z, w]) / 2.0f;
            float dF_dz = (field[x, y, z + 1, w] - field[x, y, z - 1, w]) / 2.0f;
            float dF_dw = (field[x, y, z, w + 1] - field[x, y, z, w - 1]) / 2.0f;

            gradientX[x, y, z, w] = dF_dx;
            gradientY[x, y, z, w] = dF_dy;
            gradientZ[x, y, z, w] = dF_dz;
            gradientW[x, y, z, w] = dF_dw;
        }

        // Handle boundary points with one-sided differences
        for (int x = 0; x < xSize; x++)
        for (int y = 0; y < ySize; y++)
        for (int z = 0; z < zSize; z++)
        for (int w = 0; w < wSize; w++)
        {
            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1 || z == 0 || z == zSize - 1 || w == 0 || w == wSize - 1)
            {
                float dF_dx = x < xSize - 1 ? (field[x + 1, y, z, w] - field[x, y, z, w]) : (field[x, y, z, w] - field[x - 1, y, z, w]);
                float dF_dy = y < ySize - 1 ? (field[x, y + 1, z, w] - field[x, y, z, w]) : (field[x, y, z, w] - field[x, y - 1, z, w]);
                float dF_dz = z < zSize - 1 ? (field[x, y, z + 1, w] - field[x, y, z, w]) : (field[x, y, z, w] - field[x, y, z - 1, w]);
                float dF_dw = w < wSize - 1 ? (field[x, y, z, w + 1] - field[x, y, z, w]) : (field[x, y, z, w] - field[x, y, z, w - 1]);

                gradientX[x, y, z, w] = dF_dx;
                gradientY[x, y, z, w] = dF_dy;
                gradientZ[x, y, z, w] = dF_dz;
                gradientW[x, y, z, w] = dF_dw;
            }
        }

        return (gradientX, gradientY, gradientZ, gradientW);
    }

    [Obsolete("undefined", true)]
    public static void Divergence(float[,,,] field) => throw new NotImplementedException();

    [Obsolete("undefined", true)]
    public static void Curl(float[,,,] field) => throw new NotImplementedException();
}

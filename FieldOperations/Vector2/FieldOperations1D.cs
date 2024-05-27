namespace FieldOperations;

public static partial class FieldOperations
{
    public static Vector2[] Gradient(Vector2[] scalarField)
    {
        int dimX = scalarField.Length;
        Vector2[] gradX = new Vector2[dimX];

        for (int i = 1; i < dimX - 1; i++)
        {
            gradX[i] = (scalarField[i + 1] - scalarField[i - 1]) / 2;
        }

        // Handle boundary conditions if needed, e.g., using one-sided differences.
        gradX[0] = scalarField.Length > 1 ? scalarField[1] - scalarField[0] : Vector2.Zero;
        gradX[dimX - 1] = scalarField.Length > 1 ? scalarField[dimX - 1] - scalarField[dimX - 2] : Vector2.Zero;

        return gradX;
    }

    public static Vector2[] Divergence(Vector2[] field)
    {
        int dimX = field.Length;
        Vector2[] div = new Vector2[dimX];

        for (int i = 1; i < dimX - 1; i++)
        {
            div[i] = (field[i + 1] - field[i - 1]) / 2;
        }

        // Handle boundary conditions if needed, e.g., using one-sided differences.
        div[0] = field.Length > 1 ? field[1] - field[0] : Vector2.Zero;
        div[dimX - 1] = field.Length > 1 ? field[dimX - 1] - field[dimX - 2] : Vector2.Zero;

        return div;
    }

    public static Vector2[] Curl(Vector2[] field)
    {
        int dimX = field.Length;
        Vector2[] curl = new Vector2[dimX];

        for (int i = 0; i < dimX; i++)
        {
            // In 1D, the curl is not defined, so we can return a zero vector for each element.
            curl[i] = Vector2.Zero;
        }

        return curl;
    }
}

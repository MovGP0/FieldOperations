namespace FieldOperations;

public static partial class FieldOperations
{
    /// <summary>
    /// Computes the divergence of a 1D vector field.
    /// </summary>
    public static float[] Divergence(Vector3[] field)
    {
        int size = field.Length;
        float[] divergence = new float[size];

        for (int i = 1; i < size - 1; i++)
        {
            // Central difference for interior points
            float dFx_dx = (field[i + 1].X - field[i - 1].X) / 2.0f;
            divergence[i] = dFx_dx;
        }

        // One-sided difference for the boundaries
        if (size > 1)
        {
            divergence[0] = field[1].X - field[0].X;
            divergence[size - 1] = field[size - 1].X - field[size - 2].X;
        }

        return divergence;
    }

    public static Vector3[] Grad(Vector3[] field)
    {
        int size = field.Length;
        Vector3[] gradient = new Vector3[size];

        for (int i = 1; i < size - 1; i++)
        {
            gradient[i] = (field[i + 1] - field[i - 1]) / 2.0f;
        }

        // Handle boundaries using one-sided differences
        if (size > 1)
        {
            gradient[0] = field[1] - field[0];
            gradient[size - 1] = field[size - 1] - field[size - 2];
        }

        return gradient;
    }
}
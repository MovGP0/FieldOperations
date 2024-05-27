namespace FieldOperations;

public static partial class FieldOperations
{
    /// <summary>
    /// Computes the gradient (derivative) of a scalar field in one dimension.
    /// </summary>
    /// <param name="field">A 1D array representing the scalar field.</param>
    /// <returns>A 1D array representing the gradient (derivative) of the scalar field.</returns>
    public static float[] Gradient(float[] field)
    {
        int length = field.Length;
        float[] gradient = new float[length];

        for (int i = 1; i < length - 1; i++)
        {
            gradient[i] = (field[i + 1] - field[i - 1]) / 2;
        }

        // Handling boundaries with forward and backward differences
        if (length > 1)
        {
            gradient[0] = field[1] - field[0]; // Forward difference for the first element
            gradient[length - 1] = field[length - 1] - field[length - 2]; // Backward difference for the last element
        }

        return gradient;
    }

    [Obsolete("undefined", true)]
    public static void Divergence(float[] field) => throw new NotImplementedException();

    [Obsolete("undefined", true)]
    public static void Curl(float[] field) => throw new NotImplementedException();
}

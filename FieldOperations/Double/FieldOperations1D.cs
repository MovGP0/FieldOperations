namespace FieldOperations;

public static partial class FieldOperations
{
    /// <summary>
    /// Computes the gradient (derivative) of a scalar field in one dimension.
    /// </summary>
    /// <param name="scalarField">A 1D array representing the scalar field.</param>
    /// <returns>A 1D array representing the gradient (derivative) of the scalar field.</returns>
    public static double[] Gradient(double[] scalarField)
    {
        int length = scalarField.Length;
        double[] gradient = new double[length];

        for (int i = 1; i < length - 1; i++)
        {
            gradient[i] = (scalarField[i + 1] - scalarField[i - 1]) / 2;
        }

        // Handling boundaries with forward and backward differences
        if (length > 1)
        {
            gradient[0] = scalarField[1] - scalarField[0]; // Forward difference for the first element
            gradient[length - 1] = scalarField[length - 1] - scalarField[length - 2]; // Backward difference for the last element
        }

        return gradient;
    }
}

namespace FieldOperations;

public static partial class FieldOperations
{
    public static Vector4[] Gradient(Vector4[] field)
    {
        int size = field.Length;

        Vector4[] gradient = new Vector4[size];

        // Handle interior points with central differences
        for (int i = 1; i < size - 1; i++)
        {
            float dFx_dx = (field[i + 1].X - field[i - 1].X) / 2.0f;
            float dFy_dx = (field[i + 1].Y - field[i - 1].Y) / 2.0f;
            float dFz_dx = (field[i + 1].Z - field[i - 1].Z) / 2.0f;
            float dFw_dx = (field[i + 1].W - field[i - 1].W) / 2.0f;

            gradient[i] = new Vector4(dFx_dx, dFy_dx, dFz_dx, dFw_dx);
        }

        // Handle boundary points with one-sided differences
        if (size > 1)
        {
            // For the first point
            gradient[0] = new Vector4(
                (field[1].X - field[0].X),
                (field[1].Y - field[0].Y),
                (field[1].Z - field[0].Z),
                (field[1].W - field[0].W)
            );

            // For the last point
            gradient[size - 1] = new Vector4(
                (field[size - 1].X - field[size - 2].X),
                (field[size - 1].Y - field[size - 2].Y),
                (field[size - 1].Z - field[size - 2].Z),
                (field[size - 1].W - field[size - 2].W)
            );
        }

        return gradient;
    }
}
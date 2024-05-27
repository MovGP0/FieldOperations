namespace FieldOperations;

public static partial class FieldOperations
{
    /// <summary>
    /// Computes the gradient of a scalar field in three dimensions.
    /// </summary>
    /// <param name="scalarField">A 3D array representing the scalar field.</param>
    /// <returns>
    /// A tuple containing three 3D arrays representing the partial derivatives of the scalar field in the x, y, and z directions.
    /// </returns>
    public static (double[,,] gradX, double[,,] gradY, double[,,] gradZ) Gradient(double[,,] scalarField)
    {
        int dimX = scalarField.GetLength(0);
        int dimY = scalarField.GetLength(1);
        int dimZ = scalarField.GetLength(2);
        double[,,] gradX = new double[dimX, dimY, dimZ];
        double[,,] gradY = new double[dimX, dimY, dimZ];
        double[,,] gradZ = new double[dimX, dimY, dimZ];

        for (int i = 1; i < dimX - 1; i++)
        {
            for (int j = 1; j < dimY - 1; j++)
            {
                for (int k = 1; k < dimZ - 1; k++)
                {
                    gradX[i, j, k] = (scalarField[i + 1, j, k] - scalarField[i - 1, j, k]) / 2;
                    gradY[i, j, k] = (scalarField[i, j + 1, k] - scalarField[i, j - 1, k]) / 2;
                    gradZ[i, j, k] = (scalarField[i, j, k + 1] - scalarField[i, j, k - 1]) / 2;
                }
            }
        }

        return (gradX, gradY, gradZ);
    }

    /// <summary>
    /// Computes the divergence of a vector field in three dimensions.
    /// </summary>
    /// <param name="fieldX">A 3D array representing the x-components of the vector field.</param>
    /// <param name="fieldY">A 3D array representing the y-components of the vector field.</param>
    /// <param name="fieldZ">A 3D array representing the z-components of the vector field.</param>
    /// <returns>
    /// A 3D array representing the divergence of the vector field.
    /// </returns>
    public static double[,,] Divergence(double[,,] fieldX, double[,,] fieldY, double[,,] fieldZ)
    {
        int dimX = fieldX.GetLength(0);
        int dimY = fieldX.GetLength(1);
        int dimZ = fieldX.GetLength(2);
        double[,,] div = new double[dimX, dimY, dimZ];

        for (int i = 1; i < dimX - 1; i++)
        {
            for (int j = 1; j < dimY - 1; j++)
            {
                for (int k = 1; k < dimZ - 1; k++)
                {
                    double dFx_dx = (fieldX[i + 1, j, k] - fieldX[i - 1, j, k]) / 2;
                    double dFy_dy = (fieldY[i, j + 1, k] - fieldY[i, j - 1, k]) / 2;
                    double dFz_dz = (fieldZ[i, j, k + 1] - fieldZ[i, j, k - 1]) / 2;
                    div[i, j, k] = dFx_dx + dFy_dy + dFz_dz;
                }
            }
        }

        return div;
    }

    /// <summary>
    /// Computes the curl of a vector field in three dimensions.
    /// </summary>
    /// <param name="fieldX">A 3D array representing the x-components of the vector field.</param>
    /// <param name="fieldY">A 3D array representing the y-components of the vector field.</param>
    /// <param name="fieldZ">A 3D array representing the z-components of the vector field.</param>
    /// <returns>
    /// A tuple containing three 3D arrays representing the curl components of the vector field.
    /// </returns>
    public static (double[,,] curlX, double[,,] curlY, double[,,] curlZ) Curl(double[,,] fieldX, double[,,] fieldY, double[,,] fieldZ)
    {
        int dimX = fieldX.GetLength(0);
        int dimY = fieldX.GetLength(1);
        int dimZ = fieldX.GetLength(2);
        double[,,] curlX = new double[dimX, dimY, dimZ];
        double[,,] curlY = new double[dimX, dimY, dimZ];
        double[,,] curlZ = new double[dimX, dimY, dimZ];

        for (int i = 1; i < dimX - 1; i++)
        {
            for (int j = 1; j < dimY - 1; j++)
            {
                for (int k = 1; k < dimZ - 1; k++)
                {
                    double dFz_dy = (fieldZ[i, j + 1, k] - fieldZ[i, j - 1, k]) / 2;
                    double dFy_dz = (fieldY[i, j, k + 1] - fieldY[i, j, k - 1]) / 2;
                    double dFx_dz = (fieldX[i, j, k + 1] - fieldX[i, j, k - 1]) / 2;
                    double dFz_dx = (fieldZ[i + 1, j, k] - fieldZ[i - 1, j, k]) / 2;
                    double dFy_dx = (fieldY[i + 1, j, k] - fieldY[i - 1, j, k]) / 2;
                    double dFx_dy = (fieldX[i, j + 1, k] - fieldX[i, j - 1, k]) / 2;

                    curlX[i, j, k] = dFz_dy - dFy_dz;
                    curlY[i, j, k] = dFx_dz - dFz_dx;
                    curlZ[i, j, k] = dFy_dx - dFx_dy;
                }
            }
        }

        return (curlX, curlY, curlZ);
    }
}

namespace FieldOperations;

public static partial class FieldOperations
{
    /// <summary>
    /// Computes the gradient of a scalar field in four dimensions.
    /// </summary>
    /// <param name="scalarField">A 4D array representing the scalar field.</param>
    /// <returns>
    /// A tuple containing four 4D arrays representing the partial derivatives of the scalar field in the x, y, z, and w directions.
    /// </returns>
    public static (double[,,,] gradX, double[,,,] gradY, double[,,,] gradZ, double[,,,] gradW) Gradient(double[,,,] scalarField)
    {
        int dimX = scalarField.GetLength(0);
        int dimY = scalarField.GetLength(1);
        int dimZ = scalarField.GetLength(2);
        int dimW = scalarField.GetLength(3);
        double[,,,] gradX = new double[dimX, dimY, dimZ, dimW];
        double[,,,] gradY = new double[dimX, dimY, dimZ, dimW];
        double[,,,] gradZ = new double[dimX, dimY, dimZ, dimW];
        double[,,,] gradW = new double[dimX, dimY, dimZ, dimW];

        for (int i = 1; i < dimX - 1; i++)
        {
            for (int j = 1; j < dimY - 1; j++)
            {
                for (int k = 1; k < dimZ - 1; k++)
                {
                    for (int l = 1; l < dimW - 1; l++)
                    {
                        gradX[i, j, k, l] = (scalarField[i + 1, j, k, l] - scalarField[i - 1, j, k, l]) / 2;
                        gradY[i, j, k, l] = (scalarField[i, j + 1, k, l] - scalarField[i, j - 1, k, l]) / 2;
                        gradZ[i, j, k, l] = (scalarField[i, j, k + 1, l] - scalarField[i, j, k - 1, l]) / 2;
                        gradW[i, j, k, l] = (scalarField[i, j, k, l + 1] - scalarField[i, j, k, l - 1]) / 2;
                    }
                }
            }
        }

        return (gradX, gradY, gradZ, gradW);
    }

    /// <summary>
    /// Computes the divergence of a vector field in four dimensions.
    /// </summary>
    /// <param name="fieldX">A 4D array representing the x-components of the vector field.</param>
    /// <param name="fieldY">A 4D array representing the y-components of the vector field.</param>
    /// <param name="fieldZ">A 4D array representing the z-components of the vector field.</param>
    /// <param name="fieldW">A 4D array representing the w-components of the vector field.</param>
    /// <returns>
    /// A 4D array representing the divergence of the vector field.
    /// </returns>
    public static double[,,,] Divergence(double[,,,] fieldX, double[,,,] fieldY, double[,,,] fieldZ, double[,,,] fieldW)
    {
        int dimX = fieldX.GetLength(0);
        int dimY = fieldX.GetLength(1);
        int dimZ = fieldX.GetLength(2);
        int dimW = fieldX.GetLength(3);
        double[,,,] div = new double[dimX, dimY, dimZ, dimW];

        for (int i = 1; i < dimX - 1; i++)
        {
            for (int j = 1; j < dimY - 1; j++)
            {
                for (int k = 1; k < dimZ - 1; k++)
                {
                    for (int l = 1; l < dimW - 1; l++)
                    {
                        double dFx_dx = (fieldX[i + 1, j, k, l] - fieldX[i - 1, j, k, l]) / 2;
                        double dFy_dy = (fieldY[i, j + 1, k, l] - fieldY[i, j - 1, k, l]) / 2;
                        double dFz_dz = (fieldZ[i, j, k + 1, l] - fieldZ[i, j, k - 1, l]) / 2;
                        double dFw_dw = (fieldW[i, j, k, l + 1] - fieldW[i, j, k, l - 1]) / 2;
                        div[i, j, k, l] = dFx_dx + dFy_dy + dFz_dz + dFw_dw;
                    }
                }
            }
        }

        return div;
    }

    /// <summary>
    /// Computes the curl of a vector field in four dimensions.
    /// </summary>
    /// <param name="fieldX">A 4D array representing the x-components of the vector field.</param>
    /// <param name="fieldY">A 4D array representing the y-components of the vector field.</param>
    /// <param name="fieldZ">A 4D array representing the z-components of the vector field.</param>
    /// <param name="fieldW">A 4D array representing the w-components of the vector field.</param>
    /// <returns>
    /// A tuple containing six 4D arrays representing the components of the curl in the xy, xz, xw, yz, yw, and zw planes.
    /// </returns>
    public static (double[,,,] curlXY, double[,,,] curlXZ, double[,,,] curlXW, double[,,,] curlYZ, double[,,,] curlYW, double[,,,] curlZW) Curl(double[,,,] fieldX, double[,,,] fieldY, double[,,,] fieldZ, double[,,,] fieldW)
    {
        int dimX = fieldX.GetLength(0);
        int dimY = fieldX.GetLength(1);
        int dimZ = fieldX.GetLength(2);
        int dimW = fieldX.GetLength(3);
        double[,,,] curlXY = new double[dimX, dimY, dimZ, dimW];
        double[,,,] curlXZ = new double[dimX, dimY, dimZ, dimW];
        double[,,,] curlXW = new double[dimX, dimY, dimZ, dimW];
        double[,,,] curlYZ = new double[dimX, dimY, dimZ, dimW];
        double[,,,] curlYW = new double[dimX, dimY, dimZ, dimW];
        double[,,,] curlZW = new double[dimX, dimY, dimZ, dimW];

        for (int i = 1; i < dimX - 1; i++)
        {
            for (int j = 1; j < dimY - 1; j++)
            {
                for (int k = 1; k < dimZ - 1; k++)
                {
                    for (int l = 1; l < dimW - 1; l++)
                    {
                        curlXY[i, j, k, l] = (fieldY[i, j + 1, k, l] - fieldY[i, j - 1, k, l] - (fieldX[i, j, k + 1, l] - fieldX[i, j, k - 1, l])) / 2;
                        curlXZ[i, j, k, l] = (fieldZ[i, j, k + 1, l] - fieldZ[i, j, k - 1, l] - (fieldX[i + 1, j, k, l] - fieldX[i - 1, j, k, l])) / 2;
                        curlXW[i, j, k, l] = (fieldW[i, j, k, l + 1] - fieldW[i, j, k, l - 1] - (fieldX[i + 1, j, k, l] - fieldX[i - 1, j, k, l])) / 2;
                        curlYZ[i, j, k, l] = (fieldZ[i, j, k + 1, l] - fieldZ[i, j, k - 1, l] - (fieldY[i + 1, j, k, l] - fieldY[i - 1, j, k, l])) / 2;
                        curlYW[i, j, k, l] = (fieldW[i, j, k, l + 1] - fieldW[i, j, k, l - 1] - (fieldY[i, j + 1, k, l] - fieldY[i, j - 1, k, l])) / 2;
                        curlZW[i, j, k, l] = (fieldW[i, j, k, l + 1] - fieldW[i, j, k, l - 1] - (fieldZ[i, j, k + 1, l] - fieldZ[i, j, k - 1, l])) / 2;
                    }
                }
            }
        }

        return (curlXY, curlXZ, curlXW, curlYZ, curlYW, curlZW);
    }
}

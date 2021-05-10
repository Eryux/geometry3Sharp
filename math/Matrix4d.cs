using System;

namespace g3
{
    public struct Matrix4d
    {
        public Vector4d Row0;
        public Vector4d Row1;
        public Vector4d Row2;
        public Vector4d Row3;

        public Matrix4d(bool bIdentity)
        {
            if (bIdentity)
            {
                Row0 = new Vector4d(1, 0, 0, 0);
                Row1 = new Vector4d(0, 1, 0, 0);
                Row2 = new Vector4d(0, 0, 1, 0);
                Row3 = new Vector4d(0, 0, 0, 1);
            }
            else
            {
                Row0 = Vector4d.Zero;
                Row1 = Vector4d.Zero;
                Row2 = Vector4d.Zero;
                Row3 = Vector4d.Zero;
            }
        }

        public Matrix4d(double[,] mat)
        {
            Row0 = new Vector4d(mat[0, 0], mat[0, 1], mat[0, 2], mat[0, 3]);
            Row1 = new Vector4d(mat[1, 0], mat[1, 1], mat[1, 2], mat[1, 3]);
            Row2 = new Vector4d(mat[2, 0], mat[2, 1], mat[2, 2], mat[2, 3]);
            Row3 = new Vector4d(mat[3, 0], mat[3, 1], mat[3, 2], mat[3, 3]);
        }

        public Matrix4d(double[] mat)
        {
            Row0 = new Vector4d(mat[0], mat[1], mat[2], mat[3]);
            Row1 = new Vector4d(mat[4], mat[5], mat[6], mat[7]);
            Row2 = new Vector4d(mat[8], mat[9], mat[10], mat[11]);
            Row3 = new Vector4d(mat[12], mat[13], mat[14], mat[15]);
        }

        public Matrix4d(Func<int, double> matBufferF)
        {
            Row0 = new Vector4d(matBufferF(0), matBufferF(1), matBufferF(2), matBufferF(3));
            Row1 = new Vector4d(matBufferF(4), matBufferF(5), matBufferF(6), matBufferF(7));
            Row2 = new Vector4d(matBufferF(8), matBufferF(9), matBufferF(10), matBufferF(11));
            Row3 = new Vector4d(matBufferF(12), matBufferF(13), matBufferF(14), matBufferF(15));
        }

        public Matrix4d(Func<int, int, double> matBufferF)
        {
            Row0 = new Vector4d(matBufferF(0, 0), matBufferF(0, 1), matBufferF(0, 2), matBufferF(0, 3));
            Row1 = new Vector4d(matBufferF(1, 0), matBufferF(1, 1), matBufferF(1, 2), matBufferF(1, 3));
            Row2 = new Vector4d(matBufferF(2, 0), matBufferF(2, 1), matBufferF(2, 2), matBufferF(2, 3));
            Row3 = new Vector4d(matBufferF(3, 0), matBufferF(3, 1), matBufferF(3, 2), matBufferF(3, 3));
        }

        public Matrix4d(double m00, double m11, double m22, double m33)
        {
            Row0 = new Vector4d(m00, 0, 0, 0);
            Row1 = new Vector4d(0, m11, 0, 0);
            Row2 = new Vector4d(0, 0, m22, 0);
            Row3 = new Vector4d(0, 0, 0, m33);
        }

        public Matrix4d(Vector4d v1, Vector4d v2, Vector4d v3, Vector4d v4, bool bRows)
        {
            if (bRows)
            {
                Row0 = v1;
                Row1 = v2;
                Row2 = v3;
                Row3 = v4;
            }
            else
            {
                Row0 = new Vector4d(v1.x, v1.y, v1.z, v1.w);
                Row1 = new Vector4d(v2.x, v2.y, v2.z, v2.w);
                Row2 = new Vector4d(v3.x, v3.y, v3.z, v3.w);
                Row3 = new Vector4d(v4.x, v4.y, v4.z, v4.w);
            }
        }

        public Matrix4d(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
        {
            Row0 = new Vector4d(m00, m01, m02, m03);
            Row1 = new Vector4d(m10, m11, m12, m13);
            Row2 = new Vector4d(m20, m21, m22, m23);
            Row3 = new Vector4d(m30, m31, m32, m33);
        }


        public static readonly Matrix4d Identity = new Matrix4d(true);
        public static readonly Matrix4d Zero = new Matrix4d(false);


        public double this[int r, int c]
        {
            get
            {
                return (r == 0) ? Row0[c] : ((r == 1) ? Row1[c] : ((r == 2) ? Row2[c] : Row3[c]));
            }
            set
            {
                if (r == 0) Row0[c] = value;
                else if (r == 1) Row1[c] = value;
                else if (r == 2) Row2[c] = value;
                else Row3[c] = value;
            }
        }

        public double this[int i]
        {
            get
            {
                return (i > 11) ? Row3[i % 3] : ((i > 7) ? Row2[i % 3] : ((i > 3) ? Row1[i % 3] : Row0[i % 3]));
            }
            set
            {
                if (i > 11) Row3[i % 3] = value;
                else if (i > 7) Row2[i % 3] = value;
                else if (i > 3) Row1[i % 3] = value;
                else Row0[i % 3] = value;
            }
        }


        public Vector4d Row(int i)
        {
            return (i == 0) ? Row0 : ((i == 1) ? Row1 : ((i == 2) ? Row2 : Row3));
        }

        public Vector4d Column(int i)
        {
            if (i == 0) return new Vector4d(Row0.x, Row1.x, Row2.x, Row3.x);
            else if (i == 1) return new Vector4d(Row0.y, Row1.y, Row2.y, Row3.y);
            else if (i == 2) return new Vector4d(Row0.z, Row1.z, Row2.z, Row3.z);
            else return new Vector4d(Row0.w, Row1.w, Row2.w, Row3.w);
        }


        public double[] ToBuffer()
        {
            return new double[16] {
                Row0.x, Row0.y, Row0.z, Row0.w,
                Row1.x, Row1.y, Row1.z, Row1.w,
                Row2.x, Row2.y, Row2.z, Row2.w,
                Row3.x, Row3.y, Row3.z, Row3.w
            };
        }

        public void ToBuffer(double[] buf)
        {
            buf[0] = Row0.x; buf[1] = Row0.y; buf[2] = Row0.z; buf[3] = Row0.w;
            buf[4] = Row1.x; buf[5] = Row1.y; buf[6] = Row1.z; buf[7] = Row1.w;
            buf[8] = Row2.x; buf[9] = Row2.y; buf[10] = Row2.z; buf[11] = Row2.w;
            buf[12] = Row3.x; buf[13] = Row3.y; buf[14] = Row3.z; buf[15] = Row3.w;
        }


        public static Matrix4d operator *(Matrix4d mat, double f)
        {
            return new Matrix4d(
                mat.Row0.x * f, mat.Row0.y * f, mat.Row0.z * f, mat.Row0.w * f,
                mat.Row1.x * f, mat.Row1.y * f, mat.Row1.z * f, mat.Row1.w * f,
                mat.Row2.x * f, mat.Row2.y * f, mat.Row2.z * f, mat.Row2.w * f,
                mat.Row3.x * f, mat.Row3.y * f, mat.Row3.z * f, mat.Row3.w * f
            );
        }

        public static Matrix4d operator *(double f, Matrix4d mat)
        {
            return mat * f;
        }


        public static Vector4d operator *(Matrix4d mat, Vector4d v)
        {
            return new Vector4d(
                mat.Row0.x * v.x + mat.Row0.y * v.y + mat.Row0.z * v.z + mat.Row0.w * v.w,
                mat.Row1.x * v.x + mat.Row1.y * v.y + mat.Row1.z * v.z + mat.Row1.w * v.w,
                mat.Row2.x * v.x + mat.Row2.y * v.y + mat.Row2.z * v.z + mat.Row2.w * v.w,
                mat.Row3.x * v.x + mat.Row3.y * v.y + mat.Row3.z * v.z + mat.Row3.w * v.w
            );
        }

        public Vector4d Multiply(ref Vector4d v)
        {
            return new Vector4d(
                Row0.x * v.x + Row0.y * v.y + Row0.z * v.z + Row0.w * v.w,
                Row1.x * v.x + Row1.y * v.y + Row1.z * v.z + Row1.w * v.w,
                Row2.x * v.x + Row2.y * v.y + Row2.z * v.z + Row2.w * v.w,
                Row3.x * v.x + Row3.y * v.y + Row3.z * v.z + Row3.w * v.w
            );
        }

        public void Multiply(ref Vector4d v, ref Vector4d vOut)
        {
            vOut.x = Row0.x * v.x + Row0.y * v.y + Row0.z * v.z + Row0.w * v.w;
            vOut.y = Row1.x * v.x + Row1.y * v.y + Row1.z * v.z + Row1.w * v.w;
            vOut.z = Row2.x * v.x + Row2.y * v.y + Row2.z * v.z + Row2.w * v.w;
            vOut.w = Row3.x * v.x + Row3.y * v.y + Row3.z * v.z + Row3.w * v.w;
        }


        public static Matrix4d operator *(Matrix4d mat1, Matrix4d mat2)
        {
            double m00 = mat1.Row0.x * mat2.Row0.x + mat1.Row0.y * mat2.Row1.x + mat1.Row0.z * mat2.Row2.x + mat1.Row0.w * mat2.Row3.x;
            double m01 = mat1.Row0.x * mat2.Row0.y + mat1.Row0.y * mat2.Row1.y + mat1.Row0.z * mat2.Row2.y + mat1.Row0.w * mat2.Row3.y;
            double m02 = mat1.Row0.x * mat2.Row0.z + mat1.Row0.y * mat2.Row1.z + mat1.Row0.z * mat2.Row2.z + mat1.Row0.w * mat2.Row3.z;
            double m03 = mat1.Row0.x * mat2.Row0.w + mat1.Row0.y * mat2.Row1.w + mat1.Row0.z * mat2.Row2.w + mat1.Row0.w * mat2.Row3.w;

            double m10 = mat1.Row1.x * mat2.Row0.x + mat1.Row1.y * mat2.Row1.x + mat1.Row1.z * mat2.Row2.x + mat1.Row1.w * mat2.Row3.x;
            double m11 = mat1.Row1.x * mat2.Row0.y + mat1.Row1.y * mat2.Row1.y + mat1.Row1.z * mat2.Row2.y + mat1.Row1.w * mat2.Row3.y;
            double m12 = mat1.Row1.x * mat2.Row0.z + mat1.Row1.y * mat2.Row1.z + mat1.Row1.z * mat2.Row2.z + mat1.Row1.w * mat2.Row3.z;
            double m13 = mat1.Row1.x * mat2.Row0.w + mat1.Row1.y * mat2.Row1.w + mat1.Row1.z * mat2.Row2.w + mat1.Row1.w * mat2.Row3.w;

            double m20 = mat1.Row2.x * mat2.Row0.x + mat1.Row2.y * mat2.Row1.x + mat1.Row2.z * mat2.Row2.x + mat1.Row2.w * mat2.Row3.x;
            double m21 = mat1.Row2.x * mat2.Row0.y + mat1.Row2.y * mat2.Row1.y + mat1.Row2.z * mat2.Row2.y + mat1.Row2.w * mat2.Row3.x;
            double m22 = mat1.Row2.x * mat2.Row0.z + mat1.Row2.y * mat2.Row1.z + mat1.Row2.z * mat2.Row2.z + mat1.Row2.w * mat2.Row3.x;
            double m23 = mat1.Row2.x * mat2.Row0.w + mat1.Row2.y * mat2.Row1.w + mat1.Row2.z * mat2.Row2.w + mat1.Row2.w * mat2.Row3.w;

            double m30 = mat1.Row3.x * mat2.Row0.x + mat1.Row3.y * mat2.Row1.x + mat1.Row3.z * mat2.Row2.x + mat1.Row3.w * mat2.Row3.x;
            double m31 = mat1.Row3.x * mat2.Row0.y + mat1.Row3.y * mat2.Row1.y + mat1.Row3.z * mat2.Row2.y + mat1.Row3.w * mat2.Row3.x;
            double m32 = mat1.Row3.x * mat2.Row0.z + mat1.Row3.y * mat2.Row1.z + mat1.Row3.z * mat2.Row2.z + mat1.Row3.w * mat2.Row3.x;
            double m33 = mat1.Row3.x * mat2.Row0.w + mat1.Row3.y * mat2.Row1.w + mat1.Row3.z * mat2.Row2.w + mat1.Row3.w * mat2.Row3.w;

            return new Matrix4d(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
        }


        public static Matrix4d operator +(Matrix4d mat1, Matrix4d mat2)
        {
            return new Matrix4d(
                mat1.Row0 + mat2.Row0, mat1.Row1 + mat2.Row1, mat1.Row2 + mat2.Row2, mat1.Row3 + mat2.Row3, true
            );
        }
        public static Matrix4d operator -(Matrix4d mat1, Matrix4d mat2)
        {
            return new Matrix4d(
                mat1.Row0 - mat2.Row0, mat1.Row1 - mat2.Row1, mat1.Row2 - mat2.Row2, mat1.Row3 - mat2.Row3, true
            );
        }


        public double Determinant
        {
            get
            {
                double m00 = Row0.x; double m01 = Row0.y; double m02 = Row0.z; double m03 = Row0.w;
                double m10 = Row1.x; double m11 = Row1.y; double m12 = Row1.z; double m13 = Row1.w;
                double m20 = Row2.x; double m21 = Row2.y; double m22 = Row2.z; double m23 = Row2.w;
                double m30 = Row3.x; double m31 = Row3.y; double m32 = Row3.z; double m33 = Row3.w;

                return
                    (m00 * m11 * m22 * m33) - (m00 * m11 * m23 * m32) + (m00 * m12 * m23 * m31) - (m00 * m12 * m21 * m33)
                    + (m00 * m13 * m21 * m32) - (m00 * m13 * m22 * m31) - (m01 * m12 * m23 * m30) + (m01 * m12 * m20 * m33)
                    - (m01 * m13 * m20 * m32) + (m01 * m13 * m22 * m30) - (m01 * m10 * m22 * m33) + (m01 * m10 * m23 * m32)
                                                                                            + (m02 * m13 * m20 * m31) -
                    (m02 * m13 * m21 * m30) + (m02 * m10 * m21 * m33) - (m02 * m10 * m23 * m31)
                    + (m02 * m11 * m23 * m30) - (m02 * m11 * m20 * m33) - (m03 * m10 * m21 * m32) + (m03 * m10 * m22 * m31)
                    - (m03 * m11 * m22 * m30) + (m03 * m11 * m20 * m32) - (m03 * m12 * m20 * m31) + (m03 * m12 * m21 * m30);
            }
        }


        public Matrix4d Transpose()
        {
            return new Matrix4d(
                Row0.x, Row1.x, Row2.x, Row3.x,
                Row0.y, Row1.y, Row2.y, Row3.y,
                Row0.z, Row1.z, Row2.z, Row3.z,
                Row0.w, Row1.w, Row2.w, Row3.w
            );
        }


        public Matrix4d Inverse()
        {
            double m00 = Row0.x; double m01 = Row0.y; double m02 = Row0.z; double m03 = Row0.w;
            double m10 = Row1.x; double m11 = Row1.y; double m12 = Row1.z; double m13 = Row1.w;
            double m20 = Row2.x; double m21 = Row2.y; double m22 = Row2.z; double m23 = Row2.w;
            double m30 = Row3.x; double m31 = Row3.y; double m32 = Row3.z; double m33 = Row3.w;

            double i00 = m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m21 * m13 * m32 + m31 * m12 * m23 - m31 * m13 * m22;
            double i01 = -m01 * m22 * m33 + m01 * m23 * m32 + m21 * m02 * m33 - m21 * m03 * m32 - m31 * m02 * m23 + m31 * m03 * m22;
            double i02 = m01 * m12 * m33 - m01 * m13 * m32 - m11 * m02 * m33 + m11 * m03 * m32 + m31 * m02 * m13 - m31 * m03 * m12;
            double i03 = -m01 * m12 * m23 + m01 * m13 * m22 + m11 * m02 * m23 - m11 * m03 * m22 - m21 * m02 * m13 + m21 * m03 * m12;

            double i10 = -m10 * m22 * m33 + m10 * m23 * m32 + m20 * m12 * m33 - m20 * m13 * m32 - m30 * m12 * m23 + m30 * m13 * m22;
            double i11 = m00 * m22 * m33 - m00 * m23 * m32 - m20 * m02 * m33 + m20 * m03 * m32 + m30 * m02 * m23 - m30 * m03 * m22;
            double i12 = -m00 * m12 * m33 + m00 * m13 * m32 + m10 * m02 * m33 - m10 * m03 * m32 - m30 * m02 * m13 + m30 * m03 * m12;
            double i13 = m00 * m12 * m23 - m00 * m13 * m22 - m10 * m02 * m23 + m10 * m03 * m22 + m20 * m02 * m13 - m20 * m03 * m12;

            double i20 = m10 * m21 * m33 - m10 * m23 * m31 - m20 * m11 * m33 + m20 * m13 * m31 + m30 * m11 * m23 - m30 * m13 * m21;
            double i21 = -m00 * m21 * m33 + m00 * m23 * m31 + m20 * m01 * m33 - m20 * m03 * m31 - m30 * m01 * m23 + m30 * m03 * m21;
            double i22 = m00 * m11 * m33 - m00 * m13 * m31 - m10 * m01 * m33 + m10 * m03 * m31 + m30 * m01 * m13 - m30 * m03 * m11;
            double i23 = -m00 * m11 * m23 + m00 * m13 * m21 + m10 * m01 * m23 - m10 * m03 * m21 - m20 * m01 * m13 + m20 * m03 * m11;

            double i30 = -m10 * m21 * m32 + m10 * m22 * m31 + m20 * m11 * m32 - m20 * m12 * m31 - m30 * m11 * m22 + m30 * m12 * m21;
            double i31 = m00 * m21 * m32 - m00 * m22 * m31 - m20 * m01 * m32 + m20 * m02 * m31 + m30 * m01 * m22 - m30 * m02 * m21;
            double i32 = -m00 * m11 * m32 + m00 * m12 * m31 + m10 * m01 * m32 - m10 * m02 * m31 - m30 * m01 * m12 + m30 * m02 * m11;
            double i33 = m00 * m11 * m22 - m00 * m12 * m21 - m10 * m01 * m22 + m10 * m02 * m21 + m20 * m01 * m12 - m20 * m02 * m11;

            double det = m00 * i00 + m01 * i10 + m02 * i20 + m03 * i30;

            if (Math.Abs(det) < double.Epsilon)
            {
                throw new Exception("Matrix4d.Inverse: matrix is not invertible");
            }

            det = 1.0f / det;

            return new Matrix4d(
                i00 * det, i01 * det, i02 * det, i03 * det,
                i10 * det, i11 * det, i12 * det, i13 * det,
                i20 * det, i21 * det, i22 * det, i23 * det,
                i30 * det, i31 * det, i32 * det, i33 * det
            );
        }


        public bool EpsilonEqual(Matrix4d mat, double epsilon)
        {
            return Row0.EpsilonEqual(mat.Row0, epsilon)
                && Row1.EpsilonEqual(mat.Row1, epsilon)
                && Row2.EpsilonEqual(mat.Row2, epsilon)
                && Row3.EpsilonEqual(mat.Row3, epsilon);
        }


        public override string ToString()
        {
            return string.Format("[{0}] [{1}] [{2}] [{3}]", Row0, Row1, Row2, Row3);
        }

        public string ToString(string fmt)
        {
            return string.Format("[{0}] [{1}] [{2}] [{3}]", Row0.ToString(fmt), Row1.ToString(fmt), Row2.ToString(fmt), Row3.ToString(fmt));
        }
    }
}
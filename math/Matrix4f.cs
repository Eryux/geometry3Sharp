using System;

namespace g3
{
    public struct Matrix4f
    {
        public Vector4f Row0;
        public Vector4f Row1;
        public Vector4f Row2;
        public Vector4f Row3;

        public Matrix4f(bool bIdentity)
        {
            if (bIdentity) 
            {
                Row0 = new Vector4f(1f, 0f, 0f, 0f);
                Row1 = new Vector4f(0f, 1f, 0f, 0f);
                Row2 = new Vector4f(0f, 0f, 1f, 0f);
                Row3 = new Vector4f(0f, 0f, 0f, 1f);
            } 
            else
            {
                Row0 = Vector4f.Zero;
                Row1 = Vector4f.Zero; 
                Row2 = Vector4f.Zero; 
                Row3 = Vector4f.Zero;
            }
        }

        public Matrix4f(float[,] mat)
        {
            Row0 = new Vector4f(mat[0, 0], mat[0, 1], mat[0, 2], mat[0, 3]);
            Row1 = new Vector4f(mat[1, 0], mat[1, 1], mat[1, 2], mat[1, 3]);
            Row2 = new Vector4f(mat[2, 0], mat[2, 1], mat[2, 2], mat[2, 3]);
            Row3 = new Vector4f(mat[3, 0], mat[3, 1], mat[3, 2], mat[3, 3]);
        }

        public Matrix4f(float[] mat)
        {
            Row0 = new Vector4f(mat[0], mat[1], mat[2], mat[3]);
            Row1 = new Vector4f(mat[4], mat[5], mat[6], mat[7]);
            Row2 = new Vector4f(mat[8], mat[9], mat[10], mat[11]);
            Row3 = new Vector4f(mat[12], mat[13], mat[14], mat[15]);
        }

        public Matrix4f(Func<int, float> matBufferF)
        {
            Row0 = new Vector4f(matBufferF(0), matBufferF(1), matBufferF(2), matBufferF(3));
            Row1 = new Vector4f(matBufferF(4), matBufferF(5), matBufferF(6), matBufferF(7));
            Row2 = new Vector4f(matBufferF(8), matBufferF(9), matBufferF(10), matBufferF(11));
            Row3 = new Vector4f(matBufferF(12), matBufferF(13), matBufferF(14), matBufferF(15));
        }

        public Matrix4f(Func<int, int, float> matBufferF)
        {
            Row0 = new Vector4f(matBufferF(0, 0), matBufferF(0, 1), matBufferF(0, 2), matBufferF(0, 3));
            Row1 = new Vector4f(matBufferF(1, 0), matBufferF(1, 1), matBufferF(1, 2), matBufferF(1, 3));
            Row2 = new Vector4f(matBufferF(2, 0), matBufferF(2, 1), matBufferF(2, 2), matBufferF(2, 3));
            Row3 = new Vector4f(matBufferF(3, 0), matBufferF(3, 1), matBufferF(3, 2), matBufferF(3, 3));
        }

        public Matrix4f(float m00, float m11, float m22, float m33)
        {
            Row0 = new Vector4f(m00, 0f, 0f, 0f);
            Row1 = new Vector4f(0f, m11, 0f, 0f);
            Row2 = new Vector4f(0f, 0f, m22, 0f);
            Row3 = new Vector4f(0f, 0f, 0f, m33);
        }

        public Matrix4f(Vector4f v1, Vector4f v2, Vector4f v3, Vector4f v4, bool bRows)
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
                Row0 = new Vector4f(v1.x, v1.y, v1.z, v1.w);
                Row1 = new Vector4f(v2.x, v2.y, v2.z, v2.w);
                Row2 = new Vector4f(v3.x, v3.y, v3.z, v3.w);
                Row3 = new Vector4f(v4.x, v4.y, v4.z, v4.w);
            }
        }

        public Matrix4f(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
        {
            Row0 = new Vector4f(m00, m01, m02, m03);
            Row1 = new Vector4f(m10, m11, m12, m13);
            Row2 = new Vector4f(m20, m21, m22, m23);
            Row3 = new Vector4f(m30, m31, m32, m33);
        }


        public static readonly Matrix4f Identity = new Matrix4f(true);
        public static readonly Matrix4f Zero = new Matrix4f(false);


        public float this[int r, int c]
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

        public float this[int i]
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


        public Vector4f Row(int i)
        {
            return (i == 0) ? Row0 : ((i == 1) ? Row1 : ((i == 2) ? Row2 : Row3));
        }

        public Vector4f Column(int i)
        {
            if (i == 0) return new Vector4f(Row0.x, Row1.x, Row2.x, Row3.x);
            else if (i == 1) return new Vector4f(Row0.y, Row1.y, Row2.y, Row3.y);
            else if (i == 2) return new Vector4f(Row0.z, Row1.z, Row2.z, Row3.z);
            else return new Vector4f(Row0.w, Row1.w, Row2.w, Row3.w);
        }


        public float[] ToBuffer()
        {
            return new float[16] {
                Row0.x, Row0.y, Row0.z, Row0.w,
                Row1.x, Row1.y, Row1.z, Row1.w,
                Row2.x, Row2.y, Row2.z, Row2.w,
                Row3.x, Row3.y, Row3.z, Row3.w
            };
        }

        public void ToBuffer(float[] buf)
        {
            buf[0] = Row0.x; buf[1] = Row0.y; buf[2] = Row0.z; buf[3] = Row0.w;
            buf[4] = Row1.x; buf[5] = Row1.y; buf[6] = Row1.z; buf[7] = Row1.w;
            buf[8] = Row2.x; buf[9] = Row2.y; buf[10] = Row2.z; buf[11] = Row2.w;
            buf[12] = Row3.x; buf[13] = Row3.y; buf[14] = Row3.z; buf[15] = Row3.w;
        }


        public static Matrix4f operator *(Matrix4f mat, float f)
        {
            return new Matrix4f(
                mat.Row0.x * f, mat.Row0.y * f, mat.Row0.z * f, mat.Row0.w * f,
                mat.Row1.x * f, mat.Row1.y * f, mat.Row1.z * f, mat.Row1.w * f,
                mat.Row2.x * f, mat.Row2.y * f, mat.Row2.z * f, mat.Row2.w * f,
                mat.Row3.x * f, mat.Row3.y * f, mat.Row3.z * f, mat.Row3.w * f
            );
        }

        public static Matrix4f operator *(float f, Matrix4f mat)
        {
            return mat * f;
        }


        public static Vector4f operator *(Matrix4f mat, Vector4f v)
        {
            return new Vector4f(
                mat.Row0.x * v.x + mat.Row0.y * v.y + mat.Row0.z * v.z + mat.Row0.w * v.w,
                mat.Row1.x * v.x + mat.Row1.y * v.y + mat.Row1.z * v.z + mat.Row1.w * v.w,
                mat.Row2.x * v.x + mat.Row2.y * v.y + mat.Row2.z * v.z + mat.Row2.w * v.w,
                mat.Row3.x * v.x + mat.Row3.y * v.y + mat.Row3.z * v.z + mat.Row3.w * v.w
            );
        }

        public Vector4f Multiply(ref Vector4f v)
        {
            return new Vector4f(
                Row0.x * v.x + Row0.y * v.y + Row0.z * v.z + Row0.w * v.w,
                Row1.x * v.x + Row1.y * v.y + Row1.z * v.z + Row1.w * v.w,
                Row2.x * v.x + Row2.y * v.y + Row2.z * v.z + Row2.w * v.w,
                Row3.x * v.x + Row3.y * v.y + Row3.z * v.z + Row3.w * v.w
            );
        }

        public void Multiply(ref Vector4f v, ref Vector4f vOut)
        {
            vOut.x = Row0.x * v.x + Row0.y * v.y + Row0.z * v.z + Row0.w * v.w;
            vOut.y = Row1.x * v.x + Row1.y * v.y + Row1.z * v.z + Row1.w * v.w;
            vOut.z = Row2.x * v.x + Row2.y * v.y + Row2.z * v.z + Row2.w * v.w;
            vOut.w = Row3.x * v.x + Row3.y * v.y + Row3.z * v.z + Row3.w * v.w;
        }


        public static Matrix4f operator *(Matrix4f mat1, Matrix4f mat2)
        {
            float m00 = mat1.Row0.x * mat2.Row0.x + mat1.Row0.y * mat2.Row1.x + mat1.Row0.z * mat2.Row2.x + mat1.Row0.w * mat2.Row3.x;
            float m01 = mat1.Row0.x * mat2.Row0.y + mat1.Row0.y * mat2.Row1.y + mat1.Row0.z * mat2.Row2.y + mat1.Row0.w * mat2.Row3.y;
            float m02 = mat1.Row0.x * mat2.Row0.z + mat1.Row0.y * mat2.Row1.z + mat1.Row0.z * mat2.Row2.z + mat1.Row0.w * mat2.Row3.z;
            float m03 = mat1.Row0.x * mat2.Row0.w + mat1.Row0.y * mat2.Row1.w + mat1.Row0.z * mat2.Row2.w + mat1.Row0.w * mat2.Row3.w;

            float m10 = mat1.Row1.x * mat2.Row0.x + mat1.Row1.y * mat2.Row1.x + mat1.Row1.z * mat2.Row2.x + mat1.Row1.w * mat2.Row3.x;
            float m11 = mat1.Row1.x * mat2.Row0.y + mat1.Row1.y * mat2.Row1.y + mat1.Row1.z * mat2.Row2.y + mat1.Row1.w * mat2.Row3.y;
            float m12 = mat1.Row1.x * mat2.Row0.z + mat1.Row1.y * mat2.Row1.z + mat1.Row1.z * mat2.Row2.z + mat1.Row1.w * mat2.Row3.z;
            float m13 = mat1.Row1.x * mat2.Row0.w + mat1.Row1.y * mat2.Row1.w + mat1.Row1.z * mat2.Row2.w + mat1.Row1.w * mat2.Row3.w;

            float m20 = mat1.Row2.x * mat2.Row0.x + mat1.Row2.y * mat2.Row1.x + mat1.Row2.z * mat2.Row2.x + mat1.Row2.w * mat2.Row3.x;
            float m21 = mat1.Row2.x * mat2.Row0.y + mat1.Row2.y * mat2.Row1.y + mat1.Row2.z * mat2.Row2.y + mat1.Row2.w * mat2.Row3.x;
            float m22 = mat1.Row2.x * mat2.Row0.z + mat1.Row2.y * mat2.Row1.z + mat1.Row2.z * mat2.Row2.z + mat1.Row2.w * mat2.Row3.x;
            float m23 = mat1.Row2.x * mat2.Row0.w + mat1.Row2.y * mat2.Row1.w + mat1.Row2.z * mat2.Row2.w + mat1.Row2.w * mat2.Row3.w;

            float m30 = mat1.Row3.x * mat2.Row0.x + mat1.Row3.y * mat2.Row1.x + mat1.Row3.z * mat2.Row2.x + mat1.Row3.w * mat2.Row3.x;
            float m31 = mat1.Row3.x * mat2.Row0.y + mat1.Row3.y * mat2.Row1.y + mat1.Row3.z * mat2.Row2.y + mat1.Row3.w * mat2.Row3.x;
            float m32 = mat1.Row3.x * mat2.Row0.z + mat1.Row3.y * mat2.Row1.z + mat1.Row3.z * mat2.Row2.z + mat1.Row3.w * mat2.Row3.x;
            float m33 = mat1.Row3.x * mat2.Row0.w + mat1.Row3.y * mat2.Row1.w + mat1.Row3.z * mat2.Row2.w + mat1.Row3.w * mat2.Row3.w;

            return new Matrix4f(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
        }


        public static Matrix4f operator +(Matrix4f mat1, Matrix4f mat2)
        {
            return new Matrix4f(
                mat1.Row0 + mat2.Row0, mat1.Row1 + mat2.Row1, mat1.Row2 + mat2.Row2, mat1.Row3 + mat2.Row3, true
            );
        }
        public static Matrix4f operator -(Matrix4f mat1, Matrix4f mat2)
        {
            return new Matrix4f(
                mat1.Row0 - mat2.Row0, mat1.Row1 - mat2.Row1, mat1.Row2 - mat2.Row2, mat1.Row3 - mat2.Row3, true
            );
        }


        public float Determinant
        {
            get
            {
                float m00 = Row0.x; float m01 = Row0.y; float m02 = Row0.z; float m03 = Row0.w;
                float m10 = Row1.x; float m11 = Row1.y; float m12 = Row1.z; float m13 = Row1.w;
                float m20 = Row2.x; float m21 = Row2.y; float m22 = Row2.z; float m23 = Row2.w;
                float m30 = Row3.x; float m31 = Row3.y; float m32 = Row3.z; float m33 = Row3.w;

                return
                    (m00* m11 * m22 * m33) - (m00* m11 * m23 * m32) + (m00* m12 * m23 * m31) - (m00* m12 * m21 * m33)
                    + (m00* m13 * m21 * m32) - (m00* m13 * m22 * m31) - (m01* m12 * m23 * m30) + (m01* m12 * m20 * m33)
                    - (m01* m13 * m20 * m32) + (m01* m13 * m22 * m30) - (m01* m10 * m22 * m33) + (m01* m10 * m23 * m32)
                                                                                            + (m02* m13 * m20 * m31) -
                    (m02* m13 * m21 * m30) + (m02* m10 * m21 * m33) - (m02* m10 * m23 * m31)
                    + (m02* m11 * m23 * m30) - (m02* m11 * m20 * m33) - (m03* m10 * m21 * m32) + (m03* m10 * m22 * m31)
                    - (m03* m11 * m22 * m30) + (m03* m11 * m20 * m32) - (m03* m12 * m20 * m31) + (m03* m12 * m21 * m30);
            }
        }


        public Matrix4f Transpose()
        {
            return new Matrix4f(
                Row0.x, Row1.x, Row2.x, Row3.x,
                Row0.y, Row1.y, Row2.y, Row3.y,
                Row0.z, Row1.z, Row2.z, Row3.z,
                Row0.w, Row1.w, Row2.w, Row3.w
            );
        }


        public Matrix4f Inverse()
        {
            float m00 = Row0.x; float m01 = Row0.y; float m02 = Row0.z; float m03 = Row0.w;
            float m10 = Row1.x; float m11 = Row1.y; float m12 = Row1.z; float m13 = Row1.w;
            float m20 = Row2.x; float m21 = Row2.y; float m22 = Row2.z; float m23 = Row2.w;
            float m30 = Row3.x; float m31 = Row3.y; float m32 = Row3.z; float m33 = Row3.w;

            float i00 = m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m21 * m13 * m32 + m31 * m12 * m23 - m31 * m13 * m22;
            float i01 = -m01 * m22 * m33 + m01 * m23 * m32 + m21 * m02 * m33 - m21 * m03 * m32 - m31 * m02 * m23 + m31 * m03 * m22;
            float i02 = m01 * m12 * m33 - m01 * m13 * m32 - m11 * m02 * m33 + m11 * m03 * m32 + m31 * m02 * m13 - m31 * m03 * m12;
            float i03 = -m01 * m12 * m23 + m01 * m13 * m22 + m11 * m02 * m23 - m11 * m03 * m22 - m21 * m02 * m13 + m21 * m03 * m12;

            float i10 = -m10 * m22 * m33 + m10 * m23 * m32 + m20 * m12 * m33 - m20 * m13 * m32 - m30 * m12 * m23 + m30 * m13 * m22;
            float i11 = m00 * m22 * m33 - m00 * m23 * m32 - m20 * m02 * m33 + m20 * m03 * m32 + m30 * m02 * m23 - m30 * m03 * m22;
            float i12 = -m00 * m12 * m33 + m00 * m13 * m32 + m10 * m02 * m33 - m10 * m03 * m32 - m30 * m02 * m13 + m30 * m03 * m12;
            float i13 = m00 * m12 * m23 - m00 * m13 * m22 - m10 * m02 * m23 + m10 * m03 * m22 + m20 * m02 * m13 - m20 * m03 * m12;

            float i20 = m10 * m21 * m33 - m10 * m23 * m31 - m20 * m11 * m33 + m20 * m13 * m31 + m30 * m11 * m23 - m30 * m13 * m21;
            float i21 = -m00 * m21 * m33 + m00 * m23 * m31 + m20 * m01 * m33 - m20 * m03 * m31 - m30 * m01 * m23 + m30 * m03 * m21;
            float i22 = m00 * m11 * m33 - m00 * m13 * m31 - m10 * m01 * m33 + m10 * m03 * m31 + m30 * m01 * m13 - m30 * m03 * m11;
            float i23 = -m00 * m11 * m23 + m00 * m13 * m21 + m10 * m01 * m23 - m10 * m03 * m21 - m20 * m01 * m13 + m20 * m03 * m11;

            float i30 = -m10 * m21 * m32 + m10 * m22 * m31 + m20 * m11 * m32 - m20 * m12 * m31 - m30 * m11 * m22 + m30 * m12 * m21;
            float i31 = m00 * m21 * m32 - m00 * m22 * m31 - m20 * m01 * m32 + m20 * m02 * m31 + m30 * m01 * m22 - m30 * m02 * m21;
            float i32 = -m00 * m11 * m32 + m00 * m12 * m31 + m10 * m01 * m32 - m10 * m02 * m31 - m30 * m01 * m12 + m30 * m02 * m11;
            float i33 = m00 * m11 * m22 - m00 * m12 * m21 - m10 * m01 * m22 + m10 * m02 * m21 + m20 * m01 * m12 - m20 * m02 * m11;

            float det = m00 * i00 + m01 * i10 + m02 * i20 + m03 * i30;

            if (Math.Abs(det) < float.Epsilon)
            {
                throw new Exception("Matrix4f.Inverse: matrix is not invertible");
            }

            det = 1.0f / det;

            return new Matrix4f(
                i00 * det, i01 * det, i02 * det, i03 * det,
                i10 * det, i11 * det, i12 * det, i13 * det,
                i20 * det, i21 * det, i22 * det, i23 * det,
                i30 * det, i31 * det, i32 * det, i33 * det
            );
        }


        public bool EpsilonEqual(Matrix4f mat, float epsilon)
        {
            return Row0.EpsilonEqual(mat.Row0, epsilon)
                && Row1.EpsilonEqual(mat.Row1, epsilon)
                && Row2.EpsilonEqual(mat.Row2, epsilon)
                && Row3.EpsilonEqual(mat.Row3, epsilon);
        }


        public void Normalize()
        {
            float d = Determinant;
            Row0 /= d; Row1 /= d; Row2 /= d; Row3 /= d;
        }

        public Matrix4f Normalized()
        {
            Matrix4f m = this;
            m.Normalize();
            return m;
        }


        public static Matrix4f CreateTranslation(Vector3f v)
        {
            Matrix4f m = Identity;
            m.Row3.x = v.x; m.Row3.y = v.y; m.Row3.z = v.z;
            return m;
        }

        public Vector3f ExtractTranslation()
        {
            return new Vector3f(Row3.x, Row3.y, Row3.z);
        }

        public Matrix4f ClearTranslation()
        {
            Matrix4f m = this;
            m.Row3.x = 0f; m.Row3.y = 0f; m.Row3.z = 0f;
            return m;
        }


        public static Matrix4f CreateScale(float scale)
        {
            Matrix4f m = Identity;
            m.Row0.x = scale; m.Row1.y = scale; m.Row2.z = scale;
            return m;
        }

        public static Matrix4f CreateScale(Vector3f scale)
        {
            Matrix4f m = Identity;
            m.Row0.x = scale.x; m.Row1.y = scale.y; m.Row2.z = scale.z;
            return m;
        }

        public Vector3f ExtractScale()
        {
            Vector3f Row0xyz = new Vector3f(Row0.x, Row0.y, Row0.z);
            Vector3f Row1xyz = new Vector3f(Row1.x, Row1.y, Row1.z);
            Vector3f Row2xyz = new Vector3f(Row2.x, Row2.y, Row2.z);
            return new Vector3f(Row0xyz.Length, Row1xyz.Length, Row2xyz.Length);
        }

        public Matrix4f ClearScale()
        {
            Matrix4f m = this;
            Vector3f Row0xyz = new Vector3f(Row0.x, Row0.y, Row0.z).Normalized;
            Vector3f Row1xyz = new Vector3f(Row1.x, Row1.y, Row1.z).Normalized;
            Vector3f Row2xyz = new Vector3f(Row2.x, Row2.y, Row2.z).Normalized;
            m.Row0 = new Vector4f(Row0xyz.x, Row0xyz.y, Row0xyz.z, Row0.w);
            m.Row1 = new Vector4f(Row1xyz.x, Row1xyz.y, Row1xyz.z, Row0.w);
            m.Row2 = new Vector4f(Row2xyz.x, Row2xyz.y, Row2xyz.z, Row0.w);
            return m;
        }


        public static Matrix4f CreateRotationFromAxisAngle(Vector3f axis, float angle)
        {
            axis.Normalize();

            float cos = (float)Math.Cos(-angle);
            float sin = (float)Math.Sin(-angle);
            float t = 1f - cos;

            float txx = t * axis.x * axis.x; float txy = t * axis.x * axis.y; float txz = t * axis.x * axis.z;
            float tyy = t * axis.y * axis.y; float tyz = t * axis.y * axis.z; float tzz = t * axis.z * axis.z;

            float sinx = sin * axis.x; float siny = sin * axis.y; float sinz = sin * axis.z;

            Matrix4f m = Identity;
            m.Row0 = new Vector4f(txx + cos, txy - sinz, txz + siny, 0f);
            m.Row1 = new Vector4f(txy + sinz, tyy + cos, tyz - sinx, 0f);
            m.Row2 = new Vector4f(txy - siny, tyz + sinx, tzz + cos, 0f);
            m.Row3 = new Vector4f(0f, 0f, 0f, 1f);
            return m;
        }

        public static Matrix4f CreateRotationFromQuaternion(Quaternionf quat)
        {
            float angle;
            Vector3f axis = quat.ToAxisAngle(out angle);
            return CreateRotationFromAxisAngle(axis, angle);
        }

        public Quaternionf ExtractRotation()
        {
            Vector3f row0xyz = new Vector3f(Row0.x, Row0.y, Row0.z).Normalized;
            Vector3f row1xyz = new Vector3f(Row1.x, Row1.y, Row1.z).Normalized;
            Vector3f row2xyz = new Vector3f(Row2.x, Row2.y, Row2.z).Normalized;

            Quaternionf q = new Quaternionf();

            double trace = 0.25 * (row0xyz.x + row1xyz.y + row2xyz.z + 1.0);

            if (trace > 0)
            {
                double sq = Math.Sqrt(trace);

                q.w = (float)sq;
                sq = 1.0 / (4.0 * sq);
                q.x = (float)((row1xyz.z - row2xyz.y) * sq);
                q.y = (float)((row2xyz.x - row0xyz.z) * sq);
                q.z = (float)((row0xyz.y - row1xyz.x) * sq);
            }
            else if (row0xyz.x > row1xyz.y && row0xyz.x > row2xyz.z)
            {
                double sq = 2.0 * Math.Sqrt(1.0 + row0xyz.x - row1xyz.y - row2xyz.z);

                q.w = (float)(0.25 * sq);
                sq = 1.0 / sq;
                q.x = (float)((row2xyz.y - row1xyz.z) * sq);
                q.y = (float)((row1xyz.x - row0xyz.y) * sq);
                q.z = (float)((row2xyz.x - row0xyz.z) * sq);
            }
            else if (row1xyz.y > row2xyz.z)
            {
                double sq = 2.0 * Math.Sqrt(1.0 + row1xyz.y - row0xyz.x - row2xyz.z);

                q.w = (float)(0.25 * sq);
                sq = 1.0 / sq;
                q.x = (float)((row2xyz.x - row0xyz.z) * sq);
                q.y = (float)((row1xyz.x - row0xyz.y) * sq);
                q.z = (float)((row2xyz.y - row1xyz.z) * sq);
            }
            else
            {
                double sq = 2.0 * Math.Sqrt(1.0 + row2xyz.z - row0xyz.x - row1xyz.y);

                q.w = (float)(0.25 * sq);
                sq = 1.0 / sq;
                q.x = (float)((row1xyz.x - row0xyz.y) * sq);
                q.y = (float)((row2xyz.x - row0xyz.z) * sq);
                q.z = (float)((row2xyz.y - row1xyz.z) * sq);
            }

            q.Normalize();
            return q;
        }

        public Matrix4f ClearRotation()
        {
            Matrix4f m = this;

            Vector3f row0xyz = new Vector3f(Row0.x, Row0.y, Row0.z);
            Vector3f row1xyz = new Vector3f(Row1.x, Row1.y, Row1.z);
            Vector3f row2xyz = new Vector3f(Row2.x, Row2.y, Row2.z);

            m.Row0.x = row0xyz.Length; 
            m.Row1.y = row1xyz.Length; 
            m.Row2.z = row2xyz.Length;

            return m;
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
using System;
using UnityEngine;

public struct Quaternion : IEquatable<Quaternion> {
        public double X;
        public double Y;
        public double Z;
        public double W;

    

        static Quaternion identity = new Quaternion(0, 0, 0, 1);

        public Quaternion(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(Vector3 vectorPart, double scalarPart)
        {
            this.X = vectorPart.x;
            this.Y = vectorPart.y;
            this.Z = vectorPart.z;
            this.W = scalarPart;
        }

        public static Quaternion Identity
        {
            get { return identity; }
        }

        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }

        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result.X = quaternion1.X + quaternion2.X;
            result.Y = quaternion1.Y + quaternion2.Y;
            result.Z = quaternion1.Z + quaternion2.Z;
            result.W = quaternion1.W + quaternion2.W;
        }

        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            Quaternion quaternion;
            double x = value2.X;
            double y = value2.Y;
            double z = value2.Z;
            double w = value2.W;
            double num4 = value1.X;
            double num3 = value1.Y;
            double num2 = value1.Z;
            double num = value1.W;
            double num12 = (y * num2) - (z * num3);
            double num11 = (z * num4) - (x * num2);
            double num10 = (x * num3) - (y * num4);
            double num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }

        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            double x = value2.X;
            double y = value2.Y;
            double z = value2.Z;
            double w = value2.W;
            double num4 = value1.X;
            double num3 = value1.Y;
            double num2 = value1.Z;
            double num = value1.W;
            double num12 = (y * num2) - (z * num3);
            double num11 = (z * num4) - (x * num2);
            double num10 = (x * num3) - (y * num4);
            double num9 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num12;
            result.Y = ((y * num) + (num3 * w)) + num11;
            result.Z = ((z * num) + (num2 * w)) + num10;
            result.W = (w * num) - num9;
        }

        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion quaternion;
            quaternion.X = -value.X;
            quaternion.Y = -value.Y;
            quaternion.Z = -value.Z;
            quaternion.W = value.W;
            return quaternion;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, double angle)
        {
            Quaternion quaternion;
            double num2 = angle * 0.5f;
            double num = (double)Math.Sin((double)num2);
            double num3 = (double)Math.Cos((double)num2);
            quaternion.X = axis.x * num;
            quaternion.Y = axis.y * num;
            quaternion.Z = axis.z * num;
            quaternion.W = num3;
            return quaternion;

        }

        public static void CreateFromAxisAngle(ref Vector3 axis, double angle, out Quaternion result)
        {
            double num2 = angle * 0.5f;
            double num = (double)Math.Sin((double)num2);
            double num3 = (double)Math.Cos((double)num2);
            result.X = axis.x * num;
            result.Y = axis.y * num;
            result.Z = axis.z * num;
            result.W = num3;
        }

        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            double num8 = (matrix[0,0] + matrix[1, 1]) + matrix[2, 2];
            Quaternion quaternion = new Quaternion();
            if (num8 > 0f)
            {
                double num = (double)Math.Sqrt((double)(num8 + 1f));
                quaternion.W = num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (matrix[1, 2] - matrix[2, 1]) * num;
                quaternion.Y = (matrix[2, 0] - matrix[0, 2]) * num;
                quaternion.Z = (matrix[0, 1] - matrix[1, 0]) * num;
                return quaternion;
            }
            if ((matrix[0, 0] >= matrix[1, 1]) && (matrix[0, 0] >= matrix[2, 2]))
            {
                double num7 = (double)Math.Sqrt((double)(((1f + matrix[0, 0]) - matrix[1, 1]) - matrix[2, 2]));
                double num4 = 0.5f / num7;
                quaternion.X = 0.5f * num7;
                quaternion.Y = (matrix[0, 1] + matrix[1, 0]) * num4;
                quaternion.Z = (matrix[0, 2] + matrix[2, 0]) * num4;
                quaternion.W = (matrix[1, 2] - matrix[2, 1]) * num4;
                return quaternion;
            }
            if (matrix[1, 1] > matrix[2, 2])
            {
                double num6 = (double)Math.Sqrt((double)(((1f + matrix[1, 1]) - matrix[0, 0]) - matrix[2, 2]));
                double num3 = 0.5f / num6;
                quaternion.X = (matrix[1, 0] + matrix[0, 1]) * num3;
                quaternion.Y = 0.5f * num6;
                quaternion.Z = (matrix[2, 1] + matrix[1, 2]) * num3;
                quaternion.W = (matrix[2, 0] - matrix[0, 2]) * num3;
                return quaternion;
            }
            double num5 = (double)Math.Sqrt((double)(((1f + matrix[2, 2]) - matrix[0, 0]) - matrix[1, 1]));
            double num2 = 0.5f / num5;
            quaternion.X = (matrix[2, 0] + matrix[0, 2]) * num2;
            quaternion.Y = (matrix[2, 1] + matrix[1, 2]) * num2;
            quaternion.Z = 0.5f * num5;
            quaternion.W = (matrix[0, 1] - matrix[1, 0]) * num2;

            return quaternion;
        }

        public static void CreateFromRotationMatrix(ref Matrix4x4 matrix, out Quaternion result)
        {
            double num8 = (matrix[0, 0] + matrix[1, 1]) + matrix[2, 2];
            if (num8 > 0f)
            {
                double num = (double)Math.Sqrt((double)(num8 + 1f));
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix[1, 2] - matrix[2, 1]) * num;
                result.Y = (matrix[2, 0] - matrix[0, 2]) * num;
                result.Z = (matrix[0, 1] - matrix[1, 0]) * num;
            }
            else if ((matrix[0, 0] >= matrix[1, 1]) && (matrix[0, 0] >= matrix[2, 2]))
            {
                double num7 = (double)Math.Sqrt((double)(((1f + matrix[0, 0]) - matrix[1, 1]) - matrix[2, 2]));
                double num4 = 0.5f / num7;
                result.X = 0.5f * num7;
                result.Y = (matrix[0, 1] + matrix[1, 0]) * num4;
                result.Z = (matrix[0, 2] + matrix[2, 0]) * num4;
                result.W = (matrix[1, 2] - matrix[2, 1]) * num4;
            }
            else if (matrix[1, 1] > matrix[2, 2])
            {
                double num6 = (double)Math.Sqrt((double)(((1f + matrix[1, 1]) - matrix[0, 0]) - matrix[2, 2]));
                double num3 = 0.5f / num6;
                result.X = (matrix[1, 0] + matrix[0, 1]) * num3;
                result.Y = 0.5f * num6;
                result.Z = (matrix[2, 1] + matrix[1, 2]) * num3;
                result.W = (matrix[2, 0] - matrix[0, 2]) * num3;
            }
            else
            {
                double num5 = (double)Math.Sqrt((double)(((1f + matrix[2, 2]) - matrix[0, 0]) - matrix[1, 1]));
                double num2 = 0.5f / num5;
                result.X = (matrix[2, 0] + matrix[0, 2]) * num2;
                result.Y = (matrix[2, 1] + matrix[1, 2]) * num2;
                result.Z = 0.5f * num5;
                result.W = (matrix[0, 1] - matrix[1, 0]) * num2;
            }
        }

        public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            Quaternion quaternion;
            double num9 = roll * 0.5f;
            double num6 = (double)Math.Sin((double)num9);
            double num5 = (double)Math.Cos((double)num9);
            double num8 = pitch * 0.5f;
            double num4 = (double)Math.Sin((double)num8);
            double num3 = (double)Math.Cos((double)num8);
            double num7 = yaw * 0.5f;
            double num2 = (double)Math.Sin((double)num7);
            double num = (double)Math.Cos((double)num7);
            quaternion.X = ((num * num4) * num5) + ((num2 * num3) * num6);
            quaternion.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
            quaternion.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
            quaternion.W = ((num * num3) * num5) + ((num2 * num4) * num6);
            return quaternion;
        }

        public static void CreateFromYawPitchRoll(double yaw, double pitch, double roll, out Quaternion result)
        {
            double num9 = roll * 0.5f;
            double num6 = (double)Math.Sin((double)num9);
            double num5 = (double)Math.Cos((double)num9);
            double num8 = pitch * 0.5f;
            double num4 = (double)Math.Sin((double)num8);
            double num3 = (double)Math.Cos((double)num8);
            double num7 = yaw * 0.5f;
            double num2 = (double)Math.Sin((double)num7);
            double num = (double)Math.Cos((double)num7);
            result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
            result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
            result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
            result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            double x = quaternion1.X;
            double y = quaternion1.Y;
            double z = quaternion1.Z;
            double w = quaternion1.W;
            double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            double num5 = 1f / num14;
            double num4 = -quaternion2.X * num5;
            double num3 = -quaternion2.Y * num5;
            double num2 = -quaternion2.Z * num5;
            double num = quaternion2.W * num5;
            double num13 = (y * num2) - (z * num3);
            double num12 = (z * num4) - (x * num2);
            double num11 = (x * num3) - (y * num4);
            double num10 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num13;
            quaternion.Y = ((y * num) + (num3 * w)) + num12;
            quaternion.Z = ((z * num) + (num2 * w)) + num11;
            quaternion.W = (w * num) - num10;
            return quaternion;
        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            double x = quaternion1.X;
            double y = quaternion1.Y;
            double z = quaternion1.Z;
            double w = quaternion1.W;
            double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            double num5 = 1f / num14;
            double num4 = -quaternion2.X * num5;
            double num3 = -quaternion2.Y * num5;
            double num2 = -quaternion2.Z * num5;
            double num = quaternion2.W * num5;
            double num13 = (y * num2) - (z * num3);
            double num12 = (z * num4) - (x * num2);
            double num11 = (x * num3) - (y * num4);
            double num10 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num13;
            result.Y = ((y * num) + (num3 * w)) + num12;
            result.Z = ((z * num) + (num2 * w)) + num11;
            result.W = (w * num) - num10;
        }

        public static double Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return ((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W));
        }

        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out double result)
        {
            result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Quaternion)
            {
                flag = this.Equals((Quaternion)obj);
            }
            return flag;
        }

        public bool Equals(Quaternion other)
        {
            return ((((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z)) && (this.W == other.W));
        }

        public override int GetHashCode()
        {
            return (((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode()) + this.W.GetHashCode());
        }

        public static Quaternion Inverse(Quaternion quaternion)
        {
            Quaternion quaternion2;
            double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            double num = 1f / num2;
            quaternion2.X = -quaternion.X * num;
            quaternion2.Y = -quaternion.Y * num;
            quaternion2.Z = -quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            double num = 1f / num2;
            result.X = -quaternion.X * num;
            result.Y = -quaternion.Y * num;
            result.Z = -quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public double Length()
        {
            double num = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            return (double)Math.Sqrt((double)num);
        }

        public double LengthSquared()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }

        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, double amount)
        {
            double num = amount;
            double num2 = 1f - num;
            Quaternion quaternion = new Quaternion();
            double num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f)
            {
                quaternion.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            }
            else
            {
                quaternion.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            double num4 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            double num3 = 1f / ((double)Math.Sqrt((double)num4));
            quaternion.X *= num3;
            quaternion.Y *= num3;
            quaternion.Z *= num3;
            quaternion.W *= num3;
            return quaternion;
        }

        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, double amount, out Quaternion result)
        {
            double num = amount;
            double num2 = 1f - num;
            double num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f)
            {
                result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            }
            else
            {
                result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            double num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
            double num3 = 1f / ((double)Math.Sqrt((double)num4));
            result.X *= num3;
            result.Y *= num3;
            result.Z *= num3;
            result.W *= num3;
        }

        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, double amount)
        {
            double num2;
            double num3;
            Quaternion quaternion;
            double num = amount;
            double num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            bool flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                double num5 = (double)Math.Acos((double)num4);
                double num6 = (double)(1.0 / Math.Sin((double)num5));
                num3 = ((double)Math.Sin((double)((1f - num) * num5))) * num6;
                num2 = flag ? (((double)-Math.Sin((double)(num * num5))) * num6) : (((double)Math.Sin((double)(num * num5))) * num6);
            }
            quaternion.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
            quaternion.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
            quaternion.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
            quaternion.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
            return quaternion;
        }

        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, double amount, out Quaternion result)
        {
            double num2;
            double num3;
            double num = amount;
            double num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
            bool flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                double num5 = (double)Math.Acos((double)num4);
                double num6 = (double)(1.0 / Math.Sin((double)num5));
                num3 = ((double)Math.Sin((double)((1f - num) * num5))) * num6;
                num2 = flag ? (((double)-Math.Sin((double)(num * num5))) * num6) : (((double)Math.Sin((double)(num * num5))) * num6);
            }
            result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
            result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
            result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
            result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
        }

        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }

        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result.X = quaternion1.X - quaternion2.X;
            result.Y = quaternion1.Y - quaternion2.Y;
            result.Z = quaternion1.Z - quaternion2.Z;
            result.W = quaternion1.W - quaternion2.W;
        }

        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            double x = quaternion1.X;
            double y = quaternion1.Y;
            double z = quaternion1.Z;
            double w = quaternion1.W;
            double num4 = quaternion2.X;
            double num3 = quaternion2.Y;
            double num2 = quaternion2.Z;
            double num = quaternion2.W;
            double num12 = (y * num2) - (z * num3);
            double num11 = (z * num4) - (x * num2);
            double num10 = (x * num3) - (y * num4);
            double num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }

        public static Quaternion Multiply(Quaternion quaternion1, double scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }

        public static void Multiply(ref Quaternion quaternion1, double scaleFactor, out Quaternion result)
        {
            result.X = quaternion1.X * scaleFactor;
            result.Y = quaternion1.Y * scaleFactor;
            result.Z = quaternion1.Z * scaleFactor;
            result.W = quaternion1.W * scaleFactor;
        }

        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            double x = quaternion1.X;
            double y = quaternion1.Y;
            double z = quaternion1.Z;
            double w = quaternion1.W;
            double num4 = quaternion2.X;
            double num3 = quaternion2.Y;
            double num2 = quaternion2.Z;
            double num = quaternion2.W;
            double num12 = (y * num2) - (z * num3);
            double num11 = (z * num4) - (x * num2);
            double num10 = (x * num3) - (y * num4);
            double num9 = ((x * num4) + (y * num3)) + (z * num2);
            result.X = ((x * num) + (num4 * w)) + num12;
            result.Y = ((y * num) + (num3 * w)) + num11;
            result.Z = ((z * num) + (num2 * w)) + num10;
            result.W = (w * num) - num9;
        }

        public static Quaternion Negate(Quaternion quaternion)
        {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }

        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = -quaternion.W;
        }

        public void Normalize()
        {
            double num2 = (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W);
            double num = 1f / ((double)Math.Sqrt((double)num2));
            this.X *= num;
            this.Y *= num;
            this.Z *= num;
            this.W *= num;
        }

        public static Quaternion Normalize(Quaternion quaternion)
        {
            Quaternion quaternion2;
            double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            double num = 1f / ((double)Math.Sqrt((double)num2));
            quaternion2.X = quaternion.X * num;
            quaternion2.Y = quaternion.Y * num;
            quaternion2.Z = quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }

        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            double num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
            double num = 1f / ((double)Math.Sqrt((double)num2));
            result.X = quaternion.X * num;
            result.Y = quaternion.Y * num;
            result.Z = quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }

        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            double x = quaternion1.X;
            double y = quaternion1.Y;
            double z = quaternion1.Z;
            double w = quaternion1.W;
            double num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
            double num5 = 1f / num14;
            double num4 = -quaternion2.X * num5;
            double num3 = -quaternion2.Y * num5;
            double num2 = -quaternion2.Z * num5;
            double num = quaternion2.W * num5;
            double num13 = (y * num2) - (z * num3);
            double num12 = (z * num4) - (x * num2);
            double num11 = (x * num3) - (y * num4);
            double num10 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num13;
            quaternion.Y = ((y * num) + (num3 * w)) + num12;
            quaternion.Z = ((z * num) + (num2 * w)) + num11;
            quaternion.W = (w * num) - num10;
            return quaternion;
        }

        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
        }

        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z))
            {
                return (quaternion1.W != quaternion2.W);
            }
            return true;
        }

        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            double x = quaternion1.X;
            double y = quaternion1.Y;
            double z = quaternion1.Z;
            double w = quaternion1.W;
            double num4 = quaternion2.X;
            double num3 = quaternion2.Y;
            double num2 = quaternion2.Z;
            double num = quaternion2.W;
            double num12 = (y * num2) - (z * num3);
            double num11 = (z * num4) - (x * num2);
            double num10 = (x * num3) - (y * num4);
            double num9 = ((x * num4) + (y * num3)) + (z * num2);
            quaternion.X = ((x * num) + (num4 * w)) + num12;
            quaternion.Y = ((y * num) + (num3 * w)) + num11;
            quaternion.Z = ((z * num) + (num2 * w)) + num10;
            quaternion.W = (w * num) - num9;
            return quaternion;
        }

        public static Quaternion operator *(Quaternion quaternion1, double scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }

        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }

        public static Quaternion operator -(Quaternion quaternion)
        {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append(" Z:");
            sb.Append(this.Z);
            sb.Append(" W:");
            sb.Append(this.W);
            sb.Append("}");
            return sb.ToString();
        }

        internal Matrix4x4 ToMatrix()
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            ToMatrix(out matrix);
            return matrix;
        }

        internal void ToMatrix(out Matrix4x4 matrix)
        {
            Quaternion.ToMatrix(this, out matrix);
        }

        internal static void ToMatrix(Quaternion quaternion, out Matrix4x4 matrix)
        {

            double x2 = quaternion.X * quaternion.X;
            double y2 = quaternion.Y * quaternion.Y;
            double z2 = quaternion.Z * quaternion.Z;
            double xy = quaternion.X * quaternion.Y;
            double xz = quaternion.X * quaternion.Z;
            double yz = quaternion.Y * quaternion.Z;
            double wx = quaternion.W * quaternion.X;
            double wy = quaternion.W * quaternion.Y;
            double wz = quaternion.W * quaternion.Z;
            matrix = new Matrix4x4();

            matrix[0, 0] = (float)(1.0f - 2.0f * (y2 + z2));
            matrix[0, 1] = (float)(2.0f * (xy - wz));
            matrix[0, 2] = (float)(2.0f * (xz + wy));
            matrix[0, 3] = 0.0f;

            matrix[1, 0] = (float)(2.0f * (xy + wz));
            matrix[1, 1] = (float)(1.0f - 2.0f * (x2 + z2));
            matrix[1, 2] = (float)(2.0f * (yz - wx));
            matrix[1, 3] = 0.0f;

            matrix[2, 0] = (float)(2.0f * (xz - wy));
            matrix[2, 1] = (float)(2.0f * (yz + wx));
            matrix[2, 2] = (float)(1.0f - 2.0f * (x2 + y2));
            matrix[2, 3] = 0.0f;

            matrix[3, 0] = (float)(2.0f * (xz - wy));
            matrix[3, 1] = (float)(2.0f * (yz + wx));
            matrix[3, 2] = (float)(1.0f - 2.0f * (x2 + y2));
            matrix[3, 3] = 0.0f;

        /*return Matrix4x4( 1.0f - 2.0f * (y2 + z2), 2.0f * (xy - wz), 2.0f * (xz + wy), 0.0f,
            2.0f * (xy + wz), 1.0f - 2.0f * (x2 + z2), 2.0f * (yz - wx), 0.0f,
            2.0f * (xz - wy), 2.0f * (yz + wx), 1.0f - 2.0f * (x2 + y2), 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);*/

    }

    internal Vector3 Xyz
        {
            get
            {
                return new Vector3((float) X, (float) Y, (float) Z);
            }

            set
            {
                X = value.x;
                Y = value.y;
                Z = value.z;
            }
        }
    }

using System;
using System.Text;
using UnityEngine;

public struct Quaternion : IEquatable<Quaternion> {
    private double _x;
    private double _y;
    private double _z;
    private double _w;
    private const double Tolerance = 0.001;


    public Quaternion(double x, double y, double z, double w)
    {
        _x = x;
        _y = y;
        _z = z;
        _w = w;
    }

    public Quaternion(Vector3 vectorPart, double scalarPart)
    {
        _x = vectorPart.x;
        _y = vectorPart.y;
        _z = vectorPart.z;
        _w = scalarPart;
    }

    public static Quaternion Identity { get; } = new Quaternion(0, 0, 0, 1);

    /// <summary>
    /// Addition de 2 Quaternion
    /// </summary>
    /// <param name="quaternion1"> 1er Quaternion</param>
    /// <param name="quaternion2"> 2e Quaternion</param>
    /// <returns></returns>
    public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion
        {
            _x = quaternion1._x + quaternion2._x,
            _y = quaternion1._y + quaternion2._y,
            _z = quaternion1._z + quaternion2._z,
            _w = quaternion1._w + quaternion2._w
        };
        return quaternion;
    }
    /// <summary>
    /// Surchage Addition de Quaternion avec out
    /// </summary>
    /// <param name="quaternion1"></param>
    /// <param name="quaternion2"></param>
    /// <param name="result"></param>
    public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
    {
        result._x = quaternion1._x + quaternion2._x;
        result._y = quaternion1._y + quaternion2._y;
        result._z = quaternion1._z + quaternion2._z;
        result._w = quaternion1._w + quaternion2._w;
    }

    public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
    {
        var quaternion = new Quaternion();
        var x = value2._x;
        var y = value2._y;
        var z = value2._z;
        var w = value2._w;
        var num4 = value1._x;
        var num3 = value1._y;
        var num2 = value1._z;
        var num = value1._w;
        var num12 = y * num2 - z * num3;
        var num11 = z * num4 - x * num2;
        var num10 = x * num3 - y * num4;
        var num9 = x * num4 + y * num3 + z * num2;
        quaternion._x = x * num + num4 * w + num12;
        quaternion._y = y * num + num3 * w + num11;
        quaternion._z = z * num + num2 * w + num10;
        quaternion._w = w * num - num9;
        return quaternion;
    }

    public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
    {
        var x = value2._x;
        var y = value2._y;
        var z = value2._z;
        var w = value2._w;
        var num4 = value1._x;
        var num3 = value1._y;
        var num2 = value1._z;
        var num = value1._w;
        var num12 = y * num2 - z * num3;
        var num11 = z * num4 - x * num2;
        var num10 = x * num3 - y * num4;
        var num9 = x * num4 + y * num3 + z * num2;
        result._x = x * num + num4 * w + num12;
        result._y = y * num + num3 * w + num11;
        result._z = z * num + num2 * w + num10;
        result._w = w * num - num9;
    }

    public void Conjugate()
    {
        _x = -_x;
        _y = -_y;
        _z = -_z;
    }

    public static Quaternion Conjugate(Quaternion value)
    {
        var quaternion = new Quaternion {_x = -value._x, _y = -value._y, _z = -value._z, _w = value._w};
        return quaternion;
    }

    public static void Conjugate(ref Quaternion value, out Quaternion result)
    {
        result._x = -value._x;
        result._y = -value._y;
        result._z = -value._z;
        result._w = value._w;
    }
    /// <summary>
    /// Creation de Quaternion avec un axe en Vector3 et un angle en double
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Quaternion CreateFromAxisAngle(Vector3 axis, double angle)
    {
        var quaternion = new Quaternion();
        var num2 = angle * 0.5f;
        var num = Math.Sin(num2);
        var num3 = Math.Cos(num2);
        quaternion._x = axis.x * num;
        quaternion._y = axis.y * num;
        quaternion._z = axis.z * num;
        quaternion._w = num3;
        return quaternion;

    }
    /// <summary>
    /// Surcharge Creation de Quaternion avec un axe en Vector3 et un angle en double
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="angle"></param>
    /// <param name="result"></param>
    public static void CreateFromAxisAngle(ref Vector3 axis, double angle, out Quaternion result)
    {
        var num2 = angle * 0.5f;
        var num = Math.Sin(num2);
        var num3 = Math.Cos(num2);
        result._x = axis.x * num;
        result._y = axis.y * num;
        result._z = axis.z * num;
        result._w = num3;
    }

    public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
    {
        double num8 = matrix[0,0] + matrix[1, 1] + matrix[2, 2];
        var quaternion = new Quaternion();
        if (num8 > 0f)
        {
            var num = Math.Sqrt(num8 + 1f);
            quaternion._w = num * 0.5f;
            num = 0.5f / num;
            quaternion._x = (matrix[1, 2] - matrix[2, 1]) * num;
            quaternion._y = (matrix[2, 0] - matrix[0, 2]) * num;
            quaternion._z = (matrix[0, 1] - matrix[1, 0]) * num;
            return quaternion;
        }
        if (matrix[0, 0] >= matrix[1, 1] && matrix[0, 0] >= matrix[2, 2])
        {
            var num7 = Math.Sqrt(1f + matrix[0, 0] - matrix[1, 1]) - matrix[2, 2];
            var num4 = 0.5f / num7;
            quaternion._x = 0.5f * num7;
            quaternion._y = (matrix[0, 1] + matrix[1, 0]) * num4;
            quaternion._z = (matrix[0, 2] + matrix[2, 0]) * num4;
            quaternion._w = (matrix[1, 2] - matrix[2, 1]) * num4;
            return quaternion;
        }
        if (matrix[1, 1] > matrix[2, 2])
        {
            var num6 = Math.Sqrt(1f + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
            var num3 = 0.5f / num6;
            quaternion._x = (matrix[1, 0] + matrix[0, 1]) * num3;
            quaternion._y = 0.5f * num6;
            quaternion._z = (matrix[2, 1] + matrix[1, 2]) * num3;
            quaternion._w = (matrix[2, 0] - matrix[0, 2]) * num3;
            return quaternion;
        }
        var num5 = Math.Sqrt(1f + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
        var num2 = 0.5f / num5;
        quaternion._x = (matrix[2, 0] + matrix[0, 2]) * num2;
        quaternion._y = (matrix[2, 1] + matrix[1, 2]) * num2;
        quaternion._z = 0.5f * num5;
        quaternion._w = (matrix[0, 1] - matrix[1, 0]) * num2;

        return quaternion;
    }

    public static void CreateFromRotationMatrix(ref Matrix4x4 matrix, out Quaternion result)
    {
        double num8 = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];
        if (num8 > 0f)
        {
            var num = Math.Sqrt(num8 + 1f);
            result._w = num * 0.5f;
            num = 0.5f / num;
            result._x = (matrix[1, 2] - matrix[2, 1]) * num;
            result._y = (matrix[2, 0] - matrix[0, 2]) * num;
            result._z = (matrix[0, 1] - matrix[1, 0]) * num;
        }
        else if (matrix[0, 0] >= matrix[1, 1] && matrix[0, 0] >= matrix[2, 2])
        {
            var num7 = Math.Sqrt(1f + matrix[0, 0] - matrix[1, 1] - matrix[2, 2]);
            var num4 = 0.5f / num7;
            result._x = 0.5f * num7;
            result._y = (matrix[0, 1] + matrix[1, 0]) * num4;
            result._z = (matrix[0, 2] + matrix[2, 0]) * num4;
            result._w = (matrix[1, 2] - matrix[2, 1]) * num4;
        }
        else if (matrix[1, 1] > matrix[2, 2])
        {
            var num6 = Math.Sqrt(1f + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
            var num3 = 0.5f / num6;
            result._x = (matrix[1, 0] + matrix[0, 1]) * num3;
            result._y = 0.5f * num6;
            result._z = (matrix[2, 1] + matrix[1, 2]) * num3;
            result._w = (matrix[2, 0] - matrix[0, 2]) * num3;
        }
        else
        {
            var num5 = Math.Sqrt(1f + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
            var num2 = 0.5f / num5;
            result._x = (matrix[2, 0] + matrix[0, 2]) * num2;
            result._y = (matrix[2, 1] + matrix[1, 2]) * num2;
            result._z = 0.5f * num5;
            result._w = (matrix[0, 1] - matrix[1, 0]) * num2;
        }
    }

    public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
    {
        Quaternion quaternion = new Quaternion();
        var num9 = roll * 0.5f;
        var num6 = Math.Sin(num9);
        var num5 = Math.Cos(num9);
        var num8 = pitch * 0.5f;
        var num4 = Math.Sin(num8);
        var num3 = Math.Cos(num8);
        var num7 = yaw * 0.5f;
        var num2 = Math.Sin(num7);
        var num = Math.Cos(num7);
        quaternion._x = num * num4 * num5 + num2 * num3 * num6;
        quaternion._y = num2 * num3 * num5 - num * num4 * num6;
        quaternion._z = num * num3 * num6 - num2 * num4 * num5;
        quaternion._w = num * num3 * num5 + num2 * num4 * num6;
        return quaternion;
    }

    public static void CreateFromYawPitchRoll(double yaw, double pitch, double roll, out Quaternion result)
    {
        var num9 = roll * 0.5f;
        var num6 = Math.Sin(num9);
        var num5 = Math.Cos(num9);
        var num8 = pitch * 0.5f;
        var num4 = Math.Sin(num8);
        var num3 = Math.Cos(num8);
        var num7 = yaw * 0.5f;
        var num2 = Math.Sin(num7);
        var num = Math.Cos(num7);
        result._x = num * num4 * num5 + num2 * num3 * num6;
        result._y = num2 * num3 * num5 - num * num4 * num6;
        result._z = num * num3 * num6 - num2 * num4 * num5;
        result._w = num * num3 * num5 + num2 * num4 * num6;
    }

    public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion();
        var x = quaternion1._x;
        var y = quaternion1._y;
        var z = quaternion1._z;
        var w = quaternion1._w;
        var num14 = quaternion2._x * quaternion2._x + quaternion2._y * quaternion2._y + quaternion2._z * quaternion2._z + quaternion2._w * quaternion2._w;
        var num5 = 1f / num14;
        var num4 = -quaternion2._x * num5;
        var num3 = -quaternion2._y * num5;
        var num2 = -quaternion2._z * num5;
        var num = quaternion2._w * num5;
        var num13 = y * num2 - z * num3;
        var num12 = z * num4 - x * num2;
        var num11 = x * num3 - y * num4;
        var num10 = x * num4 + y * num3 + z * num2;
        quaternion._x = x * num + num4 * w + num13;
        quaternion._y = y * num + num3 * w + num12;
        quaternion._z = z * num + num2 * w + num11;
        quaternion._w = w * num - num10;
        return quaternion;
    }

    public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
    {
        var x = quaternion1._x;
        var y = quaternion1._y;
        var z = quaternion1._z;
        var w = quaternion1._w;
        var num14 = quaternion2._x * quaternion2._x + quaternion2._y * quaternion2._y + quaternion2._z * quaternion2._z + quaternion2._w * quaternion2._w;
        var num5 = 1f / num14;
        var num4 = -quaternion2._x * num5;
        var num3 = -quaternion2._y * num5;
        var num2 = -quaternion2._z * num5;
        var num = quaternion2._w * num5;
        var num13 = y * num2 - z * num3;
        var num12 = z * num4 - x * num2;
        var num11 = x * num3 - y * num4;
        var num10 = x * num4 + y * num3 + z * num2;
        result._x = x * num + num4 * w + num13;
        result._y = y * num + num3 * w + num12;
        result._z = z * num + num2 * w + num11;
        result._w = w * num - num10;
    }

    public static double Dot(Quaternion quaternion1, Quaternion quaternion2)
    {
        return quaternion1._x * quaternion2._x + quaternion1._y * quaternion2._y + quaternion1._z * quaternion2._z + quaternion1._w * quaternion2._w;
    }

    public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out double result)
    {
        result = quaternion1._x * quaternion2._x + quaternion1._y * quaternion2._y + quaternion1._z * quaternion2._z + quaternion1._w * quaternion2._w;
    }

    public override bool Equals(object obj)
    {
        var flag = false;
        if (obj is Quaternion quaternion)
        {
            flag = Equals(quaternion);
        }
        return flag;
    }

    public bool Equals(Quaternion other)
    {
        return Math.Abs(_x - other._x) < Tolerance && Math.Abs(_y - other._y) < Tolerance && Math.Abs(_z - other._z) < Tolerance && Math.Abs(_w - other._w) < Tolerance;
    }

    public override int GetHashCode()
    {
        return _x.GetHashCode() + _y.GetHashCode() + _z.GetHashCode() + _w.GetHashCode();
    }

    public static Quaternion Inverse(Quaternion quaternion)
    {
        var quaternion2 = new Quaternion();
        var num2 = quaternion._x * quaternion._x + quaternion._y * quaternion._y + quaternion._z * quaternion._z + quaternion._w * quaternion._w;
        var num = 1f / num2;
        quaternion2._x = -quaternion._x * num;
        quaternion2._y = -quaternion._y * num;
        quaternion2._z = -quaternion._z * num;
        quaternion2._w = quaternion._w * num;
        return quaternion2;
    }

    public static void Inverse(ref Quaternion quaternion, out Quaternion result)
    {
        var num2 = quaternion._x * quaternion._x + quaternion._y * quaternion._y + quaternion._z * quaternion._z + quaternion._w * quaternion._w;
        var num = 1f / num2;
        result._x = -quaternion._x * num;
        result._y = -quaternion._y * num;
        result._z = -quaternion._z * num;
        result._w = quaternion._w * num;
    }

    public double Length()
    {
        var num = _x * _x + _y * _y + _z * _z + _w * _w;
        return Math.Sqrt(num);
    }

    public double LengthSquared()
    {
        return _x * _x + _y * _y + _z * _z + _w * _w;
    }

    public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, double amount)
    {
        var num = amount;
        var num2 = 1f - num;
        var quaternion = new Quaternion();
        var num5 = quaternion1._x * quaternion2._x + quaternion1._y * quaternion2._y + quaternion1._z * quaternion2._z + quaternion1._w * quaternion2._w;
        if (num5 >= 0f)
        {
            quaternion._x = num2 * quaternion1._x + num * quaternion2._x;
            quaternion._y = num2 * quaternion1._y + num * quaternion2._y;
            quaternion._z = num2 * quaternion1._z + num * quaternion2._z;
            quaternion._w = num2 * quaternion1._w + num * quaternion2._w;
        }
        else
        {
            quaternion._x = num2 * quaternion1._x - num * quaternion2._x;
            quaternion._y = num2 * quaternion1._y - num * quaternion2._y;
            quaternion._z = num2 * quaternion1._z - num * quaternion2._z;
            quaternion._w = num2 * quaternion1._w - num * quaternion2._w;
        }
        var num4 = quaternion._x * quaternion._x + quaternion._y * quaternion._y + quaternion._z * quaternion._z + quaternion._w * quaternion._w;
        var num3 = 1f / Math.Sqrt(num4);
        quaternion._x *= num3;
        quaternion._y *= num3;
        quaternion._z *= num3;
        quaternion._w *= num3;
        return quaternion;
    }

    public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, double amount, out Quaternion result)
    {
        var num = amount;
        var num2 = 1f - num;
        var num5 = quaternion1._x * quaternion2._x + quaternion1._y * quaternion2._y + quaternion1._z * quaternion2._z + quaternion1._w * quaternion2._w;
        if (num5 >= 0f)
        {
            result._x = num2 * quaternion1._x + num * quaternion2._x;
            result._y = num2 * quaternion1._y + num * quaternion2._y;
            result._z = num2 * quaternion1._z + num * quaternion2._z;
            result._w = num2 * quaternion1._w + num * quaternion2._w;
        }
        else
        {
            result._x = num2 * quaternion1._x - num * quaternion2._x;
            result._y = num2 * quaternion1._y - num * quaternion2._y;
            result._z = num2 * quaternion1._z - num * quaternion2._z;
            result._w = num2 * quaternion1._w - num * quaternion2._w;
        }
        var num4 = result._x * result._x + result._y * result._y + result._z * result._z + result._w * result._w;
        var num3 = 1f / Math.Sqrt(num4);
        result._x *= num3;
        result._y *= num3;
        result._z *= num3;
        result._w *= num3;
    }

    public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, double amount)
    {
        double num2;
        double num3;
        var quaternion = new Quaternion();
        var num = amount;
        var num4 = quaternion1._x * quaternion2._x + quaternion1._y * quaternion2._y + quaternion1._z * quaternion2._z + quaternion1._w * quaternion2._w;
        var flag = false;
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
            var num5 = Math.Acos(num4);
            var num6 = 1.0 / Math.Sin(num5);
            num3 = Math.Sin((1f - num) * num5) * num6;
            num2 = flag ? -Math.Sin(num * num5) * num6 : Math.Sin(num * num5) * num6;
        }
        quaternion._x = num3 * quaternion1._x + num2 * quaternion2._x;
        quaternion._y = num3 * quaternion1._y + num2 * quaternion2._y;
        quaternion._z = num3 * quaternion1._z + num2 * quaternion2._z;
        quaternion._w = num3 * quaternion1._w + num2 * quaternion2._w;
        return quaternion;
    }

    public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, double amount, out Quaternion result)
    {
        double num2;
        double num3;
        var num = amount;
        var num4 = quaternion1._x * quaternion2._x + quaternion1._y * quaternion2._y + quaternion1._z * quaternion2._z + quaternion1._w * quaternion2._w;
        var flag = false;
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
            var num5 = Math.Acos(num4);
            var num6 = 1.0 / Math.Sin(num5);
            num3 = Math.Sin((1f - num) * num5) * num6;
            num2 = flag ? -Math.Sin(num * num5) * num6 : Math.Sin(num * num5) * num6;
        }
        result._x = num3 * quaternion1._x + num2 * quaternion2._x;
        result._y = num3 * quaternion1._y + num2 * quaternion2._y;
        result._z = num3 * quaternion1._z + num2 * quaternion2._z;
        result._w = num3 * quaternion1._w + num2 * quaternion2._w;
    }

    public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion
        {
            _x = quaternion1._x - quaternion2._x,
            _y = quaternion1._y - quaternion2._y,
            _z = quaternion1._z - quaternion2._z,
            _w = quaternion1._w - quaternion2._w
        };
        return quaternion;
    }

    public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
    {
        result._x = quaternion1._x - quaternion2._x;
        result._y = quaternion1._y - quaternion2._y;
        result._z = quaternion1._z - quaternion2._z;
        result._w = quaternion1._w - quaternion2._w;
    }

    public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion();
        var x = quaternion1._x;
        var y = quaternion1._y;
        var z = quaternion1._z;
        var w = quaternion1._w;
        var num4 = quaternion2._x;
        var num3 = quaternion2._y;
        var num2 = quaternion2._z;
        var num = quaternion2._w;
        var num12 = y * num2 - z * num3;
        var num11 = z * num4 - x * num2;
        var num10 = x * num3 - y * num4;
        var num9 = x * num4 + y * num3 + z * num2;
        quaternion._x = x * num + num4 * w + num12;
        quaternion._y = y * num + num3 * w + num11;
        quaternion._z = z * num + num2 * w + num10;
        quaternion._w = w * num - num9;
        return quaternion;
    }

    public static Quaternion Multiply(Quaternion quaternion1, double scaleFactor)
    {
        var quaternion = new Quaternion
        {
            _x = quaternion1._x * scaleFactor,
            _y = quaternion1._y * scaleFactor,
            _z = quaternion1._z * scaleFactor,
            _w = quaternion1._w * scaleFactor
        };
        return quaternion;
    }

    public static void Multiply(ref Quaternion quaternion1, double scaleFactor, out Quaternion result)
    {
        result._x = quaternion1._x * scaleFactor;
        result._y = quaternion1._y * scaleFactor;
        result._z = quaternion1._z * scaleFactor;
        result._w = quaternion1._w * scaleFactor;
    }

    public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
    {
        var x = quaternion1._x;
        var y = quaternion1._y;
        var z = quaternion1._z;
        var w = quaternion1._w;
        var num4 = quaternion2._x;
        var num3 = quaternion2._y;
        var num2 = quaternion2._z;
        var num = quaternion2._w;
        var num12 = y * num2 - z * num3;
        var num11 = z * num4 - x * num2;
        var num10 = x * num3 - y * num4;
        var num9 = x * num4 + y * num3 + z * num2;
        result._x = x * num + num4 * w + num12;
        result._y = y * num + num3 * w + num11;
        result._z = z * num + num2 * w + num10;
        result._w = w * num - num9;
    }

    public static Quaternion Negate(Quaternion quaternion)
    {
        var quaternion2 = new Quaternion
        {
            _x = -quaternion._x, _y = -quaternion._y, _z = -quaternion._z, _w = -quaternion._w
        };
        return quaternion2;
    }

    public static void Negate(ref Quaternion quaternion, out Quaternion result)
    {
        result._x = -quaternion._x;
        result._y = -quaternion._y;
        result._z = -quaternion._z;
        result._w = -quaternion._w;
    }

    public void Normalize()
    {
        var num2 = _x * _x + _y * _y + _z * _z + _w * _w;
        var num = 1f / Math.Sqrt(num2);
        _x *= num;
        _y *= num;
        _z *= num;
        _w *= num;
    }

    public static Quaternion Normalize(Quaternion quaternion)
    {
        var quaternion2 = new Quaternion();
        var num2 = quaternion._x * quaternion._x + quaternion._y * quaternion._y + quaternion._z * quaternion._z + quaternion._w * quaternion._w;
        var num = 1f / Math.Sqrt(num2);
        quaternion2._x = quaternion._x * num;
        quaternion2._y = quaternion._y * num;
        quaternion2._z = quaternion._z * num;
        quaternion2._w = quaternion._w * num;
        return quaternion2;
    }

    public static void Normalize(ref Quaternion quaternion, out Quaternion result)
    {
        var num2 = quaternion._x * quaternion._x + quaternion._y * quaternion._y + quaternion._z * quaternion._z + quaternion._w * quaternion._w;
        var num = 1f / Math.Sqrt(num2);
        result._x = quaternion._x * num;
        result._y = quaternion._y * num;
        result._z = quaternion._z * num;
        result._w = quaternion._w * num;
    }

    public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion
        {
            _x = quaternion1._x + quaternion2._x,
            _y = quaternion1._y + quaternion2._y,
            _z = quaternion1._z + quaternion2._z,
            _w = quaternion1._w + quaternion2._w
        };
        return quaternion;
    }

    public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion();
        var x = quaternion1._x;
        var y = quaternion1._y;
        var z = quaternion1._z;
        var w = quaternion1._w;
        var num14 = quaternion2._x * quaternion2._x + quaternion2._y * quaternion2._y + quaternion2._z * quaternion2._z + quaternion2._w * quaternion2._w;
        var num5 = 1f / num14;
        var num4 = -quaternion2._x * num5;
        var num3 = -quaternion2._y * num5;
        var num2 = -quaternion2._z * num5;
        var num = quaternion2._w * num5;
        var num13 = y * num2 - z * num3;
        var num12 = z * num4 - x * num2;
        var num11 = x * num3 - y * num4;
        var num10 = x * num4 + y * num3 + z * num2;
        quaternion._x = x * num + num4 * w + num13;
        quaternion._y = y * num + num3 * w + num12;
        quaternion._z = z * num + num2 * w + num11;
        quaternion._w = w * num - num10;
        return quaternion;
    }

    public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
    {
        return Math.Abs(quaternion1._x - quaternion2._x) < Tolerance && Math.Abs(quaternion1._y - quaternion2._y) < Tolerance && Math.Abs(quaternion1._z - quaternion2._z) < Tolerance && Math.Abs(quaternion1._w - quaternion2._w) < Tolerance;
    }

    public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
    {
        if (Math.Abs(quaternion1._x - quaternion2._x) < Tolerance && Math.Abs(quaternion1._y - quaternion2._y) < Tolerance && Math.Abs(quaternion1._z - quaternion2._z) < Tolerance)
        {
            return Math.Abs(quaternion1._w - quaternion2._w) > Tolerance;
        }
        return true;
    }

    public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion();
        var x = quaternion1._x;
        var y = quaternion1._y;
        var z = quaternion1._z;
        var w = quaternion1._w;
        var num4 = quaternion2._x;
        var num3 = quaternion2._y;
        var num2 = quaternion2._z;
        var num = quaternion2._w;
        var num12 = y * num2 - z * num3;
        var num11 = z * num4 - x * num2;
        var num10 = x * num3 - y * num4;
        var num9 = x * num4 + y * num3 + z * num2;
        quaternion._x = x * num + num4 * w + num12;
        quaternion._y = y * num + num3 * w + num11;
        quaternion._z = z * num + num2 * w + num10;
        quaternion._w = w * num - num9;
        return quaternion;
    }

    public static Quaternion operator *(Quaternion quaternion1, double scaleFactor)
    {
        var quaternion = new Quaternion
        {
            _x = quaternion1._x * scaleFactor,
            _y = quaternion1._y * scaleFactor,
            _z = quaternion1._z * scaleFactor,
            _w = quaternion1._w * scaleFactor
        };
        return quaternion;
    }

    public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
    {
        var quaternion = new Quaternion
        {
            _x = quaternion1._x - quaternion2._x,
            _y = quaternion1._y - quaternion2._y,
            _z = quaternion1._z - quaternion2._z,
            _w = quaternion1._w - quaternion2._w
        };
        return quaternion;
    }

    public static Quaternion operator -(Quaternion quaternion)
    {
        var quaternion2 = new Quaternion
        {
            _x = -quaternion._x, _y = -quaternion._y, _z = -quaternion._z, _w = -quaternion._w
        };
        return quaternion2;
    }

    public override string ToString()
    {
        var sb = new StringBuilder(32);
        sb.Append("{X:");
        sb.Append(_x);
        sb.Append(" Y:");
        sb.Append(_y);
        sb.Append(" Z:");
        sb.Append(_z);
        sb.Append(" W:");
        sb.Append(_w);
        sb.Append("}");
        return sb.ToString();
    }

    internal Matrix4x4 ToMatrix()
    {
        ToMatrix(out var matrix);
        return matrix;
    }

    internal void ToMatrix(out Matrix4x4 matrix)
    {
        ToMatrix(this, out matrix);
    }

    internal static void ToMatrix(Quaternion quaternion, out Matrix4x4 matrix)
    {

        var x2 = quaternion._x * quaternion._x;
        var y2 = quaternion._y * quaternion._y;
        var z2 = quaternion._z * quaternion._z;
        var xy = quaternion._x * quaternion._y;
        var xz = quaternion._x * quaternion._z;
        var yz = quaternion._y * quaternion._z;
        var wx = quaternion._w * quaternion._x;
        var wy = quaternion._w * quaternion._y;
        var wz = quaternion._w * quaternion._z;
        matrix = new Matrix4x4
        {
            [0, 0] = (float) (1.0f - 2.0f * (y2 + z2)),
            [0, 1] = (float) (2.0f * (xy - wz)),
            [0, 2] = (float) (2.0f * (xz + wy)),
            [0, 3] = 0.0f,
            [1, 0] = (float) (2.0f * (xy + wz)),
            [1, 1] = (float) (1.0f - 2.0f * (x2 + z2)),
            [1, 2] = (float) (2.0f * (yz - wx)),
            [1, 3] = 0.0f,
            [2, 0] = (float) (2.0f * (xz - wy)),
            [2, 1] = (float) (2.0f * (yz + wx)),
            [2, 2] = (float) (1.0f - 2.0f * (x2 + y2)),
            [2, 3] = 0.0f,
            [3, 0] = (float) (2.0f * (xz - wy)),
            [3, 1] = (float) (2.0f * (yz + wx)),
            [3, 2] = (float) (1.0f - 2.0f * (x2 + y2)),
            [3, 3] = 0.0f
        };
    } 
    
    internal Vector3 Xyz
    {
        get => new Vector3((float) _x, (float) _y, (float) _z);

        set
        {
            _x = value.x;
            _y = value.y;
            _z = value.z;
        }
    }
}

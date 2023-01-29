using System;
using System.Collections.Generic;
using System.Text;
using SMath =System.Math;

namespace ArgusLib.Math
{
	public class Matrix33Double : Matrix33<DoubleWrapper>
	{
		public Matrix33Double(double value)
			: base(new DoubleWrapper(value)) { }

		public Matrix33Double()
			: this(0) { }

		public Matrix33Double(double[,] values)
			: base()
		{
			if (values.GetLength(0) != 3)
				throw new DimensionException();
			if (values.GetLength(1) != 3)
				throw new DimensionException();

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					this[i, j] = values[i, j];
				}
			}
		}

		public Matrix33Double(Matrix<DoubleWrapper> matrix)
			: base(matrix) { }

		public static Matrix33Double Identity
		{
			get
			{
				Matrix33Double m = new Matrix33Double();
				for (int i = 0; i < 3; i++)
					m[i, i] = 1;
				return m;
			}
		}

		public static Matrix33Double GetRotationMatrixX(double angle)
		{
			return new Matrix33Double(new double[,]{
				{1,0,0},
				{0,SMath.Cos(angle), -SMath.Sin(angle)},
				{0,SMath.Sin(angle),SMath.Cos(angle)}});
		}

		public static Matrix33Double GetRotationMatrixY(double angle)
		{
			return new Matrix33Double(new double[,]{
				{SMath.Cos(angle),0,SMath.Sin(angle)},
				{0,1,0},
				{-SMath.Sin(angle),0,SMath.Cos(angle)}});
		}

		public static Matrix33Double GetRotationMatrixZ(double angle)
		{
			return new Matrix33Double(new double[,]{
				{SMath.Cos(angle), -SMath.Sin(angle),0},
				{SMath.Sin(angle),SMath.Cos(angle),0},
				{0,0,1}});
		}

		public static Matrix33Double GetScalingMatrix(double scaleX, double scaleY, double scaleZ)
		{
			return new Matrix33Double(new double[,]{
				{scaleX,0,0},
				{0,scaleY,0},
				{0,0,scaleZ}});
		}

		/// <summary>
		/// Experimental!
		/// Returns the angles (in radians) a vector is rotated around the x-, y- and z-Axis.
		/// </summary>
		/// <param name="transformationMatrix"></param>
		/// <returns></returns>
		public static Vector3Double ExtractAngles(Matrix33Double transformationMatrix)
		{
			Vector3Double RetVal = new Vector3Double();
			Matrix<DoubleWrapper> m = new Matrix<DoubleWrapper>(3, 1, 0);
			
			m[0, 0] = 1; // m = (1 0 0)
			Matrix<DoubleWrapper> transformed = transformationMatrix * m;
			RetVal.Z = SMath.Atan2(transformed[1, 0], transformed[0, 0]); // arctan(y/x)
			
			m[0, 0] = 0;
			m[1, 0] = 1; // m = (0 1 0)
			transformed = transformationMatrix * m;
			RetVal.X = SMath.Atan2(transformed[2, 0], transformed[1, 0]); // arctan(z/y)

			m[1, 0] = 0;
			m[2, 0] = 1; // m = (0 0 1)
			transformed = transformationMatrix * m;
			RetVal.Y = SMath.Atan2(transformed[0,0 ], transformed[2, 0]); // arctan(x/z)

			return RetVal;
		}

		public static Vector3Double ExtractScaling(Matrix33Double transformationMatrix)
		{
			Matrix<DoubleWrapper> m = new Matrix<DoubleWrapper>(3, 1, 1);
			m = transformationMatrix * m;

			return new Vector3Double(m.RowVectors[0]);
		}
	}
}

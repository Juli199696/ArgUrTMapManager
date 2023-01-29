using System;
using System.Collections.Generic;
using SMath = System.Math;

namespace ArgusLib.Math
{
	/// <summary>
	/// A struct representing a vector in 3D.
	/// </summary>
	public struct Vector3
	{
		/// <summary>
		/// Gets or sets a <see cref="double"/> representing the x-coordinate of the <see cref="Vector3"/>.
		/// </summary>
		public double X { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="double"/> representing the y-coordinate of the <see cref="Vector3"/>.
		/// </summary>
		public double Y { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="double"/> representing the z-coordinate of the <see cref="Vector3"/>.
		/// </summary>
		public double Z { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="Vector3"/>.
		/// </summary>
		public Vector3(double x, double y, double z)
			: this()
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Gets the length of the <see cref="Vector3"/>.
		/// </summary>
		/// <returns>A <see cref="double"/> representing the length of the <see cref="Vector3"/>.</returns>
		public double GetLength()
		{
			return SMath.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
		}

		/// <summary>
		/// Gets the distance between two points.
		/// </summary>
		/// <param name="a">A <see cref="Vector3"/> representing the first point.</param>
		/// <param name="b">A <see cref="Vector3"/> representing the second point.</param>
		/// <returns>A <see cref="double"/> representing the distance between the two points.</returns>
		public static double GetDistance(Vector3 a, Vector3 b)
		{
			return Vector3.Subtract(a, b).GetLength();
		}

		/// <summary>
		/// Adds two <see cref="Vector3"/>.
		/// </summary>
		public static Vector3 Add(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		/// <summary>
		/// Adds two <see cref="Vector3"/>.
		/// </summary>
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return Vector3.Add(a,b);
		}

		/// <summary>
		/// Subtracts one <see cref="Vector3"/> from another.
		/// </summary>
		public static Vector3 Subtract(Vector3 a, Vector3 b)
		{
			return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		/// <summary>
		/// Subtracts one <see cref="Vector3"/> from another.
		/// </summary>
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return Vector3.Subtract(a, b);
		}

		/// <summary>
		/// Multiplies a <see cref="Vector3"/> with a scalar (<see cref="double"/>).
		/// </summary>
		public static Vector3 Multiply(Vector3 v, double a)
		{
			v.X *= a;
			v.Y *= a;
			v.Z *= a;
			return v;
		}

		/// <summary>
		/// Multiplies a <see cref="Vector3"/> with a scalar (<see cref="double"/>).
		/// </summary>
		public static Vector3 operator *(Vector3 v, double a)
		{
			return Vector3.Multiply(v, a);
		}

		/// <summary>
		/// Multiplies a <see cref="Vector3"/> with a scalar (<see cref="double"/>).
		/// </summary>
		public static Vector3 operator *(double a, Vector3 v)
		{
			return Vector3.Multiply(v, a);
		}

		/// <summary>
		/// Calculates the scalarprodukt of two <see cref="Vector3"/>.
		/// </summary>
		public static double ScalarProduct(Vector3 a, Vector3 b)
		{
			return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
		}

		/// <summary>
		/// Calculates the crossproduct of two <see cref="Vector3"/>.
		/// </summary>
		public static Vector3 CrossProduct(Vector3 a, Vector3 b)
		{
			Vector3 v = new Vector3();
			v.X = a.Y * b.Z - a.Z * b.Y;
			v.Y = a.Z * b.X - a.X * b.Z;
			v.Z = a.X * b.Y - a.Y * b.X;
			return v;
		}

		/// <summary>
		/// Gets the two points in <paramref name="points"/> which are the farthest away from each other
		/// and returns the distance.
		/// </summary>
		/// <param name="points">A <see cref="IEnumerable<Vector3>"/> specifying points in the 3D-space.</param>
		/// <param name="index1">The index of the first point.</param>
		/// <param name="index2">The index of the second point.</param>
		/// <returns>The distance between the two points.</returns>
		public static double GetGreatestDistance(IEnumerable<Vector3> points, out int index1, out int index2)
		{
			int i = -1;
			double distance = 0;
			index1 = -1;
			index2 = -1;
			foreach (Vector3 v1 in points)
			{
				int j = -1;
				i++;
				foreach (Vector3 v2 in points)
				{
					j++;
					if (i == j)
						continue;
					double d = Vector3.GetDistance(v1, v2);
					if (d > distance)
					{
						distance = d;
						index1 = i;
						index2 = j;
					}
				}
			}

			if (i < 2)
				throw new ArgumentException("Must at least contain two elements", "points");
			return distance;
		}

		/// <summary>
		/// Gets the distance from each point in <paramref name="points"/> to the point <paramref name="p"/>.
		/// The returned values are sorted from greatest to smallest distance.
		/// </summary>
		/// <param name="p">A <see cref="Vector3"/> specifying a point in the 3D-space.</param>
		/// <param name="points">A <see cref="IEnumerable<Vector3>"/> specifying points in the 3D-space.</param>
		/// <param name="indices">
		/// A <see cref="List<int>"/> containing the indices of the points corresponding to
		/// the order in the returned list of distances.
		/// </param>
		/// <returns></returns>
		public static List<double> GetGreatestDistance(Vector3 p, IEnumerable<Vector3> points, out List<int> indices)
		{
			List<double> distances = new List<double>();
			indices = new List<int>();
			int i = -1;
			foreach (Vector3 v in points)
			{
				i++;
				double d = Vector3.GetDistance(p, v);
				for (int j = 0; j < distances.Count; j++)
				{
					if (d > distances[j])
					{
						distances.Insert(j, d);
						indices.Insert(j, i);
						d = -1;
						break;
					}
				}
				if (d < 0)
					continue;
				distances.Add(d);
				indices.Add(i);
			}
			return distances;
		}

		/// <summary>
		/// Gets the two points in <paramref name="points"/> which are nearest
		/// and returns the distance.
		/// </summary>
		/// <param name="points">A <see cref="IEnumerable<Vector3>"/> specifying points in the 3D-space.</param>
		/// <param name="index1">The index of the first point.</param>
		/// <param name="index2">The index of the second point.</param>
		/// <returns>The distance between the two points.</returns>
		public static double GetSmallestDistance(IEnumerable<Vector3> points, out int index1, out int index2)
		{
			int i = -1;
			double distance = double.PositiveInfinity;
			index1 = -1;
			index2 = -1;
			foreach (Vector3 v1 in points)
			{
				int j = -1;
				i++;
				foreach (Vector3 v2 in points)
				{
					j++;
					if (i == j)
						continue;
					double d = Vector3.GetDistance(v1, v2);
					if (d < distance)
					{
						distance = d;
						index1 = i;
						index2 = j;
					}
				}
			}

			if (i < 2)
				throw new ArgumentException("Must at least contain two elements", "points");
			return distance;
		}

		/// <summary>
		/// Gets the distance from each point in <paramref name="points"/> to the point <paramref name="p"/>.
		/// The returned values are sorted from smallest to greatest distance.
		/// </summary>
		/// <param name="p">A <see cref="Vector3"/> specifying a point in the 3D-space.</param>
		/// <param name="points">A <see cref="IEnumerable<Vector3>"/> specifying points in the 3D-space.</param>
		/// <param name="indices">
		/// A <see cref="List<int>"/> containing the indices of the points corresponding to
		/// the order in the returned list of distances.
		/// </param>
		/// <returns></returns>
		public static List<double> GetSmallestDistance(Vector3 p, IEnumerable<Vector3> points, out List<int> indices)
		{
			List<double> distances = new List<double>();
			indices = new List<int>();
			int i = -1;
			foreach (Vector3 v in points)
			{
				i++;
				double d = Vector3.GetDistance(p, v);
				for (int j = 0; j < distances.Count; j++)
				{
					if (d < distances[j])
					{
						distances.Insert(j, d);
						indices.Insert(j, i);
						d = -1;
						break;
					}
				}
				if (d < 0)
					continue;
				distances.Add(d);
				indices.Add(i);
			}
			return distances;
		}
	}
}

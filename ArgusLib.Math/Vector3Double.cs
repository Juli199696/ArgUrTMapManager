using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public class Vector3Double : Vector3<DoubleWrapper>
	{
		public Vector3Double()
			: this(0, 0, 0) { }

		public Vector3Double(double x, double y, double z)
			: base(new DoubleWrapper(x), new DoubleWrapper(y), new DoubleWrapper(z)) { }

		public Vector3Double(Vector3Double vector)
			: base(vector.X, vector.Y, vector.Z) { }

		public Vector3Double(Vector3<DoubleWrapper> vector)
			: this(vector.X, vector.Y, vector.Z) { }

		public Vector3Double(Vector<DoubleWrapper> vector)
			: this(new Vector3<DoubleWrapper>(vector)) { }

		public static double GetDistance(Vector3Double a, Vector3Double b)
		{
			return System.Math.Sqrt((b - a).GetSquaredEuclidNorm());
		}

		/// <summary>
		/// Gets the two points in <paramref name="points"/> which are the farthest away from each other
		/// and returns the distance.
		/// </summary>
		/// <param name="points">A <see cref="IEnumerable<Vector3>"/> specifying points in the 3D-space.</param>
		/// <param name="index1">The index of the first point.</param>
		/// <param name="index2">The index of the second point.</param>
		/// <returns>The distance between the two points.</returns>
		public static double GetGreatestDistance(IEnumerable<Vector3Double> points, out int index1, out int index2)
		{
			int i = -1;
			double distance = 0;
			index1 = -1;
			index2 = -1;
			foreach (Vector3Double v1 in points)
			{
				int j = -1;
				i++;
				foreach (Vector3Double v2 in points)
				{
					j++;
					if (i == j)
						continue;
					double d = Vector3Double.GetDistance(v1, v2);
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
		public static List<double> GetGreatestDistance(Vector3Double p, IEnumerable<Vector3Double> points, out List<int> indices)
		{
			List<double> distances = new List<double>();
			indices = new List<int>();
			int i = -1;
			foreach (Vector3Double v in points)
			{
				i++;
				double d = Vector3Double.GetDistance(p, v);
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
		/// <param name="points">A <see cref="IEnumerable{Vector3}"/> specifying points in the 3D-space.</param>
		/// <param name="index1">The index of the first point.</param>
		/// <param name="index2">The index of the second point.</param>
		/// <returns>The distance between the two points.</returns>
		public static double GetSmallestDistance(IEnumerable<Vector3Double> points, out int index1, out int index2)
		{
			int i = -1;
			double distance = double.PositiveInfinity;
			index1 = -1;
			index2 = -1;
			foreach (Vector3Double v1 in points)
			{
				int j = -1;
				i++;
				foreach (Vector3Double v2 in points)
				{
					j++;
					if (i == j)
						continue;
					double d = Vector3Double.GetDistance(v1, v2);
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
		/// <param name="points">A <see cref="IEnumerable{Vector3}"/> specifying points in the 3D-space.</param>
		/// <param name="indices">
		/// A <see cref="List<int>"/> containing the indices of the points corresponding to
		/// the order in the returned list of distances.
		/// </param>
		/// <returns></returns>
		public static List<double> GetSmallestDistance(Vector3Double p, IEnumerable<Vector3Double> points, out List<int> indices)
		{
			List<double> distances = new List<double>();
			indices = new List<int>();
			int i = -1;
			foreach (Vector3Double v in points)
			{
				i++;
				double d = Vector3Double.GetDistance(p, v);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	public static class GenerateMatrix
	{

		public struct Point
		{
			public int x, y;
			public Point(int px, int py)
			{
				x = px;
				y = py;
			}
		}

	

		/// <summary>
		/// Generates a random coordinates to render the souls 
		/// </summary>
		/// <returns></returns>
		public static List<Point> GenerateRandomShape() 
		{
	
			int randomNumber = Mathf.FloorToInt(UnityEngine.Random.Range(1f, 6f));

			switch (randomNumber)
			{
				case 1:
					return Cross();
				case 2:
					return ChesseBoard();
				case 3:
					return Eye();
				case 4:
					return Fence();
				case 5:
					return Square();
			}

			return null;
		}




		/// <summary>
		/// It creates a Cross 
		/// </summary>
		/// The 1 respresents a soul and 0 an empty space
		/// <returns>	
		/// 1 0 0 0 0 1
		/// 0 1 0 0 1 0
		/// 0 0 1 1 0 0
		/// 0 0 1 1 0 0
		/// 0 1 0 0 1 0
		/// 1 0 0 0 0 1
		/// </returns>

		private static List<Point> Cross()
	   {
			List<Point> coordinate = new List<Point> {
				new Point(1, 1),
				new Point(1, 6),
				new Point(2, 2),
				new Point(2, 5),
				new Point(3, 3),
				new Point(3, 4),
				new Point(4, 3),
				new Point(4, 4),
				new Point(5, 2),
				new Point(5, 5),
				new Point(6, 1),
				new Point(6, 6),
			 }; 

			return coordinate;
		}

		/// <summary>
		/// Generates a Chesse board
		/// </summary>
	    /// The 1 respresents a soul and 0 an empty space
		/// <returns> 
		/// 1 1 0 0 1 1
		/// 1 1 0 0 1 1
		/// 0 0 1 1 0 0
		/// 0 0 1 1 0 0
		/// 1 1 0 0 1 1
		/// 1 1 0 0 1 1
		/// </returns>
		private static List<Point> ChesseBoard()
		{
			List<Point> coordinate = new List<Point> {
				new Point(1, 1),
				new Point(1, 2),
				new Point(1, 5),
				new Point(1, 6),
				new Point(2, 1),
				new Point(2, 2),
				new Point(2, 5),
				new Point(2, 6),
				new Point(3, 3),
				new Point(3, 4),
				new Point(4, 3),
				new Point(4, 4),
				new Point(5, 1),
				new Point(5, 2),
				new Point(5, 5),
				new Point(5, 6),
				new Point(6, 1),
				new Point(6, 2),
				new Point(6, 5),
				new Point(6, 6),
			 };

			return coordinate;
		}

		/// <summary>
		/// Generates an Eye
		/// </summary>
		/// The 1 respresents a soul and 0 an empty space
		/// <returns> 
		/// 0 0 1 1 0 0
		/// 0 1 0 0 1 0
		/// 1 0 1 1 0 1
		/// 1 0 1 1 0 1
		/// 0 1 0 0 1 0
		/// 0 0 1 1 0 0
		/// </returns>
		public static List<Point> Eye()
		{
			List<Point> coordinate = new List<Point> {
				new Point(1, 3),
				new Point(1, 4),
				new Point(2, 2),
				new Point(2, 5),
				new Point(3, 1),
				new Point(3, 3),
				new Point(3, 4),
				new Point(3, 6),
				new Point(4, 1),
				new Point(4, 3),
				new Point(4, 4),
				new Point(4, 6),
				new Point(5, 2),
				new Point(5, 5),
				new Point(6, 3),
				new Point(6, 4)
			 };

			return coordinate;
		}


		/// <summary>
		/// Generates an Fence
		/// </summary>
		/// The 1 respresents a soul and 0 an empty space
		/// <returns> 
		/// 1 0 0 0 0 1
		/// 1 1 0 0 1 1
		/// 1 0 1 1 0 1
		/// 1 0 1 1 0 1
		/// 1 1 0 0 1 1
		/// 1 0 0 0 0 1
		/// </returns>

		private static List<Point> Fence()
		{
			List<Point> coordinate = new List<Point> {
				new Point(1, 1),
				new Point(1, 6),
				new Point(2, 1),
				new Point(2, 2),
				new Point(2, 5),
				new Point(2, 6),
				new Point(3, 1),
				new Point(3, 3),
				new Point(3, 4),
				new Point(3, 6),
				new Point(4, 1),
				new Point(4, 3),
				new Point(4, 4),
				new Point(4, 6),
				new Point(5, 1),
				new Point(5, 2),
		        new Point(5, 5),
				new Point(5, 6),
			    new Point(6, 1),
				new Point(6, 6),
			 };

			return coordinate;
		}

		/// <summary>
		/// Generates an Square
		/// </summary>
		/// The 1 respresents a soul and 0 an empty space
		/// <returns> 
		/// 1 1 1 1 1 1
		/// 1 0 0 0 0 1
		/// 1 0 1 1 0 1
		/// 1 0 1 1 0 1
		/// 1 0 0 0 0 1
		/// 1 1 1 1 1 1
		/// </returns>
		private static List<Point> Square()
		{
			List<Point> coordinate = new List<Point> {
				new Point(1, 1),
				new Point(1, 2),
				new Point(1, 3),
				new Point(1, 4),
				new Point(1, 5),
				new Point(1, 6),
				new Point(2, 1),
				new Point(2, 6),
				new Point(3, 1),
				new Point(3, 3),
				new Point(3, 4),
				new Point(3, 6),
				new Point(4, 1),
				new Point(4, 3),
				new Point(4, 4),
				new Point(4, 6),
				new Point(5, 1),
				new Point(5, 6),
				new Point(6, 1),
				new Point(6, 2),
				new Point(6, 3),
				new Point(6, 4),
				new Point(6, 5),
				new Point(6, 6),
			 };

			return coordinate;

		}

	}
}

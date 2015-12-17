using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GameOfLive
{

	public class Tests
	{
		[TestCase]
		public void Calling_Next_Genertion_Should_Return_A_Valid_Board()
		{
			var board = new Board(3,4);
			var nextGen = board.CreateNextGeneration();

			Assert.AreEqual(3, nextGen.GetLength(0) );	
			Assert.AreEqual(4, nextGen.GetLength(1) );	
		}

		[TestCase]
		public void Calling_Get_Living_Neighbor_Count_Is_Correct()
		{
			var board = new Board(3,3);

			Assert.AreEqual(0, board.GetLivingNeighborCount(0, 0) );
		}

		[TestCase]
		public void Initialize_1_1_Sets_one_living_Cell()
		{
			var x = 1;
			var y = 1;
			var board = new Board(3,3);
			board.SetLivingCell(y, x);

			Assert.IsTrue(board.InitGeneration[y,x]);
		}

		[TestCase]
		public void One_living_Cell_Without_neighbors_dies()
		{
			var x = 1;
			var y = 1;
			var board = new Board(3, 3);
			board.SetLivingCell(y, x);

			var nextGen = board.CreateNextGeneration();

			Assert.IsFalse(nextGen[y, x]);
		}

		[TestCase]
		public void One_dead_Cell_should_be_alive_with_3_living_neighbors()
		{
			var x = 0;
			var y = 0;
			var board = new Board(3, 3);
			board.SetLivingCell(y, x);
			board.SetLivingCell(y+1, x);
			board.SetLivingCell(y+2, x);

			var nextGen = board.CreateNextGeneration();

			Assert.IsTrue(nextGen[1,1]);
			
		}

		[TestCase]
		public void A_Dead_Cell_With_Two_Neighbors_Stays_Dead()
		{
			var x = 0;
			var y = 0;
			var board = new Board(3, 3);
			board.SetLivingCell(y+1, x);
			board.SetLivingCell(y, x+1);

			var nextGen = board.CreateNextGeneration();

			Assert.IsFalse(nextGen[y,x]);

		}

		[TestCase]
		public void A_Living_Cell_With_Two_Neighbors_Stays_Alive()
		{
			var x = 0;
			var y = 0;
			var board = new Board(3, 3);
			board.SetLivingCell(y, x);
			board.SetLivingCell(y + 1, x);
			board.SetLivingCell(y, x + 1);

			var nextGen = board.CreateNextGeneration();

			Assert.IsTrue(nextGen[y, x]);
		}

		[TestCase]
		public void A_Living_Cell_With_More_Than_3_Neighbors_Dies()
		{
			
			var board = new Board(3, 3);
			board.SetLivingCell(1, 1);
			board.SetLivingCell(2, 2);
			board.SetLivingCell(1, 2);
			board.SetLivingCell(2,1);
			board.SetLivingCell(0,1);

			var nextGen = board.CreateNextGeneration();

			Assert.IsFalse(nextGen[1,1]);
		}

		[TestCase]
		public void A_Living_Cell_With_One_Neighbor_Dies()
		{
			var board = new Board(3,3);
			board.SetLivingCell(0,0);
			board.SetLivingCell(0,1);

			var nextGen = board.CreateNextGeneration();

			Assert.IsFalse(nextGen[0, 0]);
			Assert.IsFalse(nextGen[0, 1]);
		}


	}

	//public class Praesi
	//{
	//	public static void Main(string[] args)
	//	{
	//		var board = new Board(5,5);
	//		board.SetLivingCell(1,1);
	//		board.SetLivingCell(2,2);
	//		board.SetLivingCell(3,3);
	//		for (int a = 0; a > 100; a++)
	//		{
	//			for (int i = 0; i < 5; i++)
	//			{
	//				for (int j = 0; j < 5; j++)
	//				{
	//					Console.Write(board.InitGeneration[i][j]);
	//				}
	//				Console.WriteLine();
	//			}
	//			board = board.CreateNextGeneration();
	//		}

	//	}
	//}

	public class Board
	{
		private readonly int _height;
		private readonly int _width;

		public Board(int height, int width)
		{
			this._width = width;
			this._height = height;
			InitGeneration = new bool[height, width];
		}

		public bool[,] InitGeneration { get; }

		public bool[,] CreateNextGeneration()
		{
			var nextGen = new Board(_height,_width);

			for (int y = 0; y < _height; y++)
			{
				for (int x = 0; x < _width; x++)
				{
					var count = GetLivingNeighborCount(y, x);
					if (count == 3)
					{
						nextGen.SetLivingCell(y,x);
					}
					else if (count == 2)
					{
						if (InitGeneration[y, x])
						{
							nextGen.SetLivingCell(y, x);
						}
					}
				}
			}
			return nextGen.InitGeneration;
		}

		public int GetLivingNeighborCount(int row, int col)
		{
			int countLivingCells = 0;

			int lowerRowLimit = Math.Max(row-1, 0);
			int upperRowLimit = Math.Min(_height-1, row + 1);
			int lowerColLimit = Math.Max(col - 1, 0);
			int upperColLimit = Math.Min(_width - 1, col + 1);
			for (int i = lowerRowLimit; i <= upperRowLimit; i++)
			{
				for (int j = lowerColLimit; j <= upperColLimit; j++)
				{
					if(i == row && j == col)
						continue;

					if (this.InitGeneration[i, j])
						countLivingCells++;
				}
			}

			return countLivingCells;
		}

		public void SetLivingCell(int y, int x)
		{
			InitGeneration[y, x] = true;
		}
	}
}

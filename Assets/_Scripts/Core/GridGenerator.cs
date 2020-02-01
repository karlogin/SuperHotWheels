using System;
using UnityEngine;

namespace SuperHotWheels.Core
{
	public class GridGenerator : MonoBehaviour
	{
		[Header("Grid Dimension (in cms)")]
		[SerializeField] private int gridSizeX;
		[SerializeField] private int gridSizeY;

		[SerializeField] private int blockSize;

		private int gridDimensionX;
		private int gridDimensionY;
		private int[,] grid;

		private void OnEnable()
		{
			InitializeGrid();
		}

		private void OnDisable()
		{
			//
		}

		private void InitializeGrid()
		{
			int gridDimensionX = gridSizeX / blockSize;
			int gridDimensionY = gridSizeY / blockSize;

			grid = new int[gridDimensionX, gridDimensionY];

			ClearGrid();
		}

		private void ClearGrid()
		{
			for (int i = 0; i < gridDimensionX; i++)
			{
				for (int j = 0; j < gridDimensionY; j++)
				{
					grid[i, j] = -1;
				}
			}
		}
	}
}

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using SuperHotWheels.PathFinding;
using System.Collections.Generic;

namespace SuperHotWheels.Core
{
	public class GridGenerator : MonoBehaviour
	{
		[Header("Grid Dimension (in cms)")]
		[SerializeField] private int gridSizeX;
		[SerializeField] private int gridSizeY;

		[SerializeField] private int blockSize;

		[Space]
		[SerializeField] private GameObject baseObject;
		[SerializeField] private Material baseMaterial;

		[Space]
		[SerializeField] private Transform blocksParent;

		private int gridDimensionX;
		private int gridDimensionY;
		private int[,] grid;

		System.Random random = new System.Random();

		[SerializeField]
		GridBlockObject[] blocks;

		Vector2 start;
		Vector2 end;

		private void OnEnable()
		{
			InitializeGrid();
		}

		private void OnDisable()
		{
			//
		}

		[Button]
		private void UpdateGrid()
		{
			baseObject.transform.localScale = new Vector3((float)gridSizeX / 1000f, 1.0f, (float)gridSizeY / 1000f);
		}

		private void InitializeGrid()
		{
			gridDimensionX = gridSizeX / blockSize;
			gridDimensionY = gridSizeY / blockSize;

			grid = new int[gridDimensionX, gridDimensionY];

			ClearGrid();
			SetStartAndEnd();
			BuildTrack();
		}

		private void BuildTrack()
		{
			float[,] gridFloat = new float[gridDimensionX, gridDimensionY];

			System.Random random = new System.Random();
			for (int i = 0; i < gridDimensionX; i++)
			{
				for (int j = 0; j < gridDimensionY; j++)
				{
					gridFloat[i, j] = (float)random.NextDouble();
				}
			}

			PathFinding.Grid grids = new PathFinding.Grid(gridFloat);
			List<Point> path = Pathfinding.FindPath(grids, new Point((int)start.x, (int)start.y), new Point((int)end.x, (int)end.y));
			foreach (var data in path)
			{
				Debug.Log(data.x + " :: " + data.y);
				if (data.x != start.x && data.y != start.y)
				{ 
				
				}
				var straightBlock = GetBlock(BlockType.STRAIGHT);
				var st1 = Instantiate(straightBlock.BlockAsset, new Vector3(data.x, 0.0f, data.y), Quaternion.identity, blocksParent);
			}
		}

		private void SetStartAndEnd()
		{
			int startX = random.Next(0, gridDimensionX);
			int startY = random.Next(0, gridDimensionY);

			int endX = random.Next(0, gridDimensionX);
			int endY = random.Next(0, gridDimensionY);

			start = new Vector2(startX, startY);
			end = new Vector2(endX, endY);

			var startBlock = GetBlock(BlockType.START);
			var endBlock = GetBlock(BlockType.END);

			Instantiate(startBlock.BlockAsset, new Vector3(startX, 0.0f, startY), Quaternion.identity, blocksParent);
			Instantiate(endBlock.BlockAsset, new Vector3(endX, 0.0f, endY), Quaternion.identity, blocksParent);
			grid[startX, startY] = (int)BlockType.START;
			grid[endX, endY] = (int)BlockType.END;

			var straightBlock = GetBlock(BlockType.STRAIGHT);
			var st1 = Instantiate(straightBlock.BlockAsset, new Vector3(startX, 0.0f, startY + 1), Quaternion.identity, blocksParent);
			var st2 = Instantiate(straightBlock.BlockAsset, new Vector3(endX, 0.0f, endY + 1), Quaternion.identity, blocksParent);

			st1.GetComponentInChildren<Block>().BlockRotation = 90;
			st2.GetComponentInChildren<Block>().BlockRotation = 90;
			st1.GetComponentInChildren<Block>().UpdateBlock();
			st2.GetComponentInChildren<Block>().UpdateBlock();

			grid[startX, startY + 1] = (int)BlockType.STRAIGHT;
			grid[endX, endY + 1] = (int)BlockType.STRAIGHT;
		}

		private GridBlockObject GetBlock(BlockType blockType)
		{
			return Array.Find(blocks, m => m.BlockType == blockType);
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

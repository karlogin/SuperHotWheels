using UnityEngine;

namespace SuperHotWheels.Core
{
	[CreateAssetMenu(fileName = "NewBlock", menuName = "Super Hot Wheels/New Block", order = 1)]
	public class GridBlockObject : ScriptableObject
	{
		[SerializeField] private BlockType blockType;
		[SerializeField] private GameObject blockAsset;

		public BlockType BlockType { get => blockType; set => blockType = value; }
		public GameObject BlockAsset { get => blockAsset; set => blockAsset = value; }
	}
}

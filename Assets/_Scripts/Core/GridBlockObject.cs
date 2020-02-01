using UnityEngine;

namespace SuperHotWheels.Core
{
	[CreateAssetMenu(fileName = "NewBlock", menuName = "Super Hot Wheels/New Block", order = 1)]
	public class GridBlockObject : ScriptableObject
	{
		[SerializeField] private BlockType blockType;
		[SerializeField] private GameObject blockAsset;
	}
}

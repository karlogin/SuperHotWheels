using UnityEngine;

namespace SuperHotWheels.Core
{
	[CreateAssetMenu(fileName = "NewBlock", menuName = "Super Hot Wheels/New Block", order = 1)]
	public class GridBlockObject : ScriptableObject
	{
		[Header("Block Dimension (in cms)")]
		[SerializeField] private int sizeX;
		[SerializeField] private int sizeY;
		[Space]
		[SerializeField] private GameObject blockAsset;
	}
}

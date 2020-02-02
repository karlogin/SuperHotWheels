using UnityEngine;

namespace SuperHotWheels.Core
{
	public class Block : MonoBehaviour
	{
		[SerializeField] private float blockRotation;

		public float BlockRotation { get => blockRotation; set => blockRotation = value; }

		public void UpdateBlock()
		{
			transform.localEulerAngles = new Vector3(0.0f, BlockRotation, 0.0f);
		}
	}
}

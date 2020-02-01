using UnityEngine;

namespace SuperHotWheels.Core
{
	public class Block : MonoBehaviour
	{
		[SerializeField] private float blockRotation;

		private void OnValidate()
		{
			transform.localEulerAngles = new Vector3(0.0f, blockRotation, 0.0f);
		}
	}
}

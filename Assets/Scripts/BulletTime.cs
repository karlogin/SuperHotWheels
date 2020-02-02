/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
	[SerializeField] private Transform trackerHead;
	[SerializeField] private Transform trackerHandL;
	[SerializeField] private Transform trackerHandR;

	private Vector3 headPos, lHandPos, rHandPos;
	private Vector3 lastHeadPos, lastLHandPos, lastRHandPos;

	private void Update()
	{
		headPos = trackerHead.localPosition;
		lHandPos = trackerHandL.localPosition;
		rHandPos = trackerHandR.localPosition;

		if (headPos != lastHeadPos || lHandPos != lastLHandPos || rHandPos != lastRHandPos)
		{
			Time.timeScale += Time.deltaTime;

			lastHeadPos = trackerHead.localPosition;
			lastLHandPos = trackerHandL.localPosition;
			lastRHandPos = trackerHandR.localPosition;
		}
		else
		{
			Time.timeScale = 0.01f;
		}
	}
}
*/
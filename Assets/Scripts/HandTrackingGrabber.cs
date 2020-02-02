using UnityEngine;
using OculusSampleFramework;
using System;

public class HandTrackingGrabber : OVRGrabber
{
	private Hand hand;
	public float pinchThreshold = 0.7f;

	protected override void Start()
	{
		base.Start();
		hand = GetComponent<Hand>();
	}

	public override void Update()
	{
		base.Update();
		CheckPick();
	}

	bool grabbed = false;

	private void CheckPick()
	{
		float pinchStrength = hand.PinchStrength(OVRPlugin.HandFinger.Index);
		bool isPinching = pinchStrength > pinchThreshold;

		if (!m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
		{
			GrabBegin();
/*			m_grabbedObj.GetComponent<Material>().color = Color.green;*/
			grabbed = true;
		}
		else if (m_grabbedObj && !isPinching)
		{
			if (grabbed)
			{
				m_grabbedObj.GetComponent<TrackPiece>().DropPiece();
/*				m_grabbedObj.GetComponent<Material>().color = Color.white;*/
				grabbed = false;
			}
			GrabEnd();
		}
	}

}

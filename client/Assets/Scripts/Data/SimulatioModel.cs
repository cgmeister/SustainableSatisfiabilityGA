using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SimulationModel {

	public const int BGLIMIT = 300;

	public byte rateOfFire = 0;
	public byte projectileSpeed = 0;
	public byte horizontalMoveSpeed = 0;
	public byte reqDegradation = 0;
	public byte minThreshold = 0;
	public byte maxThreshold = 0;
	public byte reqNum = 0;
	public byte reqDegree = 0;

	public List<Vector2> minMaxThresholdList;

	public void GenerateStartingData(){
		rateOfFire = Convert.ToByte(UnityEngine.Random.Range (1, 1));
		projectileSpeed = Convert.ToByte(UnityEngine.Random.Range (1, 1));
		horizontalMoveSpeed = Convert.ToByte(UnityEngine.Random.Range (1, 1));

		reqDegradation = Convert.ToByte(UnityEngine.Random.Range (1, 2));
		minThreshold = Convert.ToByte(UnityEngine.Random.Range (3, 6));
		//minThreshold = Convert.ToByte(UnityEngine.Random.Range (1, 1));
		maxThreshold = Convert.ToByte(UnityEngine.Random.Range (8, 10));
		//maxThreshold = Convert.ToByte(UnityEngine.Random.Range (2, 2));
		reqNum = Convert.ToByte(UnityEngine.Random.Range (4, 4));
		reqDegree = Convert.ToByte(UnityEngine.Random.Range (8, 12));
		//reqDegree = Convert.ToByte(UnityEngine.Random.Range (2, 2));

		DebugValues ();
	}

	public void DebugValues(){
		Debug.Log ("Rate of Fire: " + rateOfFire);
		Debug.Log ("Projectile Speed: " + projectileSpeed);
		Debug.Log ("Horizontal Move Speed: " + horizontalMoveSpeed);
		Debug.Log ("Req Degradation " + reqDegradation);
		Debug.Log ("Minimun Threshold: " + minThreshold);
		Debug.Log ("Maximun Threshold: " + maxThreshold);
		Debug.Log ("Requirement Number: " + reqNum);
		Debug.Log ("Requirement Degree: " + reqDegree);
	}
}


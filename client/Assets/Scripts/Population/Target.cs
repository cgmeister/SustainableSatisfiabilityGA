using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : MonoBehaviour {

	public Transform container;
	public int lane = 0;
	public int speed = 1;
	public float rate = 1f;
	public int decreaseRate = 1;
	public bool isEnabled = false;
	public float totalTime = 0f;
	public float offset = 0;

	private Vector3 targetPosCache = Vector3.zero;

	// Use this for initialization
	void Start () {
		AddListeners ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnabled){
			totalTime += Time.deltaTime;
			if (totalTime >= rate){
				totalTime = 0;
				targetPosCache = transform.localPosition;
				float limit = -(offset / 2f);
				if (transform.localPosition.y >= limit) {
					targetPosCache.y = limit;
					transform.localPosition = targetPosCache;
				} else {
					targetPosCache.y += offset * speed;
					Debug.Log (offset);
					Debug.Log (speed);
					transform.localPosition = targetPosCache;
				}
			}
		}
	}

	private void AddListeners(){
		Projectile.OnProjectileHit += HandleOnProjectileHit;
	}

	private void RemoveListeners(){
		Projectile.OnProjectileHit -= HandleOnProjectileHit;
	}

	void OnDestroy(){
		RemoveListeners ();
	}

	void HandleOnProjectileHit (int laneIndex){
		if (laneIndex == lane){
			Debug.Log ("Hit lane: " + laneIndex);
			targetPosCache.y -= offset * speed;
		}
	}
}

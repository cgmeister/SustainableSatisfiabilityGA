using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {
	public static event Action<int> OnProjectileHit;
	public Transform container;
	public int laneIndex = 0;
	public int speed = 1;
	public bool isEnabled = false;
	public float totalTime = 0;
	public int rate = 1;
	public float offset = 0;
	private Vector3 projectilePos = Vector3.zero; 
	private bool hit = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (isEnabled){								
			totalTime += Time.deltaTime;
			if (totalTime >= rate){
				totalTime = 0;
				projectilePos = transform.localPosition;
				projectilePos.y += speed * offset;
				transform.localPosition = projectilePos;
				if (transform.localPosition.y > 0){
					Destroy (gameObject);
				}
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		Debug.Log ("Projectile Hit");
		if (hit) 
			return;
		hit = true;
		if (OnProjectileHit != null){
			OnProjectileHit (laneIndex);
		}
		Destroy (gameObject);
	}
}

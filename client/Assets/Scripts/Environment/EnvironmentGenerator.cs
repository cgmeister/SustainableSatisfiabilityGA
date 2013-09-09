using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject _thresholdPrefab = null;

	[SerializeField]
	private GameObject _lanePrefab = null;

	[SerializeField]
	private GameObject _objectPrefab = null;

	public List<GameObject> minThresholdGOList;
	public List<GameObject> maxThresholdGOList;

	public List<Vector2> minMaxThresholdList;

	public List<GameObject> objectsToSat;

	//List<GameObject> _edges = null;
	public void GenerateEnvironment(Transform container, int minT, int maxT, int reqNum, int degree, float offset){
		minThresholdGOList = new List<GameObject> ();
		maxThresholdGOList = new List<GameObject> ();

		minMaxThresholdList = new List<Vector2> ();

		// create the edges
		int len = reqNum + 1;
		for (int i=0; i<len; i++){
			// Generate Lanes
			GameObject lane = (GameObject) Instantiate (_lanePrefab, Vector3.zero, Quaternion.identity);
			lane.transform.parent = container;
			lane.transform.localScale = Vector3.one;
			lane.transform.transform.localPosition = Vector3.zero;

			Vector3 laneLocalScale = lane.transform.GetChild (0).localScale;
			Vector3 laneLocalPosition = lane.transform.GetChild (0).localPosition;
			Vector3 laneScale = lane.transform.localScale;

			lane.transform.localScale = new Vector3 (laneScale.x, degree, laneScale.z);
			lane.transform.GetChild(0).localPosition = new Vector3 ((i * offset) + laneLocalScale.x/2 , laneLocalPosition.y, -0.01f);

			// continue if last requirement achieved
			if (i == reqNum)
				continue;

			// Generate threshold
			GameObject minThresh = (GameObject)Instantiate (_thresholdPrefab, Vector3.zero, Quaternion.identity);
			GameObject maxThresh = (GameObject)Instantiate (_thresholdPrefab, Vector3.zero, Quaternion.identity);

			minThresholdGOList.Add (minThresh);
			maxThresholdGOList.Add (maxThresh);

			minThresh.transform.parent = container;
			maxThresh.transform.parent = container;

			Vector3 minThreshScale = minThresh.transform.localScale;
			Vector3 maxThreshScale = maxThresh.transform.localScale;

			Vector2 tVect = GenerateRandomThreshold (minT, maxT);
			minMaxThresholdList.Add (tVect);

			minThresh.transform.localPosition = new Vector3 (((i + 1) * offset) - minThreshScale.x/2f , ((-tVect.x + 1) * 0.1f) - minThreshScale.y/2, -0.01f);
			if (maxT > 19) {
				maxT = 19;
			}
			maxThresh.transform.localPosition = new Vector3 (((i + 1) * offset) - maxThreshScale.x/2f, (-tVect.y * 0.1f) + maxThreshScale.y/2, -0.01f);
		}

		// set the player
		// initialize the projectiles
	}

	private Vector2 GenerateRandomThreshold(int minT, int maxT){
		Vector2 tVect = new Vector2 ();
		tVect.x = Random.Range (minT, (int)(maxT-1));
		tVect.y = Random.Range ((int) (tVect.x + 1), maxT);
		return tVect;
	}

	public void RemoveEnvironmentContents(Transform container){
		int len = container.childCount;
		for (int i=len-1; i>=0; i--){
			Destroy (container.GetChild (i).gameObject);
		}
	}
}

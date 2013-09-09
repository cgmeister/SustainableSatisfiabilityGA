using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetController : MonoBehaviour {

	[SerializeField]
	private Transform targetPrefab = null;

	public Transform targetContainer = null;

	private List<Target> _targetList;
	private int _minThreshold = 1;
	private int _maxThreshold = 2;
	private int _reqDegree = 2;
	private int _reqNum = 2;
	private int _reqDegradation = 1;
	private float _offset = 0;

	private Vector3 _targetPosCache = Vector3.zero;

	private List<Vector2> _minMaxThresholdList;

	public void IsPaused(bool value){
		foreach (Target target in _targetList){
			target.isEnabled = value;
		}
	}

	public void UpdateData(SimulationModel simModel, float offset){
		_minThreshold = simModel.minThreshold;
		_maxThreshold = simModel.minThreshold;
		_reqDegree = simModel.reqDegree;
		_reqNum = simModel.reqNum;
		_reqDegradation = simModel.reqDegradation;
		_offset = offset;
		_minMaxThresholdList = simModel.minMaxThresholdList;
	}

	public void InitializeTargets(){
		_targetList = new List<Target> ();
		for (int i=0; i<_reqNum; i++){
			GameObject targetGO = (GameObject) Instantiate (targetPrefab.gameObject, Vector3.zero, Quaternion.identity);				
			targetGO.transform.parent = targetContainer;
			targetGO.transform.localPosition = Vector3.zero;
			_targetPosCache.x = (_offset * ((2f * i) + 1)) + _offset/2f;
			Debug.Log ("Max Threshold: " + _minMaxThresholdList[i].y + " - Lane: " + i+1);
			_targetPosCache.y = (-_offset * _minMaxThresholdList[i].y) - _offset/2f;
			targetGO.transform.localPosition = _targetPosCache;
			Target target = targetGO.GetComponent<Target>();
			_targetList.Add(target);
			target.container = targetContainer;
			target.offset = _offset;
			Debug.Log ("Offset: " + _offset);
			target.lane = i + 1;
			target.speed = 1;
			target.rate = _reqDegradation;
			target.isEnabled = true;
		}
	}
	
	public void RemoveTargetContents(){
		int len = targetContainer.childCount;
		for (int i=len-1; i>=0; i--){
			Destroy (targetContainer.GetChild (i).gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

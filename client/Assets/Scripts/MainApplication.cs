using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MainApplication : MonoBehaviour {

	private const string RESUME = "Resume";
	private const string PAUSE = "Pause";

	[SerializeField]
	public UIInfoView infoView = null;

	[SerializeField]
	public EnvironmentGenerator envController = null;

	[SerializeField]
	public TargetController targetController = null;

	[SerializeField]
	public Player playerController = null;

	[SerializeField]
	public GameObject container = null;

	[SerializeField]
	public GameObject playerContainer = null;

	[SerializeField]
	public GameObject projectileContainer = null;

	[SerializeField]
	public GameObject targetContainer = null;

	private GAModel _gaModel = null;
	private SimulationModel _simModel;
	private bool _canStart = false;
	private int _errorCount = 0;

	// Use this for initialization
	void Start () {
		InitDefaultData ();
		AddListeners ();
	}

	// Update is called once per frame
	void Update () {
		
	}
		
	private void AddListeners(){
		infoView.startButton.onClick += OnStartClick;
		infoView.stopButton.onClick += OnStopClick;
		infoView.clearButton.onClick += OnClearClick;

		UIEventListener.Get(infoView.rateOfFireValue.gameObject).onInput += OnRateOfFireChanged;
		UIEventListener.Get(infoView.projectileSpeedValue.gameObject).onInput += OnProjectileSpeedChanged;
		UIEventListener.Get(infoView.horizontalMoveSpeedValue.gameObject).onInput += OnHorizontalMoveSpeedValueChanged;
		UIEventListener.Get(infoView.reqDegradationInput.gameObject).onInput += OnReqDegradationChanged;
		UIEventListener.Get(infoView.minThreshLabelInput.gameObject).onInput += OnMinThresholdChanged;
		UIEventListener.Get(infoView.maxThreshLabelInput.gameObject).onInput += OnMaxThresholdChanged;
		UIEventListener.Get(infoView.reqNumInput.gameObject).onInput += OnReqNumChanged;
		UIEventListener.Get(infoView.reqDegreeInput.gameObject).onInput += OnReqDegreeChanged;
	}

	private void RemoveListeners(){
		infoView.startButton.onClick -= OnStartClick;
		infoView.stopButton.onClick -= OnStopClick;
		infoView.clearButton.onClick -= OnClearClick;

		UIEventListener.Get(infoView.rateOfFireValue.gameObject).onInput -= OnRateOfFireChanged;
		UIEventListener.Get(infoView.projectileSpeedValue.gameObject).onInput -= OnProjectileSpeedChanged;
		UIEventListener.Get(infoView.horizontalMoveSpeedValue.gameObject).onInput -= OnHorizontalMoveSpeedValueChanged;
		UIEventListener.Get(infoView.reqDegradationInput.gameObject).onInput -= OnReqDegradationChanged;
		UIEventListener.Get(infoView.minThreshLabelInput.gameObject).onInput -= OnMinThresholdChanged;
		UIEventListener.Get(infoView.maxThreshLabelInput.gameObject).onInput -= OnMaxThresholdChanged;
		UIEventListener.Get(infoView.reqNumInput.gameObject).onInput -= OnReqNumChanged;
		UIEventListener.Get(infoView.reqDegreeInput.gameObject).onInput -= OnReqDegreeChanged;
	}

	private void OnClearClick(){
		infoView.consoleText.text = "";
	}
	
	private void OnRateOfFireChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.rateOfFireValue.text, str);
		_simModel.rateOfFire = Convert.ToByte (int.Parse(filStr));
		if (infoView.rateOfFireValue.text != ""){
			infoView.rateOfFireValue.text = filStr;
		}
		Debug.Log ("Rate of Fire Changed: " + str + " : " + _simModel.rateOfFire);
	}

	private void OnProjectileSpeedChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.projectileSpeedValue.text, str);
		_simModel.projectileSpeed = Convert.ToByte (int.Parse(filStr));
		if (infoView.projectileSpeedValue.text != "") {
			infoView.projectileSpeedValue.text = filStr;
		}
		Debug.Log ("Rate of Fire Changed: " + str + " : " + _simModel.rateOfFire);
	}

	private void OnHorizontalMoveSpeedValueChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.horizontalMoveSpeedValue.text, str);
		_simModel.horizontalMoveSpeed = Convert.ToByte (int.Parse(filStr));
		if (infoView.horizontalMoveSpeedValue.text != "") {
			infoView.horizontalMoveSpeedValue.text = filStr;
		}
		Debug.Log ("Horizontal Move Speed Changed: " + str + " : " + _simModel.horizontalMoveSpeed);
	}

	private void OnReqDegradationChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.reqDegradationInput.text, str);
		_simModel.reqDegradation = Convert.ToByte (int.Parse(filStr));
		if (infoView.reqDegradationInput.text != "") {
			infoView.reqDegradationInput.text = filStr;
		}
		Debug.Log ("Requirement Degradation Speed Changed: " + str + " : " + _simModel.reqDegradation);
	}

	private void OnMinThresholdChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.minThreshLabelInput.text, str);
		int value = int.Parse (filStr);

		if (value > _simModel.maxThreshold){
			value = _simModel.maxThreshold - 1;
			infoView.minThreshLabelInput.text = value.ToString();
		} else if (infoView.minThreshLabelInput.text != "") {
			infoView.minThreshLabelInput.text = filStr;
		}
		_simModel.minThreshold = Convert.ToByte (int.Parse(filStr));

		Debug.Log ("Minimum Threshold Changed: " + str + " : " + _simModel.minThreshold);
	}

	private void OnMaxThresholdChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.maxThreshLabelInput.text, str);
		int value = int.Parse (filStr);

		if (value < _simModel.minThreshold){
			//value = _simModel.minThreshold + 1;
			//infoView.maxThreshLabelInput.text = value.ToString();
			//infoView.consoleText.text += "Maximum threshold lower than minimum threshold \n";
		} else if (infoView.maxThreshLabelInput.text != "") {
			infoView.maxThreshLabelInput.text = filStr;
		}
		_simModel.maxThreshold = Convert.ToByte (value);

		Debug.Log ("Maximum Threshold Changed: " + str + " : " + _simModel.maxThreshold);
	}

	private void OnReqNumChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput (infoView.reqNumInput.text, str);
		int value = int.Parse (filStr);
		if (value > 9) {
			value = 9;
			infoView.reqNumInput.text = "9";
		} else if (infoView.reqNumInput.text != "") {
			infoView.reqNumInput.text = filStr;
		}
		_simModel.reqNum = Convert.ToByte (value);

		Debug.Log ("Requirement Number Changed: " + str + " : " + _simModel.reqNum);
	}

	private void OnReqDegreeChanged(GameObject go, string str){
		string filStr = StringUtility.FilterStringInput(infoView.reqDegreeInput.text, str);
		int value = int.Parse (filStr);

		if (value > 19){
			value = 19;
			infoView.reqDegreeInput.text = "19";
		} else if (infoView.reqDegreeInput.text != "") {
			infoView.reqDegreeInput.text = filStr;
		}
		_simModel.reqDegree = Convert.ToByte (value);

		Debug.Log ("Requirement Degree Changed: " + str + " : " + _simModel.reqDegree);
	}
	
	private void InitDefaultData(){
		_simModel = new SimulationModel ();
		_simModel.GenerateStartingData ();
		infoView.UpdateDisplayLabels (_simModel);		

		playerController.UpdateData(_simModel, 0.1f, 0.1f);
		playerController.container = playerContainer.transform;
		playerController.projectileContainer = projectileContainer.transform;

		targetController.UpdateData (_simModel, 0.1f);
		targetController.targetContainer = targetContainer.transform;
	}

	private void OnStartClick(){
		_canStart = true;
		_errorCount = 0;
		CheckConfig ();
		if (_canStart){
			Debug.Log ("Click Started");
			infoView.EnableAllLabels (false);
			envController.RemoveEnvironmentContents (container.transform);
			playerController.RemoveProjectileContents ();
			targetController.RemoveTargetContents ();
			Debug.Log (_simModel.reqNum);
			envController.GenerateEnvironment(
					container.transform,
					Convert.ToInt32(_simModel.minThreshold),
					Convert.ToInt32(_simModel.maxThreshold),
					Convert.ToInt32(_simModel.reqNum),
					Convert.ToInt32(_simModel.reqDegree),
					0.2f);
			int lane = 1;
			foreach (Vector2 minMax in envController.minMaxThresholdList){
				infoView.consoleText.text += "///////// Lane " + (lane) + " /////////\n";
				infoView.consoleText.text += "Minimum Threshold: " + minMax.x + "\n";
				infoView.consoleText.text += "Maximum Threshold: " + minMax.y + "\n";
				lane++;
			}

			_simModel.minMaxThresholdList = envController.minMaxThresholdList;
			
			playerController.UpdateData(_simModel, 0.1f, 0.1f);
			targetController.UpdateData (_simModel, 0.1f);

			playerController.InitializePlayer ();
			targetController.InitializeTargets();
		}
	}

	private void CheckConfig(){
		if (_simModel.maxThreshold < _simModel.minThreshold) {
				infoView.consoleText.text += "ERROR: Maximum threshold lower than minimum threshold \n";
				_errorCount++;
		} 
		if (_simModel.minThreshold == _simModel.maxThreshold){
			infoView.consoleText.text += "ERROR: Minimum threshold cannot be equal to maximum threshold \n";
			_errorCount++;
		}

		if(_simModel.reqDegree < _simModel.maxThreshold){ 
			infoView.consoleText.text += "ERROR: Requirement degree cannot be lower than the max threshold \n";
			_errorCount++;
		} 

		if (_simModel.minThreshold == 0){
			infoView.consoleText.text += "ERROR: Minimum threshold cannot be 0\n";
			_errorCount++;
		}

		if (_simModel.maxThreshold == 0){
			infoView.consoleText.text += "ERROR: Maximum threshold cannot be 0 or less than the minimum\n";
			_errorCount++;
		}

		if (_simModel.reqDegree < 2){
			infoView.consoleText.text += "ERROR: Requirement degree or lane depth cannot be < 2\n";
			_errorCount++;
		}

		if (_simModel.reqNum < 2){
			infoView.consoleText.text += "ERROR: Requirement number or  cannot be < 2\n";
			_errorCount++;
		}

		if (_errorCount > 0){
			_canStart = false;
		}
	}

	private void OnStopClick(){
		Debug.Log (infoView.stopButtonLabel.text);
		infoView.EnableAllLabels (true);
		switch (infoView.stopButtonLabel.text){
			case RESUME:{
				PauseGame ();
				infoView.stopButtonLabel.text = PAUSE;
				break;
			}
			case PAUSE:{
				ResumeGame ();
				infoView.stopButtonLabel.text = RESUME;
				break;
			}
		}
	}

	private void PauseGame(){
		targetController.IsPaused (true);
		playerController.IsPaused (true);
	}

	private void ResumeGame(){
		targetController.IsPaused (false);
		playerController.IsPaused (false);
	}

	private void StartGA(){

	}
}

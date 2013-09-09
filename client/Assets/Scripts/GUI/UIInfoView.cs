using UnityEngine;
using System.Collections;
using System;
using Optimera.GA;

public class UIInfoView : MonoBehaviour {

	public UILabel generationValue = null; // The generation of the value

	// With input
	public UIInput rateOfFireValue = null; 
	public UIInput projectileSpeedValue = null;
	public UIInput horizontalMoveSpeedValue = null;

	public UIInput reqDegradationInput = null;
	public UIInput minThreshLabelInput = null;
	public UIInput maxThreshLabelInput = null;
	public UIInput reqNumInput = null;
	public UIInput reqDegreeInput = null;

	// Console
	public UIDraggablePanel consolePanel = null;
	public UILabel consoleText = null;

	// Buttons
	public ClickUtility startButton = null;
	public ClickUtility stopButton = null;
	public ClickUtility clearButton = null;

	// Button Labels
	public UILabel startButtonLabel = null;
	public UILabel stopButtonLabel = null;
	public UILabel clearButtonLabel = null;

	void Start(){
		AddListeners ();

	}

	void OnDestroy(){
		RemoveListeners ();
	}

	private void AddListeners(){
	
	}

	private void RemoveListeners(){

	}

	public void UpdateDisplayLabels(SimulationModel simModel){
		rateOfFireValue.text = Convert.ToInt32(simModel.rateOfFire).ToString();
		projectileSpeedValue.text = Convert.ToInt32(simModel.projectileSpeed).ToString();
		horizontalMoveSpeedValue.text = Convert.ToInt32(simModel.horizontalMoveSpeed).ToString();
		reqDegradationInput.text = Convert.ToInt32(simModel.reqDegradation).ToString();
		minThreshLabelInput.text = Convert.ToInt32(simModel.minThreshold).ToString();
		maxThreshLabelInput.text = Convert.ToInt32(simModel.maxThreshold).ToString();
		reqNumInput.text = Convert.ToInt32(simModel.reqNum).ToString();
		reqDegreeInput.text = Convert.ToInt32(simModel.reqDegree).ToString();
	}

	public void EnableAllLabels(bool value){
		reqDegradationInput.enabled = value; 
		minThreshLabelInput.enabled = value; 
		maxThreshLabelInput.enabled = value; 
		reqNumInput.enabled = value; 
		reqDegreeInput.enabled = value; 
	}
}


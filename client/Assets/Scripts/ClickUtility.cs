using UnityEngine;
using System.Collections;
using System;

public class ClickUtility : MonoBehaviour {

	public event Action onClick;

	private void OnClick(){
		if (onClick != null){
			onClick ();
		}
	}
}

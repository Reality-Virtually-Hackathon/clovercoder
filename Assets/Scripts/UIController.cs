using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public void closeView () {
		GameObject[] currentModels = GameObject.FindGameObjectsWithTag ("CurrentModel");
		foreach (GameObject currentModel in currentModels) {
			if (currentModel != null) {
				GameObject.Destroy (currentModel);
			}
		}
		GameManager.modelInstantiated = false;
	}

	public void chooseModel (int dropdown) {
		HelloARController arController = gameObject.GetComponent<HelloARController> ();
			switch (dropdown) {
				case 0:
				arController.robotObject.SetActive (false);
				arController.andyObject.SetActive (true);
					break;
				case 1: 
				arController.robotObject.SetActive (true);
				arController.andyObject.SetActive (false);
					break;
			}
		}
	
}

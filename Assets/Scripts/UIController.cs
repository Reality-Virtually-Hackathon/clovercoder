using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;

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

	public void chooseModel (string modelType) {
		HelloARController arController = gameObject.GetComponent<HelloARController> ();
		if (GameManager.modelInstantiated) {
			switch (modelType) {
				case "andy":
					arController.andyObject.SetActive (true);
					arController.robotObject.SetActive (false);
					break;
				case "robot": 
					arController.robotObject.SetActive (true);
					arController.andyObject.SetActive (false);
					break;
			}
		}
	}
}

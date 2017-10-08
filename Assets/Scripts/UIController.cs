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

	public void chooseModel (Dropdown dropdown) {
		HelloARController arController = gameObject.GetComponent<HelloARController> ();
		if (GameManager.modelInstantiated) {
			switch (dropdown.value) {
				case 0:
				arController.andyObject.GetComponent<MeshRenderer>().enabled = true;
				arController.robotObject.GetComponent<MeshRenderer>().enabled = false;
					break;
				case 1: 
				arController.robotObject.GetComponent<MeshRenderer>().enabled = true;
				arController.andyObject.GetComponent<MeshRenderer>().enabled = false;
					break;
			}
		}
	}
}

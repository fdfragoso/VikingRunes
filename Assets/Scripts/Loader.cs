using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject gameController;
    public DrawLine drawLine;

	// Use this for initialization
	void Awake () {
		if (GameController.instance == null)
			Instantiate (gameController);

        if (DrawLine.instance == null)
            Instantiate(drawLine);
    }
}

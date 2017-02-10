using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Image OdeToCodeWelcome;

	// Use this for initialization
	void Awake () {
		PersistantValues.Save ();
		PersistantValues.Load ();
		OdeToCodeWelcome.rectTransform.sizeDelta = new Vector2 (Screen.width, Screen.height);
	}

	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1"))
			SceneManager.LoadScene ("LoginScreen");
	}
}

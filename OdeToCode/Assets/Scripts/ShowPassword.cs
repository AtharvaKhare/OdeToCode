using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPassword : MonoBehaviour {

	public Text display;
	public InputField UserIDInput;


	public void OnSearchClick()
	{
		display.text = "Name - " + PersistantValues.UserDatabase [int.Parse (UserIDInput.text)].Name + "\n";
		string password = Crypto.Md5Sum (PersistantValues.UserDatabase [int.Parse (UserIDInput.text)].UserID + " " + PersistantValues.UserDatabase [int.Parse (UserIDInput.text)].Name);

		display.text += "Password - " + password.Substring (0, 6);	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

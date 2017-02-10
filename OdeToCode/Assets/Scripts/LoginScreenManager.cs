using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScreenManager : MonoBehaviour {

	public Text LoginIDText, PasswordText, SlotsText;

	public InputField LoginInputField;
	public InputField PasswordInputField;
	public Button LoginButton;

	public Dropdown DropdownSlot;

	// Use this for initialization
	void Awake () {
		

		PersistantValues.selectedSlot = 0;
		DropdownSlot.onValueChanged.AddListener (delegate {
			myDropdownValueChangedHandler (DropdownSlot);
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void RemoveDialogue(GameObject Dialogue)
	{
		Dialogue.GetComponent<RectTransform> ().position = new Vector2 (3000, 3000);
	}

	private void myDropdownValueChangedHandler(Dropdown target)
	{
		PersistantValues.selectedSlot = target.value;
	}
	public void OnLoginButtonClick()
	{
		if(PersistantValues.selectedSlot == 0)
		{
			//EditorUtility.DisplayDialog ("No slot selected!", "Please select one slot.", "Okay");
			Dialogue.GenerateDialogue("Please select appropriate slot!");
		}
		else
		{
		PersistantValues.currentUser = PersistantValues.UserDatabase [int.Parse (LoginInputField.text)];
		string password = Crypto.Md5Sum (PersistantValues.currentUser.UserID + " " + PersistantValues.currentUser.Name);
		//Administrator login
			if (PasswordInputField.text == password.Substring (0, 12)) {
				if (PersistantValues.selectedSlot == 1)
					SceneManager.LoadScene ("Everyone's result", LoadSceneMode.Single);
				else
					SceneManager.LoadScene ("Password Management", LoadSceneMode.Single);
			}
			if (LoginInputField.text == "0")
				Debug.Log ("Admin Login: " + password.Substring (0, 12));
		password = password.Substring (0, 6);
		Debug.Log ("Password: " + password);
		if (password == PasswordInputField.text) 
		{
			Debug.Log ("Login Successful!");
			PersistantValues.currentUser.slot = PersistantValues.selectedSlot;
			SceneManager.LoadScene ("QuestionAnswerScreen");
			PersistantValues.MaxEasyQuestions = PersistantValues.SlotDatabase [PersistantValues.selectedSlot].MaxEasyQuestions;
				PersistantValues.MaxMediumQuestions = PersistantValues.SlotDatabase [PersistantValues.selectedSlot].MaxMediumQuestions;
				PersistantValues.MaxHardQuestions = PersistantValues.SlotDatabase [PersistantValues.selectedSlot].MaxHardQuestions;
				Debug.Log (PersistantValues.MaxEasyQuestions + PersistantValues.MaxMediumQuestions+PersistantValues.MaxHardQuestions);
		}
			else{
				//EditorUtility.DisplayDialog ("Invalid Username or Password!", "Please check your username or password.", "Okay");
				Dialogue.GenerateDialogue("Invalid username or password.");
			}
		}

	}
			void Destroy()
			{
				DropdownSlot.onValueChanged.RemoveAllListeners ();
			}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuestionAnswerScript : MonoBehaviour {
	public Text timeDisplay;
	public Toggle OptionA, OptionB, OptionC, OptionD;
	public Text QuestionText, TextStaticQuestions;
	public Button SkipButton, NextButton;
	public int easyQuestionIndex, mediumQuestionIndex, hardQuestionIndex;
	public int easyIndex, mediumIndex, hardIndex;
	float timeLeft;
	int skipCountRemaining;

	void Awake()
	{
		
		easyIndex = mediumIndex = hardIndex = 0;
		easyQuestionIndex = mediumQuestionIndex = hardQuestionIndex = 0;
		PersistantValues.MaxEasyQuestions = PersistantValues.SlotDatabase[PersistantValues.currentUser.slot].MaxEasyQuestions;
		PersistantValues.easyQuestionSelector = new int[PersistantValues.DatabaseEasy];
		for (int i = 0; i < PersistantValues.DatabaseEasy; ++i) {
			PersistantValues.easyQuestionSelector [i] = 1;
		}

		PersistantValues.mediumQuestionSelector = new int[PersistantValues.DatabaseMedium];
		for (int i = 0; i < PersistantValues.DatabaseMedium; ++i)
			PersistantValues.mediumQuestionSelector [i] = 1;


		PersistantValues.hardQuestionSelector = new int[PersistantValues.DatabaseHard];
		for (int i = 0; i < PersistantValues.DatabaseHard; ++i)
			PersistantValues.hardQuestionSelector [i] = 1;
		timeLeft = PersistantValues.SlotDatabase[PersistantValues.currentUser.slot].time;
		timeDisplay.text = (((int)timeLeft/60 == 0)?("0"):(floatToString ((timeLeft / 60))))  + ":" +  ((((int)((int)(timeLeft) %60)/10) == 0) ?  "0" : "" )+ floatToString((timeLeft%60));

		skipCountRemaining = (int)((PersistantValues.MaxEasyQuestions + PersistantValues.MaxMediumQuestions + PersistantValues.MaxHardQuestions) / 5);
		SkipButton.GetComponentsInChildren<Text> ()[0].text = "Skip ("+skipCountRemaining+" remaining)";

	}

	// Use this for initialization
	void Start () {
		PersistantValues.currentUser.score = 0;
		easyIndex = Random.Range (0, PersistantValues.DatabaseEasy);
		mediumIndex = Random.Range (0, PersistantValues.DatabaseMedium);
		hardIndex = Random.Range (0, PersistantValues.DatabaseHard);
		PersistantValues.easyQuestionSelector [easyIndex] = 0;
		TextStaticQuestions.text = "Question 1:";
		QuestionText.text =  PersistantValues.EasyQuestions [easyIndex].Question;
		OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.EasyQuestions [easyIndex].OptionA;
		OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.EasyQuestions [easyIndex].OptionB;
		OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.EasyQuestions [easyIndex].OptionC;
		OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.EasyQuestions [easyIndex].OptionD;

		easyQuestionIndex = 1;

		Debug.Log ("Max easy questions = " + PersistantValues.MaxEasyQuestions);
		Debug.Log ("Max medium questions = " + PersistantValues.MaxMediumQuestions);
		Debug.Log ("Max hard questions = " + PersistantValues.MaxHardQuestions);
	}
	
	// Update is called once per frame
	void Update () {
		

		if (skipCountRemaining == 0) {
			Destroy (SkipButton);
		}
		timeLeft = timeLeft - Time.deltaTime;
		if (timeLeft > 0) {
			if (timeLeft < 600) {
				timeDisplay.color = new Color (0, 0, 1, 1);
				if(timeLeft < 120)
					timeDisplay.color = new Color (1, 0, 0, 1);
					
			}
			timeDisplay.text = (((int)timeLeft / 60 == 0) ? ("0") : (floatToString ((timeLeft / 60)))) + ":" + ((((int)((int)(timeLeft) %60)/10) == 0) ?  "0" : "" ) + floatToString ((timeLeft % 60));
		}
		else {
			EndGame ();
		}
	}

	public void OnNextClick()
	{
		Debug.Log (easyQuestionIndex + " " + mediumQuestionIndex + " " + hardQuestionIndex);
		if (ticked ()) {
			if (easyQuestionIndex < PersistantValues.MaxEasyQuestions) {
				int answerIndex = PersistantValues.EasyQuestions [easyIndex].answer;
				if ((answerIndex == 1 && OptionA.isOn == true) || (answerIndex == 2 && OptionB.isOn == true) || (answerIndex == 3 && OptionC.isOn == true) || (answerIndex == 4 && OptionD.isOn == true)) {
					PersistantValues.currentUser.score += 4;
					Debug.Log ("Adding 4.");
				} else {
					PersistantValues.currentUser.score += 8;
					Debug.Log ("Adding 8.");
				}
			} else if (mediumQuestionIndex < PersistantValues.MaxMediumQuestions) {

				int answerIndex = PersistantValues.MediumQuestions [mediumIndex].answer;
				if ((answerIndex == 1 && OptionA.isOn == true) || (answerIndex == 2 && OptionB.isOn == true) || (answerIndex == 3 && OptionC.isOn == true) || (answerIndex == 4 && OptionD.isOn == true)) {
					PersistantValues.currentUser.score += 4;
					Debug.Log ("Adding 4.");
				} else {
					PersistantValues.currentUser.score += 8;
					Debug.Log ("Adding 8.");
				}

			} else if (hardQuestionIndex < PersistantValues.MaxHardQuestions) {

				int answerIndex = PersistantValues.HardQuestions [hardIndex].answer;
				if ((answerIndex == 1 && OptionA.isOn == true) || (answerIndex == 2 && OptionB.isOn == true) || (answerIndex == 3 && OptionC.isOn == true) || (answerIndex == 4 && OptionD.isOn == true)) {
					PersistantValues.currentUser.score += 4;
					Debug.Log ("Adding 4.");
				} else {
					PersistantValues.currentUser.score += 8;
					Debug.Log ("Adding 8.");
				}
			} else {
				EndGame ();
				//PersistantValues.currentUser.deviation = (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) * 4 - PersistantValues.currentUser.score;
				//SceneManager.LoadScene ("ResultScreen");
			}
			if (easyQuestionIndex != PersistantValues.MaxEasyQuestions) {

				while (PersistantValues.easyQuestionSelector [easyIndex] == 0) {
					easyIndex = Random.Range (0, PersistantValues.DatabaseEasy);
				}

				QuestionText.text = PersistantValues.EasyQuestions [easyIndex].Question;
				OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.EasyQuestions [easyIndex].OptionA;
				OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.EasyQuestions [easyIndex].OptionB;
				OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.EasyQuestions [easyIndex].OptionC;
				OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.EasyQuestions [easyIndex].OptionD;
				++easyQuestionIndex;
				TextStaticQuestions.text = "Question " + (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) + ":";

				PersistantValues.easyQuestionSelector [easyIndex] = 0;
			}
			else if(mediumQuestionIndex != PersistantValues.MaxMediumQuestions)
			{
				while (PersistantValues.mediumQuestionSelector [mediumIndex] == 0) {
					mediumIndex = Random.Range (0, PersistantValues.DatabaseMedium);
				}

				QuestionText.text = PersistantValues.MediumQuestions [mediumIndex].Question;
				OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.MediumQuestions [mediumIndex].OptionA;
				OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.MediumQuestions [mediumIndex].OptionB;
				OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.MediumQuestions [mediumIndex].OptionC;
				OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.MediumQuestions [mediumIndex].OptionD;
				++mediumQuestionIndex;
				TextStaticQuestions.text = "Question " + (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) + ":";

				PersistantValues.mediumQuestionSelector [mediumIndex] = 0;
			}
			else if(hardQuestionIndex != PersistantValues.MaxHardQuestions)
			{
				while (PersistantValues.hardQuestionSelector [hardIndex] == 0) {
					hardIndex = Random.Range (0, PersistantValues.DatabaseHard);
				}

				QuestionText.text = PersistantValues.HardQuestions [hardIndex].Question;
				OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.HardQuestions [hardIndex].OptionA;
				OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.HardQuestions [hardIndex].OptionB;
				OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.HardQuestions [hardIndex].OptionC;
				OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.HardQuestions [hardIndex].OptionD;
				++hardQuestionIndex;
				TextStaticQuestions.text = "Question " + (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) + ":";

				PersistantValues.hardQuestionSelector [hardIndex] = 0;
			}
			else
			{
				EndGame ();
				//PersistantValues.currentUser.deviation = (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) * 4 - PersistantValues.currentUser.score;
				//SceneManager.LoadScene ("ResultScreen");
			}



		} else {
			//EditorUtility.DisplayDialog ("No slot selected!", "Please select one slot.", "Okay");
			Dialogue.GenerateDialogue("No answer selected!");
		}


		OptionA.isOn = OptionB.isOn = OptionC.isOn = OptionD.isOn = false;	

	}

	public void OnSkipClick()
	{
		if (skipCountRemaining > 0) {
			Debug.Log (easyQuestionIndex + " " + mediumQuestionIndex + " " + hardQuestionIndex);
			if (ticked ()) {
				Dialogue.GenerateDialogue ("Please uncheck the answer and then skip the question!");
			} else {
				skipCountRemaining = skipCountRemaining - 1;
				PersistantValues.currentUser.score += 1;
				Debug.Log ("Adding 1");

				if (easyQuestionIndex != PersistantValues.MaxEasyQuestions) {

					while (PersistantValues.easyQuestionSelector [easyIndex] == 0) {
						easyIndex = Random.Range (0, PersistantValues.DatabaseEasy);
					}

					QuestionText.text = PersistantValues.EasyQuestions [easyIndex].Question;
					OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.EasyQuestions [easyIndex].OptionA;
					OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.EasyQuestions [easyIndex].OptionB;
					OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.EasyQuestions [easyIndex].OptionC;
					OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.EasyQuestions [easyIndex].OptionD;
					++easyQuestionIndex;
					TextStaticQuestions.text = "Question " + (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) + ":";

					PersistantValues.easyQuestionSelector [easyIndex] = 0;
				} else if (mediumQuestionIndex != PersistantValues.MaxMediumQuestions) {
					while (PersistantValues.mediumQuestionSelector [mediumIndex] == 0) {
						mediumIndex = Random.Range (0, PersistantValues.DatabaseMedium);
					}

					QuestionText.text = PersistantValues.MediumQuestions [mediumIndex].Question;
					OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.MediumQuestions [mediumIndex].OptionA;
					OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.MediumQuestions [mediumIndex].OptionB;
					OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.MediumQuestions [mediumIndex].OptionC;
					OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.MediumQuestions [mediumIndex].OptionD;
					++mediumQuestionIndex;
					TextStaticQuestions.text = "Question " + (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) + ":";

					PersistantValues.mediumQuestionSelector [mediumIndex] = 0;
				} else if (hardQuestionIndex != PersistantValues.MaxHardQuestions) {
					while (PersistantValues.hardQuestionSelector [hardIndex] == 0) {
						hardIndex = Random.Range (0, PersistantValues.DatabaseHard);
					}

					QuestionText.text = PersistantValues.HardQuestions [hardIndex].Question;
					OptionA.GetComponentInChildren<Text> ().text = "A: " + PersistantValues.HardQuestions [hardIndex].OptionA;
					OptionB.GetComponentInChildren<Text> ().text = "B: " + PersistantValues.HardQuestions [hardIndex].OptionB;
					OptionC.GetComponentInChildren<Text> ().text = "C: " + PersistantValues.HardQuestions [hardIndex].OptionC;
					OptionD.GetComponentInChildren<Text> ().text = "D: " + PersistantValues.HardQuestions [hardIndex].OptionD;
					++hardQuestionIndex;
					TextStaticQuestions.text = "Question " + (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) + ":";

					PersistantValues.hardQuestionSelector [hardIndex] = 0;
				} else {
					EndGame ();
					//PersistantValues.currentUser.deviation = (easyQuestionIndex + mediumQuestionIndex + hardQuestionIndex) * 4 - PersistantValues.currentUser.score;
					//SceneManager.LoadScene ("ResultScreen");
					//EditorUtility.DisplayDialog ("No more questions!", PersistantValues.currentUser.Name + " scored " +PersistantValues.currentUser.score, "Okay");
				}
			}
			SkipButton.GetComponentsInChildren<Text> ()[0].text = "Skip ("+skipCountRemaining+" remaining)";
		} else {
		}
	}
	public void EndGame()
	{
		//int questionsLeft = PersistantValues.MaxEasyQuestions + PersistantValues.MaxHardQuestions + PersistantValues.MaxMediumQuestions - easyQuestionIndex - mediumQuestionIndex - hardQuestionIndex;
		//PersistantValues.currentUser.score += (questionsLeft);

		PersistantValues.currentUser.deviation = Mathf.Abs((PersistantValues.MaxEasyQuestions + PersistantValues.MaxHardQuestions + PersistantValues.MaxMediumQuestions) * 4 - PersistantValues.currentUser.score);

		SceneManager.LoadScene ("ResultScreen");
	}

	public void RemoveDialogue(GameObject Dialogue)
	{
		Dialogue.GetComponent<RectTransform> ().position = new Vector2 (3000, 3000);
	}

	bool ticked()
	{
		return (OptionA.isOn || OptionB.isOn || OptionC.isOn || OptionD.isOn);
	}

	string floatToString(float input)
	{
		int no = (int) input;
		Stack<int> s = new Stack<int> ();
		string output = "";
		while (no > 0) {
			s.Push (no % 10);
			no = no / 10;
		}

		while (s.Count > 0) {
			output = output + (char)(s.Pop () + 48);
		}
		return output;
	}
}

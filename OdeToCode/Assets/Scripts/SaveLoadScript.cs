using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using UnityEngine.UI;

public class SaveLoadScript : MonoBehaviour {

	public InputField InputFieldQuestion, InputFieldA, InputFieldB, InputFieldC, InputFieldD, InputFieldAnswer;
	public Toggle ToggleEasy, ToggleMedium, ToggleHard;
	public Text TextDatabasePath;


	public int selectedSlot;
	public UserInfo currentUser;
	public List<UserInfo> UserDatabase;
	public List<QuestionsEasy> EasyQuestions;
	public List<QuestionsMedium> MediumQuestions;
	public List<QuestionsHard> HardQuestions;
	public int MaxEasyQuestions, MaxMediumQuestions, MaxHardQuestions;
	public int sectionMax;
	public int[] easyQuestionSelector, mediumQuestionSelector, hardQuestionSelector;
	public List<Slots> SlotDatabase;
	int indexEasy = 0, indexMedium = 0, indexHard = 0;

	public void Save()
	{
		if (File.Exists (Application.persistentDataPath + "/Easy.txt")) {
			BinaryFormatter questionsLoadEasy = new BinaryFormatter ();
			FileStream fileEasy = File.Open (Application.persistentDataPath + "/Easy.txt", FileMode.Open);
			EasyQuestions = (List<QuestionsEasy>)questionsLoadEasy.Deserialize (fileEasy);
			fileEasy.Close ();
			Debug.Log ("Last Added question: " + EasyQuestions [indexEasy - 1].Question);
		} else
			Debug.Log ("File does not exist.");

		if (File.Exists (Application.persistentDataPath + "/Medium.txt")) {
			BinaryFormatter questionsLoadMedium = new BinaryFormatter ();
			FileStream fileMedium = File.Open (Application.persistentDataPath + "/Medium.txt", FileMode.Open);
			MediumQuestions = (List<QuestionsMedium>)questionsLoadMedium.Deserialize (fileMedium);
			fileMedium.Close ();
			Debug.Log ("Last Added question: " + MediumQuestions [indexMedium - 1].Question);
		} else
			Debug.Log ("File does not exist.");

		if (File.Exists (Application.persistentDataPath + "/Hard.txt")) {
			BinaryFormatter questionsLoadHard = new BinaryFormatter ();
			FileStream fileHard = File.Open (Application.persistentDataPath + "/Hard.txt", FileMode.Open);
			HardQuestions = (List<QuestionsHard>)questionsLoadHard.Deserialize (fileHard);
			fileHard.Close ();
			Debug.Log ("Last Added question: " + HardQuestions [indexHard - 1].Question);
		} else
			Debug.Log ("File does not exist.");
		/*
		BinaryFormatter save = new BinaryFormatter();
		FileStream saveFile = File.Create(Application.persistentDataPath + "/Database.txt");
		List<UserInfo> Users = new List<UserInfo>();
		UserInfo entry1 = new UserInfo ();
		entry1.UserID = 0;
		entry1.Name = "Malav Warke";
		Users.Add (entry1);
		UserInfo entry2 = new UserInfo ();
		entry2.UserID = 1;
		entry2.Name = "Atharva Khare";
		Users.Add (entry2);
		bf.Serialize (file, Users);
		file.Close ();*/
		if (ToggleEasy.isOn) {
			BinaryFormatter questionsSaveEasy = new BinaryFormatter ();
			FileStream fileEasy = File.Create (Application.persistentDataPath + "/Easy.txt");
			QuestionsEasy qe1 = new QuestionsEasy ();
			qe1.Question = InputFieldQuestion.text;
			InputFieldQuestion.text = "";
			qe1.OptionA = InputFieldA.text;
			InputFieldA.text = "";
			qe1.OptionB = InputFieldB.text;
			InputFieldB.text = "";
			qe1.OptionC = InputFieldC.text;
			InputFieldC.text = "";
			qe1.OptionD = InputFieldD.text;
			InputFieldD.text = "";
			qe1.answer = int.Parse (InputFieldAnswer.text);
			InputFieldAnswer.text = "";
			EasyQuestions.Add (qe1);
			questionsSaveEasy.Serialize (fileEasy, EasyQuestions);
			++indexEasy;
			fileEasy.Close ();
		}

		if (ToggleMedium.isOn) {
			BinaryFormatter questionsSaveMedium = new BinaryFormatter ();
			FileStream fileMedium = File.Create (Application.persistentDataPath + "/Medium.txt");
			QuestionsMedium qe1 = new QuestionsMedium ();
			qe1.Question = InputFieldQuestion.text;
			InputFieldQuestion.text = "";
			qe1.OptionA = InputFieldA.text;
			InputFieldA.text = "";
			qe1.OptionB = InputFieldB.text;
			InputFieldB.text = "";
			qe1.OptionC = InputFieldC.text;
			InputFieldC.text = "";
			qe1.OptionD = InputFieldD.text;
			InputFieldD.text = "";
			qe1.answer = int.Parse (InputFieldAnswer.text);
			InputFieldAnswer.text = "";
			MediumQuestions.Add (qe1);
			questionsSaveMedium.Serialize (fileMedium, MediumQuestions);
			++indexMedium;
			fileMedium.Close ();
		}

		if (ToggleHard.isOn) {
			BinaryFormatter questionsSaveHard = new BinaryFormatter ();
			FileStream fileHard = File.Create (Application.persistentDataPath + "/Hard.txt");
			QuestionsHard qe1 = new QuestionsHard ();
			qe1.Question = InputFieldQuestion.text;
			InputFieldQuestion.text = "";
			qe1.OptionA = InputFieldA.text;
			InputFieldA.text = "";
			qe1.OptionB = InputFieldB.text;
			InputFieldB.text = "";
			qe1.OptionC = InputFieldC.text;
			InputFieldC.text = "";
			qe1.OptionD = InputFieldD.text;
			InputFieldD.text = "";
			qe1.answer = int.Parse (InputFieldAnswer.text);
			InputFieldAnswer.text = "";
			HardQuestions.Add (qe1);
			questionsSaveHard.Serialize (fileHard, HardQuestions);
			++indexHard;
			fileHard.Close ();
		}
		/*
		Slots dummy = new Slots ();
		Slots slot1 = new Slots ();
		Slots slot2 = new Slots ();
		Slots slot3 = new Slots ();
		Slots slot4 = new Slots ();
		Slots slot5 = new Slots ();
		slot1.MaxEasyQuestions = 3;
		List<Slots> storeSlots = new List<Slots>();
		storeSlots.Add(dummy);
		storeSlots.Add(slot1);
		storeSlots.Add(slot2);
		storeSlots.Add(slot3);
		storeSlots.Add(slot4);
		storeSlots.Add(slot5);

		BinaryFormatter slotF = new BinaryFormatter();
		FileStream slotsFile = File.Create(Application.persistentDataPath + "/Slots.txt");
		slotF.Serialize(slotsFile, storeSlots);
		slotsFile.Close();
		*/

	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/Database.txt"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Database.txt", FileMode.Open);
			UserDatabase = (List<UserInfo>) bf.Deserialize(file);
			file.Close ();
		}

		if (File.Exists (Application.persistentDataPath + "/Easy.txt"))
		{
			BinaryFormatter easy = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Easy.txt", FileMode.Open);
			EasyQuestions = (List<QuestionsEasy>) easy.Deserialize(file);
			file.Close ();
		}

		if (File.Exists (Application.persistentDataPath + "/Slots.txt"))
		{
			BinaryFormatter slotF = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Slots.txt", FileMode.Open);
			SlotDatabase = (List<Slots>) slotF.Deserialize(file);
			file.Close ();
		}
	}
	void Start () {
		TextDatabasePath.text = Application.persistentDataPath;
	}

	void Update () {
		
	}
}

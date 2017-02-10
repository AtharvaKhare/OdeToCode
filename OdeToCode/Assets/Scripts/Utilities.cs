using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using UnityEngine.UI;
using System.Text;

[System.Serializable]
public class UserInfo
{
	public int UserID, score, deviation;
	public string Name;
	public int slot;

}


[System.Serializable]
public class QuestionsEasy
{
	public string Question, OptionA, OptionB, OptionC, OptionD;
	public int answer;
}

[System.Serializable]
public class QuestionsMedium
{
	public string Question, OptionA, OptionB, OptionC, OptionD;
	public int answer;
}

[System.Serializable]
public class QuestionsHard
{
	public string Question, OptionA, OptionB, OptionC, OptionD;
	public int answer;
}

[System.Serializable]
public class Slots
{
	public int MaxEasyQuestions, MaxMediumQuestions, MaxHardQuestions;
	public float time;
}

public static class PersistantValues
{
	public static int DatabaseEasy, DatabaseMedium, DatabaseHard;
	public static int selectedSlot;
	public static UserInfo currentUser;
	public static List<UserInfo> UserDatabase;
	public static List<QuestionsEasy> EasyQuestions;
	public static List<QuestionsMedium> MediumQuestions;
	public static List<QuestionsHard> HardQuestions;
	public static int MaxEasyQuestions, MaxMediumQuestions, MaxHardQuestions;
	public static int sectionMax;
	public static int[] easyQuestionSelector, mediumQuestionSelector, hardQuestionSelector;
	public static List<Slots> SlotDatabase;

	public static void Save()
	{
		UserDatabase = new List<UserInfo> ();
		Debug.Log (Application.dataPath);
		UserInfo[] entry = new UserInfo [150];
		entry [0] = new UserInfo ();
		entry[0].UserID = 0;
		entry[0].Name = "Atharva Khare";
		UserDatabase.Add (entry[0]);

		for (int x = 1; x < 108; ++x) {
			entry[x] = new UserInfo ();
			entry[x].UserID = x;
			entry[x].Name = "Dummy Entry - " + x;
			UserDatabase.Add (entry[x]);

		}

		for (int x = 108; x < 150; ++x) {
			entry[x] = new UserInfo ();
			entry[x].UserID = x;
			int t = x - 107;
			entry[x].Name = "On Spot Entry - " + t;
			UserDatabase.Add (entry[x]);

		}


		SlotDatabase = new List<Slots>();
		Slots dummy = new Slots ();
		Slots slot1 = new Slots ();
		Slots slot2 = new Slots ();
		Slots slot3 = new Slots ();
		Slots slot4 = new Slots ();
		Slots slot5 = new Slots ();
		slot1.MaxEasyQuestions = 10;
		slot1.MaxMediumQuestions = 10;
		slot1.MaxHardQuestions = 23;
		slot1.time = 1320;
		slot2.MaxEasyQuestions = 9;
		slot2.MaxMediumQuestions = 10;
		slot2.MaxHardQuestions = 20;
		slot2.time = 1140;
		slot3.MaxEasyQuestions = 9;
		slot3.MaxMediumQuestions = 9;
		slot3.MaxHardQuestions = 18;
		slot3.time = 1080;
		slot4.MaxEasyQuestions = 5;
		slot4.MaxMediumQuestions = 17;
		slot4.MaxHardQuestions = 23;
		slot4.time = 1380;
		slot5.MaxEasyQuestions = 7;
		slot5.MaxMediumQuestions = 10;
		slot5.MaxHardQuestions = 17;
		slot5.time = 1020;
		SlotDatabase.Add(dummy);
		SlotDatabase.Add(slot1);
		SlotDatabase.Add(slot2);
		SlotDatabase.Add(slot3);
		SlotDatabase.Add(slot4);
		SlotDatabase.Add(slot5);


	}

	public static void Load()
	{

		if (File.Exists (Application.persistentDataPath + "/Easy.txt"))
		{
			BinaryFormatter easy = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Easy.txt", FileMode.Open);
			EasyQuestions = (List<QuestionsEasy>) easy.Deserialize(file);
			DatabaseEasy = 54;
			file.Close ();
		}

		if (File.Exists (Application.persistentDataPath + "/Medium.txt"))
		{
			BinaryFormatter medium = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Medium.txt", FileMode.Open);
			MediumQuestions = (List<QuestionsMedium>) medium.Deserialize(file);
			DatabaseMedium = 54;
			file.Close ();
		}

		if (File.Exists (Application.persistentDataPath + "/Hard1.txt"))
		{
			BinaryFormatter hard = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Hard1.txt", FileMode.Open);
			HardQuestions = (List<QuestionsHard>) hard.Deserialize(file);

			file.Close ();
		}
		if (File.Exists (Application.persistentDataPath + "/Hard2.txt"))
		{
			BinaryFormatter medium = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Hard2.txt", FileMode.Open);
			List<QuestionsHard> Hard2 = (List<QuestionsHard>) medium.Deserialize(file);
			for (int i = 0; i < 24; ++i)
				HardQuestions.Add (Hard2 [i]);
			DatabaseHard = 49;
			file.Close ();
		}
		if (File.Exists (Application.persistentDataPath + "/scores.txt")) {
			string line;
			StreamReader theReader = new StreamReader(Application.persistentDataPath + "/scores.txt", Encoding.Default);
			using (theReader) {
				line = theReader.ReadToEnd();
				line = Crypto.XOREncryption(line);
				Debug.Log (line);
			}
			theReader.Close ();
		} else {
			System.IO.File.WriteAllText(Application.persistentDataPath + "/scores.txt", Crypto.XOREncryption("User ID\tName\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tScore\t\tDeviation\n"));
		}
	
	}
}

public static class Crypto
{

	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	public static string Sha1Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
		byte[] hashBytes = sha1.ComputeHash (bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	public static string XOREncryption(string strToEncrypt)
	{
		string codeword = "93c58d0af897";
		string res = "";
		for (int i = 0; i < strToEncrypt.Length; i++)
		{
			res += (char)(strToEncrypt[i] ^ codeword[i % codeword.Length]);
		}
		return res;
	}

}


public static class Dialogue
{
	public static void GenerateDialogue(string Message)
	{
		GameObject Dialogue = GameObject.Find ("DisplayDialogue");
		Dialogue.GetComponentsInChildren<Text> ()[0].text = Message;
		Dialogue.GetComponent<RectTransform> ().position = new Vector2 (Screen.width / 2, Screen.height / 2);
	}
	public static void RemoveDialogue()
	{
	}
}


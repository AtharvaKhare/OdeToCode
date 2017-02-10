using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System.Text;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	public Text displayScore;

	// Use this for initialization
	void Awake () 
	{
		displayScore.text = displayScore.text + "\nYou scored " + PersistantValues.currentUser.score + " with deviation of " + PersistantValues.currentUser.deviation + ".";
		displayScore.text = displayScore.text + "\nYour details:\n" + PersistantValues.currentUser.UserID + " : " + PersistantValues.currentUser.Name;

		if (File.Exists (Application.persistentDataPath + "/scores.txt")) {
			string line;
			StreamReader theReader = new StreamReader(Application.persistentDataPath + "/scores.txt", Encoding.Default);
			using (theReader) {
					line = theReader.ReadToEnd();
					line = Crypto.XOREncryption(line);
				Debug.Log (line);
			}
			theReader.Close ();
			line += PersistantValues.currentUser.UserID + "\t\t\t\t" + PersistantValues.currentUser.Name + "\t\t\t\t\t\t" + PersistantValues.currentUser.score + "\t\t" + PersistantValues.currentUser.deviation + "\n";
			System.IO.File.WriteAllText (Application.persistentDataPath + "/scores.txt",  Crypto.XOREncryption(line));
		} else {
			displayScore.text += "\nError saving score, please request volunteer to note down your score.";
		}

	}
}

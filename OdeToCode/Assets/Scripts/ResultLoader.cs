using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System.Text;
using UnityEngine.UI;

public class ResultLoader : MonoBehaviour {

	public Text display;

	// Use this for initialization
	void Start () {
		if (File.Exists (Application.persistentDataPath + "/scores.txt")) {
			string line;
			StreamReader theReader = new StreamReader(Application.persistentDataPath + "/scores.txt", Encoding.Default);
			using (theReader) {
				line = theReader.ReadToEnd();
				line = Crypto.XOREncryption(line);
				Debug.Log (line);

			}
			theReader.Close ();
			display.text += "\n" + line;
		}
	}

	// Update is called once per frame
	void Update () 
	{
	}
}

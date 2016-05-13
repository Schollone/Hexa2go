using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Hexa2Go {

	public enum Languages {
		English,
		French,
		German,
		Italian,
		Russian,
		Spanish
	}

	public delegate void LanguageChangeHandler ();

	public class LocalizationManager {

		public event LanguageChangeHandler OnLanguageChanged;

		private static LocalizationManager _instance = null;
		private static Hashtable textTable;

		public static LocalizationManager Instance {
			get {
				if (_instance == null) {
					_instance = new LocalizationManager ();
				}
				return _instance;
			}
		}


		public void LoadLanguage (string filename) {
			Debug.Log ("Load Language");

			string fullpath = "Languages/" + filename;
			
			TextAsset textAsset = (TextAsset)Resources.Load (fullpath);
			if (textAsset == null) {
				Debug.Log (fullpath + " file not found.");
				return;
			}
			
			if (textTable == null) {
				textTable = new Hashtable ();
			}
			textTable.Clear ();
			
			StringReader reader = new StringReader (textAsset.text);
			string key;
			string value;
			while (true) {
				key = reader.ReadLine ();
				value = reader.ReadLine ();
				if (key != null && value != null) {
					textTable.Add (key, value);
				} else {
					break;
				}
			}
			
			reader.Close ();

			if (OnLanguageChanged != null) {
				OnLanguageChanged ();
			}
		}

		public static string GetText (string key) {
			return  (string)textTable [key];
		}
	}


}
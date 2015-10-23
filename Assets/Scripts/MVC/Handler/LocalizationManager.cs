using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Hexa2Go {

	public class LocalizationManager {

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


		public static void LoadLanguage (string filename) {
			Debug.Log ("Load Language");

			LocalizationManager l = Instance;

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
		}

		public static string GetText (string key) {
			return  (string)textTable [key];
		}
	}


}
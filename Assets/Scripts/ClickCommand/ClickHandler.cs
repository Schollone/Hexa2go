using UnityEngine;
using System;
using System.Collections.Generic;

namespace Hexa2Go {

	public class ClickHandler {

		private static ClickHandler _instance;

		private IDictionary<ClickTypes, IClickCommand> clickCommandMap;

		private ClickHandler () {
			clickCommandMap = new Dictionary<ClickTypes, IClickCommand> ();
			clickCommandMap.Add (ClickTypes.ThrowDice, new ThrowDiceCommand ());
			clickCommandMap.Add (ClickTypes.SelectCharacter, new SelectCharacterCommand ());
			clickCommandMap.Add (ClickTypes.AcceptCharacter, new AcceptCharacterCommand ());
			clickCommandMap.Add (ClickTypes.SelectHexagon, new SelectHexagonCommand ());
			clickCommandMap.Add (ClickTypes.AcceptHexagon, new AcceptHexagonCommand ());
			//clickCommandMap.Add (ClickTypes.FocusHexagonTarget, new FocusHexagonCommand ());
			//clickCommandMap.Add (ClickTypes.AcceptFocusedHexagonTarget, new AcceptFocusedCHexagonCommand ());
		}

		public static ClickHandler Instance {
			get {
				if (ClickHandler._instance == null) {
					ClickHandler._instance = new ClickHandler ();
				}
				return ClickHandler._instance;
			}
		}

		public void OnClick (ClickTypes type, object data = null) {
			IClickCommand command = null;
			clickCommandMap.TryGetValue (type, out command);
			if (command != null) {
				command.Execute (data);
			} else {
				Debug.LogError ("Command not found: " + type);
			}

		}
	}
}


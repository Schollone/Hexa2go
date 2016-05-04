using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class PlayerModel : IPlayerModel {

		private TeamColor _teamColor;
		private int _savedCharacters = 0;
		private string _name = "";
		
		public PlayerModel (TeamColor teamColor, string name) {
			_teamColor = teamColor;
			_name = name;
			_savedCharacters = 0;
		}
		
		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
		}

		public int SavedCharacters {
			get {
				return _savedCharacters;
			}
		}

		public string Name {
			get {
				return _name;
			}
		}

		public void RemoveCharacter() {
			_savedCharacters++;
			if (_savedCharacters >= 3) {
				if (OnMatchFinished != null) {
					OnMatchFinished(this, new EventArgs());
				}
			}
		}

		public event EventHandler<EventArgs> OnMatchFinished;
	}

}
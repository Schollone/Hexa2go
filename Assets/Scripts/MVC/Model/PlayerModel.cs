using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class PlayerModel : IPlayerModel {

		private TeamColor _teamColor;
		private bool _isCOM = false;
		
		public PlayerModel(TeamColor newTeamColor, bool isCOM = false) {
			this._teamColor = newTeamColor;
			this._isCOM = isCOM;
		}
		
		public TeamColor teamColor {
			get {
				return _teamColor;
			}
		}
		
		public bool isComputer {
			get {
				return _isCOM;
			}
		}
	}

}
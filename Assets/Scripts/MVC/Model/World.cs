using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {

	public class World {
		
		/*private static World world;
		
		public const int height = 7;
		public const int width = 10;

		private Grid _grid;
		private string _diceValue1;
		private string _diceValue2;
		private bool _pasch;
		private IPlayerModel _activePlayer;
		private Dictionary<TeamColor, PlayerModel> _players;

		private World() {
			//this._grid = new Grid (width, height);
			/*
			this._players = new Dictionary<TeamColor, PlayerModel> ();
			this._players.Add(TeamColor.BLUE, new PlayerModel (TeamColor.BLUE));
			this._players.Add(TeamColor.RED, new PlayerModel (TeamColor.RED));
			this._activePlayer = this.players[TeamColor.BLUE];*/
		/*}
		
		public static World Instance {
			get {
				if (world == null) {
					world = new World();
				}
				return world;
			}
		}
		
		public Grid grid {
			get {
				return _grid;
			}
		}

		public string DiceValue1 {
			get {
				return _diceValue1;
			}
		}

		public string DiceValue2 {
			get {
				return _diceValue2;
			}
		}

		public bool Pasch {
			get {
				_pasch = false;
				if (_diceValue1 == _diceValue2) {
					_pasch = true;
				}
				return _pasch;
			}
		}

		public Dictionary<TeamColor, PlayerModel> players {
			get {
				return _players;
			}
		}

		public IPlayerModel activePlayer {
			get {
				return _activePlayer;
			}
			set {
				_activePlayer = value;
			}
		}*/
	}

}
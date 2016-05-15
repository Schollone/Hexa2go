using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hexa2Go {

	public class HexagonFacade {

		private HexagonHandler _hexagonHandler;
		private IHexagonModel _selectedHexagon;
		private IHexagonModel _focusedHexagon;

		public HexagonFacade () {
			_hexagonHandler = new HexagonHandler (10, 7);
		}

		public IHexagonModel SelectedHexagon {
			get {
				return _selectedHexagon;
			}
		}

		public IHexagonModel FocusedHexagon {
			get {
				return _focusedHexagon;
			}
		}

		public void ReferCharacterToItsHexagon (ICharacterModel character) {
			IHexagonController hexagon = _hexagonHandler.Get (character.GridPos);
			hexagon.Model.AddCharacter (character);
		}

		public bool CheckCharacterMoveable (ICharacterController character) {
			IHexagonController hexagon = _hexagonHandler.Get(character.Model.GridPos);
			foreach (GridPos neighorPos in hexagon.Model.Neighbors) {
				IHexagonController neighbor = _hexagonHandler.Get(neighorPos);
				if (neighbor.Model.State.IsActivated && !neighbor.Model.IsBlocked) {
					return true;
				}
			}
			return false;
		}

		public void SelectCharacter (GridPos gridPos) {
			IHexagonController controller = _hexagonHandler.Get (gridPos);
			SelectCharacter (controller);
		}
		
		public void SelectCharacter (IHexagonController hexagon) {
			_hexagonHandler.ResetField ();

			// Remark last selected hexagon
			if (_selectedHexagon != null) {
				Debug.LogWarning (_selectedHexagon.State.GetType () + " - " + _selectedHexagon.State.IsHome + " _ " + _selectedHexagon.GridPos);
				if (_selectedHexagon.IsBlocked) {
					_selectedHexagon.State.MarkAsBlocked ();
				} else {
					_selectedHexagon.State.MarkAsNormal ();
				}
			}
			
			// Mark next hexagon
			_focusedHexagon = null;
			
			UIHandler.Instance.AcceptController.View.Hide ();
			
			_selectedHexagon = hexagon.Model;
			_selectedHexagon.State.MarkAsSelectable ();
			InitFocusableNeighborsOfCharacter (_selectedHexagon.GridPos);

			hexagon.View.PlaySelectionClip();
		}

		public void InitSelectableHexagons () {
			_hexagonHandler.InitSelectableHexagons ();
		}

		public void SelectHexagon (GridPos gridPos) {
			IHexagonController controller = _hexagonHandler.Get (gridPos);
			SelectHexagon (controller);
		}

		public void SelectHexagon (IHexagonController hexagon) {
			_hexagonHandler.ResetDeactivatedField ();


			if (_selectedHexagon != null) {
				_selectedHexagon.State.MarkAsSelectable ();
			}
			ResetSelectionInfos();

			_selectedHexagon = hexagon.Model;

			InitFocusableNeighborsOfHexagon (_selectedHexagon);
			_selectedHexagon.State.MarkAsSelected ();

			hexagon.View.PlaySelectionClip();
		}

		private void InitFocusableNeighborsOfHexagon (IHexagonModel hexagon) {
			_hexagonHandler.InitFocusableNeighborsOfHexagon (hexagon.GridPos);
		}

		private void InitFocusableNeighborsOfCharacter (GridPos gridPos) {
			_hexagonHandler.InitFocusableNeighbors (gridPos);
		}

		public void FocusHexagon (GridPos gridPos, IPlayer player) {
			IHexagonController hexagon = _hexagonHandler.Get(gridPos);
			FocusHexagon (hexagon, player);
		}

		public void FocusHexagon (IHexagonController hexagon, IPlayer player) {
			if (_focusedHexagon != null) {
				_focusedHexagon.State.MarkAsFocusable ();
			}

			_focusedHexagon = hexagon.Model;
			hexagon.Model.State.MarkAsFocused ();

			player.HandleAcceptButton ();

			hexagon.View.PlayFocusClip();
		}

		public void ResetField () {
			_hexagonHandler.ResetField ();
		}

		public void ResetSelectionInfos () {
			_selectedHexagon = null;
			_focusedHexagon = null;
		}

		public void RemoveCharacter (ICharacterModel character) {
			_focusedHexagon.RemoveCharacter(character);
			_hexagonHandler.Get(_focusedHexagon.GridPos).View.PlayExplosion();
		}

		public void PlayEndlessExplosion (TeamColor teamColor) {
			_hexagonHandler.Get(teamColor).First().View.PlayExplosion(true);
		}



		public IHexagonController GetHexagonToFocus () {
			TeamColor targetTeamColor = GameManager.Instance.GetGameMode().CurrentPlayer.Model.TeamColor;
			IHexagonController target = _hexagonHandler.Get(targetTeamColor).First();
			Debug.Log(target + " - " + SelectedHexagon);
			Nullable<GridPos> nextPos = _hexagonHandler.GetNextHexagonToFocus (SelectedHexagon.GridPos, target.Model.GridPos);
			//Debug.LogWarning (nextPos);
			
			IHexagonController hexagon = _hexagonHandler.Get ((GridPos)nextPos);
			return hexagon;
		}

		public bool Strategy (IList<ICharacterController> characters, bool fromCharacterToTarget = true, bool checkForShortDistance = false) {
			Nullable<GridPos> nextPos = null;

			//Debug.LogWarning (characters.Count + " - fromCharacterToTarget: " + fromCharacterToTarget + " _ checkForShortDistance: " + checkForShortDistance);

			if (characters != null) {
				
				foreach (ICharacterController character in characters) {
					//ICharacterController character = characters [i];

					if (character.Model.IsInGame) {
					
						nextPos = fromCharacterToTarget ? StrategyFromCharacterToTarget (character, checkForShortDistance) : StrategyFromTargetToCharacter (character, checkForShortDistance);
						
						if (nextPos != null) {
							Debug.Log (nextPos + " !!! ");
							_focusedHexagon = _hexagonHandler.Get ( (GridPos) nextPos).Model;
							//_hexagonHandler.SetHexagonToPosition ((GridPos)nextPos);
							Debug.Log ("_focusedHexagon " + _focusedHexagon);
							return true;
						}
					}
				}
			} else { // choose a random hexagon
				IHexagonController selectedHexagon = null;
				_hexagonHandler.InitSelectableHexagons ();
				IList<IHexagonController> selectableHexagons = _hexagonHandler.GetMoveableHexagons ();
				
				foreach (IHexagonController hexagon in selectableHexagons) {
					if (hexagon.Model.State.IsHome) {
						continue;
					}
					GridPos gridPos = hexagon.Model.GridPos;

					IList<ICharacterModel> list = hexagon.Model.GetCharacters ();
					if (list.Count <= 0) {
						selectedHexagon = hexagon;
						break;
					}
				}

				foreach (GridPos neighborPos in selectedHexagon.Model.Neighbors) {
					IHexagonController neighbor = _hexagonHandler.Get (neighborPos);
					if (_hexagonHandler.IsFocusableForHexagon (selectedHexagon, neighbor)) {
						//_hexagonHandler.SetHexagonToPosition (neighborPos);
						Debug.Log ("Override!");
						_focusedHexagon = _hexagonHandler.Get (neighborPos).Model;
						break;
					}
				}
				_selectedHexagon = selectedHexagon.Model;
			}
			
			return false;
		}

		public Nullable<GridPos> StrategyFromCharacterToTarget (ICharacterController startCharacter, bool checkForShortDistance) {
			IHexagonController start = _hexagonHandler.Get (startCharacter.Model.GridPos);
			IHexagonController target = _hexagonHandler.Get (startCharacter.Model.TeamColor).First ();
			if (target == null) {
				return null;
			}
			//Debug.LogWarning ("Start Pos: " + start.Model.GridPos);
			//Debug.LogWarning ("Ziel Pos: " + target.Model.GridPos);
			int distanceFromOldPosition = _hexagonHandler.GetDistance (start, target.Model.GridPos);
			
			Nullable<GridPos> result = _hexagonHandler.CheckDistanceFromNeighbors (target, start, distanceFromOldPosition, checkForShortDistance);
			if (result != null) {
				_selectedHexagon = _hexagonHandler.Get (start.Model.GridPos).Model;
			}
			Debug.Log ("StrategyFromCharacterToTarget: " + result);
			return result;
		}
		
		public Nullable<GridPos> StrategyFromTargetToCharacter (ICharacterController targetCharacter, bool checkForShortDistance) {
			IHexagonController target = _hexagonHandler.Get (targetCharacter.Model.GridPos);
			IHexagonController start = _hexagonHandler.Get (targetCharacter.Model.TeamColor).First ();
			
			if (start == null) {
				return null;
			}
			//Debug.LogWarning ("Start Pos: " + start.Model.GridPos);
			//Debug.LogWarning ("Ziel Pos: " + target.Model.GridPos);
			
			int distanceFromOldPosition = _hexagonHandler.GetDistance (start, target.Model.GridPos);
			if (distanceFromOldPosition == 1) {
				return null;
			}
			
			Nullable<GridPos> result = _hexagonHandler.CheckDistanceFromNeighbors (target, start, distanceFromOldPosition, checkForShortDistance);
			if (result != null) {
				_selectedHexagon = _hexagonHandler.Get (start.Model.GridPos).Model;
			}
			Debug.Log ("StrategyFromTargetToCharacter: " + result);
			return result;
		}
	}
}


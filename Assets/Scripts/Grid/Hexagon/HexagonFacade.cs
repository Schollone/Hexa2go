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
			_hexagonHandler.GetTarget(teamColor).View.PlayExplosion(true);
		}
	}
}


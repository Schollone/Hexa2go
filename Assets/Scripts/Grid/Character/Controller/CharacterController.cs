using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public class CharacterController : ICharacterController {

		private ICharacterView _view;
		private ICharacterModel _model;
		private int _dist;
		
		public CharacterController (GridPos gridPos, TeamColor teamColor, CharacterType type) {

			_dist = int.MaxValue;

			string prefabName = "";
			CharacterFacade.PrefabNames.TryGetValue (type, out prefabName);

			GameObject prefab = Resources.Load (prefabName, typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate (prefab);
			GameObject characters = GameObject.Find ("Characters");
			instance.transform.SetParent (characters.transform);
			ICharacterView view = instance.GetComponent<ICharacterView> ();
			
			_view = view;
			
			_model = new CharacterModel (gridPos, teamColor, type);

			_model.OnUpdatedData += HandleOnUpdatedData;
			_model.OnGridPosChanged += HandleOnGridPosChanged;
			_model.OnTargetReached += HandleOnTargetReached;

			Color color = HexagonColors.GetColor (_model.TeamColor);
			_view.Init (gridPos, _model.OffsetPosition, color);
		}

		void HandleOnUpdatedData (object sender, EventArgs e) {
			View.UpdateState (Model.State);
			if (Model.State.IsSelected) {
				View.Select ();
			} else {
				View.Deselect ();
			}
		}

		void HandleOnTargetReached (object sender, EventArgs e) {
			View.Remove ();
		}

		void HandleOnGridPosChanged (object sender, EventArgs e) {
			IList<ICharacterModel> characters = GameManager.Instance.GridFacade.HexagonFacade.FocusedHexagon.GetCharacters();
			Model.OffsetPosition = GridHelper.GetOffsetPosition(characters);

			View.Move (Model.GridPos, Model.OffsetPosition, true);
		}

		#region ICharacterController implementation
		public ICharacterModel Model {
			get {
				return this._model;
			}
		}
		public ICharacterView View {
			get {
				return this._view;
			}
		}

		public int Distance {
			get {
				return _dist;
			}
			set {
				_dist = value;
			}
		}
		#endregion
	}
	
}
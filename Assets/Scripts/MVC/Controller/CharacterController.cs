using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterController : ICharacterController {

		private ICharacterView _view;
		private ICharacterModel _model;
		private int _dist;

		public CharacterController (GridPos gridPos, string prefabName, TeamColor teamColor) {

			_dist = int.MaxValue;

			GameObject prefab = Resources.Load (prefabName, typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate (prefab);
			GameObject characters = GameObject.Find ("Characters");
			instance.transform.SetParent (characters.transform);
			ICharacterView view = instance.GetComponent<ICharacterView> ();
			
			_view = view;
			
			_model = new CharacterModel (gridPos, teamColor);
			_model.OnSelectionChanged += HandleOnSelectionChanged;

			_model.OnGridPosChanged += HandleOnGridPosChanged;
			_model.OnTargetReached += HandleOnTargetReached;

			_view.Init (gridPos, _model.OffsetPosition);
		}

		void HandleOnTargetReached (object sender, CharacterValueChangedEventArgs e) {
			View.Remove ();
		}

		void HandleOnGridPosChanged (object sender, CharacterValueChangedEventArgs e) {
			/*if (GameManager.Instance.ButtonHandler.DicesController.Pasch) {
				View.Move (Model.GridPos, Model.OffsetPosition, true);
			} else {
				View.Move (Model.GridPos, Model.OffsetPosition, true);
			}*/
			View.Move (Model.GridPos, Model.OffsetPosition, true);
		}

		void HandleOnSelectionChanged (object sender, CharacterValueChangedEventArgs e) {
			if (Model.IsSelected) {
				View.Select ();
			} else {
				View.Deselect ();
			}
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
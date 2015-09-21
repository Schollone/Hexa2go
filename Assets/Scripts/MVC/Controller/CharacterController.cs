using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterController : ICharacterController {

		private ICharacterView _view;
		private ICharacterModel _model;

		public CharacterController(GridPos gridPos, string prefabName, TeamColor teamColor) {

			GameObject prefab = Resources.Load(prefabName, typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate(prefab);
			GameObject characters = GameObject.Find("Characters");
			instance.transform.SetParent(characters.transform);
			ICharacterView view = instance.GetComponent<ICharacterView>();
			view.Init(gridPos);
			
			_view = view;
			
			_model = new CharacterModel(gridPos, teamColor);
			_model.OnSelectionChanged += HandleOnSelectionChanged;

			_model.OnGridPosChanged += HandleOnGridPosChanged;
			_model.OnTargetReached += HandleOnTargetReached;
			
			//_model.OnActivationChanged += HandleOnActivationChanged;
			//_model.OnDeclaredTargetChanged += HandleOnDeclaredTargetChanged;
		}

		void HandleOnTargetReached (object sender, CharacterValueChangedEventArgs e) {
			View.Remove();
		}

		void HandleOnGridPosChanged (object sender, CharacterValueChangedEventArgs e) {
			ICharacterModel model = (ICharacterModel) sender;
			//model.GridPos
			View.Move(model.GridPos);
		}

		void HandleOnSelectionChanged (object sender, CharacterValueChangedEventArgs e) {
			//GameManager.Instance.GridHandler.HexagonHandler.TintHexagon(Model.GridPos);
			ICharacterModel model = (ICharacterModel) sender;
			if (model.IsSelected) {
				//GameManager.Instance.GridHandler.HexagonHandler.TintHexagon(Model.GridPos);
				View.Select();
			} else {
				//GameManager.Instance.GridHandler.HexagonHandler.TintHexagon(Model.GridPos);
				View.Deselect();
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
		#endregion
	}
	
}
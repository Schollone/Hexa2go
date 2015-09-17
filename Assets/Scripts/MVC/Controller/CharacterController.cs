using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterController : ICharacterController {

		private ICharacterView _view;
		private ICharacterModel _model;

		public CharacterController(GridPos gridPos, string prefabName) {



			GameObject prefab = Resources.Load(prefabName, typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate(prefab);
			GameObject characters = GameObject.Find("Characters");
			instance.transform.SetParent(characters.transform);
			ICharacterView view = instance.GetComponent<ICharacterView>();
			view.Init(gridPos);
			
			_view = view;
			
			_model = new CharacterModel(gridPos);
			_model.OnSelectionChanged += HandleOnSelectionChanged;
			
			//_model.OnActivationChanged += HandleOnActivationChanged;
			//_model.OnDeclaredTargetChanged += HandleOnDeclaredTargetChanged;
		}

		void HandleOnSelectionChanged (object sender, CharacterValueChangedEventArgs e) {
			//GameManager.Instance.GridHandler.HexagonHandler.TintHexagon(Model.GridPos);
			if (e.IsSelected) {
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
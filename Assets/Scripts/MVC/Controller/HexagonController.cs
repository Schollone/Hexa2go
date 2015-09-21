using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class HexagonController : IHexagonController {

		private readonly IHexagonModel _hexagonModel;
		private readonly IHexagonView _hexagonView;

		public HexagonController (GridPos gridPos) {
			GameObject prefab = Resources.Load ("Hexagon", typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate (prefab);

			GameObject grid = GameObject.Find ("Grid");
			instance.transform.SetParent (grid.transform);
			IHexagonView view = instance.GetComponent<IHexagonView> ();
			view.Init (gridPos);

			_hexagonView = view;

			_hexagonModel = new HexagonModel (gridPos);
			
			_hexagonModel.OnActivationChanged += HandleOnActivationChanged;
			_hexagonModel.OnDeclaredTargetChanged += HandleOnDeclaredTargetChanged;
			_hexagonModel.OnSelectionChanged += HandleOnSelectionChanged;
		}

		void HandleOnSelectionChanged (object sender, HexagonValueChangedEventArgs e) {
			if (Model.IsSelected) {
				View.Select ();
			} else {
				View.ResetTint ();
			}
		}

		void HandleOnDeclaredTargetChanged (object sender, HexagonValueChangedEventArgs e) {
			Color color = HexagonColors.GetColor (Model.TeamColor, View.DefaultBorderColor);
			View.DeclareTarget (color);
		}

		void HandleOnActivationChanged (object sender, HexagonValueChangedEventArgs e) {
			if (Model.IsActivated) {
				View.Activate ();
			} else {
				View.Deactivate ();
			}
		}

		#region IHexagonController implementation

		public IHexagonModel Model {
			get {
				return this._hexagonModel;
			}
		}

		public IHexagonView View {
			get {
				return this._hexagonView;
			}
		}

		#endregion
	}

}
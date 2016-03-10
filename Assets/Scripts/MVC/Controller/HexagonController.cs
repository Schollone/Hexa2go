using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class HexagonController : IHexagonController {

		private readonly IHexagonModel _hexagonModel;
		private readonly IHexagonView _hexagonView;
		private Nullable<GridPos> _pred;
		private int _dist;

		public HexagonController (GridPos gridPos) {
			GameObject prefab = Resources.Load ("Hexagon", typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate (prefab);

			_pred = null;
			_dist = int.MaxValue;

			GameObject grid = GameObject.Find ("Grid");
			instance.transform.SetParent (grid.transform);
			IHexagonView view = instance.GetComponent<IHexagonView> ();


			_hexagonView = view;

			_hexagonModel = new HexagonModel (gridPos);
			
			_hexagonModel.OnActivationChanged += HandleOnActivationChanged;
			_hexagonModel.OnSelectionChanged += HandleOnSelectionChanged;

			_hexagonModel.OnUpdatedData += HandleUpdatedData;

			_hexagonView.Init (gridPos, Model.State);
			_hexagonView.OnClicked += HandleOnClicked;;
		}

		void HandleOnClicked (object sender, EventArgs e) {
			Debug.Log("Clicked on: " + Model.GridPos);
			GameManager.Instance.GameModeHandler.GetGameMode().GetMatchState().HandleClick(Model);
		}

		void HandleUpdatedData (object sender, EventArgs e) {
			//IHexagonModel model = (IHexagonModel)sender;
			bool hexagonWasActivated = View.IsActivated;
			View.UpdateState (Model.State);

			if (Model.State.IsActivated) {
				if (hexagonWasActivated) {
					View.Activate (Model.State, false);
				} else {
					View.Activate (Model.State, true);
				}
			} else {
				if (hexagonWasActivated) {
					View.Deactivate (true);
				} else {
					View.Deactivate (false);
				}
			}
		}












		void HandleOnSelectionChanged (object sender, HexagonValueChangedEventArgs e) {
			if (Model.IsSelected) {
				View.Select ();
			} else {
				View.ResetTint ();
			}
		}

		void HandleOnActivationChanged (object sender, HexagonValueChangedEventArgs e) {
			//GameManager.Instance.GetCurrentMatchState ().OnHexagonActivationChange (this);

			GameManager.Instance.GameModeHandler.GetGameMode ().GetMatchState ().OnHexagonActivationChange (this);
			/*if (Model.IsActivated) {
				Color color = HexagonColors.GetColor (Model.TeamColor);

				if (GameManager.Instance.MatchState == MatchState.NullState) {
					View.Activate (color);
				} else {
					View.Activate (color, true);
				}

			} else {
				if (GameManager.Instance.MatchState == MatchState.NullState) {
					View.Deactivate ();
				} else {
					View.Deactivate (true);
				}
			}*/
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

		public Nullable<GridPos> Pred {
			get {
				return _pred;
			}
			set {
				_pred = value;
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
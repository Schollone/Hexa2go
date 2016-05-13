using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public class HexagonController : IHexagonController {

		private readonly IHexagonModel _hexagonModel;
		private readonly IHexagonView _hexagonView;
		private Nullable<GridPos> _pred;
		private int _dist;
		private bool _visited;

		public HexagonController (GridPos gridPos) {
			GameObject prefab = Resources.Load ("Hexagon", typeof(GameObject)) as GameObject;
			GameObject instance = GameObject.Instantiate (prefab);

			_pred = null;
			_dist = int.MaxValue;
			_visited = false;

			GameObject grid = GameObject.Find ("Grid");
			instance.transform.SetParent (grid.transform);
			IHexagonView view = instance.GetComponent<IHexagonView> ();


			_hexagonView = view;

			_hexagonModel = new HexagonModel (gridPos);
			_hexagonModel.OnUpdatedData += HandleUpdatedData;

			_hexagonView.Init (gridPos, Model.State);
			_hexagonView.OnClicked += HandleOnClicked;
			_hexagonView.OnCheckIsBlocked += HandleOnCheckIsBlocked;
		}

		private void HandleOnCheckIsBlocked (object sender, EventArgs e) {
			if (Model.IsBlocked) {
				Model.State.MarkAsBlocked ();
			}
		}

		private void HandleOnClicked (object sender, EventArgs e) {
			if (GameManager.Instance.GetGameMode().CurrentPlayer is Player) {
				Debug.Log ("Clicked on: " + Model.GridPos);
				GameManager.Instance.GetGameMode ().GetMatchState ().HandleClick (this);
			}
		}

		private void HandleUpdatedData (object sender, EventArgs e) {
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
					View.Deactivate (Model.State, true);
				} else {
					View.Deactivate (Model.State, false);
				}
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

		public bool Visited {
			get {
				return _visited;
			}
			set {
				_visited = value;
			}
		}

		#endregion
	}

}
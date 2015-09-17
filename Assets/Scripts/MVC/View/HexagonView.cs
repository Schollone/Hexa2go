﻿using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class HexagonView : MonoBehaviour, IHexagonView {

		private Color _defaultAreaColor;
		private Color _defaultBorderColor;

		private TeamColor _teamColor;

		void Awake () {	
			_defaultAreaColor = HexagonColors.WHITE;
			_defaultBorderColor = HexagonColors.WHITE;
		}

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public Color DefaultAreaColor {
			get {
				return _defaultAreaColor;
			}
			set {
				_defaultAreaColor = value;
			}
		}

		public Color DefaultBorderColor {
			get {
				return _defaultBorderColor;
			}
			set {
				_defaultBorderColor = value;
			}
		}

		private void TintBorder(Color color) {
			transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = color;
		}
		
		private void TintArea(Color color) {
			transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = color;
		}

		#region IHexagonView implementation

		public void Init (GridPos gridPos) {
			Vector3 tmp = GridHelper.HexagonPosition(gridPos);
			transform.position = tmp;
		}

		public void Select () {
			Debug.Log("Select");
			TintArea(HexagonColors.ORANGE);
			//TintBorder(HexagonColors.ORANGE);
		}

		public void Deselect () {
			Debug.Log("Deselect");
			TintArea(_defaultAreaColor);
			TintBorder(_defaultBorderColor);
		}

		public void Focus () {
			TintArea(HexagonColors.GREEN);
			TintBorder(HexagonColors.GREEN);
		}

		public void Focusable () {
			TintBorder(HexagonColors.GREEN);
		}

		public void ResetTint () {
			TintArea(_defaultAreaColor);
			TintBorder(_defaultBorderColor);
		}

		public void Activate () {
			_defaultAreaColor = HexagonColors.LIGHT_GRAY;
			_defaultBorderColor = HexagonColors.LIGHT_GRAY;
			TintArea(_defaultAreaColor);
			TintBorder(_defaultBorderColor);
			
			transform.position = new Vector3(transform.position.x, -0.3f, transform.position.z);
		}

		public void Deactivate () {
			_defaultAreaColor = HexagonColors.WHITE;
			_defaultBorderColor = HexagonColors.WHITE;
			TintArea(_defaultAreaColor);
			TintBorder(_defaultBorderColor);
			
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}

		public void DeclareTarget(Color color) {
			_defaultAreaColor = color;
			_defaultBorderColor = color;
			TintArea(color);
			TintBorder(color);
		}

		public Vector3 SlotPosition1 {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public Vector3 SlotPosition2 {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public TeamColor TeamColor {
			get {
				return this._teamColor;
			}
			set {
				_teamColor = value;
			}
		}

		#endregion
	}

}
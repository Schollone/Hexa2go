﻿using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class AcceptView : AbstractButtonView {

		// Use this for initialization
		protected override void Start () {
			base.Start ();
		}

		// Update is called once per frame
		protected override void Update () {
			base.Update ();
		}

		public override void Show () {
			if (_gameObject != null) {
				gameObject.SetActive (true);
				Enable();
			}
		}
		
		public override void Hide () {
			if (_gameObject != null) {
				gameObject.SetActive (false);
			}
		}
	}

}
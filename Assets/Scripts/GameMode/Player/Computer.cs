using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public enum AIType {
		Constructive,
		Destructive,
		Mixed
	}

	public delegate bool StrategyDelegate ();
	
	public class Computer : AbstractPlayer {
		AIType _aiType;

		public Computer () {
			_model = new PlayerModel (TeamColor.BLUE, (LocalizationManager.GetText (TextIdentifier.COMPUTER.ToString ())));

			_model.OnMatchFinished += HandleOnMatchFinished;

			_aiType = AIType.Constructive;
			int random = new System.Random(Guid.NewGuid ().GetHashCode ()).Next (0, 3);
			_aiType = (AIType)random;
		}
		
		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.Name;
			GameManager.Instance.GetGameMode ().GetPlayers () [1].View.UpdatePlayer (color, name);

			//this.OnClick (ClickTypes.ThrowDice);
			ClickHandler.Instance.OnClick (ClickTypes.ThrowDice);
			Debug.Log ("Throw Dice Computer");
		}
		
		public override void Throwing () {
			Debug.Log ("Throwing Computer");
		}
		
		public override void SelectCharacter () {
			UIHandler.Instance.DicesController.Disable ();
			Debug.Log ("SelectCharacter Computer");

			// TODO choose a character with the best next movement.
			
			IHexagonController hexagon = GameManager.Instance.GridFacade.HexagonFacade.GetHexagonToFocus();

			ContainerObject data = new ContainerObject (hexagon.Model.GridPos, this);
			ClickHandler.Instance.OnClick (ClickTypes.FocusHexagon, data);

			ClickHandler.Instance.OnClick (ClickTypes.FinishCharacterMove);
		}

		public override void HandleAcceptButton () {
			UIHandler.Instance.AcceptController.View.Hide ();
		}
		
		public override void SelectHexagon () {
			//this.OnClick (ClickTypes.SelectHexagon);
			//this.OnClick (ClickTypes.AcceptHexagon);
			UIHandler.Instance.DicesController.Disable ();
			Debug.Log ("SelectHexagon Computer");

			GameManager gm = GameManager.Instance;

			//IHexagonController hexagon = GameManager.Instance.GridFacade.HexagonFacade.GetHexagonToFocus();



			/*ICollection<ICharacterController> opponentCollection = GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values;
			ICharacterController[] opponentCharacters = new ICharacterController[opponentCollection.Count];
			opponentCollection.CopyTo (opponentCharacters, 0);
			
			ICollection<ICharacterController> playerCollection = GameManager.Instance.GridHandler.CharacterHandler_P1.Characters.Values;
			ICharacterController[] playerCharacters = new ICharacterController[playerCollection.Count];
			playerCollection.CopyTo (playerCharacters, 0);*/
			
			//opponentCharacters = hexagonHandler.SortCharacterByDistance (opponentCharacters);
			//playerCharacters = hexagonHandler.SortCharacterByDistance (playerCharacters);

			IList<ICharacterController> playerCharacters = gm.GridFacade.CharacterFacade.GetCharacters (gm.GetGameMode ().GetPlayers()[0].Model.TeamColor);
			IList<ICharacterController> opponentCharacters = gm.GridFacade.CharacterFacade.GetCharacters (gm.GetGameMode ().GetPlayers()[1].Model.TeamColor);
			
			List<StrategyDelegate> l = new List<StrategyDelegate> ();
			l.Add (() => gm.GridFacade.HexagonFacade.Strategy (opponentCharacters, true, true));
			l.Add (() => gm.GridFacade.HexagonFacade.Strategy (opponentCharacters, false, true));
			l.Add (() => gm.GridFacade.HexagonFacade.Strategy (playerCharacters, true));
			l.Add (() => gm.GridFacade.HexagonFacade.Strategy (playerCharacters, false));
			
			if (_aiType == AIType.Destructive) {
				l = new List<StrategyDelegate> ();
				l.Add (() => gm.GridFacade.HexagonFacade.Strategy (playerCharacters, true));
				l.Add (() => gm.GridFacade.HexagonFacade.Strategy (playerCharacters, false));
				l.Add (() => gm.GridFacade.HexagonFacade.Strategy (opponentCharacters, true, true));
				l.Add (() => gm.GridFacade.HexagonFacade.Strategy (opponentCharacters, false, true));
			} else if (_aiType == AIType.Mixed) {
				GridHelper.Shuffle (l);
			}
			l.Add (() => gm.GridFacade.HexagonFacade.Strategy (null));
			
			foreach (StrategyDelegate action in l) {
				bool hasResult = action ();
				if (hasResult) {
					break;
				}
			}

			GridPos selectedHexagonPos = gm.GridFacade.HexagonFacade.SelectedHexagon.GridPos;
			GridPos focusedHexagonPos = gm.GridFacade.HexagonFacade.FocusedHexagon.GridPos;

			ClickHandler.Instance.OnClick (ClickTypes.SelectHexagon, selectedHexagonPos);
			ContainerObject data = new ContainerObject (focusedHexagonPos, this);
			ClickHandler.Instance.OnClick (ClickTypes.FocusHexagon, data);

			ClickHandler.Instance.OnClick (ClickTypes.FinishHexagonMove);
		}
		
		public override void Moving () {
			Debug.Log ("Moving Computer");
		}
	}
}


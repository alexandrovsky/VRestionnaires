using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace VRestionnaire {
	public class ButtonSkin:MonoBehaviour, ISkinHandler {

		public Button button;

		public void ApplySkin(UISkinData skin)
		{
			button.transition = Selectable.Transition.ColorTint;
			button.colors = skin.buttonColorBlock;
			TMP_Text[] labels = button.GetComponentsInChildren<TMP_Text>();
			foreach(TMP_Text label in labels) {
				label.color = skin.buttonTextColor;
			}
			
			//button.image.sprite = skin.buttonSprite;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VRestionnaire {
	public class ButtonSkin:MonoBehaviour, ISkinHandler {

		public Button button;

		public void ApplySkin(UISkinData skin)
		{
			button.transition = Selectable.Transition.ColorTint;
			button.colors = skin.colorBlock;
			button.image.sprite = skin.buttonSprite;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace VRestionnaire {
	[RequireComponent(typeof(TMP_Text))]
	public class SkinText:MonoBehaviour, ISkinHandler {

		public TextFormat textFormat;

		public void ApplySkin(UISkinData skin)
		{
			TMP_Text text = GetComponent<TMP_Text>();
			text.font = skin.font;
			text.color = skin.normalTextColor;
			text.enableAutoSizing = false;
			switch(textFormat) {
			case TextFormat.Header1:
				text.fontSize = skin.fontSizeH1;
				break;
			case TextFormat.Header2:
				text.fontSize = skin.fontSizeH2;
				break;
			case TextFormat.Body:
				text.fontSize = skin.fontSizeBody;
				break;
			case TextFormat.Small:
				text.fontSize = skin.fontSizeSmall;
				break;
			}
			print( gameObject.transform.parent.parent.name + ">>" + gameObject.transform.parent.name + ">>" + gameObject.GetInstanceID() + "font size" + text.fontSize);
		}
	}

}

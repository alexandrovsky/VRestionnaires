using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace VRestionnaire {
	[RequireComponent(typeof(TMP_Text))]
	public class SkinText:MonoBehaviour, ISkinHandler {



		public void ApplySkin(UISkinData skin)
		{
			TMP_Text text = GetComponent<TMP_Text>();
			text.font = skin.font;
			text.fontSize = skin.fontSize;
			text.color = skin.normalTextColor;
			text.enableAutoSizing = false;
		}
	}

}

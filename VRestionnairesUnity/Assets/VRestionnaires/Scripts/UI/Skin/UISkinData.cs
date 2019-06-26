using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace VRestionnaire {

	
	[CreateAssetMenu(menuName = "UI Skin Data")]
	public class UISkinData:ScriptableObject {
		[Header("Test Settings")]
		public TMP_FontAsset font;
		public Color normalTextColor = new Color(0.0f,0.0f,0.0f,1f);

		[Header("Selectables Settings")]
		public ColorBlock colorBlock = new ColorBlock {
			normalColor = new Color(1f,1f,1f,1f),
			highlightedColor = new Color(0.96f,0.96f,0.96f,1f),
			pressedColor = new Color(0.78f,0.78f,0.78f,1),
			selectedColor = new Color(0.96f,0.96f,0.96f,1f),
			disabledColor = new Color(0.78f,0.78f,0.78f,0.5f),
			colorMultiplier = 1,
			fadeDuration = 0.1f
		};

		[Header("Toggle")]
		public Vector2 toggleSize = new Vector2(0.6f,0.6f);

		[Header("Background")]
		public Sprite panelSprite;
		public Color backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);

		[Header("Button")]
		public Sprite buttonSprite;
		
		


	}
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace VRestionnaire {

	public enum TextFormat {
		Header1,
		Header2,
		Body,
		Small
	}

	[CreateAssetMenu(menuName = "UI Skin Data")]
	public class UISkinData:ScriptableObject {
		[Header("Canvas")]
		public Vector2 canvasSize = new Vector2(16,16);
		public Vector3 canvasScale = Vector3.one;
		public float radioZOffset = -0.01f;


		[Header("Text Settings")]
		public TMP_FontAsset font;
		public Color normalTextColor = new Color(0.0f,0.0f,0.0f,1f);
		public Color requiredAsterixColor = new Color(1.0f,0.0f,0.0f,1f);
		public float fontSizeH1 = 0.5f;
		public float fontSizeH2 = 0.4f;
		public float fontSizeBody = 0.3f;
		public float fontSizeSmall = 0.18f;

		//[Header("Selectables Settings")]
		[Header("Button")]
		public Color buttonTextColor = Color.black;
		public ColorBlock buttonColorBlock = new ColorBlock {
			normalColor = new Color(1f,1f,1f,1f),
			highlightedColor = new Color(0.96f,0.96f,0.96f,1f),
			pressedColor = new Color(0.78f,0.78f,0.78f,1),
			selectedColor = new Color(0.96f,0.96f,0.96f,1f),
			disabledColor = new Color(0.78f,0.78f,0.78f,0.5f),
			colorMultiplier = 1,
			fadeDuration = 0.1f
		};

		[Header("SubmitButton")]
		public ColorBlock submitColorBlock  = new ColorBlock{
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

		[Header("Animation")]
		public float questionTransitionTime = 0.5f;

		//public Sprite buttonSprite;




	}
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VRestionnaire {
	[RequireComponent(typeof(Image))]
	public class BackgroundImage:MonoBehaviour {

		public Image image;

		public void ApplySkin(Sprite sprite, Color color)
		{
			image.sprite = sprite;
			image.color = color;
		}
		
	}

}

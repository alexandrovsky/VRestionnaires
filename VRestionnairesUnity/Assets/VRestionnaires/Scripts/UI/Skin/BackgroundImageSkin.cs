using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VRestionnaire {
	[RequireComponent(typeof(Image))]
	public class BackgroundImageSkin:MonoBehaviour, ISkinHandler {

		public Image image;



		public void ApplySkin(UISkinData skin)
		{

			image.sprite = skin.panelSprite;
			image.color = skin.backgroundColor;
		}
	}

}

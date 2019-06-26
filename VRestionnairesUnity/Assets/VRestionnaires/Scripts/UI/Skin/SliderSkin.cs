using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRestionnaire {


	public class SliderSkin:MonoBehaviour, ISkinHandler {


		public Image fillImage;
		public Image backgroundImage;

		void ISkinHandler.ApplySkin(UISkinData skin)
		{
			fillImage.color = skin.colorBlock.selectedColor;
			backgroundImage.color = skin.normalTextColor;
		}
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace VRestionnaire {

	public class RadioGroup : ItemGroup {

		public RadioGroup(string questionId, bool allowSwitchOff) : base(questionId,allowSwitchOff) { }
	}
}
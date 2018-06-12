using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Discode.EventSystem.Example
{
	public class Caster : MonoBehaviour
	{
		public IEnumerator Start()
		{
			EventDispatcher.RaiseEvent("OnEvent", this);

			yield return new WaitForSeconds(0.5f);

			EventDispatcher.RaiseEvent("OnEvent", this);

			yield return new WaitForSeconds(2);

			EventDispatcher.RaiseEvent("OnSomeOtherEvent", this);

			yield return new WaitForSeconds(1);

			EventDispatcher.RaiseEvent(EventTags.SOME_EVENT, this);

			yield return new WaitForSeconds(2);

			EventDispatcher.RaiseEvent(EventTags.SOME_OTHER_EVENT, this);
		}
	}
}

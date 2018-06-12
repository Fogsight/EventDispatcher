using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Discode.EventSystem.Example
{
	public class Reciever : MonoBehaviour
	{
		[SerializeField]
		private bool recievesOnEvent = true;

		[SerializeField]
		private bool recievesOnSomeOtherEvent = true;

		private void OnEnable()
		{
			if (recievesOnEvent)
			{
				EventDispatcher.AddListener("OnEvent", OnEventRecieved);
			}

			if (recievesOnSomeOtherEvent)
			{
				EventDispatcher.AddListener("OnSomeOtherEvent", OnSomeOtherEventRecieved);
			}
		}

		private void OnDisable()
		{
			EventDispatcher.RemoveListener("OnEvent", OnEventRecieved);
			EventDispatcher.RemoveListener("OnSomeOtherEvent", OnSomeOtherEventRecieved);
		}

		private void OnEventRecieved(IEventData eventData)
		{
			// Either cast or change to strong typed basd on data needs.

			Debug.Log(name + ": Event Data Recieved");
		}

		private void OnSomeOtherEventRecieved(IEventData eventData)
		{
			Debug.Log(name + ": Some other Event Data Recieved");
		}
	}
}
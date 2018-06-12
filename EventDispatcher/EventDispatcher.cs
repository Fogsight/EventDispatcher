using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Discode.EventSystem
{
	public static class EventDispatcher
	{	
		private static Dictionary<string, EventHandler> eventHandlers = new Dictionary<string, EventHandler>();

		/// <summary>
		/// Clears all registered listeners
		/// </summary>
		public static void RemoveAllListeners()
		{
			eventHandlers.Clear();
		}

		/// <summary>
		/// Adds the handler to a specified event handler based on ID.
		/// This allow the listeners to be raised when any event matches.
		/// </summary>
		/// <param name="eventID"></param>
		public static void AddListener(string eventID, EventHandler eventHandler)
		{
			if (string.IsNullOrEmpty(eventID) || eventHandler == null)
			{
				return;
			}

			if (!eventHandlers.ContainsKey(eventID))
			{
				eventHandlers.Add(eventID, null);
			}
			eventHandlers[eventID] += eventHandler;
		}

		public static void RemoveListener(string eventID, EventHandler eventHandler)
		{
			if (eventHandlers.ContainsKey(eventID))
			{
				eventHandlers[eventID] -= eventHandler;

				if (eventHandlers[eventID] == null)
				{
					eventHandlers.Remove(eventID);
				}
			}
		}

		public static void RaiseEvent(string eventID, object sender)
		{
			// Create event info
			EventData eventData = EventData.Alloate();
			eventData.EventID = eventID;
			eventData.Sender = sender;

			// Send it
			RaiseEvent(eventData);

			EventData.Release(eventData);
		}

		public static void RaiseEvent(IEventData eventData)
		{
			if (eventData  == null)
			{
				return;
			}

			eventData.Sent = true;
			eventHandlers[eventData.EventID](eventData);
		}
	}
}

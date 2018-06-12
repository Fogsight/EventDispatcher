using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Discode.EventSystem
{
	public interface IEventData
	{
		string EventID { get; set; }
		object Sender { get; set; }
		bool Sent { get; set; }
		bool Handled { get; set; }
		void Clear();
	}
}

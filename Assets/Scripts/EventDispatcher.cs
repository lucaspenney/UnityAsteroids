using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: determine if C# delegates cause reference counting on target object
public delegate void EventDispatcherDelegate( object evtData );

public interface IEventDispatcher
{
	void addListener (string evtName, EventDispatcherDelegate callback);
	void dropListener (string evtName, EventDispatcherDelegate callback);
	void dispatch (string evtName, object evt) ;

}
public class EventDispatcher : IEventDispatcher
{
	Dictionary<string,List<EventDispatcherDelegate>> m_listeners = new Dictionary<string, List<EventDispatcherDelegate>>();

	public void addListener( string evtName, EventDispatcherDelegate callback )
	{
		List<EventDispatcherDelegate> evtListeners = null;
		if (m_listeners.TryGetValue (evtName, out evtListeners)) 
		{
			evtListeners.Remove(callback); //make sure we dont add duplicate
			evtListeners.Add(callback);
		} else {
			evtListeners = new List<EventDispatcherDelegate>();
			evtListeners.Add(callback);

			m_listeners.Add(evtName, evtListeners);
		}
	}
	public void dropListener( string evtName, EventDispatcherDelegate callback )
	{
		List<EventDispatcherDelegate> evtListeners = null;
		if (m_listeners.TryGetValue (evtName, out evtListeners)) 
		{
			for( int i=0;i< evtListeners.Count; i++)
			{
				evtListeners.Remove(callback);
			}
		}
	}
	public void dispatch( string evtName, object evt ) 
	{
		//FIXME: might need to COPY the list<dispatchers> here so that an 
		//	event listener that results in adding/removing listeners does 
		//	not invalidate this for loop

		List<EventDispatcherDelegate> evtListeners = null;
		if (m_listeners.TryGetValue (evtName, out evtListeners)) 
		{
			for (int i = evtListeners.Count - 1; i >= 0; i--)
			{
				evtListeners[i](evt);
			}
		}
	}
}
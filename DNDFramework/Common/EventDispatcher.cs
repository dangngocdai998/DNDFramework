using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DNDFramework;
public class EventDispatcher
{
    static Dictionary<string, Dictionary<string, Action<object>>> eventPools = new Dictionary<string, Dictionary<string, Action<object>>>();

    // Register to listen for eventID, callback will be invoke when event with eventID be raise
    public static void RegisterListener(EventID eventID, string nameObj, Action<object> callback)
    {
        if (!eventPools.ContainsKey(eventID.ToString()))
        {
            eventPools.Add(eventID.ToString(), new Dictionary<string, Action<object>>());
        }

        if (!eventPools[eventID.ToString()].ContainsKey(nameObj))
        {
            eventPools[eventID.ToString()].Add(nameObj, callback);
        }
        // eventPools[eventID.ToString()][nameObj] = callback;
        // eventPools[eventID.ToString()] += callback;
    }

    // Post event, this will notify all listener which register to listen for eventID
    public static void PostEvent(EventID eventID, Component sender, object param = null)
    {
        if (eventPools.ContainsKey(eventID.ToString()))
        {
            // eventPools[eventID.ToString()]?.Invoke(param);
            Dictionary<string, Action<object>> dicvalue = new Dictionary<string, Action<object>>(eventPools[eventID.ToString()]);
            if (dicvalue.Values.Count > 0)
            {
                foreach (var item in dicvalue.Values)
                {
                    item?.Invoke(param);
                }
            }
        }
    }

    // Use for Unregister, not listen for an event anymore.
    public static void RemoveListener(EventID eventID, string nameObj)
    {
        if (eventPools.ContainsKey(eventID.ToString()))
        {
            if (eventPools[eventID.ToString()].ContainsKey(nameObj))
            {
                eventPools[eventID.ToString()].Remove(nameObj);
            }
            if (eventPools[eventID.ToString()].Count <= 0)
            {
                eventPools.Remove(eventID.ToString());
            }
        }
    }
    /// <summary>
    /// Clears all the listener.
    /// </summary>
    public static void ClearAllListener()
    {
        eventPools.Clear();
    }
}

/// An Extension class, declare some "shortcut" for using EventDispatcher
public static class EventDispatcherExtension
{
    /// Use for registering with EventDispatcher
    public static void RegisterListener(this MonoBehaviour sender, EventID eventID, Action<object> callback)
    {
        // string fillter = sender.GetType() + sender.name;
        // Debug.Log("Register: " + fillter);
        EventDispatcher.RegisterListener(eventID, sender.GetType() + sender.GetInstanceID().ToString(), callback);
    }

    public static void RemoveListener(this MonoBehaviour sender, EventID eventID)
    {
        EventDispatcher.RemoveListener(eventID, sender.GetType() + sender.GetInstanceID().ToString());
    }


    /// Post event with param
    public static void PostEvent(this MonoBehaviour sender, EventID eventID, object param)
    {
        EventDispatcher.PostEvent(eventID, sender, param);
    }


    /// Post event with no param (param = null)
    public static void PostEvent(this MonoBehaviour sender, EventID eventID)
    {
        EventDispatcher.PostEvent(eventID, sender, null);
    }

    public static void PostEvent(EventID eventID, object param)
    {
        EventDispatcher.PostEvent(eventID, null, param);
    }


    /// Post event with no param (param = null)
    public static void PostEvent(EventID eventID)
    {
        EventDispatcher.PostEvent(eventID, null, null);
    }
}

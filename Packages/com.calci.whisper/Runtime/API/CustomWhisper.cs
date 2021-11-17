using System;
using System.Collections.Generic;
using System.Linq;
using Calci.Whisperer;
using UnityEngine;

public abstract class CustomWhisper<TKey> 
{
	#region Essential

	private static Dictionary<TKey, List<MsgActionBase>> actions;

	static CustomWhisper()
	{
		DomainReset();
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void DomainReset()
	{
		actions = new Dictionary<TKey, List<MsgActionBase>>();

#if UNITY_EDITOR
		WhisperDebugger.isDestroyed = false;
#endif
	}

	#endregion

	#region Internal

	private static List<MsgActionBase> FindActions(TKey eventName)
	{
#if UNITY_EDITOR
		if (!WhisperDebugger.isDestroyed)
			WhisperDebugger.Instance.Init();
#endif

		if (actions.TryGetValue(eventName, out List<MsgActionBase> list))
		{
			return list;
		}

		return null;
	}

	private static void RegisterInternal(TKey eventName, MsgActionBase msgAction)
	{
#if UNITY_EDITOR
		if (!WhisperDebugger.isDestroyed)
			WhisperDebugger.Instance.Init();
#endif

		if (actions.TryGetValue(eventName, out List<MsgActionBase> list))
		{
			list.Add(msgAction);
		}
		else
		{
			list = new List<MsgActionBase> { msgAction };
			actions[eventName] = list;
		}
	}

	#endregion

	#region PUBLIC API

	public static void Register(TKey eventName, Action action)
	{
		MsgAction msgAction = new MsgAction();
		msgAction.Init(action);

		RegisterInternal(eventName, msgAction);
	}

	public static void Register<T1>(TKey eventName, Action<T1> action)
	{
		MsgAction<T1> msgAction = new MsgAction<T1>();
		msgAction.Init(action);

		RegisterInternal(eventName, msgAction);
	}
	
	public static void Register<T1, T2>(TKey eventName, Action<T1, T2> action)
	{
		MsgAction<T1, T2> msgAction = new MsgAction<T1, T2>();
		msgAction.Init(action);

		RegisterInternal(eventName, msgAction);
	}
	
	public static void Register<T1, T2, T3>(TKey eventName, Action<T1, T2, T3> action)
	{
		MsgAction<T1, T2, T3> msgAction = new MsgAction<T1, T2, T3>();
		msgAction.Init(action);

		RegisterInternal(eventName, msgAction);
	}

	public static void Unregister(TKey eventName, Action action)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				MsgAction act = (MsgAction)list[i];

				if (act.Equals(action))
				{
					list.RemoveAt(i);
					break;
				}
			}
		}
	}

	public static void Unregister<T1>(TKey eventName, Action<T1> action)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				MsgAction<T1> act = (MsgAction<T1>)list[i];

				if (act.Equals(action))
				{
					list.RemoveAt(i);
					break;
				}
			}
		}
	}
	
	public static void Unregister<T1, T2>(TKey eventName, Action<T1, T2> action)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				MsgAction<T1, T2> act = (MsgAction<T1, T2>)list[i];

				if (act.Equals(action))
				{
					list.RemoveAt(i);
					break;
				}
			}
		}
	}
	
	public static void Unregister<T1, T2, T3>(TKey eventName, Action<T1, T2, T3> action)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				MsgAction<T1, T2, T3> act = (MsgAction<T1, T2, T3>)list[i];

				if (act.Equals(action))
				{
					list.RemoveAt(i);
					break;
				}
			}
		}
	}

	public static void Execute(TKey eventName)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			foreach (MsgActionBase actionBase in list)
			{
				(actionBase as MsgAction)?.Invoke();
			}
		}
	}

	public static void Execute<T1>(TKey eventName, T1 arg1)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			foreach (MsgActionBase actionBase in list)
			{
				(actionBase as MsgAction<T1>)?.Invoke(arg1);
			}
		}
	}
	
	public static void Execute<T1, T2>(TKey eventName, T1 arg1, T2 arg2)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			foreach (MsgActionBase actionBase in list)
			{
				(actionBase as MsgAction<T1, T2>)?.Invoke(arg1, arg2);
			}
		}
	}
	
	public static void Execute<T1, T2, T3>(TKey eventName, T1 arg1, T2 arg2, T3 arg3)
	{
		List<MsgActionBase> list = FindActions(eventName);
		if (list != null)
		{
			foreach (MsgActionBase actionBase in list)
			{
				(actionBase as MsgAction<T1, T2, T3>)?.Invoke(arg1, arg2, arg3);
			}
		}
	}

	#endregion

	#region DEBUGGER

#if UNITY_EDITOR
	internal static IReadOnlyList<TKey> GetManagedActionKeys()
	{
		return actions.Keys.ToList();
	}

	internal static IReadOnlyList<MsgActionBase> GetManagedActions(TKey key)
	{
		return actions[key].ToList();
	}
#endif

	#endregion
}
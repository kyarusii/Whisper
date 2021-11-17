using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Calci.Whisperer
{
	internal class WhisperMono : MonoBehaviour, IDisposable
	{
		private static WhisperMono _instance = default;

		public static WhisperMono Inst {
			get
			{
#if UNITY_EDITOR
				Assert.IsTrue(Application.isPlaying);
#endif

				if (_instance == null)
				{
					_instance = FindObjectOfType<WhisperMono>();

					if (_instance == null)
					{
						_instance = new GameObject($"MessageMono").AddComponent<WhisperMono>();
						DontDestroyOnLoad(_instance.gameObject);
					}
				}

				return _instance;
			}
		}

		private void OnDestroy()
		{
			_instance = null;
		}

#if UNITY_EDITOR && UNITY_2019_3_OR_NEWER
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ReloadDomain()
		{
			_instance = null;
		}
#endif

		#region Essential

		private readonly Dictionary<string, List<MsgActionBase>>
			_global = new Dictionary<string, List<MsgActionBase>>();

		public void Dispose()
		{
			_global.Clear();
		}

		#endregion

		#region Internal

		private static List<MsgActionBase> FindActions(string eventName)
		{
			if (Inst._global.TryGetValue(eventName, out List<MsgActionBase> list))
			{
				return list;
			}

			return null;
		}

		private static void RegisterInternal(string eventName, MsgActionBase msgAction)
		{
			if (Inst._global.TryGetValue(eventName, out List<MsgActionBase> list))
			{
				list.Add(msgAction);
			}
			else
			{
				list = new List<MsgActionBase> { msgAction };
				Inst._global[eventName] = list;
			}
		}

		private void OnDisable()
		{
			if (gameObject != null && !gameObject.activeSelf)
			{
				return;
			}

			Dispose();
		}

		#endregion

		#region PUBLIC API

		public static void Register(string eventName, Action action)
		{
			MsgAction msgAction = new MsgAction();
			msgAction.Init(action);

			RegisterInternal(eventName, msgAction);
		}

		public static void Register<T1>(string eventName, Action<T1> action)
		{
			MsgAction<T1> msgAction = new MsgAction<T1>();
			msgAction.Init(action);

			RegisterInternal(eventName, msgAction);
		}

		public static void Unregister(string eventName, Action action)
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

		public static void Unregister<T1>(string eventName, Action<T1> action)
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

		public static void Execute(string eventName)
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

		public static void Execute<T1>(string eventName, T1 arg1)
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

		#endregion
	}
}
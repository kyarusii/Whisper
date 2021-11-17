#if UNITY_EDITOR
using UnityEngine;

namespace Calci.Whisperer
{
	internal sealed class WhisperDebugger : MonoBehaviour
	{
		public static bool isDestroyed;
		private static WhisperDebugger _instance = default;

		private bool isInitialized = default;
		
		public static WhisperDebugger Instance {
			get
			{
				if (_instance == default)
				{
					Create();
				}

				return _instance;
			}
		}


		public void Init()
		{
			if (isInitialized) return;
			
			isInitialized = true;
		}
		
		public static void Create()
		{
			if (!Application.isPlaying) return;
			if (_instance != null) return;
			
			GameObject container = new GameObject("[MessageDebugger]");
			_instance = container.AddComponent<WhisperDebugger>();
			
			DontDestroyOnLoad(container);
		}

		private void OnDestroy()
		{
			_instance = default;
			isDestroyed = true;
		}
	}
}
#endif
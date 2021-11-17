using UnityEngine;

public class CustomWhisperUsage : MonoBehaviour
{
	private void Awake()
	{
		IntWhisper.Register<string>(100, Debug.Log);
	}

	private void OnEnable()
	{
		GameObjectWhisper.Execute<bool>(gameObject, true);
	}

	private void OnDisable()
	{
		GameObjectWhisper.Execute<bool>(gameObject, false);
	}
}
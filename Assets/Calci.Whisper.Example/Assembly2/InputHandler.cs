using UnityEngine;

namespace Calci.Whisperer.Example.Assembly2
{
	public class InputHandler : MonoBehaviour
	{
		private void Update()
		{
			for (int i = 97; i < 123; i++)
			{
				var keyCode = (KeyCode)i;
				if (Input.GetKeyDown(keyCode))
				{
					Whisper.Execute("OnKeyDown", keyCode);
				}
			}
		}
	}
}
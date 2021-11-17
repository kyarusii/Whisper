using System;
using UnityEngine;

namespace Calci.Whisperer.Example.Assembly1
{
    public class InputProcessor : MonoBehaviour
    {
        private void Awake()
        {
            Whisper.Register<KeyCode>("OnKeyDown", OnKeyDown);
        }

        private void OnKeyDown(KeyCode keyCode)
        {
            Debug.Log($"OnKeyDown : {keyCode}");
        }
    }
}
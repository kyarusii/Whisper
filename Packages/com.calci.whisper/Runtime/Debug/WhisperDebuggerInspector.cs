#if UNITY_EDITOR

using System.Diagnostics;
using System.IO;
using UnityEditor;

namespace Calci.Whisperer
{
	[CustomEditor(typeof(WhisperDebugger))]
	internal sealed class WhisperDebuggerInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			var keys = Whisper.GetManagedActionKeys();
			if (keys.Count > 0)
			{
				EditorGUILayout.LabelField("Keys", $"{keys.Count}", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				{
					foreach (string key in keys)
					{
						var actions = Whisper.GetManagedActions(key);
						EditorGUILayout.LabelField($"{key}", $"{actions.Count}");
						if (actions.Count > 0)
						{
							EditorGUI.indentLevel++;
							{
								foreach (MsgActionBase action in actions)
								{
									EditorGUILayout.LabelField($"{action.Get().Method.DeclaringType}::{action.Get().Method}");
									EditorGUI.indentLevel++;
									// EditorGUILayout.LabelField($"at line {action.lineNumber} ({action.caller})");
									
									// StackFrame stackFrame = new System.Diagnostics.StackTrace(1).GetFrame(1);
									// string fileName = stackFrame.GetFileName();
									// string methodName = stackFrame.GetMethod().ToString();
									// int lineNumber = stackFrame.GetFileLineNumber();
									//
									// EditorGUILayout.LabelField(
									// 	$"{methodName}({Path.GetFileName(fileName)}:{lineNumber})");
									EditorGUI.indentLevel--;
								}
							}
							EditorGUI.indentLevel--;
						}
					}
				}
				EditorGUI.indentLevel--;
			}
		}
	}
}

#endif

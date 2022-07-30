
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponEvent), editorForChildClasses: true)]
public class WeaponEventEditor : Editor
{
        
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("m"));
        GUI.enabled = Application.isPlaying;
        WeaponEvent e = target as WeaponEvent;
        if (GUILayout.Button("Raise"))
            e.Raise(e.testProperty);
        }
    }

                     
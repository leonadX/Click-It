using System.IO;
using UnityEditor;
using UnityEngine;

public class EventParametersCreatorWindow : EditorWindow
{
    string myString = "intList";
    string myClassString = "int[]";
    bool onlyEditorClass = false;
    bool UnityClass = false;
    string path = "Assets/Scripts/SO EventSystem";

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/Event Parameters Creator Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EventParametersCreatorWindow window = (EventParametersCreatorWindow)EditorWindow.GetWindow(typeof(EventParametersCreatorWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        GUILayout.Label("Current path: " + path, EditorStyles.miniLabel);
        GUILayout.Space(10);
        myString = EditorGUILayout.TextField("Name of Parameter(Fancy) ", myString);
        myClassString = EditorGUILayout.TextField(new GUIContent("Name of Class* ", "Same as the Fancy Parameter unless you are dealing with arrays and lists"), myClassString);
        onlyEditorClass = EditorGUILayout.Toggle(new GUIContent("Only Editor Class", "Debugging purposes or for corecting issues"), onlyEditorClass);
        UnityClass = EditorGUILayout.Toggle(new GUIContent("Is Unity Class?", "Is the class using the Unity Engine namespace"), UnityClass);

        if (GUILayout.Button("Create Asset", GUILayout.ExpandHeight(false)))
        {
            if (onlyEditorClass)
            {
                CreateEventEditor(myString, myClassString);
            }
            else
            {
                CreateEvent(myString, myClassString);
                CreateSOEvent(myString, myClassString);
                CreateListenerEvent(myString, myClassString);
                CreateEventEditor(myString, myClassString);
            }
            AssetDatabase.Refresh();
        }
    }
    void CreateEvent(string selected, string clas)
    {
        // remove whitespace and minus
        string name = selected.Replace(" ", "_");
        name = name.Replace("-", "_");
        string copyPath = path + "/DataTypes/Unity" + name + "Event.cs";
        Debug.Log("Creating Classfile: " + copyPath);
        /*        if (File.Exists(copyPath) == false)
                { // do not overwrite*/
        using (StreamWriter outfile =
            new StreamWriter(copyPath))
        {
            outfile.Write
                (
                @"
using UnityEngine.Events;
" + (UnityClass ? "using UnityEngine;" : "") + @"
[System.Serializable] public class Unity" + name + @"Event : UnityEvent<" + clas + @"> { }
                     ");
        }//File written
        //}
    }
    void CreateSOEvent(string selected, string clas)
    {
        string name = selected.Replace(" ", "_");
        name = name.Replace("-", "_");
        string copyPath = path + "/Events/" + name + "Event.cs";
        Debug.Log("Creating Classfile: " + copyPath);
        /*        if (File.Exists(copyPath) == false)
                { // do not overwrite*/
        using (StreamWriter outfile =
            new StreamWriter(copyPath))
        {
            outfile.Write
                (
                @"
using UnityEngine;


[CreateAssetMenu(fileName = ""New " + name + @" Event"", menuName = ""Game Events /" + name + @" Event"")]
public class " + name + @"Event : BaseGameEvent<" + clas + @">
{
        public " + clas + @" testProperty;
}
                     ");
        }
    }
    void CreateListenerEvent(string selected, string clas)
    {
        string name = selected.Replace(" ", "_");
        name = name.Replace("-", "_");
        string copyPath = path + "/Listeners/" + name + "EventListener.cs";
        Debug.Log("Creating Classfile: " + copyPath);
        /*        if (File.Exists(copyPath) == false)
                { // do not overwrite*/
        using (StreamWriter outfile =
            new StreamWriter(copyPath))
        {
            outfile.Write
                (
                @"
" + (UnityClass ? "using UnityEngine;" : "") + @"
public class " + name + @"EventListener : BaseGameEventListener<" + clas + @", " + name + @"Event, Unity" + name + @"Event>
{ }
                     ");
        }
    }
    void CreateEventEditor(string selected, string clas)
    {
        // remove whitespace and minus
        string name = selected.Replace(" ", "_");
        name = name.Replace("-", "_");
        string copyPath = path + "/Editor/" + name + "EventEditor.cs";
        Debug.Log("Creating Classfile: " + copyPath);
        /*        if (File.Exists(copyPath) == false)
                { // do not overwrite*/
        using (StreamWriter outfile =
            new StreamWriter(copyPath))
        {
            outfile.Write
                (
                @"
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(" + name + @"Event), editorForChildClasses: true)]
public class " + name + @"EventEditor : Editor
{
        
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        //EditorGUILayout.PropertyField(serializedObject.FindProperty(""m""));
        GUI.enabled = Application.isPlaying;
        " + name + @"Event e = target as " + name + @"Event;
        if (GUILayout.Button(""Raise""))
            e.Raise(e.testProperty);
        }
    }

                     ");
        }//File written
        //}
    }
}

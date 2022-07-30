using GoogleSheetsToUnity;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
#endif

[CreateAssetMenu(menuName = "GTSU/Examples/CodexEntry")]
public class CodexEntry : ScriptableObject
{
    [ShowIf("ShowE"), Label("Loading.......")]
    public Empty E;

    [HideInInspector]
    public bool ShowE = false;

    public string sheetID;
    public string workSheet;

    [Space]
    public string Name;

    public string Header;
    public string conflict;

    [Space]
    public List<int> ConflictBtwVillages = new List<int>();
    public List<int> War = new List<int>();
    public List<int> TerroristAttack = new List<int>();
    public List<int> NaturalDisater = new List<int>();

    [Button("Update Object")]
    public void UpdateObjects()
    {
        ShowE = true;
        UpdateStats(UpdateEntry, true);
    }
    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(sheetID, workSheet), callback, mergedCells);
    }
    public void UpdateEntry(GstuSpreadSheet ss)
    {
        ss.rows.secondaryKeyLink = ss.rows.secondaryKeyLink.ToDictionary(x => x.Key.Trim().Replace(" ", String.Empty), x => x.Value);
        /*        foreach (var author in ss.rows.secondaryKeyLink)
                {
                    Debug.Log(string.Format("Key: {0}, Value: {1}", author.Key, author.Value));
                }*/
        Header = ss[Name, "Header"].value;
        conflict = ss[Name, "Conflict"].value;

        ConflictBtwVillages.Clear();
        foreach (var value in ss[Name, "Conflict between two villages", true])
        {
            ConflictBtwVillages.Add(int.Parse(value.value.ToString()));
        }

        War.Clear();
        foreach (var value in ss[Name, "War", true])
        {
            War.Add(int.Parse(value.value.ToString()));
        }

        TerroristAttack.Clear();
        foreach (var value in ss[Name, "Terrorist Attack", true])
        {
            TerroristAttack.Add(int.Parse(value.value.ToString()));
        }

        NaturalDisater.Clear();
        foreach (var value in ss[Name, "Natural disaster", true])
        {
            NaturalDisater.Add(int.Parse(value.value.ToString()));
        }

        ShowE = false;
    }
}

[System.Serializable]
public struct Empty
{
}

/*#if UNITY_EDITOR
[CustomEditor(typeof(CodexEntry))]
public class CodexEntryEditor : Editor
{
    CodexEntry CE;

    private void OnEnable()
    {
        CE = (CodexEntry)target;
    }

    public override void OnInspectorGUI()
    {
        if (CE.ShowE)
            GUILayout.Label("Loading");
        base.OnInspectorGUI();

    }
}

#endif*/
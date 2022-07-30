using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.Events;
using GoogleSheetsToUnity;
using System.Linq;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(menuName = "GTSU/Examples/GlobalCodexManager")]
public class GlobalCodexEntry : ScriptableObject
{
    [FancyHeader("☺    Global Codex Entry    ☺", 1.5f, "#F1BAA7", 5.5f, order = 0)]
    [Label("")] public Empty e;
    public string sheetID;
    public string workSheet;

#if UNITY_EDITOR
    [Space]
    public FolderReference NewEntrySaveFolder;
#endif
    [Space]
    public List<CodexEntry> Codex = new List<CodexEntry>();

#if UNITY_EDITOR
    //[Button(sp: 10)]
    public void PopulateCodex()
    {
        //Object[] data = AssetDatabase.LoadAllAssetsAtPath("Assets/Scriptable Objects/Story Events/Test Event.asset");

        string[] guids = AssetDatabase.FindAssets("t:" + typeof(CodexEntry).Name);
        Codex.Clear();
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            if (!Codex.Contains(AssetDatabase.LoadAssetAtPath<CodexEntry>(path)))
                Codex.Add(AssetDatabase.LoadAssetAtPath<CodexEntry>(path));
        }

    }
    //[Button(sp: 10)]
    public void UpdateExistingCodex()
    {
        foreach (var item in Codex)
        {
            item.UpdateObjects();
        }
    }


    [Button(sp: 10)]
    public void FetchCodex()
    {

        GetData(UpdateEntry, true);
    }
    void GetData(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(sheetID, workSheet), callback, mergedCells);
    }


    void UpdateEntry(GstuSpreadSheet ss)
    {
        ss.rows.secondaryKeyLink = ss.rows.secondaryKeyLink.ToDictionary(x => x.Key.Trim().Replace(" ", String.Empty), x => x.Value);
        foreach (var author in ss.rows.secondaryKeyLink)
        {
            Debug.Log(string.Format("Key: {0}, Value: {1}", author.Key, author.Value));

            CodexEntry entry = Codex.FirstOrDefault((x) => x.Name == author.Key);
            if (entry == null)
            {
                if (author.Key == "Name")
                    continue;
                CodexEntry asset = ScriptableObject.CreateInstance<CodexEntry>();

                AssetDatabase.CreateAsset(asset, NewEntrySaveFolder.Path + "/" + author.Key + ".asset");

                asset.Name = author.Key;
                asset.sheetID = sheetID;
                asset.workSheet = workSheet;
                asset.UpdateEntry(ss);
                //Selection.activeObject = asset;
            }
            else
            {
                entry.UpdateEntry(ss);
            }
        }
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
    }
#endif

}

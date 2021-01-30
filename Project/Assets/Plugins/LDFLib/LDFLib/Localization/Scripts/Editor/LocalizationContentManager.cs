using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LDF.Localization;
using System.Linq;

public class LocalizationContentManager : EditorWindow
{
    private static TextAsset _jsonFile;
    private static DefaultAsset _exportFolder;

    [MenuItem("Window/LocalizationContentManager")]
    public static void OpenWindow()
    {
        GetWindow<LocalizationContentManager>();
    }

    private void OnGUI()
    {
        GUILayout.Space(30);

        _jsonFile = EditorGUILayout.ObjectField("Json: ", _jsonFile, typeof(TextAsset), false) as TextAsset;
        KeyAsstsFolderField();
        DrawButtons();
    }

    private void DrawButtons()
    {
        GUILayout.BeginHorizontal();

        using (new EditorGUI.DisabledScope(_exportFolder == null))
        {
            if (GUILayout.Button("Create New Keys From Json", GUILayout.ExpandWidth(false)))
            {
                CreateLocalizationKeysFromJson();
            }
        }

        if (GUILayout.Button("Export All Keys To Json", GUILayout.ExpandWidth(false)))
        {
            ExportLocalizationKeyAssetsToJson();
        }

        GUILayout.EndHorizontal();
    }

    private void KeyAsstsFolderField()
    {
        _exportFolder = EditorGUILayout.ObjectField(
             "Key Assets Folder",
             _exportFolder,
             typeof(DefaultAsset),
             false) as DefaultAsset;

        if (_exportFolder != null)
        {
            EditorGUILayout.HelpBox(
                "Valid folder! Name: " + _exportFolder.name,
                MessageType.Info,
                true);
        }
        else
        {
            EditorGUILayout.HelpBox(
                "Folder for new Keys is not valid!",
                MessageType.Warning,
                true);
        }
    }

    private void ExportLocalizationKeyAssetsToJson()
    {
        var localizationKeyJsonWrappers = GetLocalizationKeyAssets()
                                            .Select(key => new LocalizedStringJsonWrapper(key.name))
                                            .ToArray();

        var jsonMainWrapper = new LocalizedDictionaryJsonWrapper(localizationKeyJsonWrappers);

        var json = JsonUtility.ToJson(jsonMainWrapper, true);

        var path = GetJsonExportPath();

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }

        AssetDatabase.Refresh();
    }

    private IEnumerable<LocalizedText> GetLocalizationKeyAssets()
    {
        return AssetDatabase.FindAssets($"t:{typeof(LocalizedText).Name}")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(assetPath =>
                AssetDatabase.LoadAssetAtPath(assetPath, typeof(LocalizedText)) as LocalizedText);
    }

    private void CreateLocalizationKeysFromJson()
    {
        string jsonPath = GetPathToJson();

        if (string.IsNullOrEmpty(jsonPath))
            return;

        string json = File.ReadAllText(jsonPath);

        var jsonDictionary = JsonUtility.FromJson<LocalizedDictionaryJsonWrapper>(json);

        string localizedKeysDirectory = AssetDatabase.GetAssetPath(_exportFolder);

        var newKeysCount = 0;
        foreach (var kvp in jsonDictionary.KeyValuePairs)
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(LocalizedText).Name},{kvp.LocalizationKey}");

            bool localizationKeyAssetDoesNotExists = guids.Length == 0;

            if (localizationKeyAssetDoesNotExists)
            {
                var newKeyAsset = CreateInstance<LocalizedText>();
                newKeyAsset.name = kvp.LocalizationKey;

                if (string.IsNullOrEmpty(localizedKeysDirectory))
                    localizedKeysDirectory = EditorUtility.OpenFolderPanel("Choose default saving folder", Application.dataPath, "Resources");

                if (localizedKeysDirectory.StartsWith(Application.dataPath, System.StringComparison.Ordinal))
                {
                    localizedKeysDirectory = "Assets" + localizedKeysDirectory.Substring(Application.dataPath.Length);
                }

                if (!AssetDatabase.IsValidFolder(localizedKeysDirectory))
                    throw new System.Exception($"Path: {localizedKeysDirectory} | is not valid");

                var newPath = Path.Combine(localizedKeysDirectory, $"{newKeyAsset.name}.asset");
                Debug.Log($"Created new Localization Key: {newKeyAsset.name} \n at: {newPath}");
                AssetDatabase.CreateAsset(newKeyAsset, newPath);
                newKeysCount++;
                continue;
            }
        }
        Debug.Log($"Created {newKeysCount} New Key Assets at: {localizedKeysDirectory}");
        AssetDatabase.Refresh();
    }

    private static string GetPathToJson()
    {
        return AssetDatabase.GetAssetPath(_jsonFile);
    }

    private string GetJsonExportPath()
    {
        string path;

        if (_exportFolder == null)
        {
            path = EditorUtility.SaveFilePanelInProject("Save Json To", "Localization_Keys", "json", "Please enter a file name");
        }
        else
        {
            path = EditorUtility.SaveFilePanelInProject("Save Json To", "Localization_Keys", "json", "Please enter a file name", AssetDatabase.GetAssetPath(_exportFolder));

        }
        if (string.IsNullOrEmpty(path))
        {
            throw new System.Exception("Export destination was not set");
        }

        return path;
    }
}

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BlendShapeCopyTool : EditorWindow
{
    private SkinnedMeshRenderer sourceMesh;
    private SkinnedMeshRenderer targetMesh;

    [System.Serializable]
    private class BlendShapeData
    {
        public List<BlendShapeEntry> entries = new List<BlendShapeEntry>();
    }

    [System.Serializable]
    private class BlendShapeEntry
    {
        public string name;
        public float weight;
    }

    [MenuItem("Window/Avatar Tools/BlendShape Copy Tool")]
    public static void ShowWindow()
    {
        GetWindow<BlendShapeCopyTool>("BlendShape Copy");
    }

    private void OnGUI()
    {
        GUILayout.Label("BlendShape Copy Tool", EditorStyles.boldLabel);
        
        // --- Object to Object Copy ---
        EditorGUILayout.Space();
        GUILayout.Label("Direct Copy (Same Project)", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("SourceのBlendShape値をTargetの同じ名前のBlendShapeにコピーします。", MessageType.Info);

        sourceMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Source Mesh", sourceMesh, typeof(SkinnedMeshRenderer), true);
        targetMesh = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("Target Mesh", targetMesh, typeof(SkinnedMeshRenderer), true);

        EditorGUI.BeginDisabledGroup(sourceMesh == null || targetMesh == null);
        if (GUILayout.Button("Copy BlendShapes"))
        {
            CopyBlendShapes();
        }
        EditorGUI.EndDisabledGroup();

        // --- File Based Copy ---
        EditorGUILayout.Space();
        GUILayout.Label("File Based Copy (Inter-Project)", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("JSON形式で値を保存・読み込みします。別のプロジェクトとの受け渡しに利用してください。", MessageType.Info);

        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(sourceMesh == null);
        if (GUILayout.Button("Export to JSON"))
        {
            ExportToJson();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(targetMesh == null);
        if (GUILayout.Button("Import from JSON"))
        {
            ImportFromJson();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        // --- Utilities ---
        EditorGUILayout.Space();
        GUILayout.Label("Utilities", EditorStyles.boldLabel);
        if (GUILayout.Button("Reset Target BlendShapes"))
        {
            ResetBlendShapes(targetMesh);
        }
    }

    private void CopyBlendShapes()
    {
        if (sourceMesh == null || targetMesh == null) return;

        Undo.RecordObject(targetMesh, "Copy BlendShapes");

        Dictionary<string, float> sourceValues = GetBlendShapeValues(sourceMesh);
        int copiedCount = ApplyBlendShapeValues(targetMesh, sourceValues);

        Debug.Log($"BlendShape Copy Completed: {copiedCount} shapes copied from {sourceMesh.name} to {targetMesh.name}");
    }

    private void ExportToJson()
    {
        if (sourceMesh == null) return;

        string path = EditorUtility.SaveFilePanel("Save BlendShape Data", "", sourceMesh.name + "_blendshapes.json", "json");
        if (string.IsNullOrEmpty(path)) return;

        BlendShapeData data = new BlendShapeData();
        Mesh mesh = sourceMesh.sharedMesh;
        for (int i = 0; i < mesh.blendShapeCount; i++)
        {
            data.entries.Add(new BlendShapeEntry {
                name = mesh.GetBlendShapeName(i),
                weight = sourceMesh.GetBlendShapeWeight(i)
            });
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log($"BlendShape Data exported to: {path}");
    }

    private void ImportFromJson()
    {
        if (targetMesh == null) return;

        string path = EditorUtility.OpenFilePanel("Open BlendShape Data", "", "json");
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;

        string json = File.ReadAllText(path);
        BlendShapeData data = JsonUtility.FromJson<BlendShapeData>(json);

        Dictionary<string, float> values = new Dictionary<string, float>();
        foreach (var entry in data.entries)
        {
            values[entry.name] = entry.weight;
        }

        Undo.RecordObject(targetMesh, "Import BlendShapes from JSON");
        int appliedCount = ApplyBlendShapeValues(targetMesh, values);
        Debug.Log($"BlendShape Data imported: {appliedCount} shapes applied to {targetMesh.name}");
    }

    private Dictionary<string, float> GetBlendShapeValues(SkinnedMeshRenderer meshRenderer)
    {
        Dictionary<string, float> values = new Dictionary<string, float>();
        Mesh mesh = meshRenderer.sharedMesh;
        for (int i = 0; i < mesh.blendShapeCount; i++)
        {
            values[mesh.GetBlendShapeName(i)] = meshRenderer.GetBlendShapeWeight(i);
        }
        return values;
    }

    private int ApplyBlendShapeValues(SkinnedMeshRenderer meshRenderer, Dictionary<string, float> values)
    {
        int count = 0;
        Mesh mesh = meshRenderer.sharedMesh;
        for (int i = 0; i < mesh.blendShapeCount; i++)
        {
            string name = mesh.GetBlendShapeName(i);
            if (values.ContainsKey(name))
            {
                meshRenderer.SetBlendShapeWeight(i, values[name]);
                count++;
            }
        }
        return count;
    }

    private void ResetBlendShapes(SkinnedMeshRenderer mesh)
    {
        if (mesh == null) return;

        Undo.RecordObject(mesh, "Reset BlendShapes");
        for (int i = 0; i < mesh.sharedMesh.blendShapeCount; i++)
        {
            mesh.SetBlendShapeWeight(i, 0);
        }
        Debug.Log($"BlendShapes Reset: {mesh.name}");
    }
}

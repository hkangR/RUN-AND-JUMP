using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformCopier))]
public class TransformCopierEditor : Editor
{
    
    public override void OnInspectorGUI() {
        TransformCopier transformCopier = (TransformCopier)target;

        // 使用默认的Inspector绘制
        DrawDefaultInspector();

        // 添加一个按钮并实现其功能
        if (GUILayout.Button("Copy Transform")) {
            transformCopier.CopyTransform();
        }
    }
}

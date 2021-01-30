using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LDF.Systems.AI.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(BehaviourTree<,>),true)]
    public class BehaviourTreeEditor : Editor
    {
        Type[] _subtypes;
        private void OnEnable()
        {
            var nodeType = target.GetType().GetProperty("StartingNode").PropertyType;
            _subtypes = GetSubTypes(nodeType);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var type in _subtypes)
            {
                if (GUILayout.Button($"Create {type.Name}"))
                {
                    CreateSubAsset(type);
                }
            }
        }

        private void CreateSubAsset(Type type)
        {
            var newObject = CreateInstance(type);
            newObject.name = $"{type.Name}";

            AssetDatabase.AddObjectToAsset(newObject,target);

            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
        }

        private Type[] GetSubTypes(Type baseClass)
        {
            return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                    where !domainAssembly.IsDynamic
                    from assemblyType in domainAssembly.GetExportedTypes()
                    where !assemblyType.IsGenericType
                    where baseClass.IsAssignableFrom(assemblyType) && assemblyType != baseClass
                    select assemblyType).ToArray();
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Mirror;

namespace RTSEngine
{
    [CustomEditor(typeof(NetworkLobbyManager_Mirror), true)]
    public class NetworkLobbyManagerEditor_Mirror : Editor
    {
        NetworkLobbyManager_Mirror manager;
        SerializedObject manager_SO;

        public void OnEnable()
        {
            manager = (NetworkLobbyManager_Mirror)target;
            manager_SO = new SerializedObject(manager);
        }

        public override void OnInspectorGUI()
        {
            manager_SO.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space();

            if(GUILayout.Button("Refresh Spawnable Prefabs"))
            {
                manager_SO.FindProperty("spawnablePrefabs").ClearArray();

                Object[] prefabs = Resources.LoadAll("Prefabs", typeof(Object)) as Object[];
                for (int i = 0; i < prefabs.Length; i++) //go through all prefabs in the path "...Resources/Prefabs/"
                    if (((GameObject)prefabs[i]).GetComponent<Entity>())
                    {
                        manager_SO.FindProperty("spawnablePrefabs").InsertArrayElementAtIndex(i);
                        manager_SO.FindProperty($"spawnablePrefabs.Array.data[{i}]").objectReferenceValue = ((GameObject)prefabs[i]).GetComponent<Entity>();
                    }

                Debug.Log("[Input Manager Editor] Spawnable Prefabs list updated.");
            }

            manager_SO.ApplyModifiedProperties(); //apply all modified properties always at the end of this method.
        }
    }
}

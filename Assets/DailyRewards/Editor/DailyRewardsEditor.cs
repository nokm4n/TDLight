/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEditor;
using UnityEngine;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

namespace NiobiumStudios
{

    [CustomEditor(typeof(DailyRewards))]
    public class DailyRewardsEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            SerializedProperty instanceIdProp = serializedObject.FindProperty("instanceId");
            SerializedProperty useCloudClockProp = serializedObject.FindProperty("useCloudClock");
            SerializedProperty rewardsProp = serializedObject.FindProperty("rewards");
            SerializedProperty cloudClockListProp = serializedObject.FindProperty("cloudClockList");
            SerializedProperty isSingletonProp = serializedObject.FindProperty("isSingleton");
            SerializedProperty keepOpenProp = serializedObject.FindProperty("keepOpen");
            SerializedProperty resetPrizeProp = serializedObject.FindProperty("resetPrize");

            EditorGUILayout.PropertyField(instanceIdProp, new GUIContent("Instance ID", "Change this ID number if you are using multiple instances"));
            EditorGUILayout.PropertyField(isSingletonProp, new GUIContent("Is Singleton?", "Is it singleton or the reward is reloaded on each scene?"));
            EditorGUILayout.PropertyField(keepOpenProp, new GUIContent("Keep Open?", "Do the Canvas pops up even when there is no reward available?"));
            EditorGUILayout.PropertyField(resetPrizeProp, new GUIContent("Reset Prize?", "Resets the prize after the second day?"));

            useCloudClockProp.boolValue = EditorGUILayout.Toggle(new GUIContent("Use Cloud Clock?"), useCloudClockProp.boolValue);
            if (useCloudClockProp.boolValue && EditorTools.DrawHeader("Cloud Clock"))
            {
                // Cloud Clock List
                for (int i = 0; i < cloudClockListProp.arraySize; i++)
                {
                    var cloudClockProp = cloudClockListProp.GetArrayElementAtIndex(i);
                    var cloudClockNameProp = cloudClockProp.FindPropertyRelative("name");

                    if (EditorTools.DrawHeader(cloudClockNameProp.stringValue))
                    {
                        EditorTools.BeginContents();


                        var cloudClockUrlProp = cloudClockProp.FindPropertyRelative("url");
                        var cloudClockTimeFormatProp = cloudClockProp.FindPropertyRelative("timeFormat");

                        EditorGUILayout.PropertyField(cloudClockNameProp, new GUIContent("Name"));
                        EditorGUILayout.PropertyField(cloudClockUrlProp, new GUIContent("URL"));
                        EditorGUILayout.PropertyField(cloudClockTimeFormatProp, new GUIContent("Time Format"));

                        EditorTools.EndContents();

                        if (GUILayout.Button("Remove Cloud Clock"))
                        {
                            cloudClockListProp.DeleteArrayElementAtIndex(i);
                        }
                    }
                }

                if (GUILayout.Button("Add Cloud Clock"))
                {
                    cloudClockListProp.InsertArrayElementAtIndex(cloudClockListProp.arraySize);
                }
            }

            if (EditorTools.DrawHeader("Rewards"))
            {
                // Rewards
                for (int i = 0; i < rewardsProp.arraySize; i++)
                {
                    if (EditorTools.DrawHeader("Day " + (i + 1)))
                    {
                        EditorTools.BeginContents();
                        SerializedProperty rewardProp = rewardsProp.GetArrayElementAtIndex(i);

                        var unitRewardProp = rewardProp.FindPropertyRelative("unit");
                        var rewardQtProp = rewardProp.FindPropertyRelative("reward");
                        var rewardSpriteProp = rewardProp.FindPropertyRelative("sprite");

                        EditorGUILayout.PropertyField(unitRewardProp, new GUIContent("Unit"));
                        EditorGUILayout.PropertyField(rewardQtProp, new GUIContent("Reward"));
                        rewardSpriteProp.objectReferenceValue = EditorGUILayout.ObjectField("Sprite", rewardSpriteProp.objectReferenceValue, typeof(Sprite), false);

                        EditorTools.EndContents();

                        if (GUILayout.Button("Remove Reward"))
                        {
                            rewardsProp.DeleteArrayElementAtIndex(i);
                        }
                    }
                }

                if (GUILayout.Button("Add Reward"))
                {
                    rewardsProp.InsertArrayElementAtIndex(rewardsProp.arraySize);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
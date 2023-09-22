using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemSliderData)), CanEditMultipleObjects]
public class ItemSliderEditor : Editor
{
    public SerializedProperty
        SliderType,
        idData_One,
        idData_Two,
        idData_Three,
        nametext,
        slider,
        inputvalue;

    void OnEnable()
    {
        // Setup the SerializedProperties
        SliderType = serializedObject.FindProperty("sliderType");
        idData_One = serializedObject.FindProperty("id_data_one");
        idData_Two = serializedObject.FindProperty("id_data_two");
        idData_Three = serializedObject.FindProperty("id_data_three");
        nametext = serializedObject.FindProperty("nameText");
        slider = serializedObject.FindProperty("slider");
        inputvalue = serializedObject.FindProperty("inputValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(SliderType);

        ItemSliderData.SliderType st = (ItemSliderData.SliderType)SliderType.enumValueIndex;

        switch (st)
        {
            case ItemSliderData.SliderType.SINGLE:
                EditorGUILayout.LabelField("SLIDER SETTING", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(idData_One, new GUIContent("id_data_one"));
                EditorGUILayout.PropertyField(nametext, new GUIContent("nameText"));
                EditorGUILayout.PropertyField(slider, new GUIContent("slider"));
                EditorGUILayout.PropertyField(inputvalue, new GUIContent("inputValue"));

                break;

            case ItemSliderData.SliderType.DUO:
                EditorGUILayout.LabelField("SLIDER SETTING", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(idData_Two,new GUIContent("id_data_two"));
                EditorGUILayout.PropertyField(nametext, new GUIContent("nameText"));
                EditorGUILayout.PropertyField(slider, new GUIContent("slider"));
                EditorGUILayout.PropertyField(inputvalue, new GUIContent("inputValue"));

                break;

            case ItemSliderData.SliderType.TRIPLE:
                EditorGUILayout.LabelField("SLIDER SETTING", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(idData_Three,new GUIContent("id_data_three"));
                EditorGUILayout.PropertyField(nametext, new GUIContent("nameText"));
                EditorGUILayout.PropertyField(slider, new GUIContent("slider"));
                EditorGUILayout.PropertyField(inputvalue, new GUIContent("inputValue"));
                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
}

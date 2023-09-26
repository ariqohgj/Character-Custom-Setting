using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinda.AlterLife;

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
            case ItemSliderData.SliderType.Upper:
                EditorGUILayout.LabelField("SLIDER SETTING", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(idData_One, new GUIContent("id_data_one"));
                EditorGUILayout.PropertyField(nametext, new GUIContent("nameText"));
                EditorGUILayout.PropertyField(slider, new GUIContent("slider"));
                EditorGUILayout.PropertyField(inputvalue, new GUIContent("inputValue"));

                break;

            case ItemSliderData.SliderType.Lower:
                EditorGUILayout.LabelField("SLIDER SETTING", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(idData_One, new GUIContent("id_data_one"));
                EditorGUILayout.PropertyField(nametext, new GUIContent("nameText"));
                EditorGUILayout.PropertyField(slider, new GUIContent("slider"));
                EditorGUILayout.PropertyField(inputvalue, new GUIContent("inputValue"));

                break;

            case ItemSliderData.SliderType.None:
                EditorGUILayout.LabelField("SLIDER SETTING", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(idData_One, new GUIContent("id_data_one"));
                EditorGUILayout.PropertyField(nametext, new GUIContent("nameText"));
                EditorGUILayout.PropertyField(slider, new GUIContent("slider"));
                EditorGUILayout.PropertyField(inputvalue, new GUIContent("inputValue"));
                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
}

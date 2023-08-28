using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
public class LabelledArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public LabelledArrayAttribute(string[] names) { this.names = names; }
    public LabelledArrayAttribute(Type enumType) { names = Enum.GetNames(enumType); }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LabelledArrayAttribute))]
public class LabelledArrayDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);
        try
        {
            var path = property.propertyPath;
            int pos = int.Parse(path.Split('[').LastOrDefault().TrimEnd(']'));
            EditorGUI.PropertyField(rect, property, new GUIContent(((LabelledArrayAttribute)attribute).names[pos]), true);
        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label, true);
        }
        EditorGUI.EndProperty();
    }
}
#endif
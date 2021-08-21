using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Bgm))]
public class BgmDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect sceneRect = new Rect(position.x, position.y, 120, EditorGUIUtility.singleLineHeight);
        Rect audioRect = new Rect(position.x + 125, position.y, 130, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(sceneRect, property.FindPropertyRelative("scene"), GUIContent.none);
        EditorGUI.PropertyField(audioRect, property.FindPropertyRelative("audio"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}

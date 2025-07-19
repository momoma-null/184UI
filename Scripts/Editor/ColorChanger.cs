
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Iwashi.UI
{
    sealed class ColorChanger : EditorWindow
    {
        [SerializeField]
        Canvas _targetCanvas;

        SerializedProperty _colorSyncedColorProperty;
        SerializedProperty _rgbSyncedColorProperty;
        SerializedProperty _textColorProperty;

        readonly GUIContent themeColorLabel = new("Theme Color");
        readonly GUIContent textColorLabel = new("Text Color");

        [MenuItem("Tools/184UI/Color Changer", false, 184)]
        static void ShowWindow()
        {
            GetWindow<ColorChanger>(ObjectNames.NicifyVariableName(nameof(ColorChanger)));
        }

        void OnEnable()
        {
            CollectGraphics();
        }

        void OnDisable()
        {
            _colorSyncedColorProperty = null;
            _rgbSyncedColorProperty = null;
            _textColorProperty = null;
        }

        void OnHierarchyChange()
        {
            CollectGraphics();
        }

        void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            _targetCanvas = EditorGUILayout.ObjectField("Target Canvas", _targetCanvas, typeof(Canvas), true) as Canvas;
            if (EditorGUI.EndChangeCheck())
            {
                CollectGraphics();
            }

            if (_colorSyncedColorProperty != null)
            {
                _colorSyncedColorProperty.serializedObject.Update();
                EditorGUILayout.PropertyField(_colorSyncedColorProperty, themeColorLabel);
                if (_colorSyncedColorProperty.serializedObject.ApplyModifiedProperties())
                {
                    ApplyColor();
                }
            }
            if (_textColorProperty != null)
            {
                _textColorProperty.serializedObject.Update();
                EditorGUILayout.PropertyField(_textColorProperty, textColorLabel);
                _textColorProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        void CollectGraphics()
        {
            if (_targetCanvas == null)
            {
                _targetCanvas = null; // object null. not Unity null.
                _colorSyncedColorProperty = null;
                _rgbSyncedColorProperty = null;
                _textColorProperty = null;
                return;
            }

            // Collect Graphic Components
            var graphics = new List<Graphic>();
            _targetCanvas.GetComponentsInChildren(true, graphics);
            if (graphics == null || graphics.Count == 0)
            {
                _colorSyncedColorProperty = null;
                _rgbSyncedColorProperty = null;
                _textColorProperty = null;
                return;
            }

            // Find most common color
            var graphicGroup = graphics.Where(x => x is not TMP_Text).GroupBy(x => (Color32)x.color).OrderByDescending(x => x.Count()).FirstOrDefault();
            if (graphicGroup != null)
            {
                var themeColor = graphicGroup.Key;

                var colorSyncedGraphics = new List<Graphic>();
                var rgbSyncedGraphics = new List<Graphic>();
                foreach (var graphic in graphics)
                {
                    var graphicColor32 = (Color32)graphic.color;
                    if (graphicColor32.r == themeColor.r
                        && graphicColor32.g == themeColor.g
                        && graphicColor32.b == themeColor.b)
                    {
                        if (graphicColor32.a == themeColor.a)
                        {
                            colorSyncedGraphics.Add(graphic);
                        }
                        else
                        {
                            rgbSyncedGraphics.Add(graphic);
                        }
                    }
                }

                _colorSyncedColorProperty = new SerializedObject(colorSyncedGraphics.ToArray()).FindProperty("m_Color");

                if (rgbSyncedGraphics.Count > 0)
                {
                    _rgbSyncedColorProperty = new SerializedObject(rgbSyncedGraphics.ToArray()).FindProperty("m_Color");
                }
                else
                {
                    _rgbSyncedColorProperty = null;
                }
            }
            else
            {
                _colorSyncedColorProperty = null;
                _rgbSyncedColorProperty = null;
            }

            // Find most common text color
            var textGroup = graphics.OfType<TMP_Text>().GroupBy(x => (Color32)x.color).OrderByDescending(x => x.Count()).FirstOrDefault();
            if (textGroup != null)
            {
                _textColorProperty = new SerializedObject(textGroup.ToArray()).FindProperty("m_fontColor");
            }
            else
            {
                _textColorProperty = null;
            }
        }

        void ApplyColor()
        {
            if (_rgbSyncedColorProperty != null)
            {
                _rgbSyncedColorProperty.serializedObject.Update();
                var color = _colorSyncedColorProperty.colorValue;
                _rgbSyncedColorProperty.FindPropertyRelative("r").floatValue = color.r;
                _rgbSyncedColorProperty.FindPropertyRelative("g").floatValue = color.g;
                _rgbSyncedColorProperty.FindPropertyRelative("b").floatValue = color.b;
                _rgbSyncedColorProperty.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

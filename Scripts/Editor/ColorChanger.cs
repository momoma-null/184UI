
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
        SerializedProperty _fontProperty;

        readonly GUIContent themeColorLabel = new("Theme Color");
        readonly GUIContent textColorLabel = new("Text Color");
        readonly GUIContent fontLabel = new("Font");

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
            ResetProperties();
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
            if (_fontProperty != null)
            {
                _fontProperty.serializedObject.Update();
                EditorGUILayout.PropertyField(_fontProperty, fontLabel);
                _fontProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        void CollectGraphics()
        {
            if (_targetCanvas == null)
            {
                _targetCanvas = null; // object null. not Unity null.
                ResetProperties();
                return;
            }

            // Collect Graphic Components
            var graphics = new List<Graphic>();
            _targetCanvas.GetComponentsInChildren(true, graphics);
            if (graphics == null || graphics.Count == 0)
            {
                ResetProperties();
                return;
            }

            // Find most common color
            var graphicGroup = graphics.Where(x => x is not TMP_Text).GroupBy(x => (Color32)x.color).OrderByDescending(x => x.Count()).FirstOrDefault();
            if (graphicGroup != null)
            {
                var themeColor = graphicGroup.Key;

                var rgbSyncedGraphics = new List<Graphic>();
                foreach (var graphic in graphics.Where(x => x is not TMP_Text))
                {
                    var graphicColor32 = (Color32)graphic.color;
                    if (graphicColor32.r == themeColor.r
                        && graphicColor32.g == themeColor.g
                        && graphicColor32.b == themeColor.b
                        && graphicColor32.a != themeColor.a)
                    {
                        rgbSyncedGraphics.Add(graphic);
                    }
                }

                _colorSyncedColorProperty = new SerializedObject(graphicGroup.ToArray()).FindProperty("m_Color");

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

            // Find most common font
            var fontTextGroup = graphics.OfType<TMP_Text>().GroupBy(x => x.font).OrderByDescending(x => x.Count()).FirstOrDefault();
            if (fontTextGroup != null)
            {
                _fontProperty = new SerializedObject(fontTextGroup.ToArray()).FindProperty("m_fontAsset");
            }
            else
            {
                _fontProperty = null;
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

        void ResetProperties()
        {
            _colorSyncedColorProperty = null;
            _rgbSyncedColorProperty = null;
            _textColorProperty = null;
            _fontProperty = null;
        }
    }
}

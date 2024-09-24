
using System.Reflection;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace Iwashi.UI
{
    static class UIObjectMenu
    {
        const string COMMON_MENU_PATH = "GameObject/UI (184)/";
        const int BASE_PRIORITY = 10;

        readonly static object[] s_Parameters = new object[] { default, default };
        readonly static MethodInfo s_PlaceUIElementRoot = typeof(TMPro_CreateObjectMenu).GetMethod("PlaceUIElementRoot", BindingFlags.Static | BindingFlags.NonPublic);

        [MenuItem(COMMON_MENU_PATH + "Panel", false, BASE_PRIORITY)]
        static void CreatePanel(MenuCommand menuCommand)
        {
            var go = PlaceUIElementRoot(PrefabReference.Instance.Panel, menuCommand);

            var rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;
        }

        [MenuItem(COMMON_MENU_PATH + "Button", false, BASE_PRIORITY + 1)]
        static void CreateButton(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.Button, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Slider", false, BASE_PRIORITY + 2)]
        static void CreateSlider(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.Slider, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Dropdown", false, BASE_PRIORITY + 3)]
        static void CreateDropDown(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.Dropdown, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Input Field", false, BASE_PRIORITY + 4)]
        static void CreateInputField(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.InputField, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "URL Input Field", false, BASE_PRIORITY + 5)]
        static void CreateURLInputField(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.URLInputField, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Toggle", false, BASE_PRIORITY + 6)]
        static void CreateToggle(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.Toggle, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Toggle Group", false, BASE_PRIORITY + 7)]
        static void CreateToggleGroup(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.ToggleGroup, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Scroll View", false, BASE_PRIORITY + 8)]
        static void CreateScrollView(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.ScrollView, menuCommand);
        }

        [MenuItem(COMMON_MENU_PATH + "Tab Menu", false, BASE_PRIORITY + 9)]
        static void CreateTabMenu(MenuCommand menuCommand)
        {
            PlaceUIElementRoot(PrefabReference.Instance.TabMenu, menuCommand);
        }

        static GameObject PlaceUIElementRoot(GameObject source, MenuCommand menuCommand)
        {
            var element = Object.Instantiate(source);
            element.name = source.name;
            Undo.RegisterCreatedObjectUndo(element, string.Empty);
            s_Parameters[0] = element;
            s_Parameters[1] = menuCommand;
            s_PlaceUIElementRoot.Invoke(default, s_Parameters);
            return element;
        }

    }
}

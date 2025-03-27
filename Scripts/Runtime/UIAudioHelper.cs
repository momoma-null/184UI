
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iwashi.UI
{
    [AddComponentMenu("UI (184)/Audio Helper", 12)]
    sealed class UIAudioHelper : MonoBehaviour, IPreprocessBehaviour
    {
        [SerializeField]
        EventTrigger buttonEvent;
        [SerializeField]
        EventTrigger selectableEvent;

        void IPreprocessBehaviour.Process()
        {
            var sourceButtonEvent = default(EventTrigger);
            var sourceSelectableEvent = default(EventTrigger);
            var selectables = GetComponentsInChildren<Selectable>(true);
            foreach (var selectable in selectables)
            {
                if (selectable is Button or Toggle)
                {
                    var eventTrigger = selectable.gameObject.AddComponent<EventTrigger>();
                    if (sourceButtonEvent == default)
                    {
                        sourceButtonEvent = Instantiate(buttonEvent, transform);
                    }
                    EditorUtility.CopySerialized(sourceButtonEvent, eventTrigger);
                }
                else
                {
                    var eventTrigger = selectable.gameObject.AddComponent<EventTrigger>();
                    if (sourceSelectableEvent == default)
                    {
                        sourceSelectableEvent = Instantiate(selectableEvent, transform);
                    }
                    EditorUtility.CopySerialized(sourceSelectableEvent, eventTrigger);
                }
            }
            DestroyImmediate(sourceButtonEvent);
            DestroyImmediate(sourceSelectableEvent);
        }
    }
}
#endif 

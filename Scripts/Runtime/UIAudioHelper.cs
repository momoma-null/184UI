
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

        void IPreprocessBehaviour.Process()
        {
            var sourceEvent = Instantiate(buttonEvent, transform);
            var selectables = GetComponentsInChildren<Selectable>(true);
            foreach (var selectable in selectables)
            {
                if (selectable is Button or Toggle)
                {
                    var eventTrigger = selectable.gameObject.AddComponent<EventTrigger>();
                    EditorUtility.CopySerialized(sourceEvent, eventTrigger);
                }
            }
            DestroyImmediate(sourceEvent);
        }
    }
}
#endif 

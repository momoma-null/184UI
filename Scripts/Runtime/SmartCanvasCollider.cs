
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iwashi.UI
{
    [AddComponentMenu("UI (184)/Smart Canvas Collider", 10)]
    sealed class SmartCanvasCollider : MonoBehaviour, IPreprocessBehaviour
    {
        void IPreprocessBehaviour.Process()
        {
            if (!TryGetComponent<Canvas>(out var rootCanvas))
                return;
            if (!rootCanvas.TryGetComponent<BoxCollider>(out var collider))
            {
                collider = gameObject.AddComponent<BoxCollider>();
            }
            collider.enabled = false;
            AddColliderRecursively(transform);
        }

        void AddColliderRecursively(Transform parent)
        {
            if (parent is not RectTransform rectTransform)
                return;

            if (parent.TryGetComponent<IEventSystemHandler>(out var _))
            {
                var rect = rectTransform.rect;
                var center = rectTransform.TransformPoint(rect.center);
                center = transform.InverseTransformPoint(center);
                var newCollider = gameObject.AddComponent<BoxCollider>();
                newCollider.size = rect.size;
                newCollider.center = center;
                newCollider.isTrigger = true;
            }

            if (parent.TryGetComponent<ScrollRect>(out var _))
            {
                return;
            }

            foreach (Transform child in parent)
            {
                AddColliderRecursively(child);
            }
        }
    }
}

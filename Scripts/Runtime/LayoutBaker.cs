
using UnityEngine;
using UnityEngine.UI;

namespace Iwashi.UI
{
    sealed class LayoutBaker : MonoBehaviour, IPreprocessBehaviour
    {
        void IPreprocessBehaviour.Process()
        {
            if (TryGetComponent<ContentSizeFitter>(out var contentSizeFitter))
            {
                DestroyImmediate(contentSizeFitter);
            }
            if (TryGetComponent<LayoutGroup>(out var layoutGroup))
            {
                DestroyImmediate(layoutGroup);
            }
        }
    }
}

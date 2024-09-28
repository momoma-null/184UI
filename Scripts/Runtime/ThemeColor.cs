
using System;
using UnityEngine;

namespace Iwashi.UI
{
    [AddComponentMenu("")]
    sealed class ThemeColor : MonoBehaviour, IPreprocessBehaviour
    {
        [SerializeField]
        Color graphicColor = new Color(0.0736f, 0.861222f, 0.92f, 0.92f);
        [SerializeField]
        Color fontColor = new Color(0.92f, 0.92f, 0.92f, 1f);

        public Color GraphicColor => graphicColor;
        public Color FontColor => fontColor;
        public static event Action<ThemeColor> OnChangeColor;

        void OnValidate()
        {
            OnChangeColor?.Invoke(this);
        }

        void IPreprocessBehaviour.Process() { }
    }
}

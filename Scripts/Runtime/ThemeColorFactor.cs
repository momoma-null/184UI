
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Iwashi.UI
{
    [ExecuteAlways]
    [DefaultExecutionOrder(18)]
    [RequireComponent(typeof(Graphic))]
    sealed class ThemeColorFactor : MonoBehaviour, IPreprocessBehaviour
    {
        [SerializeField]
        ThemeColor themeColor;
        [SerializeField]
        Graphic graphic;

        void Reset()
        {
            graphic = GetComponent<Graphic>();
            themeColor = GetComponentInParent<ThemeColor>();
        }

        void OnValidate()
        {
            ThemeColor.OnChangeColor -= UpdateColor;
            ThemeColor.OnChangeColor += UpdateColor;
        }

        void UpdateColor(ThemeColor currentThemeColor)
        {
            if (currentThemeColor != themeColor)
                return;
            if (graphic is TextMeshProUGUI or Text)
            {
                graphic.color = currentThemeColor.FontColor;
            }
            else
            {
                graphic.color = currentThemeColor.GraphicColor;
            }
        }

        void IPreprocessBehaviour.Process() { }
    }
}

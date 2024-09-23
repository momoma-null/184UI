
using UnityEngine;

namespace Iwashi.UI
{
    public sealed class PrefabReference : ScriptableObject
    {
        [SerializeField]
        GameObject panel;
        [SerializeField]
        GameObject button;
        [SerializeField]
        GameObject slider;
        [SerializeField]
        GameObject dropDown;
        [SerializeField]
        GameObject inputField;
        [SerializeField]
        GameObject urlInputField;
        [SerializeField]
        GameObject toggle;
        [SerializeField]
        GameObject toggleGroup;
        [SerializeField]
        GameObject scrollView;

        public GameObject Panel => panel;
        public GameObject Button => button;
        public GameObject Slider => slider;
        public GameObject DropDown => dropDown;
        public GameObject InputField => inputField;
        public GameObject URLInputField => urlInputField;
        public GameObject Toggle => toggle;
        public GameObject ToggleGroup => toggleGroup;
        public GameObject ScrollView => scrollView;

        public static PrefabReference Instance { get; private set; }

        void OnValidate()
        {
            Instance = this;
        }
    }
}

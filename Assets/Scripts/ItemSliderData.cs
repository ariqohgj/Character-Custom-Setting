using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Cinda.AlterLife
{
    public class ItemSliderData : MonoBehaviour
    {
        [Header("SLIDER TYPE")]
        public SliderType sliderType;
        public enum SliderType
        {
            Upper,
            Lower,
            None
        }

        public int id_data_one;
        public float min;
        public float max;
        public TextMeshProUGUI nameText;
        [SerializeField] public Slider slider;
        public TMP_InputField inputValue;
        bool TextInChange = false;
        bool SliderInChange = false;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ChangeValueByTextInput()
        {
            SettingsManager.instance.skinnedMesh.SetBlendShapeWeight(id_data_one, int.Parse(inputValue.text));
            slider.value = float.Parse(inputValue.text) / 100;
        }
        public void ChangeValueBySlider(float value)
        {
            bool leg = (id_data_one == 17 || id_data_one == 15) ? true : false;
            float selisih = max - min;
            value = (leg) ? ((selisih - (value*100))/100) : value;
            //print("Masuk cuy value by slider");
            SettingsManager.instance.skinnedMesh.SetBlendShapeWeight(id_data_one, value * 100);
            SettingsManager.instance.skinnedMeshBottom.SetBlendShapeWeight(id_data_one, value * 100);
            SettingsManager.instance.skinnedMeshTop.SetBlendShapeWeight(id_data_one, value * 100);
            float valuebaru = value;
            SettingsManager.instance.isRotating = false;
            //inputValue.text = valuebaru.ToString("0.0");
        }

        public void ChangeValueBySliderSulit(float value)
        {
            bool leg = (id_data_one == 16 || id_data_one == 14) ? true : false;
            float selisih = max - min;
            value = (leg) ? (selisih - value) : value;

            SettingsManager.instance.skinnedMesh.SetBlendShapeWeight(id_data_one, value * 100);
            SettingsManager.instance.skinnedMeshBottom.SetBlendShapeWeight(id_data_one, value * 100);
            SettingsManager.instance.skinnedMeshTop.SetBlendShapeWeight(id_data_one, value * 100);
            float valuebaru = value;
            SettingsManager.instance.isRotating = false;
            //inputValue.text = valuebaru.ToString("0.0");
        }
    }
}

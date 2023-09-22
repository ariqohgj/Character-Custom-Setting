using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemSliderData : MonoBehaviour
{
    [Header("SLIDER TYPE")]
    public SliderType sliderType;
    public enum SliderType
    {
        SINGLE,
        DUO,
        TRIPLE
    }

    public int id_data_one;
    public int id_data_two;
    public int id_data_three;
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
        SettingsManager.instance.skinnedMesh.SetBlendShapeWeight(id_data_one, value *100);
        float valuebaru = value * 100;
        inputValue.text = valuebaru.ToString("0");
    }


}

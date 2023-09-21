using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSliderData : MonoBehaviour
{
    public int id_data;
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
        SettingsManager.instance.skinnedMesh.SetBlendShapeWeight(id_data, int.Parse(inputValue.text));
        slider.value = float.Parse(inputValue.text) / 100;
    }
    public void ChangeValueBySlider(float value)
    {
        SettingsManager.instance.skinnedMesh.SetBlendShapeWeight(id_data, value *100);
        float valuebaru = value * 100;
        inputValue.text = valuebaru.ToString("0");
    }


}

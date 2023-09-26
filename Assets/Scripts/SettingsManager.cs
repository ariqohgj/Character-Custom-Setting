using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cinemachine;

namespace Cinda.AlterLife
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager instance;

        public SkinnedMeshRenderer skinnedMesh;
        public ItemSliderData itemSliderData;
        public RectTransform parent;

        public float rotationSpeed = 2.0f;
        private Vector3 lastMousePosition;
        public GameObject parentSkin;
        private bool isRotating = false;
        private List<ItemSliderData> listData = new List<ItemSliderData>();
        // Start is called before the first frame update

        [Header("CAMERA WORKS")]
        public CinemachineVirtualCamera CAM_1;
        public CinemachineVirtualCamera CAM_2;
        public CinemachineVirtualCamera CAM_3;

        #region ENUM
        public enum bodyType
        {
            S,
            N,
            F,
            O
        }
        public enum category
        {

            UPPER_BODY,
            LOWER_BODY,
            NONE
        }
        #endregion

        public bodyType body;

        [System.Serializable]
        public class minmaxData
        {
            [Header("CATEGORY")]
            public category CATEGORY_SETTING;

            [Header("DATA")]
            public string shpekey;
            public int id;
            public float min;
            public float max;
            public bool shapeSulit = false;
            public List<BarrierData> barrierDatas = new List<BarrierData>();
        }

        [System.Serializable]
        public class BarrierData
        {
            public string bodyName;
            public float batasAtas;
            public float batasBawah;
            public float normalValue;

        }

        public List<minmaxData> DataMinAndMax;

        private void Awake()
        {
            // Pastikan hanya ada satu instansi yang bertahan
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        void Start()
        {
            getBlendShapes();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == parentSkin.gameObject)
                    {
                        isRotating = true;
                        lastMousePosition = Input.mousePosition;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isRotating = false;
            }
            // Memeriksa jika tombol kiri mouse ditekan
            if (isRotating)
            {

                // Menghitung perubahan posisi mouse
                Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

                // Menghitung rotasi objek hanya pada sumbu X (atas-bawah) dan Y (kiri-kanan)
                float rotationX = -mouseDelta.y * rotationSpeed;
                float rotationY = -mouseDelta.x * rotationSpeed;

                // Mengunci rotasi sumbu Z ke 0
                Vector3 currentRotation = parentSkin.transform.localEulerAngles;
                currentRotation.z = 0;

                // Mengubah rotasi objek
                parentSkin.transform.localEulerAngles = currentRotation;
                parentSkin.transform.Rotate(Vector3.up, rotationY, Space.World);
                //skinnedMesh.transform.Rotate(Vector3.right, rotationX, Space.World);
            }

            // Menyimpan posisi mouse saat ini untuk perbandingan dengan frame berikutnya
            lastMousePosition = Input.mousePosition;
        }

        public void getBlendShapes()
        {
            if (skinnedMesh.sharedMesh.blendShapeCount > 0)
            {
                for (int i = 0; i < skinnedMesh.sharedMesh.blendShapeCount; i++)
                {
                    ItemSliderData data = Instantiate(itemSliderData, parent);
                    data.id_data_one = skinnedMesh.sharedMesh.GetBlendShapeIndex(skinnedMesh.sharedMesh.GetBlendShapeName(i));
                    data.nameText.text = skinnedMesh.sharedMesh.GetBlendShapeName(i);
                    data.slider.value = skinnedMesh.GetBlendShapeWeight(i);
                    //data.inputValue.text = skinnedMesh.GetBlendShapeWeight(i).ToString();
                    data.slider.onValueChanged.AddListener(data.ChangeValueBySlider);
                    data.gameObject.SetActive(true);
                    data.sliderType = (ItemSliderData.SliderType)DataMinAndMax[i].CATEGORY_SETTING;

                    if (DataMinAndMax[i].barrierDatas.Count > 0)
                    {
                        if (DataMinAndMax[i].shapeSulit)
                        {
                            data.slider.minValue = DataMinAndMax[i].barrierDatas[((int)body)].batasBawah / 100;
                            data.slider.maxValue = DataMinAndMax[i].barrierDatas[((int)body)].batasAtas / 100;
                            data.slider.value = DataMinAndMax[i].barrierDatas[((int)body)].normalValue / 100;
                            //data.slider.value = 30;
                        }
                        else
                        {
                            data.slider.minValue = DataMinAndMax[i].barrierDatas[((int)body)].batasBawah;
                            data.slider.maxValue = DataMinAndMax[i].barrierDatas[((int)body)].batasAtas;
                            data.slider.value = DataMinAndMax[i].barrierDatas[((int)body)].normalValue;
                        }
                    }
                    else
                    {
                        if (DataMinAndMax[i].shapeSulit)
                        {
                            data.slider.minValue = DataMinAndMax[i].min / 100;
                            data.slider.maxValue = DataMinAndMax[i].max / 100;
                            //data.slider.value = 30;
                        }
                        else
                        {
                            data.slider.minValue = DataMinAndMax[i].min;
                            data.slider.maxValue = DataMinAndMax[i].max;
                        }
                    }

                    listData.Add(data);
                }
            }
        }

        public void Setting_Upper()
        {
            for(int i = 0; i < listData.Count; i++)
            {
                listData[i].gameObject.SetActive(true);
                if (listData[i].sliderType != ItemSliderData.SliderType.Upper)
                {
                    listData[i].gameObject.SetActive(false);
                }
            }
            CAM_1.Priority = 10;
            CAM_2.Priority = 11;
            CAM_3.Priority = 10;
        }

        public void Setting_Lower()
        {
            for (int i = 0; i < listData.Count; i++)
            {
                listData[i].gameObject.SetActive(true);
                if (listData[i].sliderType != ItemSliderData.SliderType.Lower)
                {
                    listData[i].gameObject.SetActive(false);
                }

            }
            CAM_1.Priority = 10;
            CAM_2.Priority = 10;
            CAM_3.Priority = 11;

        }

        public void Setting_None()
        {
            for (int i = 0; i < listData.Count; i++)
            {
                listData[i].gameObject.SetActive(true);
                if (listData[i].sliderType != ItemSliderData.SliderType.None)
                {
                    listData[i].gameObject.SetActive(false);
                }
            }

            CAM_1.Priority = 11;
            CAM_2.Priority = 10;
            CAM_3.Priority = 10;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;
using static Opsive.UltimateCharacterController.Character.CharacterFootEffects;

namespace Cinda.AlterLife
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager instance;

        public SkinnedMeshRenderer skinnedMesh;
        public SkinnedMeshRenderer skinnedMeshTop;
        public SkinnedMeshRenderer skinnedMeshBottom;


        public ItemSliderData itemSliderData;
        public RectTransform parent;

        public float rotationSpeed = 500f;
        private Vector3 lastMousePosition;
        public GameObject parentSkin;
        public bool isRotating = false;
        private List<ItemSliderData> listData = new List<ItemSliderData>();
        public EventSystem ehe;

        private Quaternion FrontRotation = Quaternion.Euler(0, 180, 0);
        private Quaternion BackRotation = Quaternion.Euler(0, 70, 0);

        // Start is called before the first frame update

        [Header("CAMERA WORKS")]
        public CinemachineVirtualCamera CAM_1;
        public CinemachineVirtualCamera CAM_2;
        public CinemachineVirtualCamera CAM_3;
        public CinemachineVirtualCamera CAM_4;

        [Header("SCALER")]
        public GameObject scalerObj;

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

        public enum TinggiType
        {
            T0,
            T1,
            T2,
            T3
        }
        #endregion

        public bodyType body;
        public TinggiType tinggiType;

        [System.Serializable]
        public class minmaxData
        {
            [Header("CATEGORY")]
            public category Category;
            [Header("DATA")]
            public string shpekey;
            public int id;
            public float min;
            public float max;
            public bool shapeSulit = false;
            public bool withCloth = false;
            public bool notInclude = false;
            public List<float> defaultValue = new();
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
                bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                if (Physics.Raycast(ray, out hit) && !isOverUI)
                {
                    if (hit.collider.gameObject == parentSkin.gameObject)
                    {
                        isRotating = true;
                        if (routineRotate != null)
                            StopCoroutine(routineRotate);
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
                    data.nameText.text = DataMinAndMax[i].shpekey;
                    data.min = DataMinAndMax[i].min;
                    data.max = DataMinAndMax[i].max;

                    //data.slider.value = DataMinAndMax[i].defaultValue;
                    //data.inputValue.text = skinnedMesh.GetBlendShapeWeight(i).ToString();
                    data.slider.onValueChanged.AddListener(data.ChangeValueBySlider);
                    data.gameObject.SetActive(true);
                    data.sliderType = (ItemSliderData.SliderType)DataMinAndMax[i].Category;

                    if (i == 1 || i == 2 || i == 9)
                    {
                        data.gameObject.SetActive(false);
                        continue;
                    }

                    //setting default value
                    if (DataMinAndMax[i].defaultValue.Count > 0)
                    {
                        switch (tinggiType)
                        {
                            case TinggiType.T0:
                                data.slider.value = DataMinAndMax[i].defaultValue[0];
                                break;
                            case TinggiType.T1:
                                data.slider.value = DataMinAndMax[i].defaultValue[1];
                                break;
                            case TinggiType.T2:
                                data.slider.value = DataMinAndMax[i].defaultValue[2];
                                break;
                            case TinggiType.T3:
                                data.slider.value = DataMinAndMax[i].defaultValue[3];
                                break;
                        }
                    }
                    else
                    {

                        data.slider.value = skinnedMesh.GetBlendShapeWeight(i);
                    }

                    // filtering yang punya batas atas dan batas bawah
                    if (DataMinAndMax[i].barrierDatas.Count > 0)
                    {
                        // Filtering untuk kaki
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
                            data.slider.value = DataMinAndMax[i].max / 100;
                        }
                        else
                        {
                            data.slider.minValue = DataMinAndMax[i].min;
                            data.slider.maxValue = DataMinAndMax[i].max;
                        }
                    }

                    if (DataMinAndMax[i].notInclude)
                    {
                        data.gameObject.SetActive(false);

                    }

                    listData.Add(data);
                    Setting_Normal();
                }
            }
        }

        public void Setting_Upper()
        {

            StartRotate(true);
        }

        public void Setting_Lower()
        {

            StartRotate(true);
        }

        public void Setting_None()
        {

            StartRotate(false);
        }

        public void Setting_Normal()
        {
            //parentSkin.transform.rotation = Quaternion.Euler(0, 180, 0);
            StartRotate(true);
        }

        Coroutine routineRotate;
        void StartRotate(bool front)
        {
            if (routineRotate != null)
                StopCoroutine(routineRotate);

            routineRotate = StartCoroutine(FrontSide(front));
        }
        IEnumerator FrontSide(bool front)
        {
            float step = rotationSpeed;

            float targetRotationY = (front) ? FrontRotation.y : BackRotation.y;
            while (parentSkin.transform.localRotation.y != targetRotationY)
            {

                parentSkin.transform.localRotation = Quaternion.RotateTowards(parentSkin.transform.rotation, (front) ? FrontRotation : BackRotation, step);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            parentSkin.transform.localRotation = (front) ? FrontRotation : BackRotation;
        }

        public void HideUnhideScaler()
        {
            if (scalerObj.activeInHierarchy)
            {
                scalerObj.SetActive(false);
                //isRotating = true;
            }
            else
            {
                scalerObj.SetActive(true);
                //isRotating = false;
            }
        }

        public void exit()
        {
            Application.Quit();
        }
    }
}

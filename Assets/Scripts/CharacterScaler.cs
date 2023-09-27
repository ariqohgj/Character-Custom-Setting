using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Cinda.AlterLife
{
    public class CharacterScaler : MonoBehaviour
    {
        const string BaseName = "Base_";

        [Header("Scalebale Renderer")]
        [SerializeField] private SkinnedMeshRenderer skinMeshRenderer;


		[Header("Input")]

        [Range(0.5f, 1.25f)]
		public float thighInput = 1f;
		[Range(0.5f, 1.25f)]
		public float calfInput = 1f;

		[Header("Scalable Length Transform")]
        [SerializeField] private BoneData boneData;
        [SerializeField] private Transform root;
        [SerializeField] private Transform chest;
        [SerializeField] private Transform pelvis;
        [SerializeField] private Transform hip;
        [SerializeField] private Transform[] ArmsUpper = new Transform[2];
        [SerializeField] private Transform[] ArmsLower = new Transform[2];
        [SerializeField] private Transform[] LegsUpper = new Transform[2];
        [SerializeField] private Transform[] LegsLower = new Transform[2];
        [SerializeField] private Transform[] Foots = new Transform[2];
        [SerializeField] private GameObject footBase;

        public SkinnedMeshRenderer SkinMesh => skinMeshRenderer;
        public Transform Pelvis => pelvis;
        private async void Awake()
        {
            await Initialize();
        }

		private void LateUpdate()
		{
            ScaleLegArmature();
		}

		[ContextMenu("Init")]
        public async void Init() => await Initialize();

        [ContextMenu("Scale")]
        public void ScaleLegArmature()
        {
			thighInput = thighInput <= 0 ? 0.01f : thighInput;
			calfInput = calfInput <= 0 ? 0.01f : calfInput;

			foreach (var item in LegsUpper)
			{
				item.localScale = new Vector3(1, thighInput, 1);
			}
			foreach (var item in LegsLower)
			{
				item.localScale = new Vector3(1, 1f / thighInput * calfInput, 1);
			}

			root.localScale = new Vector3(1, 1, root.localScale.z * (hip.localPosition.z - (footBase.transform.position.y - root.transform.position.y)) / hip.localPosition.z);
			hip.localScale = new Vector3(1, (1f / root.localScale.z), 1);
		}

        #region Task / Awaitable
		public async Awaitable Initialize()
        {
            await InitiateData();
            await InitializeBone();
        }
        private async Awaitable InitiateData()
        {
            var x = Resources.LoadAsync<BoneData>("BaseBoneData");
            while (!x.isDone)
            {
                Debug.Log(x.progress);
                await Awaitable.WaitForSecondsAsync(0);
            }
            boneData = (BoneData)x.asset;
        }
        private async Awaitable InitializeBone()
        {
            try
            {
                skinMeshRenderer = transform.Find(boneData.BaseBody).GetComponent<SkinnedMeshRenderer>();

                root = transform.Find(boneData.Root);
                chest = transform.Find(boneData.Waist);
                pelvis = transform.Find(boneData.Pelvis);
                hip =  transform.Find(boneData.Hip);

                ArmsUpper[0] = transform.Find(boneData.RightUpperArm);
                ArmsUpper[1] = transform.Find(boneData.LeftUpperArm);

                ArmsLower[0] = transform.Find(boneData.RightLowerArm);
                ArmsLower[1] = transform.Find(boneData.LeftLowerArm);

                LegsUpper[0] = transform.Find(boneData.RightUpperLeg);
                LegsUpper[1] = transform.Find(boneData.LeftUpperLeg);

                LegsLower[0] = transform.Find(boneData.RightLowerLeg);
                LegsLower[1] = transform.Find(boneData.LeftLowerLeg);

                Foots[0] = transform.Find(boneData.RightFoot);
                Foots[1] = transform.Find(boneData.LeftFoot);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

            if (footBase ==  null)
            footBase = new("footBase");

            footBase.transform.localPosition = new Vector3(Foots[0].position.x, Foots[0].position.y, Foots[0].position.z);
            footBase.transform.SetParent(hip);
            //Scale();
            await Awaitable.WaitForSecondsAsync(0);
        }
        #endregion

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
            if (footBase != null)
            Gizmos.DrawSphere(footBase.transform.position, 0.01f);
		}

		private void OnValidate()
		{
			if (EditorApplication.isPlaying) return;
            ScaleLegArmature();
		}
#endif
	}
}
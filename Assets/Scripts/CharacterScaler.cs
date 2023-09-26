using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterScaler : MonoBehaviour
{
    const string BaseName = "Base_";

    [Header("Scalebale Renderer")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

	[Header("Scalable Length Transform")]
	[SerializeField] private BoneData boneData;
    [SerializeField] private Transform chest;
	[SerializeField] private Transform[] ArmUpper = new Transform[2]; 
    [SerializeField] private Transform[] ArmLower = new Transform[2];
	[SerializeField] private Transform[] LegUpper = new Transform[2];
	[SerializeField] private Transform[] LegLower = new Transform[2];

	public SkinnedMeshRenderer MeshRenderer => meshRenderer;

	private async void Awake()
	{
        await InitiateData();
        await InitializeBone();
	}



	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async Awaitable InitiateData()
    {
        Debug.Log("ADASDSAD");
        var x = Resources.LoadAsync<BoneData>("BaseBoneData");
        while (!x.isDone)
        {
            Debug.Log(x.progress);
            await Awaitable.WaitForSecondsAsync(0);
        }
        boneData = (BoneData) x.asset;
	}

    private async Awaitable InitializeBone()
    {
        try
        {
            meshRenderer = transform.Find(boneData.BaseBody).GetComponent<SkinnedMeshRenderer>();

            chest = transform.Find( boneData.Waist).GetComponent<Transform>();

			ArmUpper[0] = transform.Find(boneData.RightUpperArm).GetComponent<Transform>();
			ArmUpper[1] = transform.Find(boneData.LeftUpperArm).GetComponent<Transform>();

            ArmLower[0] = transform.Find(boneData.RightLowerArm);
            ArmLower[1] = transform.Find(boneData.LeftLowerArm);

            LegUpper[0] = transform.Find(boneData.RightUpperLeg);
            LegUpper[1] = transform.Find(boneData.LeftUpperLeg);

            LegLower[0] = transform.Find(boneData.RightLowerLeg);
            LegLower[1] = transform.Find(boneData.LeftLowerLeg);
		}
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        

        await Awaitable.WaitForSecondsAsync(0);
	}
}

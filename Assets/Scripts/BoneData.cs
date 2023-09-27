using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Bone Data", menuName = "Mesh/Bone Data")]
public class BoneData : ScriptableObject
{
	[Header("Base Body")]
	[SerializeField] private string baseBodyName = "Base_Body";

	[Header("Armature Base")]
	[SerializeField] private string armatureBase = "Human";
	[SerializeField] private string armatureRoot = "Base_BoneRoot";
	[SerializeField] private string armatureHip = "Base_Hip";

	[Header("Armature Upper Body")]
	[SerializeField] private string armatureWaist = "Base_Waist";
	[SerializeField] private string armatureSpine01 = "Base_Spine01";
	[SerializeField] private string armatureSpine02 = "Base_Spine02";
	[SerializeField] private string armatureRightClavicle = "Base_R_Clavicle"; //Shoulder Right
	[SerializeField] private string armatureLeftClavicle = "Base_L_Clavicle"; //Shoulder Left

	[Header("Armature Hand")]
	[SerializeField] private string armatureRightUpperArm = "Base_R_Upperarm";
	[SerializeField] private string armatureLeftUpperArm = "Base_L_Upperarm";
	[SerializeField] private string armatureRightLowerArm = "Base_R_Forearm";
	[SerializeField] private string armatureLeftLowerArm = "Base_L_Forearm";

	[Header("Armature Lower Body")]
	[SerializeField] private string armaturePelvis = "Base_Pelvis";
	[SerializeField] private string armatureRightUpperLeg = "Base_R_Thigh";
	[SerializeField] private string armatureLeftUpperLeg = "Base_L_Thigh";
	[SerializeField] private string armatureRightLowerLeg = "Base_R_Calf";
	[SerializeField] private string armatureLeftLowerLeg = "Base_L_Calf";


	[Header("Foot")]
	[SerializeField] private string armatureRightFoot = "Base_R_Foot";
	[SerializeField] private string armatureLeftFoot = "Base_L_Foot";
	/// Get Base Body Name
	/// </summary>
	public string BaseBody
		=> baseBodyName;

	public string Root
		=> Path.Combine(armatureBase, armatureRoot)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	public string Hip
	=> Path.Combine(armatureBase, armatureRoot, armatureHip)
	.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);


	
	public string Pelvis
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis)
	.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	/// <summary>
	/// Get Waist / Chest
	/// </summary>
	public string Waist
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armatureWaist)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	/// <summary>
	/// Get Right Upper Arm
	/// </summary>
	public string RightUpperArm
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armatureWaist, armatureSpine01, armatureSpine02, armatureRightClavicle, armatureRightUpperArm)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
	
	/// <summary>
	/// Get Left Upper Arm
	/// </summary>
	public string LeftUpperArm
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armatureWaist, armatureSpine01, armatureSpine02, armatureLeftClavicle, armatureLeftUpperArm)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
	
	/// <summary>
	/// Get Right Lower Arm
	/// </summary>
	public string RightLowerArm
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armatureWaist, armatureSpine01, armatureSpine02, armatureRightClavicle, armatureRightUpperArm, armatureRightLowerArm)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
	
	/// <summary>
	/// Get Left Lower Arm
	/// </summary>
	public string LeftLowerArm
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armatureWaist, armatureSpine01, armatureSpine02, armatureLeftClavicle, armatureLeftUpperArm, armatureLeftLowerArm)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	/// <summary>
	/// Get Right Upper Leg / Thigh
	/// </summary>
	public string RightUpperLeg
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureRightUpperLeg)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	/// <summary>
	/// Get Left Upper Leg / Thigh
	/// </summary>
	public string LeftUpperLeg
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureLeftUpperLeg)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	/// <summary>
	/// Get Right Lower Leg / Thigh
	/// </summary>
	public string RightLowerLeg
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureRightUpperLeg, armatureRightLowerLeg)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	/// <summary>
	/// Get Left Upper Leg / Thigh
	/// </summary>
	public string LeftLowerLeg
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureLeftUpperLeg, armatureLeftLowerLeg)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	public string RightFoot
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureRightUpperLeg, armatureRightLowerLeg, armatureRightFoot)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

	public string LeftFoot
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureLeftUpperLeg, armatureLeftLowerLeg, armatureLeftFoot)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

}
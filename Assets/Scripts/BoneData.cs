using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Bone Data", menuName = "Mesh/Bone Data")]
public class BoneData : ScriptableObject
{
	[Header("Base Body")]
	[SerializeField] private string baseBodyName;

	[Header("Armature Base")]
	[SerializeField] private string armatureBase;
	[SerializeField] private string armatureRoot;
	[SerializeField] private string armatureHip;

	[Header("Armature Upper Body")]
	[SerializeField] private string armatureWaist;
	[SerializeField] private string armatureSpine01;
	[SerializeField] private string armatureSpine02;
	[SerializeField] private string armatureRightClavicle; //Shoulder Right
	[SerializeField] private string armatureLeftClavicle; //Shoulder Left

	[Header("Armature Hand")]
	[SerializeField] private string armatureRightUpperArm;
	[SerializeField] private string armatureLeftUpperArm;
	[SerializeField] private string armatureRightLowerArm;
	[SerializeField] private string armatureLeftLowerArm;

	[Header("Armature Lower Body")]
	[SerializeField] private string armaturePelvis;
	[SerializeField] private string armatureRightUpperLeg;
	[SerializeField] private string armatureLeftUpperLeg;
	[SerializeField] private string armatureRightLowerLeg;
	[SerializeField] private string armatureLeftLowerLeg;

	/// <summary>
	/// Get Base Body Name
	/// </summary>
	public string BaseBody
		=> baseBodyName;

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
		=> Path.Combine(armatureBase, armatureRoot, armatureHip, armaturePelvis, armatureLeftUpperLeg, armatureRightLowerLeg)
		.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
}
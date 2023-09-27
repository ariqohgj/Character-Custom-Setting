using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinda.AlterLife.Character {
	public class BaseScalerData : ScriptableObject
	{
		//[SerializeField] BodyType bodyType;

		[Header("Data")]
		

		[Header("Barrier")]
		[SerializeField] private string bodyName;
	}

	public struct BodyPartData
	{
		[SerializeField] private string shapekey;
		[SerializeField] private int id;
		[SerializeField] private float min;
		[SerializeField] private float max;
		[SerializeField] private bool shapeSulit;
	}
}
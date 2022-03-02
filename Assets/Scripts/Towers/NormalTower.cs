using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower
{
   
	private void Start()
	{
		ElementType = Element.NONE;
	}
	public override Debuff GetDebuff()
	{
		return null;
	}
}

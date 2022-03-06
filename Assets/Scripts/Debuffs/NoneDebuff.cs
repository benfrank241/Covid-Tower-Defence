using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneDebuff : Debuff
{

	private float timeSinceTick;

	private float tickTime;



	public NoneDebuff(Monster target) : base(target, 1)
	{


	}
}

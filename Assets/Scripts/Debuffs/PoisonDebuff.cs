using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
	
	private float	timeSinceTick;

	private float tickTime;



    public PoisonDebuff(Monster target): base(target,1)
	{


	}
}

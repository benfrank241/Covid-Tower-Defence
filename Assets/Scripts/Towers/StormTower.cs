using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Syringe

public class StormTower : Tower
{
    
	private void Start()
	{
		ElementType = Element.STORM;

		Upgrades = new TowerUpgrade[]
		{
			new TowerUpgrade (7, 6, 2, 10),
			new TowerUpgrade (15, 10, 1, 10),
		};
	}

	public override Debuff GetDebuff()
	{
		return new StormDebuff(Target,DebuffDuration);
	}

	public override string GetStats()
    {
		return string.Format("<color=#00ffffff>{0}</color>{1}", "<Size=30><b>Sanitizer</b></Size>", base.GetStats());
    }
}

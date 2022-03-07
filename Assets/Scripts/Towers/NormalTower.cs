using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower
{

	private void Start()
	{
		ElementType = Element.NONE;

		Upgrades = new TowerUpgrade[]
		{
			new TowerUpgrade (14, 11),
			new TowerUpgrade (22, 20),
		};
	}

    public override Debuff GetDebuff()
    {
        return new NoneDebuff(Target);
    }

    public override string GetStats()
	{
		return string.Format("<color=#ffff00>{0}</color>{1}", "<Size=30><b>Vaccine</b></Size>", base.GetStats());
	}


}

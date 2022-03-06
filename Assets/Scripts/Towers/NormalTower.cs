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
			new TowerUpgrade (2, 2),
			new TowerUpgrade (5, 3),
		};
	}

    public override Debuff GetDebuff()
    {
        return new NoneDebuff(Target);
    }

    public override string GetStats()
	{
		return string.Format("<color=#00ff00ff>{0}</color>{1}", "<Size=30><b>Vaccine</b></Size>", base.GetStats());
	}


}

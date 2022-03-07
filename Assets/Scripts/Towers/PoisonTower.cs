using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower
{

	private void Start()
	{
		ElementType = Element.POISON;

		Upgrades = new TowerUpgrade[]
		{
			new TowerUpgrade (6, 1, 1),
			new TowerUpgrade (12, 2, 0),
		};
	}
	
	public override Debuff GetDebuff()
	{
		return new PoisonDebuff(Target);
	}

	public override string GetStats()
	{
		if (NextUpgrade != null) //If the next is avaliable
		{
			return string.Format("<color=#00ff00ff>{0}</color>{1}", "<size=20><b>Factory</b></size> ", base.GetStats());
		}
		return string.Format("<color=#00ff00ff>{0}</color>{1}", "<size=20><b>Factory</b></size> ", base.GetStats());
	}
}

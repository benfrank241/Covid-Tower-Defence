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
			new TowerUpgrade (2, 1, 1),
			new TowerUpgrade (5, 2, 1),
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
			return string.Format("<color=#ffa500ff>{0}</color>{1}", "<size=20><b>Factory</b></size> ", base.GetStats());
		}
		return string.Format("<color=#ffa500ff>{0}</color>{1}", "<size=20><b>Factory</b></size> ", base.GetStats());
	}
}

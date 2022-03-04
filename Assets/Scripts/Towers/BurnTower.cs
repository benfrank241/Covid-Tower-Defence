using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//meddoc

public class BurnTower : Tower
{

	[SerializeField]
	private float tickTime;

	[SerializeField]
	private float tickDamage;



	public float TickTime
	{
		get
		{
			return tickTime;
		}
	}


	public float TickDamage
	{
		get
		{
			return tickDamage;
		}
	}


	private void Start()
	{
		ElementType = Element.FIRE;

		Upgrades = new TowerUpgrade[]
		{
			new TowerUpgrade (2, 2, 0.5f, 5,-0.1f,1),
			new TowerUpgrade (5, 3, 0.5f, 5,-0.1f,1),
		};
	}

	public override Debuff GetDebuff()
	{
		return new FireDebuff(TickDamage, tickTime, DebuffDuration, Target);
	}

	public override string GetStats()
	{
		if (NextUpgrade != null) //If the next is avaliable
		{
			return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2} <color=#00ff00ff>{4}</color>\nTick damage: {3} <color=#00ff00ff>+{5}</color>", "<size=20><b>Meddoc</b></size> ", base.GetStats(), TickTime, TickDamage, NextUpgrade.TickTime, NextUpgrade.SpecialDamage);
		}
		return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2}\nTick damage: {3}", "<size=20><b>Meddoc</b></size> ", base.GetStats(), TickTime, TickDamage);
	}

	public override void Upgrade()
    {
		this.tickTime += NextUpgrade.TickTime;
		this.tickDamage += NextUpgrade.SpecialDamage;
		base.Upgrade();
    }
}


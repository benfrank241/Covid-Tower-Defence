using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//hospital
public class FreezeTower : Tower
{
	[SerializeField]   
	private float slowingFactor;

    public float SlowingFactor
    {
        get
        {
            return slowingFactor;
        }
    }

    private void Start()
	{
		ElementType = Element.FROST;

		Upgrades = new TowerUpgrade[]
		{
			new TowerUpgrade (9, 2, 1, 10, -15),
			new TowerUpgrade (18, 4, 2, 15, -30),
		};
	}

	public override Debuff GetDebuff()
	{
		return new FrostDebuff(SlowingFactor,DebuffDuration,Target);
	}

	public override string GetStats()
	{
		if (NextUpgrade != null)  //If the next is avaliable
		{
			return string.Format("<color=#ffa500ff>{0}</color>{1} \nSlowing factor: {2}% <color=#00ff00ff>{3}%</color>", "<size=20><b>Hospital</b></size>", base.GetStats(), SlowingFactor, NextUpgrade.SlowingFactor);
		}

		//Returns the current upgrade
		return string.Format("<color=#ffa500ff>{0}</color>{1} \nSlowing factor: {2}%", "<size=20><b>Hospital</b></size>", base.GetStats(), SlowingFactor);
	}

	public override void Upgrade()
	{
		this.slowingFactor += NextUpgrade.SlowingFactor;
		base.Upgrade();
	}
}

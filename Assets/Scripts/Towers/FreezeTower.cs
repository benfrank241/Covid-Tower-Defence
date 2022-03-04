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
			new TowerUpgrade (2, 1, 1, 2, 10),
			new TowerUpgrade (2, 1, 1, 2, 20),
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
			return string.Format("<color=#00ffffff>{0}</color>{1} \nSlowing factor: {2}% <color=#00ff00ff>+{3}</color>", "<size=20><b>Frost</b></size>", base.GetStats(), SlowingFactor, NextUpgrade.SlowingFactor);
		}

		//Returns the current upgrade
		return string.Format("<color=#00ffffff>{0}</color>{1} \nSlowing factor: {2}%", "<size=20><b>Frost</b></size>", base.GetStats(), SlowingFactor);
	}

	public override void Upgrade()
	{
		this.slowingFactor += NextUpgrade.SlowingFactor;
		base.Upgrade();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Element {STORM,FIRE,FROST,POISON,NONE}


public  abstract class Tower : MonoBehaviour
{
    
    public int Price { get; set; }
    
    // this is the projectiles type
    [SerializeField]
    private string projectileType;
    
    private bool canAttack = true; 

    private float  attackTimer;

    [SerializeField]
    private float attackCooldown;

    public TowerUpgrade[] Upgrades { get; protected set; }
    
    private SpriteRenderer mySpriteRenderer;
    
    
    // monster stuff
    private Monster target; 
    private Queue<Monster> monsters = new Queue<Monster>();

    public int Level { get; protected set; }



    //projectile speed
    [SerializeField]
    private float projectileSpeed;



    //damaging monster
    [SerializeField]
    private int damage;

    [SerializeField]
    private float debuffDuration;

    [SerializeField]
    private float proc;


    public float DebuffDuration
    {
       get
       {
           return debuffDuration;
       }

       set
       {
           this.debuffDuration = value;
       }

    }










    public Element ElementType {get; protected set;}






    public float ProjectileSpeed
    {
        get{return projectileSpeed;}
    }
    
    public Monster Target
    {
        get{ return target;}
    }

    public TowerUpgrade NextUpgrade
    {
        get
        {
            if (Upgrades.Length > Level - 1)
            {
                return Upgrades[Level - 1];
            }

            return null;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log(target);
    }

    public void Select()
    {   
        if(mySpriteRenderer == null)
        {
             mySpriteRenderer = GetComponent<SpriteRenderer>();
        }
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        GameManager.Instance.UpdateUpgradeTip();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            monsters.Enqueue(other.GetComponent<Monster>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            target = null;
        }
    }

    private void Attack()
    {
        
        if(!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }

        }
        
    
        if(target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }
        
        if(target != null && target.IsActive)
        {
            if (canAttack){
                Shoot();
                canAttack = false;
            }
            
        }
        
    }

    public virtual string GetStats()
    {
        if (NextUpgrade != null)
        {
            return string.Format("\nLevel: {0} \nDamage: {1} <color=#00ff00ff> +{4} </color> \nProc: {2}% <color=#00ff00ff>+{5}%</color> \nDebuff: {3}sec <color=#00ff00ff>+{6}</color>", Level, damage, proc, debuffDuration, NextUpgrade.Damage, NextUpgrade.ProcChance, NextUpgrade.DebuffDuration);
        }
        return string.Format("\nLevel: {0} \nDamage: {1} \nProc: {2}% \nDebuff: {3}sec", Level, damage, proc, DebuffDuration);
    }

    private void Shoot()
    {
        SoundManager.Instance.PlaySFX("crossbow");
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Initialize(this);
    }

    public virtual void Upgrade()
    {
        GameManager.Instance.Currency -= NextUpgrade.Price;
        Price += NextUpgrade.Price;
        this.damage += NextUpgrade.Damage;
        this.proc += NextUpgrade.ProcChance;
        this.debuffDuration += NextUpgrade.DebuffDuration;
        Level++;
        GameManager.Instance.UpdateUpgradeTip();
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        
    }

    public abstract Debuff GetDebuff();


    public float Proc 
    {
        get
        {
            return proc;
        }

        set
        {
            this.proc = value;
        }
    }

}

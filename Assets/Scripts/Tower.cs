using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public int Price { get; set; }
    
    // this is the projectiles type
    [SerializeField]
    private string projectileType;
    
    private bool canAttack = true; 

    private float  attackTimer;

    [SerializeField]
    private float attackCooldown;
    
    private SpriteRenderer mySpriteRenderer;
    private Monster target; 
    private Queue<Monster> monsters = new Queue<Monster>();

    [SerializeField]
    private float projectileSpeed;

    public float ProjectileSpeed
    {
        get{return projectileSpeed;}
    }
    
    public Monster Target
    {
        get{ return target;}
    }


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Debug.Log(target);
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
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

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Initialize(this);
    }


}

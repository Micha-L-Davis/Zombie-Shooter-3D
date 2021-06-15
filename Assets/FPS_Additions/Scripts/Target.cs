 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    [Header("Health Amount")]
    public int currentHealth;

    public bool dismember;
    public SkinnedMeshRenderer bodyPart;
    public GameObject bloodLocation;
    public GameObject bloodParticle;
    public bool is_leg;
    public bool crawl;
    public bool critical;

    [SerializeField]
    private Animator _anim;


    public void Start()
    {
        _anim = transform.root.GetComponentInChildren<Animator>();
    }

    public void TakeDamage (int damageAmount)
    {
        currentHealth -= damageAmount;

        transform.root.GetComponent<Enemy_AI>().ChasePlayer = true;

        if (currentHealth <= 0)
        {
            if (dismember == true)// This zombie part can be blown off
            {
                Instantiate(bloodParticle, bloodLocation.transform.position, bloodLocation.transform.rotation);
                bodyPart.enabled = false;
     
                if (critical == true)// this is the head
                {
                    if (crawl == false)// if the zombie is crawling, it will now use the crawling death animation
                    {
                        _anim.SetTrigger("Death");
                        DisableColliders();
                    }

                    if (crawl == true) //zombie is crawling and will now use the crawling death animation
                    {
                        _anim.SetTrigger("Death_Prone");
                        DisableColliders();
                    }

                }

                if (is_leg == true)// this is a leg
                {
                    _anim.SetTrigger("Crawl");
                    TurnOnCrawlingBool();
                }
            }

            if (dismember == false)// this zombie part cannot be blown off like being shot to the chest
            {
                if (crawl == true) //zombie is crawling and will now use the crawling death animation
                {
                    _anim.SetTrigger("Death_Prone");
                    DisableColliders();
                }
               
                else //zombie is standing and will use the standing death animation
                {
                    _anim.SetTrigger("Death");
                    DisableColliders();
                }
            }
        }
    }

    public void DisableColliders()// turn off all colliders after death
    {
        foreach (Collider c in transform.root.GetComponentsInChildren<Collider>()) 
        {
            c.enabled = false;
        }
    }

    public void TurnOnCrawlingBool()// tell all components that the zombie is currently crawling
    {
        foreach (Target t in transform.root.GetComponentsInChildren<Target>())
        {
            t.crawl = true;
        }
    }
}

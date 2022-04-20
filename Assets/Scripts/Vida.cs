using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    [SerializeField] private int health = 1;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReceiveDamage(int damage)
    {
        if (health - damage <= 0)
        {
            
            if (dead)
            {
                return;
            }
            dead = true;
            
            Collider2D objectCollider = gameObject.GetComponent<Collider2D>();
            if (objectCollider)
            {
                 objectCollider.enabled = false;
            }
            
            Animator objectAnimator = gameObject.GetComponent<Animator>();
            if (objectAnimator)
            {
                objectAnimator.SetBool("Destroyed", true);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            health -= damage;
        }
        
    }

}

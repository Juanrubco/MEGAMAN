using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Pathfinding;
public class Mosquito : MonoBehaviour
{
    [SerializeField] private float Radius = 10f;

    private bool FollowPlayer;
    private AIPath path;
    private Vida health;
    Animator AnimatorObject;
    void Start()
    {
        path = GetComponent<AIPath>();
        health = GetComponent<Vida>();
        AnimatorObject = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow_Player();
    }


    private void FixedUpdate()
    {
        float xValue = path.desiredVelocity.x;
        if (xValue != 0f)
        {
            transform.localScale = new Vector2((xValue < 0f ? 1f : -1f), 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health.ReceiveDamage(1);
        }
    }
    
    private void DestroyAfterAnim()
    {
       
        Destroy(gameObject);
    }

    void Follow_Player()
    {
       Collider2D circle = Physics2D.OverlapCircle(transform.position, Radius, LayerMask.GetMask("Player"));
       if (circle)
       {
           path.isStopped = false;
       }
       else
       {
           path.isStopped = true;
       }
    }
    
}

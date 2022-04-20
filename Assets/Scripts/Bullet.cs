using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float lifetime = 4;
    private Animator animator;
    private Vector2 shootDirection;
    private Rigidbody2D body;
    private GameObject BulletOwner;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifetime);
        body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        body.velocity = shootDirection * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject != BulletOwner)
        {

            animator.SetBool("Destroyed", true);
            body.velocity = new Vector2(0f, 0f);
            body.simulated = false;
        }
    }

    private void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }

    public void SetShootDirection(Vector2 ShootDirectionNew)
    {
        shootDirection = ShootDirectionNew;
    }


    public void SetBulletOwner(GameObject newOwner)
    {
        BulletOwner = newOwner;
    }

}

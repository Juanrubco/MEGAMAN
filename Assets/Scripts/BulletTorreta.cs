using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTorreta : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] private float speed = 1;
    
    
    private Vector2 fireDirection;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        body.velocity = fireDirection * speed;
    }

    public void SetFireDirection(Vector2 newFireDirection)
    {
        fireDirection = newFireDirection;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.PlaySound("BulletExplode");
        animator.SetBool("Destroyed", true);
        body.velocity = new Vector2(0f, 0f);
        body.simulated = false;
        Destroy(gameObject, 0.1f);
    }
}

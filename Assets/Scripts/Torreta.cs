using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    [SerializeField] private Vector2 Direction = Vector2.left;
    [SerializeField] private float DelayFire = 2f;
    [SerializeField] private float Range = 20f;
    [SerializeField] private GameObject turretBullet;

    RaycastHit2D ray;

    private Vida health;
    private float lastFireTime = 0f;
    Animator AnimatorObject;
    bool boom = false;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Vida>();
        AnimatorObject = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Physics2D.Raycast(transform.position, Direction, Range, LayerMask.GetMask("Player"));
        Enemy_Fire();
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health.ReceiveDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 init = transform.position;
        Vector3 end = init + (Vector3)(Direction * Range);
        Gizmos.DrawLine(init, end);

    }

        private void Enemy_Fire()
    {
        if (AnimatorObject.GetBool("Destroyed") == false)
        {
            if (ray.collider != null)
            {

                if (Time.time > DelayFire + lastFireTime)
                {
                    GameObject ShootedBullet = Instantiate(turretBullet, transform.position, transform.rotation);
                    if (ShootedBullet)
                    {
                        Vector3 Scale = ShootedBullet.transform.localScale;
                        Scale.x = transform.localScale.x;
                        ShootedBullet.transform.localScale = Scale;
                        BulletTorreta fired = ShootedBullet.GetComponent<BulletTorreta>();
                        if (fired)
                        {
                            fired.SetFireDirection(Direction);
                        }
                    }

                    lastFireTime = Time.time;
                }
            }
        }
        else
        {
            if (!boom)
            {
                boom = true;
            }
        }
    }
}

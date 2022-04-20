using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Megaman : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpHeight = 20f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float Delay = 1;
    [SerializeField] private float HoldTime = 1;

    private Rigidbody2D body;
    private Animator animator;

    private bool isInAir = false;
    private bool isActive = false;
    private float lastFireTime = 0;
    private Vida health;
    private bool AfterAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Vida>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        Jump();
        Shoot();
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red);
        isInAir = ray.collider == null;
        animator.SetBool("isInAir", isInAir);


    }

    IEnumerator Pause()
    {
        yield return new WaitForSecondsRealtime(1);
        SoundManager.PlaySound("PlayerDeath");
        StartCoroutine(nameof(Reload));
    }
    

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(nameof(AnimationDelay));
            animator.SetLayerWeight(1, 1);
            
            if (Time.time > Delay + lastFireTime)
            {
                GameObject ShootedBullet = Instantiate(bullet, transform.position, transform.rotation);
                if (ShootedBullet)
                {
                    Bullet fired = ShootedBullet.GetComponent<Bullet>();
                    if (fired)
                    {   
                        fired.SetBulletOwner(gameObject);
                        SoundManager.PlaySound("PlayerBullet");
                        fired.SetShootDirection(new Vector2((transform.localScale.x < 0f ? -1f : 1f), 0f));
                    }
                }
                lastFireTime = Time.time;
            }
            StartCoroutine(nameof(AnimationDelay));
        }
    }

        IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(HoldTime);
        animator.SetLayerWeight(1, 0);
    }


    private void IntroAnimEnd()
    {
        isActive = true;
    }

    private void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }
        
        float xValue = Input.GetAxis("Horizontal");
        float yValue = Input.GetAxis("Vertical");

        if (xValue != 0f)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector2((xValue < 0f ? -1f : 1f), 1f);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        body.velocity = new Vector2(xValue * Speed, body.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            health.ReceiveDamage(1);
        }
    }
    
    private void DestroyAfterAnim()
    {
        if (AfterAnim)
        {
            return;
        }
        AfterAnim = true;
        Time.timeScale = 0f;
        StartCoroutine(nameof(Pause));
    }
    

    IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

        void Jump()
    {
        if (isInAir)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("Jumped", true);
            body.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
        }
        else
        {
            animator.SetBool("Jumped", false);
        }
    }
    
}

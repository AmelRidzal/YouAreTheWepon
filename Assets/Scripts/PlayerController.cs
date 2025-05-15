using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;

        private bool isGrounded;
        public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private GameManager gameManager;

        private AudioSource audioSource;
        public AudioClip shootSound;

        public SpriteRenderer spriteRenderer;
        public BoxCollider2D boxcolider;



        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform fireingPoint;
        [Range(0.1f, 2f)]
        [SerializeField] private float fireRate = 0.5f;
        private float fireTimer;




        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (facingRight)
            {
                this.transform.localScale = new Vector3((-1.7f), 1.7f, 1f);
            }
            else
            {
                this.transform.localScale = new Vector3((1.7f), 1.7f, 1f);
            }
            boxcolider.offset = new Vector2(-0.03f, -0.14f);
            boxcolider.size = new Vector2(0.5f, 1f);
            animator.SetInteger("playerSize", 1);
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                moveInput = Input.GetAxis("Horizontal");
                Vector3 direction = transform.right * moveInput;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
            }
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (!isGrounded) animator.SetInteger("playerState", 2); // Turn on jump animation

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }

            if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
            {
                if (StaticData.ammo > 0)
                {
                    fireTimer = fireRate;
                    Shoot(); 
                    audioSource.PlayOneShot(shootSound);
                    animator.SetInteger("playerState", 3); // Turn on shoot animation
                }
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.tag == "Enemy")
            {
                string s = other.gameObject.name.Substring(0, 3);
                if (s == "Gun")
                {
                    PlayerTakeDamage(1);
                }
                else
                {
                    PlayerTakeDamage(5);
                }
                    
            }


            if (other.gameObject.tag == "Damage")
            {
                if (other.gameObject.name == "bullet(Clone)")
                {
                    PlayerTakeDamage(3);
                }
            }



        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Ammo")
            {
                string s = other.gameObject.name.Substring(0, 9);
                if (s == "AmmoPack1")
                {
                    PlayerHeal(1);
                }
                else if (s == "AmmoPack2")
                {
                    PlayerHeal(2);
                }
                else if (s == "AmmoPack3")
                {
                    PlayerHeal(3);
                }
                else if (s == "AmmoPack4")
                {
                    PlayerHeal(4);
                }
                else if (s == "AmmoPack5")
                {
                    PlayerHeal(5);
                }
                else if (s == "AmmoPack6")
                {
                    PlayerHeal(6);
                }
                else if (s == "AmmoPack9")
                {
                    PlayerHeal(10);
                }
                Destroy(other.gameObject);
            }else if(other.gameObject.tag == "Trap")
            {
                PlayerTakeDamage(StaticData.ammo);
            }
        }

        private void Shoot()
        {
            Instantiate(bulletPrefab, fireingPoint.position, fireingPoint.rotation);
            StaticData.ammo -= 1;
            ChangeSize();
        }

        private void PlayerTakeDamage(int damage)
        {
            StaticData.ammo -= damage;
            ChangeSize();
            StartCoroutine(PlayerFlash(true));
        }


        private void PlayerHeal(int health)
        {
            StaticData.ammo += health;
            ChangeSize();
            //StartCoroutine(PlayerFlash(false));
        }



        private void ChangeSize()
        {


            if (StaticData.ammo <= 5)
            {
                if (facingRight)
                {
                    this.transform.localScale = new Vector3((-1f), 1f, 1f);
                }
                else
                {
                    this.transform.localScale = new Vector3((1f), 1f, 1f);
                }
                boxcolider.offset = new Vector2(-0.015f, -0.17f);
                boxcolider.size = new Vector2(0.52f, 0.95f);
                animator.SetInteger("playerSize", 0);
                jumpForce = 9f;
                movingSpeed = 8f;
            }
            else if (StaticData.ammo <= 10)
            {
                if (facingRight)
                {
                    this.transform.localScale = new Vector3((-1.7f), 1.7f, 1f);
                }
                else
                {
                    this.transform.localScale = new Vector3((1.7f), 1.7f, 1f);
                }
                boxcolider.offset = new Vector2(-0.03f, -0.14f);
                boxcolider.size = new Vector2(0.5f, 1f);
                animator.SetInteger("playerSize", 1);
                jumpForce = 8f;
                movingSpeed = 7f;
            }
            else if (StaticData.ammo <= 20)
            {
                if (facingRight)
                {
                    this.transform.localScale = new Vector3((-2.3f), 2.3f, 1f);
                }
                else
                {
                    this.transform.localScale = new Vector3((2.3f), 2.3f, 1f);
                }
                boxcolider.offset = new Vector2(-0.015f, -0.11f);
                boxcolider.size = new Vector2(0.52f, 1.08f);
                animator.SetInteger("playerSize", 2);
                jumpForce = 7.3f;
                movingSpeed = 5.5f;
            }
            else
            {
                if (facingRight)
                {
                    this.transform.localScale = new Vector3((-2.6f), 2.6f, 1f);
                }
                else
                {
                    this.transform.localScale = new Vector3((2.6f), 2.6f, 1f);
                }
                boxcolider.offset = new Vector2(-0.015f, -0.08f);
                boxcolider.size = new Vector2(0.52f, 1.15f);
                animator.SetInteger("playerSize", 3);
                jumpForce = 7;
                movingSpeed = 4;
            }


        }


        IEnumerator PlayerFlash(bool what)
        {
            if(what)
            {
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.color = Color.green;
            }
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = Color.white;
        }
    }
}

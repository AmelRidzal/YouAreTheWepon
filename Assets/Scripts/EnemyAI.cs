using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        private float moveSpeedMemory = 1f;
        public float moveSpeed = 1f;
        public LayerMask ground;
        public LayerMask wall;

        private Rigidbody2D rigidbody; 
        public Collider2D triggerCollider;
        private Canvas can;
        
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            can = GetComponentInChildren<Canvas>();
            moveSpeedMemory = moveSpeed;
        }

        void Update()
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        }

        void FixedUpdate()
        {
            if(!triggerCollider.IsTouchingLayers(ground) || triggerCollider.IsTouchingLayers(wall))
            {
                Flip();
            }   
        }
        
        private void Flip()
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            can.transform.localScale = new Vector2(can.transform.localScale.x * -1, can.transform.localScale.y);
         
            moveSpeed *= -1;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.transform.position.x > this.transform.position.x)
                {
                    if(this.transform.localScale.x < 0)
                    {
                        Flip();
                    }
                }
                else
                {
                    if (this.transform.localScale.x > 0)
                    {
                        Flip();
                    }
                }
                StartCoroutine(AttackStopMove());
            }
        }

        IEnumerator AttackStopMove()
        {   
            moveSpeedMemory=moveSpeed;
            moveSpeed = 0;
            yield return new WaitForSeconds(0.1f);
            moveSpeed = moveSpeedMemory;
        }
    }
}

using UnityEngine;

namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;
        bool isJumping = false;
        private bool alive = true;
        private bool canJump = false;
        private bool preJump = false;

        public AudioClip audioRun;
        public AudioClip audioJumpStart;
        public AudioClip audioJumpEnd;
        AudioSource audioSource;

        private int counter = 0;
        private int MaxCounter = 12;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            this.audioSource = GetComponent<AudioSource>();

        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                if (preJump != anim.GetBool("isJump"))
                {
                    if (preJump)
                    {
                        // land
                        audioSource.clip = audioJumpEnd;
                        audioSource.Play();
                    }
                    else
                    {
                        // start
                        audioSource.clip = audioJumpStart;
                        audioSource.Play();
                    }
                }
                preJump = anim.GetBool("isJump");

                Attack();
                Jump();
                Run();

            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }


        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(direction, 1, 1);

                if (!anim.GetBool("isJump"))
                {
                    anim.SetBool("isRun", true);
                    if (counter == 0)
                    {
                        audioSource.clip = audioRun;
                        audioSource.Play();
                        counter = MaxCounter;
                    }
                    else
                        counter = counter - 1;
                }
                else
                    counter = 0;


            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);

                if (!anim.GetBool("isJump"))
                {
                    anim.SetBool("isRun", true);
                    if (counter == 0)
                    {
                        audioSource.clip = audioRun;
                        audioSource.Play();
                        counter = MaxCounter;
                    }
                    else
                        counter = counter - 1;
                }
                else
                    counter = 0;


            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        void Jump()
        {
            if ((Input.GetButton("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump") && canJump)
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.tag == "wall")
            {
                canJump = true;
            }
        }
        void OnCollisionStay2D(Collision2D col)
        {
            if (col.transform.tag == "wall")
            {
                canJump = true;
            }
        }
        void OnCollisionExit2D(Collision2D col)
        {
            if (col.transform.tag == "wall")
            {
                canJump = false;
            }
        }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.S) && !anim.GetBool("isRun"))
            {
                anim.SetTrigger("attack");
            }
        }
        //void Hurt()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        anim.SetTrigger("hurt");
        //        if (direction == 1)
        //            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        //        else
        //            rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        //    }
        //}
        //void Die()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        anim.SetTrigger("die");
        //        alive = false;
        //    }
        //}
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
    }
}
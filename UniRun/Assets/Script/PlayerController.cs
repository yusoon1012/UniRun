using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;
    private Player player;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private int playerId = 0;
    private Rigidbody2D playerRigid = default;
    private Animator animator = default;
    private AudioSource playerAudio = default;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Global_Function.Assert(playerRigid != null);
        Global_Function.Assert(animator != null);
        Global_Function.Assert(playerAudio != null);
        player=ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }

        if (player.GetButtonDown("Player Jump")&&jumpCount<2)
        {
            
            jumpCount += 1;
            playerRigid.velocity = Vector2.zero;
            playerRigid.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(player.GetButtonUp("Player Jump")&&0<playerRigid.velocity.y)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }
        animator.SetBool("Ground", isGrounded);


    }

    private void Die()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigid.velocity = Vector2.zero;
        int motorIndex = 0; // the first motor
        float motorLevel = 0.2f; // full motor speed
        float duration = 0.2f; // 2 seconds

        player.SetVibration(motorIndex, motorLevel, duration);
        isDead = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
        if(collision.tag.Equals("Dead")&&isDead==false)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y>0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
  
}

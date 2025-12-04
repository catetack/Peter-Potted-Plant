using UnityEngine;

public class Spring : MonoBehaviour
{
    public float jumpForce = 10f;

    //Sound
    private AudioSource jumpSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignObjects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            jumpSound.Play(); //play sound on collision with jump pad
            //Function scripts
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity=new Vector2 (collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityX, 0);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity=Vector2.up*jumpForce;
        }
    }

    void assignObjects()
    {
        jumpSound = GetComponent<AudioSource>();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class playerLife : MonoBehaviour
{
    Rigidbody2D rb;

    //for initializing Sound Effects
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("RESPAWNED");
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Trap")
        {
            Death();
        }
    }

    private void Death()//Called when player is dead
    {
        rb.bodyType = RigidbodyType2D.Static;
        Invoke("Restart", 1f);
        audioManager.PlaySoundEffect(audioManager.respawn);
    }

    private void Restart()//Reload the scene. This could be invoked in animation frames.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

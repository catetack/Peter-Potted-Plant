using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private AudioSource finishSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finishSound = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            finishSound.Play();
            Invoke("LevelFinish", 1.8f);
        }
    }

    private void LevelFinish()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

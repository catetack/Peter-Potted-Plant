using UnityEngine;

public class reviveIcon : MonoBehaviour
{
    playerStateManager PlayerState;
    SpriteRenderer spriteRenderer;
    
    public Sprite[] sprites; // assign in Inspector
    public float frameRate = 10f; // frames per second
    
    private int currentFrame = 0;
    private float timer = 0f;
    
    float offset = 5.5f;
    
    void Start()
    {
        assignObjects();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!PlayerState.isDowned)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
            AnimateSprite();
        }
    }
    
    void AnimateSprite()
    {
        timer += Time.deltaTime;
        
        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % sprites.Length; // loop back to 0
            spriteRenderer.sprite = sprites[currentFrame];
        }
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(PlayerState.headPosition.x + offset, PlayerState.headPosition.y + offset, -1.0f);
    }
    
    void assignObjects()
    {
        PlayerState = GetComponentInParent<playerStateManager>();
        if (PlayerState == null)
        {
            Debug.LogError("playerStateManager script not found on 'PlayerState' parent.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (!renderer.isVisible)
        {
            speed *= -1;
        }
        pos += Vector2.right * speed * Time.deltaTime;
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Ame_movement player = other.collider.GetComponent<Ame_movement>();
        
        if (player != null)
        {
            if (player.get_pound_attack())
            {
                
                player.setJump_count(-1);
                Destroy(gameObject);
            }
        }
    }
}

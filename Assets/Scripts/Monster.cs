using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Serialize Field Start
    [SerializeField] float hp = 10;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Sprite dead;
    // Serialize Field End
    SpriteRenderer sprite_renderer;
    // Start is called before the first frame update

    public bool isDead = false;

    private void Awake() {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieWithCollision(collision))
        {
            StartCoroutine(Death());
        }

    }

    bool ShouldDieWithCollision(Collision2D collision)
    {
        Debug.Log("Monster Collided with " + collision.contacts[0].relativeVelocity);
        if (collision.gameObject.GetComponent<Bird>() != null)
        {
            Debug.Log("Collided with the bird.");
            hp = 0;
            return true;
        }
        float collision_force = (float)((collision.contacts[0].relativeVelocity.x + collision.contacts[0].relativeVelocity.y) * 0.7);

        if (collision.contacts[0].normal.y > 1)
        {
            Debug.Log(collision.contacts[0].normal.y);
            hp = 0;
            return true;
        }
        if (collision_force < 0)
        {
            collision_force = +collision_force;
        }
        Damage(collision_force);
        return false;
    }

    IEnumerator Death()
    {
        // kills the monster 
        sprite_renderer.sprite = dead;
        if(!isDead){
            particles.Play();
        }
        isDead = true;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        
    }

    void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Death();
            
        }
    }
}

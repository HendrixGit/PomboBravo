using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCode : MonoBehaviour
{
    private int limite;
    private SpriteRenderer spriteR;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private GameObject bomb, pontos100;
    private AudioSource audioObj;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        limite = 0;
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = sprites[0];
        audioObj = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude > 4 && col.relativeVelocity.magnitude < 10)
        {
            if (limite < sprites.Length - 1)
            {
                limite++;
                spriteR.sprite = sprites[limite];
                audioObj.clip = clips[0];
                audioObj.Play();
            }
            else if (limite == sprites.Length - 1)
            {
                Instantiate(bomb,      new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Instantiate(pontos100, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                audioObj.clip = clips[1];
                audioObj.Play();
                Destroy(gameObject,1);
                GAME_MANAGER.instance.pontosGame  += 1000;
                UI_MANAGER.instance.pontosTxt.text = GAME_MANAGER.instance.pontosGame.ToString();
            }
        }
        else if (col.relativeVelocity.magnitude > 12 && col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("clone")) {
            Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            audioObj.clip = clips[1];
            audioObj.Play();
            Destroy(gameObject,1);
            GAME_MANAGER.instance.pontosGame  += 1000;
            UI_MANAGER.instance.pontosTxt.text = GAME_MANAGER.instance.pontosGame.ToString();
        }
    }

}

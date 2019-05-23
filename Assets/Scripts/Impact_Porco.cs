using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact_Porco : MonoBehaviour
{
    private Animator animacoes;
    private int      limite = -1;
    public string[]  clips;
    [SerializeField]
    private GameObject bomb, pontos100;

    private void Start()
    {
        animacoes = GetComponent<Animator>();

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude > 4 && col.relativeVelocity.magnitude < 10)
        {
            if (limite < clips.Length - 1)
            {
                limite++;
                animacoes.Play(clips[limite]);
            }
            else if (limite == clips.Length - 1)
            {
                Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Instantiate(pontos100, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                GAME_MANAGER.instance.numPorcosCena -= 1;
                Destroy(gameObject);
            }
        }
        else if (col.relativeVelocity.magnitude > 12 && col.gameObject.CompareTag("Player"))
        {
            Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(pontos100, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            GAME_MANAGER.instance.numPorcosCena -= 1;
            Destroy(gameObject);
        }
    }
}

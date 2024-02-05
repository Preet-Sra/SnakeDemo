using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    [Header("Ranges of my food")]
    public float Xrange;
    public float Yrange;


    [Header("UI")]
    [Space(10)]
    public Text Scoretext;
    int scoreCount;
    [SerializeField] AudioClip CollectSound;
    [SerializeField]SnakeMovement snake;

    private void Start()
    {
        scoreCount = 0;
        RandomizePosition();
        
    }

    public void RandomizePosition()
    {
        Vector3 randomPosition = GetValidRandomPoint();
        transform.position = randomPosition;
       // transform.position = new Vector3(Mathf.Round(Random.Range(-Xrange, Xrange)), Mathf.Round(Random.Range(-Yrange, Yrange)), transform.position.z);
    }


    Vector3 GetValidRandomPoint()
    {
        Vector3 randomizePosition;
        do
        {
            randomizePosition = new Vector3(Mathf.Round(Random.Range(-Xrange, Xrange)), Mathf.Round(Random.Range(-Yrange, Yrange)), transform.position.z);
        } while (IsFoodOnSnake(randomizePosition));
        return randomizePosition ;
       
    }


    bool IsFoodOnSnake(Vector3 _position)
    {
       foreach(Transform snakeparts in snake.ReturnBodyParts())
        {
            if (Vector3.Distance(_position, snakeparts.position) < 1f)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlayAudio(CollectSound);
            scoreCount++;
            Scoretext.text = "Score: " + scoreCount;
            RandomizePosition();
        }
    }

    public void ResetScore()
    {
        scoreCount = 0;
        Scoretext.text = "Score: " + scoreCount;
    }


    // Sound, Scriptable for new skin
}

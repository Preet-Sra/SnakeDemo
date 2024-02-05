using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SnakeData Data;
    [SerializeField]int CurrentIndex;
    public Image SnakeDisplay;
    public float maxSpeed = 25, MaxSize = 12;
    public Image Speedbar,SnakeSize;

    [Space(5)]
    [Header("GameStart Logic")]
    public GameObject StartCanvas;
    public GameObject SnakeObj, FoodObj;
    private void Start()
    {
        ShowSnake();
        
    }

    public void ShowNextSnake()
    {
        CurrentIndex++;
        if (CurrentIndex >= Data.snakeAttributes.Count)
        {
            CurrentIndex = 0;
        }
        ShowSnake();
    }

    public void ShowPreviousSnake()
    {
        CurrentIndex--;
        if (CurrentIndex <0)
        {
            CurrentIndex = Data.snakeAttributes.Count-1;
        }
        ShowSnake();
    }

    void ShowSnake()
    {
        SnakeDisplay.sprite = Data.snakeAttributes[CurrentIndex].snakeIcon;
        float snakeSpeed=Data.snakeAttributes[CurrentIndex].snakeSpeed;
        Speedbar.fillAmount = snakeSpeed/maxSpeed;

        float snakeSize = Data.snakeAttributes[CurrentIndex].snakeStartSize;
        SnakeSize.fillAmount = snakeSize / MaxSize;
    }

    public void StartGame()
    {
        StartCanvas.SetActive(false);
        SnakeObj.SetActive(true);
        FoodObj.SetActive(true);
        SnakeObj.GetComponent<SpriteRenderer>().color = Data.snakeAttributes[CurrentIndex].snakeColor;
        SnakeMovement snakeMovement = SnakeObj.GetComponent<SnakeMovement>();
        snakeMovement.Speed = Data.snakeAttributes[CurrentIndex].snakeSpeed;
        snakeMovement.StartBodyCount = Data.snakeAttributes[CurrentIndex].snakeStartSize;
    }
}

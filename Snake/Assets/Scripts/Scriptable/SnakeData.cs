using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Snake",menuName ="Snake/SnakeAttributes")]
public class SnakeData : ScriptableObject
{
    public List<SnakeAttributes> snakeAttributes;
}

[System.Serializable]
public class SnakeAttributes
{
    public Sprite snakeIcon;
    public float snakeSpeed;
    public int snakeStartSize;
    public Color snakeColor;
}

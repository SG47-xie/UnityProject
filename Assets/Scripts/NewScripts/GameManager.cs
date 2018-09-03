using UnityEngine;
using System.Collections;

public enum GameState
{
    Hall,
    Fish,
    Niuniu,
    Car,
    Forest
}

public class GameManager : Singleton<GameManager>
{
    public GameState GameState;
    public int HallLeftLogoIdx;
    public GameManager()
    {
        HallLeftLogoIdx = 9;
        GameState = GameState.Hall;
    }
}

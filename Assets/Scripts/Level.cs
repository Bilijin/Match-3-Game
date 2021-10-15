using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum LevelType
    {
        TIMER,
        OBSTACLE,
        MOVES
    }

    public GridController grid;

    public int score1Star;
    public int score2Star;
    public int score3Star;

    public HUD hud;

    protected LevelType type;
    public LevelType Type
    {
        get { return type; }
    }

    protected int currentScore;

    protected bool didWin;
    public AudioSource audioSource;
    public AudioClip matchFx;

    // Start is called before the first frame update
    void Start()
    {
        hud.SetScore(currentScore);
        audioSource.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GameWin()
    {
        grid.GameOver();
        didWin = true;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void GameLose()
    {
        grid.GameOver();
        didWin = false;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void OnMove(int xPos, int yPos)
    {
        AudioSource.PlayClipAtPoint(matchFx, new Vector3(xPos, yPos));
    }

    public virtual void OnPieceCleared(GamePiece piece)
    {
        //update score
        currentScore += piece.score;
        hud.SetScore(currentScore);
    }

    protected virtual IEnumerator WaitForGridFill()
    {
        while (grid.IsFilling)
        {
            yield return 0;
        }

        if (didWin)
        {
            hud.OnGameWin(currentScore);
        }
        else
        {
            hud.OnGameLose();
        }
    }
}

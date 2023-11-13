using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBoard : MonoBehaviour
{
    [SerializeField] Transform topRight;
    [SerializeField] Transform botLeft;
    [Space(10)]
    [SerializeField] [Range(0f, 1f)] float damageYInfluence = .25f;
    [SerializeField] [Range(0f, 1f)] float minDamagePegSpawningRate = .25f;
    [Space(10)]
    [SerializeField] [Range(0f, 1f)] float pegSpawningRate;
    [Space(10)]
    [SerializeField] [Range(0f, 1f)] float goldPegSpawningRate;
    [Space(10)]
    [SerializeField] Transform pegParent;
    [SerializeField] float scale = .75f;

    static Peg[,] pegBoard;

    int rows;
    int cols;

    private void OnEnable()
    {
        EventManager.OnNewMonster += EventManager_OnNewMonster;
        EventManager.OnBallBottoms += EventManager_OnBallBottoms;
        EventManager.OnBoardClear += EventManager_OnBoardClear;
    }

    private void OnDisable()
    {
        EventManager.OnNewMonster -= EventManager_OnNewMonster;
        EventManager.OnBallBottoms -= EventManager_OnBallBottoms;
        EventManager.OnBoardClear -= EventManager_OnBoardClear;
    }

    private void EventManager_OnBoardClear()
    {
        NewBoard();
    }

    private void EventManager_OnBallBottoms(Ball ball)
    {
        if (isDamagePegLeft()) return;
        EventManager.Invoke(CustomEvent.BoardClear);
    }

    private void EventManager_OnNewMonster(Monster monster)
    {
        NewBoard();
    }

    public void NewBoard()
    {
        rows = (int)(Mathf.Abs(topRight.position.x - botLeft.position.x) / scale);
        cols = (int)(Mathf.Abs(topRight.position.y - botLeft.position.y) / scale);

        DeletePegs();
        SpawnPegBoard();
    }

    void DeletePegs()
    {
        foreach (Transform t in pegParent)
        {
            if (t == null) continue;
            Destroy(t.gameObject);
        }
    }

    Peg SpawnPeg(string name, Vector3 position)
    {
        return StaticSpawner.SpawnGame(name, position, pegParent).GetComponent<Peg>();
    }

    bool isPeg(int x, int y)
    {
        return pegBoard[x, y] != null;
    }

    Peg GetPeg(int x, int y)
    {
        return pegBoard[x, y];
    }

    bool isDamagePegLeft()
    {
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                Peg peg = GetPeg(x, y);

                if (!isPeg(x, y)) continue;
                if (!peg.GetComponent<DamagePeg>()) continue;

                return true;
            }
        }

        return false;
    }

    void SpawnPegBoard()
    {
        pegBoard = new Peg[rows, cols];

        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                float rowOffset = y % 2 == 0 ? scale * .5f : 0;
                float currentX = ((x * scale) + scale) + rowOffset;
                float currentY = (y * scale);

                Vector3 current = (topRight.position - new Vector3(currentX, currentY) + Vector3.right * scale * .5f);
                float yPercent = ((float)y / (float)cols) - damageYInfluence;
                yPercent = Mathf.Clamp(yPercent, minDamagePegSpawningRate, 1);

                if (Random.value < pegSpawningRate)
                {
                    if (Random.value < yPercent)
                    {
                        pegBoard[x, y] = SpawnPeg("Damage", current);
                    }
                    else
                    {
                        if (Random.value < goldPegSpawningRate)
                        {
                            pegBoard[x, y] = SpawnPeg("Gold", current);
                        }
                        else
                        {
                            pegBoard[x, y] = SpawnPeg("Neutral", current);
                        }
                    }
                }
            }
        }
    }
}

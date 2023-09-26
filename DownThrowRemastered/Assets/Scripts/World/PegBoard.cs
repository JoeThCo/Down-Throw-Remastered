using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBoard : MonoBehaviour
{
    [SerializeField] Transform topRight;
    [SerializeField] Transform botLeft;
    [Space(10)]
    [SerializeField] [Range(0f, 1f)] float pegSpawningRate;
    [SerializeField] [Range(0f, 1f)] float damagePegSpawningRate;
    [Space(10)]
    [SerializeField] Transform pegParent;
    [SerializeField] float scale = .75f;

    static Peg[,] pegBoard;

    private void OnEnable()
    {
        EventManager.OnNewMonster += EventManager_OnNewMonster;
    }

    private void OnDisable()
    {
        EventManager.OnNewMonster -= EventManager_OnNewMonster;
    }

    private void EventManager_OnNewMonster(Monster monster)
    {
        NewBoard();
    }

    public void NewBoard()
    {
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
        return ItemSpawner.SpawnGame(name, position, pegParent).GetComponent<Peg>();
    }

    bool isPeg(int x, int y)
    {
        return pegBoard[x, y] != null;
    }

    Peg GetPeg(int x, int y)
    {
        return pegBoard[x, y];
    }

    void SpawnPegBoard()
    {
        int rows = (int)(Mathf.Abs(topRight.position.x - botLeft.position.x) / scale);
        int cols = (int)(Mathf.Abs(topRight.position.y - botLeft.position.y) / scale);

        pegBoard = new Peg[rows, cols];

        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                float rowOffset = y % 2 == 0 ? scale * .5f : 0;
                float currentX = ((x * scale) + scale) + rowOffset;
                float currentY = (y * scale);

                Vector3 current = (topRight.position - new Vector3(currentX, currentY) + Vector3.right * scale * .5f);

                if (Random.value < pegSpawningRate)
                {
                    if (Random.value < damagePegSpawningRate)
                    {
                        pegBoard[x, y] = SpawnPeg("Damage", current);
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

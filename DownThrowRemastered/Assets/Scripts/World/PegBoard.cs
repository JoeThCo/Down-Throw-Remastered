using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBoard : MonoBehaviour
{
    [SerializeField] Transform topRight;
    [SerializeField] Transform botLeft;
    [Space(10)]
    [SerializeField] Transform pegParent;
    [SerializeField] float scale = .75f;

    private void Awake()
    {
        EventManager.OnNewMonster += EventManager_OnNewMonster;
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
            Destroy(t.gameObject);
        }
    }

    void SpawnPegBoard()
    {
        int rowCount = (int)(Mathf.Abs(topRight.position.x - botLeft.position.x) / scale);
        int colCount = (int)(Mathf.Abs(topRight.position.y - botLeft.position.y) / scale);

        for (int y = 0; y <= colCount; y++)
        {
            for (int x = 0; x < rowCount; x++)
            {
                float rowOffset = y % 2 == 0 ? scale * .5f : 0;

                float currentX = ((x * scale) + scale) + rowOffset;
                float currentY = (y * scale);

                Vector3 current = (topRight.position - new Vector3(currentX, currentY) + Vector3.right * scale * .5f);

                if (Random.value < .25f)
                {
                    ItemSpawner.SpawnGame("Damage", current, pegParent);
                }
                else
                {
                    ItemSpawner.SpawnGame("Neutral", current, pegParent);
                }
            }
        }
    }
}

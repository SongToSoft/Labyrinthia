using UnityEngine;

public class LabyrinthMaker : MonoBehaviour
{
    private void Start()
    {
        StaticContent.labyrinthHeight = 10;
        StaticContent.labyrinthWidth = 20;
        StaticContent.wallsSets = new int[StaticContent.labyrinthHeight, StaticContent.labyrinthWidth];
        StaticContent.labyrinth = new LabyrinthPath[StaticContent.labyrinthHeight, StaticContent.labyrinthWidth];
        MakeBorder();
        SetWallsSets();
    }

    private void MakeBorder()
    {
        for (int i = 0; i < StaticContent.labyrinthHeight; ++i)
        {
            for (int j = 0; j < StaticContent.labyrinthWidth; ++j)
            {
                StaticContent.position.x += 0.83f;
                StaticContent.labyrinth[i, j] = Instantiate(StaticContent.labyrinthPath, StaticContent.position, StaticContent.labyrinthPath.transform.rotation);
                if ((i == 0) && (j == 0))
                {
                    StaticContent.labyrinth[i, j].AddHighWall();
                    StaticContent.labyrinth[i, j].AddLeftWall();
                    continue;
                }
                if ((i == StaticContent.labyrinthHeight - 1) && (j == 0))
                {
                    StaticContent.labyrinth[i, j].AddDownWall();
                    StaticContent.labyrinth[i, j].AddLeftWall();
                    continue;
                }
                if ((i == 0) && (j == StaticContent.labyrinthWidth - 1))
                {
                    StaticContent.labyrinth[i, j].AddRightWall();
                    StaticContent.labyrinth[i, j].AddHighWall();
                    continue;
                }
                if ((i == StaticContent.labyrinthHeight - 1) && (j == StaticContent.labyrinthWidth - 1))
                {
                    StaticContent.labyrinth[i, j].AddRightWall();
                    StaticContent.labyrinth[i, j].AddDownWall();
                    continue;
                }
                if (i == 0)
                {
                    StaticContent.labyrinth[i, j].AddHighWall();
                    continue;
                }
                if (i == StaticContent.labyrinthHeight - 1)
                {
                    StaticContent.labyrinth[i, j].AddDownWall();
                    continue;
                }
                if (j == 0)
                {
                    StaticContent.labyrinth[i, j].AddLeftWall();
                    continue;
                }
                if (j == StaticContent.labyrinthWidth - 1)
                {
                    StaticContent.labyrinth[i, j].AddRightWall();
                    continue;
                }
            }
            StaticContent.position.x = -8.5f;
            StaticContent.position.y -= 0.834f;
        }
    }

    private void SetWallsSets()
    {
        System.Random random = new System.Random();

        for (int k = 0; k < StaticContent.labyrinthHeight - 1; ++k)
        {
            if (k == 0)
            {
                for (int i = 0; i < StaticContent.labyrinthWidth; ++i)
                {
                    StaticContent.wallsSets[0, i] = i + 1;
                }
            }
            else
            {
                CopyLine(k - 1, k);
                for (int i = 0; i < StaticContent.labyrinthWidth; ++i)
                {
                    if (StaticContent.labyrinth[k - 1, i].CheckDownWall())
                    {
                        StaticContent.wallsSets[k, i] = GetUniqSetNumber(k);
                    }
                }
            }
            for (int i = 1; i < StaticContent.labyrinthWidth; ++i)
            {
                if (StaticContent.wallsSets[k, i] != StaticContent.wallsSets[k, i - 1])
                {
                    int value = random.Next(0, 2);
                    if (value == 1)
                    {
                        StaticContent.wallsSets[k, i] = StaticContent.wallsSets[k, i - 1];
                    }
                    else
                    {
                        StaticContent.labyrinth[k, i - 1].AddRightWall();
                    }
                }
                else
                {
                    StaticContent.labyrinth[k, i - 1].AddRightWall();
                }
            }
            for (int i = 0; i < StaticContent.labyrinthWidth; ++i)
            {
                int setNumber = StaticContent.wallsSets[k, i];
                int setCount = GetSetCount(k, setNumber);
                if (setCount == 1)
                {
                    continue;
                }
                else
                {
                    int value = random.Next(i, i + setCount);
                    for (int j = i; j < i + setCount; ++j)
                    {
                        if ((j != value) && (j < StaticContent.labyrinthWidth))
                        {
                            StaticContent.labyrinth[k, j].AddDownWall();
                        }
                    }
                    i += setCount - 1;
                }
            }
        }
        //Correct Last Line
        CopyLine(StaticContent.labyrinthHeight - 2, StaticContent.labyrinthHeight - 1);
        for (int i = 1; i < StaticContent.labyrinthWidth; ++i)
        {
            if (StaticContent.wallsSets[StaticContent.labyrinthHeight - 1, i] != StaticContent.wallsSets[StaticContent.labyrinthHeight - 1, i - 1])
            {
                StaticContent.wallsSets[StaticContent.labyrinthHeight - 1, i] = StaticContent.wallsSets[StaticContent.labyrinthHeight - 1, i - 1];
                StaticContent.labyrinth[StaticContent.labyrinthHeight - 1, i - 1].DelRightWall();
            }
        }
        //for (int i = 0; i < StaticContent.labyrinthHeight; ++i)
        //{
        //    Debug.Log("---------------");
        //    for (int j = 0; j < StaticContent.labyrinthWidth; ++j)
        //    {
        //        Debug.Log(StaticContent.wallsSets[i, j]);
        //    }
        //}
    }

    private int GetSetCount(int i, int setNumber)
    {
        int result = 0;
        for (int j = 0; j < StaticContent.labyrinthWidth; ++j)
        {
            if (StaticContent.wallsSets[i, j] == setNumber)
            {
                ++result;
            }
        }
        return result;
    }

    private void CopyLine(int source, int destination)
    {
        for (int i = 0; i < StaticContent.labyrinthWidth; ++i)
        {
            StaticContent.wallsSets[destination, i] = StaticContent.wallsSets[source, i];
        }
    }

    private int GetUniqSetNumber(int line)
    {
        for (int i = 1; i < StaticContent.labyrinthWidth + 1; ++i)
        {
            bool flag = true;
            for (int j = 0; j < StaticContent.labyrinthWidth; ++j)
            {
                if (StaticContent.wallsSets[line, j] == i)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                return i;
            }
        }
        return 0;
    }
}

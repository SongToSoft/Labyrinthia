using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthPath : MonoBehaviour
{
    [SerializeField]
    private GameObject High, Right, Down, Left;

    public void AddHighWall()
    {
        High.SetActive(true);
    }

    public void AddRightWall()
    {
        Right.SetActive(true);
    }

    public void AddDownWall()
    {
        Down.SetActive(true);
    }

    public void AddLeftWall()
    {
        Left.SetActive(true);
    }

    public void DelHighWall()
    {
        High.SetActive(false);
    }

    public void DelRightWall()
    {
        Right.SetActive(false);
    }

    public void DelDownWall()
    {
        Down.SetActive(false);
    }

    public void DelLeftWall()
    {
        Left.SetActive(false);
    }

    public bool CheckDownWall()
    {
        return Down.activeSelf;
    }
}

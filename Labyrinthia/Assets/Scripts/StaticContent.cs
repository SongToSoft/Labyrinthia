using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class StaticContent
{
    static public int labyrinthHeight;
    static public int labyrinthWidth;
    static public Vector2 position = new Vector2(-8.5f, 4.85f);

    static public LabyrinthPath labyrinthPath = Resources.Load<LabyrinthPath>("LabyrinthPath");
    static public LabyrinthPath[,] labyrinth;

    static public int[,] wallsSets;
}

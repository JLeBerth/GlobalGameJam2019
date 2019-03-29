﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NewTileMap
{
    [MenuItem("GameObject/Tile Map")]
    public static void CreateTileMap()
    {
        GameObject go = new GameObject("Tile Map");
        go.AddComponent<TileMap>();
    }
}
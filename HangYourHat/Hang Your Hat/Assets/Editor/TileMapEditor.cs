using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{

    public TileMap map;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        map.mapSize = EditorGUILayout.Vector2Field("Map Size", map.mapSize);
        map.texture2D = (Texture2D)EditorGUILayout.ObjectField("Texture2D", map.texture2D, typeof(Texture2D), false);
        if(map.texture2D==null)
        {
            EditorGUILayout.HelpBox("No Texture has been selected yet", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.LabelField("Tile Size:", map.tileSize.x + "x" + map.tileSize.y);
            EditorGUILayout.LabelField("Grid Size In Units", map.gridSize.x + "x" + map.gridSize.y);
            EditorGUILayout.LabelField("Pixels To Units:", map.pixelstToUnits.ToString());
        }
        EditorGUILayout.EndVertical();
    }

    private void OnEnable()
    {
        map = target as TileMap;
        Tools.current = Tool.View;
        if(map.texture2D !=null)
        {
            var path = AssetDatabase.GetAssetPath(map.texture2D);
            map.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

            var sprite = (Sprite)map.spriteReferences[1];
            var width = sprite.textureRect.width;
            var height = sprite.textureRect.height;

            map.tileSize = new Vector2(width, height);

            map.pixelstToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);

            map.gridSize = new Vector2((width / map.pixelstToUnits) * map.mapSize.x, (height / map.pixelstToUnits) * map.mapSize.y);
        }
    }
}

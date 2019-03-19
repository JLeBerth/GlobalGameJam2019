using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Vector2 mapSize = new Vector2(20, 10);
    public Texture2D texture2D;
    public Vector2 tileSize = new Vector2();
    public Object[] spriteReferences;
    public Vector2 gridSize = new Vector2();
    public int pixelstToUnits = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        var pos = transform.position;

        if(texture2D != null)
        {
            Gizmos.color = Color.white;
            var centerX = pos.x + (gridSize.x / 2);
            var centerY = pos.y - (gridSize.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridSize);
        }
    }
}

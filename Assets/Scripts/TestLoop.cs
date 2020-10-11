using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // FUCKIN TEST... GOING TO REST... GG

        /*
        Regarding the bounds and why you might get more tiles than you expect: Conceptually, Unity Tilemaps have an unlimited size.
        The cellBounds grow as needed when you paint tiles, but they don't shrink again if you erase them. So when your game has a well-defined map size,
        you might get some surprises if you ever slip while editing maps. There are three ways to work around this issue:

            1. Call tilemap.CompressBounds() to restore the bounds to the outmost tiles (hoping you remembered to erase them)
            2. create the bounds object yourself with new BoundsInt(origin, size) instead of relying on cellBounds.
            3. set tilemap.origin and tilemap.size to the desired values and then call tilemap.ResizeBounds().
         */
        Tilemap tilemap = GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

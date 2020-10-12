using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public PathTile[,] tiles;
    int width;
    int height;
    Queue<(int, int)> q;
    public GameObject FloorTilemap;
    private Tilemap tm;
    int offsetX, offsetY;

    // Start is called before the first frame update
    void Start()
    {
        tm = FloorTilemap.GetComponent<Tilemap>();
        BoundsInt bounds = tm.cellBounds;
        TileBase[] tileObjects = tm.GetTilesBlock(bounds);
        width = bounds.size.x;
        height = bounds.size.y;

        int minx = 1000000000;
        int miny = 1000000000;
        int maxy = -1000000000;
        int maxx = -1000000000;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                TileBase tile = tileObjects[x + y * width];
                if (tile != null) {
                    minx = Mathf.Min(minx, x);
                    miny = Mathf.Min(miny, y);
                    maxx = Mathf.Max(maxx, x);
                    maxy = Mathf.Max(maxy, y);
                }
            }
        }
        offsetX = minx;
        offsetY = miny;
        width = maxx - minx + 1;
        height = maxy - miny + 1;
        tiles = new PathTile[width, height];
        bool[,] isTile = new bool[width, height];

        for(int x = 0; x < bounds.size.x; x++) {
            for(int y = 0; y < bounds.size.y; y++) {
                TileBase tile = tileObjects[x + y * bounds.size.x];
                if(tile != null) {
                    isTile[x - minx, y - miny] = true;
                }
            }
        }

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                if(x > 0 && isTile[x - 1, y]) {
                    tiles[x, y].left = true;
                }
                if(x+1 < width && isTile[x + 1, y]) {
                    tiles[x, y].right = true;
                }
                if(y > 0 && isTile[x, y - 1]) {
                    tiles[x, y].down = true;
                }
                if(y+1 < height && isTile[x, y + 1]) {
                    tiles[x, y].up = true;
                }
            }
        }


        q = new Queue<(int, int)>(width * height);
    }

    private Vector2Int WorldToGrid(Vector2 pos) {
        var r = tm.WorldToCell(pos);
        return (Vector2Int)r; 
    }

    private Vector2 GridToWorld(Vector2Int pos) {
        return tm.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
    }

    public Vector2 ShortestPathDirection(Vector2 source, Vector2 dest) {
        var (s,t) = ShortestPathFirst(dest, source, 2);
        if (s >= 2) {
            //Debug.Log((t[1], t[0]));
            return (t[1] - t[0]).normalized;
        }
        else { 
            return Vector2.zero;
        }
    }

    //first n places in shortest path
    public (int, Vector2[]) ShortestPathFirst(Vector2 source, Vector2 dest, int n) { 
        source = WorldToGrid(source);
        dest = WorldToGrid(dest);
        (int, int) actualDest = ((int)dest.x, (int)dest.y);
        (int, int) actualSource = ((int)source.x, (int)source.y);
        (int, int)[,] prev = new (int, int)[width,height];
        bool[,] seen = new bool[width, height];
        q.Clear();
        prev[actualSource.Item1, actualSource.Item2] = (-1, -1);
        seen[actualSource.Item1, actualSource.Item2] = true;
        q.Enqueue(actualSource);
        while (q.Count > 0) {
            int x, y;
            (x, y) = q.Dequeue();

            if (tiles[x, y].right) {
                if (!seen[x + 1, y]) {
                    prev[x + 1, y] = (x, y);
                    seen[x + 1, y] = true;
                    if((x+1, y) == actualDest) {
                        break;
                    }
                    q.Enqueue((x + 1, y));
                }
            }
            if (tiles[x, y].left) {
                if (!seen[x - 1, y]) {
                    prev[x - 1, y] = (x, y);
                    seen[x - 1, y] = true;
                    if((x-1, y) == actualDest) {
                        break;
                    }
                    q.Enqueue((x - 1, y));
                }
            }
            if (tiles[x, y].up) {
                if (!seen[x, y + 1]) {
                    prev[x, y + 1] = (x, y);
                    seen[x, y + 1] = true;
                    if((x, y+1) == actualDest) {
                        break;
                    }
                    q.Enqueue((x, y + 1));
                }
            }
            if (tiles[x, y].down) {
                if (!seen[x, y - 1]) {
                    prev[x, y - 1] = (x, y);
                    seen[x, y - 1] = true;
                    if((x, y-1) == actualDest) {
                        break;
                    }
                    q.Enqueue((x, y - 1));
                }
            }
        }
        if(!seen[actualDest.Item1, actualDest.Item2]) {
            return (0, new Vector2[0]);
        }

        Vector2[] r = new Vector2[n];
        (int, int) cur = actualDest;
        for(int i = 0; i < n; i++) {
            if(cur == (-1, -1)) {
                return (i, r);
            }
            int x, y;
            (x, y) = cur;
            r[i] = GridToWorld(new Vector2Int(x, y));
            cur = prev[x, y];
        }
        return (n, r);

    }

}

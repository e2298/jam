using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapController : MonoBehaviour
{
    PathTile[,] tiles;
    int width;
    int height;
    Queue<(int, int)> q;

    // Start is called before the first frame update
    void Start()
    {
        q = new Queue<(int, int)>(width * height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 WorldToGrid(Vector2 pos) {
        return Vector2.zero;
    }

    private Vector2 GridToWorld(Vector2 pos) {
        return Vector2.zero;
    }

    public Vector2 ShortestPathDirection(Vector2 source, Vector2 dest) {
        var (s,t) = ShortestPathFirst(source, dest, 2);
        if (s >= 2) {
            return t[1] - t[0];
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

            if (tiles[x, y].up) {
                if (!seen[x + 1, y]) {
                    prev[x + 1, y] = (x, y);
                    if((x+1, y) == actualDest) {
                        break;
                    }
                    seen[x + 1, y] = true;
                    q.Enqueue((x + 1, y));
                }
            }
            if (tiles[x, y].down) {
                if (!seen[x - 1, y]) {
                    prev[x - 1, y] = (x, y);
                    if((x-1, y) == actualDest) {
                        break;
                    }
                    seen[x - 1, y] = true;
                    q.Enqueue((x - 1, y));
                }
            }
            if (tiles[x, y].right) {
                if (!seen[x, y + 1]) {
                    prev[x, y + 1] = (x, y);
                    if((x, y+1) == actualDest) {
                        break;
                    }
                    seen[x, y + 1] = true;
                    q.Enqueue((x, y + 1));
                }
            }
            if (tiles[x, y].left) {
                if (!seen[x, y - 1]) {
                    prev[x, y - 1] = (x, y);
                    if((x, y-1) == actualDest) {
                        break;
                    }
                    seen[x, y - 1] = true;
                    q.Enqueue((x, y - 1));
                }
            }
        }
        if(!seen[actualSource.Item1, actualSource.Item2]) {
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
            r[i] = GridToWorld(new Vector2(x, y));
            cur = prev[x, y];
        }
        return (n, r);

    }

}

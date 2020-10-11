using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
    public int[] excludedLayers;
    public float range;
    public GameObject target;
    public int samples = 1000;

    private Vector2[] polygon;
    private Color[] texPixels;
    private int includedLayerMask = 0;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        int excludedLayerMask = 0;
        foreach(int i in excludedLayers) {
            excludedLayerMask |= 1 << i;
        }
        includedLayerMask = -1;
        includedLayerMask ^= excludedLayerMask;

        polygon = new Vector2[samples];
        texPixels = new Color[Camera.main.pixelWidth * Camera.main.pixelHeight];

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        int width = cam.pixelWidth;
        int height = cam.pixelHeight;
        Texture2D tex = new Texture2D(width, height);

        for (int i = 0; i < width * height; i++) {
            texPixels[i] = Color.black;
        }

        
        Vector2 center = cam.WorldToScreenPoint(target.transform.position);

        Vector2 cur = Vector2.right;
        Vector2 step = new Vector2(Mathf.Cos(2f * Mathf.PI / (float)samples), Mathf.Sin(2f * Mathf.PI / (float) samples));

        for(int i = 0; i < samples; i++, cur = complexMul(cur, step)) {
            var cast = Physics2D.Raycast(target.transform.position, cur, range);
            if (cast.collider) {
                polygon[i] = cam.WorldToScreenPoint(cast.point);
            }
            else {
                polygon[i] = ((Vector2)transform.position + cur) * range;
            }
        }

        Vector2 source = cam.WorldToScreenPoint(target.transform.position);
        for(int i = 1; i < samples; i++) {
            drawTriangle(source, polygon[i], polygon[i-1], width);
        }
        

        tex.SetPixels(texPixels);
        tex.Apply();
        sr.sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.one / 2f);
    }

    void drawTriangle(Vector2 a, Vector2 b, Vector2 c, int width) {
        int maxX = (int)Mathf.Max(a.x, b.x, c.x);
        int minX = (int)Mathf.Min(a.x, b.x, c.x);
        int maxY = (int)Mathf.Max(a.y, b.y, c.y);
        int minY = (int)Mathf.Min(a.y, b.y, c.y);

        Vector2 vs1 = b - a;
        Vector2 vs2 = c - a;
        for(int x = minX; x <= maxX; x++) {
            for(int y = minY; y <= maxY; y++) {
                Vector2 q = new Vector2(x - a.y, y - a.y);

                float d = crossP(a, b);
                float s = (float)crossP(q, b) / d;
                float t = (float)crossP(a, q) / d;

                if ((s >= 0 ) && (t >= 0) && (s + t <= 1))
                {
                    texPixels[y * width + x] = new Color(0, 0, 0, 0);
                }
             }
        }
    }

    float crossP(Vector2 a, Vector2 b) {
        return a.x * b.y - a.y * b.x;
    }

    Vector2 complexMul(Vector2 a, Vector2 b) {
        return new Vector2(a.x * b.x - a.y * b.y, a.x * b.y + a.y * b.x);
    }
}

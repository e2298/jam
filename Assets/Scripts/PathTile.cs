using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathTile {
    public int dirmask;

    public bool left {
        get {
            return (dirmask & 1) != 0;
        }
        set {
            if (value) {
                dirmask |= 1;
            }
            else {
                dirmask ^= dirmask & 1;
            }
        }
    }

    public bool right {
        get {
            return (dirmask & 2) != 0;
        }
        set {
            if (value) {
                dirmask |= 2;
            }
            else {
                dirmask ^= dirmask & 2;
            }
        }
    }

    public bool down {
        get {
            return (dirmask & 4) != 0;
        }
        set {
            if (value) {
                dirmask |= 4;
            }
            else {
                dirmask ^= dirmask & 4;
            }
        }
    }

    public bool up {
        get {
            return (dirmask & 8) != 0;
        }
        set {
            if (value) {
                dirmask |= 8;
            }
            else {
                dirmask ^= dirmask & 8;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public struct FieldPattern {
    public int x;
    public int y;
    public DotScript.Type type;

    public FieldPattern(int x, int y, DotScript.Type type)
    {
        this.x    = x;
        this.y    = y;
        this.type = type;
    }
}

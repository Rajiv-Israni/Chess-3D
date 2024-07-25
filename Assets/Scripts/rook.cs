using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rook : chessman
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool[,] move_possible()
    {
        bool[,] a = new bool[8, 8];
        chessman c;
        int i;

        // Forward
        i = currenty;
        while (true)
        {
            i++;
            if (i >= 8) break;
            c = manager.Instance.chessmans[currentx, i];
            if (c == null) a[currentx, i] = true;
            else
            {
                if (c.iswhite != iswhite) a[currentx, i] = true;
                break;
            }
        }

        // Back
        i = currenty;
        while (true)
        {
            i--;
            if (i < 0) break;
            c = manager.Instance.chessmans[currentx, i];
            if (c == null) a[currentx, i] = true;
            else
            {
                if (c.iswhite != iswhite) a[currentx, i] = true;
                break;
            }
        }

        // Left
        i = currentx;
        while (true)
        {
            i--;
            if (i < 0) break;
            c = manager.Instance.chessmans[i, currenty];
            if (c == null) a[i, currenty] = true;
            else
            {
                if (c.iswhite != iswhite) a[i, currenty] = true;
                break;
            }
        }

        // Right
        i = currentx;
        while (true)
        {
            i++;
            if (i >= 8) break;
            c = manager.Instance.chessmans[i, currenty];
            if (c == null) a[i, currenty] = true;
            else
            {
                if (c.iswhite != iswhite) a[i, currenty] = true;
                break;
            }
        }

        return a;
    }
}

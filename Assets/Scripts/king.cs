using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class king : chessman
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
        int i, j;

        

        // Left
        if (currentx > 0)
        {
            c = manager.Instance.chessmans[currentx - 1, currenty];
            if (c == null) a[currentx - 1, currenty] = true;
            else if (c.iswhite != iswhite) a[currentx - 1, currenty] = true;
        }

        // Right
        if (currentx < 7)
        {
            c = manager.Instance.chessmans[currentx + 1, currenty];
            if (c == null) a[currentx + 1, currenty] = true;
            else if (c.iswhite != iswhite) a[currentx + 1, currenty] = true;
        }

        // Top
        i = currentx - 1;
        j = currenty + 1;
        if (currenty < 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = manager.Instance.chessmans[i, j];
                    if (c == null) a[i, j] = true;
                    else if (c.iswhite != iswhite) a[i, j] = true;
                }
                i++;
            }
        }

        // Bottom
        i = currentx - 1;
        j = currenty - 1;
        if (currenty > 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = manager.Instance.chessmans[i, j];
                    if (c == null) a[i, j] = true;
                    else if (c.iswhite != iswhite) a[i, j] = true;
                }
                i++;
            }
        }

        return a;
    }
}

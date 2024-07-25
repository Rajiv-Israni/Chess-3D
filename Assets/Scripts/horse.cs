using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horse : chessman
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

        // Up / Left
        KnightMove(currentx - 1, currenty + 2, ref a);
        KnightMove(currentx - 2, currenty + 1, ref a);

        // Up / Right
        KnightMove(currentx + 1, currenty + 2, ref a);
        KnightMove(currentx + 2, currenty + 1, ref a);

        // Down / Left
        KnightMove(currentx - 1, currenty - 2, ref a);
        KnightMove(currentx - 2, currenty - 1, ref a);

        // Down / Right
        KnightMove(currentx + 1, currenty - 2, ref a);
        KnightMove(currentx + 2, currenty - 1, ref a);

        return a;
    }

    public void KnightMove(int x, int y, ref bool[,] r)
    {
        chessman c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = manager.Instance.chessmans[x, y];
            if (c == null) r[x, y] = true;
            else if (c.iswhite != iswhite) r[x, y] = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawn : chessman
{
    void start()
    {
    }

    void update()
    {
    }

    public override bool[,] move_possible()
    {
        bool[,] a = new bool[8, 8];
        chessman c, c2;
        int[] m = manager.Instance.enpassantmove;

        if (iswhite)
        {
            
            //Debug.Log(currentx);
            //Debug.Log(currenty);
            // Diagonal Left
            if (currentx != 0 && currenty != 7)
            {

                if (m[0] == currentx - 1 && m[1] == currenty + 1)
                    a[currentx - 1, currenty + 1] = true;

                c = manager.Instance.chessmans[currentx - 1, currenty + 1];
                if (c != null && !c.iswhite) 
                    a[currentx - 1, currenty + 1] = true;
            }

            // Diagonal Right
            if (currentx != 7 && currenty != 7)
            {

                if (m[0] == currentx + 1 && m[1] == currenty + 1)
                    a[currentx + 1, currenty + 1] = true;

                c = manager.Instance.chessmans[currentx + 1, currenty + 1];
                if (c != null && !c.iswhite) 
                    a[currentx + 1, currenty + 1] = true;
            }

            // Forward
            if (currenty != 7)
            {
                c = manager.Instance.chessmans[currentx, currenty + 1];
                if (c == null) 
                    a[currentx, currenty + 1] = true;
            }
            // Two Steps Forward
            if (currenty == 1)
            {
                c = manager.Instance.chessmans[currentx, currenty + 1];
                c2 = manager.Instance.chessmans[currentx, currenty + 2];
                if (c == null && c2 == null) 
                    a[currentx, currenty + 2] = true;
            }
        }
        else
        {
            
            // Diagonal Left
            if (currentx != 0 && currenty != 0)
            {

                if (m[0] == currentx - 1 && m[1] == currenty - 1)
                    a[currentx - 1, currenty - 1] = true;

                c = manager.Instance.chessmans[currentx - 1, currenty - 1];
                if (c != null && c.iswhite) 
                    a[currentx - 1, currenty - 1] = true;
            }

            // Diagonal Right
            if (currentx != 7 && currenty != 0)
            {

                if (m[0] == currentx + 1 && m[1] == currenty - 1)
                    a[currentx + 1, currenty - 1] = true;

                c = manager.Instance.chessmans[currentx + 1, currenty - 1];
                if (c != null && c.iswhite) 
                    a[currentx + 1, currenty - 1] = true;
            }

            // Forward
            if (currenty != 0)
            {
                c = manager.Instance.chessmans[currentx, currenty - 1];
                if (c == null) 
                    a[currentx, currenty - 1] = true;
            }

            // Two Steps Forward
            if (currenty == 6)
            {
                c = manager.Instance.chessmans[currentx, currenty - 1];
                c2 = manager.Instance.chessmans[currentx, currenty - 2];
                if (c == null && c2 == null) 
                    a[currentx, currenty - 2] = true;
            }
        }


        return a;
    }
}

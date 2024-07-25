using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class chessman : MonoBehaviour
{
    public int currentx { set; get; }
    public int currenty { set; get; }
    public bool iswhite;
    //public Animator pawnanimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setpos(int x, int y)
    {
        currentx = x;
        currenty = y;

    }

    public virtual bool[,] move_possible()
    {
        return new bool[8,8];
    }
}

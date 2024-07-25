using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class manager : MonoBehaviour
{
    public static manager Instance { set; get; }
    private bool[,] allowedmoves { set; get; }

    public bool iswhiteturn = true;

    public chessman[,] chessmans { set; get; }
    private chessman selectedchessman;

    private const float sizeoftile = 1.0f;
    private const float offset = 0.5f;

    public List<GameObject> pieces;
    private List<GameObject> activepieces;

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    private int selectionX = -1;
    private int selectionY = -1;

    private Animator bishopanimator;
    private Animator horseanimator;
    private Animator kinganimator;
    private Animator queenanimator;
    private Animator rookanimator;
    

    

    //private Material previous;
    //public Material selected;

    public int[] enpassantmove { set; get; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;


        activepieces = new List<GameObject>();
        chessmans = new chessman[8, 8];
        enpassantmove = new int[2] { -1, -1 };

        //black
        //king
        spawnpiece(0, 3, 0);

        //queen
        spawnpiece(1, 4, 0);

        //rooks
        spawnpiece(2, 0, 0);
        spawnpiece(2, 7, 0);

        //bishops
        spawnpiece(3, 2, 0);
        spawnpiece(3, 5, 0);

        //knights
        spawnpiece(4, 1, 0);
        spawnpiece(4, 6, 0);

        //pawns
        for (int i = 0; i < 8; i++)
        {
            spawnpiece(5, i, 1);
        }

        //white
        //king
        spawnpiece(6, 4, 7);

        //queen
        spawnpiece(7, 3, 7);

        //rooks
        spawnpiece(8, 0, 7);
        spawnpiece(8, 7, 7);

        //bishops
        spawnpiece(9, 2, 7);
        spawnpiece(9, 5, 7);

        //knights
        spawnpiece(10, 1, 7);
        spawnpiece(10, 6, 7);

        //pawns
        for (int i = 0; i < 8; i++)
        {
            spawnpiece(11, i, 6);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("boardplane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }



        Vector3 widthline = Vector3.right * 8;
        Vector3 heightline = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthline);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightline);
            }

        }

        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX, Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(Vector3.forward * (selectionY + 1) + Vector3.right * selectionX, Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }


        control();
    }


    private void selectpiece(int x, int y)
    {
        if(chessmans[x,y] == null)
        {
            return;
        }
        else if(chessmans[x, y].iswhite != iswhiteturn)
        {
            return;
        }

        bool hasamove = false;
        allowedmoves = chessmans[x, y].move_possible();
        for(int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(allowedmoves[i,j])
                {
                    hasamove = true;
                }
            }
        }
        if (!hasamove)
            return;

        
        selectedchessman = chessmans[x, y];
        boardhighlights.Instance.highlightallowedmoves(allowedmoves);
    }

    private void movepiece(int x, int y)
    {
        if (allowedmoves[x, y])
        {
            chessman c = chessmans[x, y];


            if (c != null && c.iswhite != iswhiteturn)
            {
                if (c.GetType() == typeof(king))
                {
                    endgame();
                    return;
                }
                
                
                activepieces.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            if (x == enpassantmove[0] && y == enpassantmove[1])
            {
                if (iswhiteturn)
                {
                    c = chessmans[x, y - 1];
                    //attack = true;
                    //pawnanimator.SetBool("attack", attack);
                    activepieces.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }
                else
                {
                    c = chessmans[x, y + 1];
                    //attack = true;
                    //pawnanimator.SetBool("attack", attack);
                    activepieces.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }
            }

            enpassantmove[0] = -1;
            enpassantmove[1] = -1;

            if (selectedchessman.GetType() == typeof(pawn))
            {
                if (y == 7)
                {
                    activepieces.Remove(selectedchessman.gameObject);
                    Destroy(selectedchessman.gameObject);
                    spawnpiece(1, x, y);
                    selectedchessman = chessmans[x, y];
                }

                else if (y == 0)
                {
                    activepieces.Remove(selectedchessman.gameObject);
                    Destroy(selectedchessman.gameObject);
                    spawnpiece(7, x, y);
                    selectedchessman = chessmans[x, y];
                }

                if (selectedchessman.currenty == 1 && y == 3)
                {
                    enpassantmove[0] = x;
                    enpassantmove[1] = y - 1;
                }
                else if (selectedchessman.currenty == 6 && y == 4)
                {
                    enpassantmove[0] = x;
                    enpassantmove[1] = y + 1;
                }
            }

            chessmans[selectedchessman.currentx, selectedchessman.currenty] = null;
            selectedchessman.transform.position = gettilecenter(x, y);
            selectedchessman.setpos(x, y);
            chessmans[x, y] = selectedchessman;
            iswhiteturn = !iswhiteturn;
        }

        //selectedchessman.GetComponent<MeshRenderer>().material = previous;

        boardhighlights.Instance.hidehighlights();
        selectedchessman = null;
    }

    private void spawnpiece(int index, int x, int y)
    {
        if(index > 5)
        {
            GameObject piece = Instantiate(pieces[index], gettilecenter(x, y), orientation) as GameObject;
            piece.transform.SetParent(transform);
            chessmans[x, y] = piece.GetComponent<chessman>();
            chessmans[x, y].setpos(x, y);
            activepieces.Add(piece);
        }
        else
        {
            GameObject piece = Instantiate(pieces[index], gettilecenter(x, y), Quaternion.identity) as GameObject;
            piece.transform.SetParent(transform);
            chessmans[x, y] = piece.GetComponent<chessman>();
            chessmans[x, y].setpos(x, y);
            activepieces.Add(piece);
        }
        
    }

    private Vector3 gettilecenter(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (sizeoftile * x + offset);
        origin.z += (sizeoftile * z + offset);
        return origin;
    }

    private void endgame()
    {
        if (iswhiteturn)
        {
            Debug.Log("White team wins");
        }
        else
        {
            Debug.Log("Black team wins");
        }
        foreach(GameObject piece in activepieces)
        {
            Destroy(piece);
        }

        iswhiteturn = true;

        boardhighlights.Instance.hidehighlights();

        activepieces = new List<GameObject>();
        chessmans = new chessman[8, 8];
        enpassantmove = new int[2] { -1, -1 };

        //black
        //king
        spawnpiece(0, 3, 0);

        //queen
        spawnpiece(1, 4, 0);

        //rooks
        spawnpiece(2, 0, 0);
        spawnpiece(2, 7, 0);

        //bishops
        spawnpiece(3, 2, 0);
        spawnpiece(3, 5, 0);

        //knights
        spawnpiece(4, 1, 0);
        spawnpiece(4, 6, 0);

        //pawns
        for (int i = 0; i < 8; i++)
        {
            spawnpiece(5, i, 1);
        }

        //white
        //king
        spawnpiece(6, 4, 7);

        //queen
        spawnpiece(7, 3, 7);

        //rooks
        spawnpiece(8, 0, 7);
        spawnpiece(8, 7, 7);

        //bishops
        spawnpiece(9, 2, 7);
        spawnpiece(9, 5, 7);

        //knights
        spawnpiece(10, 1, 7);
        spawnpiece(10, 6, 7);

        //pawns
        for (int i = 0; i < 8; i++)
        {
            spawnpiece(11, i, 6);
        }
    }

    public void control()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedchessman == null)
                {
                    selectpiece(selectionX, selectionY);
                }
                else
                {
                    movepiece(selectionX, selectionY);
                }
            }
        }
    }


}

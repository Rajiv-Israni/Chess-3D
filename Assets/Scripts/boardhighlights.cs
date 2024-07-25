using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardhighlights : MonoBehaviour
{

    public static boardhighlights Instance { set; get; }

    public GameObject highlightpiece;
    private List<GameObject> highlights;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject gethighlightpiece()
    {
        GameObject piece = highlights.Find(g => !g.activeSelf);
        if (piece == null)
        {
            piece = Instantiate(highlightpiece);
            highlights.Add(piece);
        }
        return piece;
    }

    public void highlightallowedmoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject piece = gethighlightpiece();
                    piece.SetActive(true);
                    piece.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void hidehighlights()
    {
        foreach (GameObject piece in highlights)
            piece.SetActive(false);
    }
}

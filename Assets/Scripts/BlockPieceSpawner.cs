using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPieceSpawner : MonoBehaviour
{
    public BlockPiece[] blockPieceList;
    public GameObject blockPrefab;
    public int count = 0;
    public GameObject[] pieceParent;

    public GameObject[] pieceButton;

    private int pieceRow = 0;

    public bool active = true;
    public MapBuilder mb;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        mb = FindObjectOfType<MapBuilder>();
        //WaitForEndOfFrame
        GenerateThreePieces();
    }
    private void Update()
    {
        if (active)
        {
            for (int i = 0; i < 3; i++)
            {
                PiecesToButtonPos(pieceParent[i], pieceButton[i]);
            }
        }
        if(count == pieceRow)
        {
            GenerateThreePieces();
        }
    }
    public void GenerateThreePieces()
    {
        for (int i = 0; i < 3; i++)
        {
            if(blockPieceList.Length>pieceRow)
            {
                GeneratePiece(blockPieceList[pieceRow], pieceParent[i].transform);
                pieceRow++;
            }    
        }    
    }

    void PiecesToButtonPos(GameObject parent, GameObject button)
    {
        parent.transform.position = button.transform.position;
    }


    void GeneratePiece(BlockPiece piece, Transform parent)
    {
        //Set Pivot Point
        Vector3 startPos = Vector3.zero;
        float totalPoints = 0;
        for (int y = 0; y < piece.layers.Length; y++)
        {
            for (int x = 0; x < piece.layers[y].columns.Length; x++)
            {
                for (int z = 0; z < piece.layers[y].columns[x].rows.Length; z++)
                {
                    if (piece.layers[y].columns[x].rows[z])
                    {
                        startPos += new Vector3(x, y, z);
                        totalPoints++;
                    }
                }
            }
        }
        if (totalPoints > 0)
        {
            startPos /= totalPoints;
        }
        parent.transform.position = startPos;
        for(int y=0; y<piece.layers.Length; y++)
        {
            for(int x=0; x<piece.layers[y].columns.Length; x++)
            {
                for(int z=0; z<piece.layers[y].columns[x].rows.Length; z++)
                {
                    if (piece.layers[y].columns[x].rows[z])
                    {
                        GameObject prefab = Instantiate(blockPrefab, new Vector3(x, y * 0.2f, z), Quaternion.identity, parent);
                    }
                }
            }
        }
    }

    /*IEnumerator ToPos()
    {
        yield return new WaitForSeconds(0.2f);
        PiecesToButtonPos(pieceParent1, pieceButton1);
        PiecesToButtonPos(pieceParent2, pieceButton2);
        PiecesToButtonPos(pieceParent3, pieceButton3);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point{
    public int x;
    public int y;
}

public class MapBuilder : MonoBehaviour
{
    [Header("Board Size")]
    [Range(2, 8)]
    public int boardX;
    [Range(2, 8)]
    public int boardY;
    [Header("Board Colors")]
    [Space(5)]
    public Color lightColor;
    public Color darkColor;
    [Space(15)]
    public GameObject boardBlock;
    public GameObject[,,] map;
    public Vector3 startPos;
    public float blockLenght;
    public GameObject boardParent;
    public GameObject cam;
    private BoardRotate br;
    public BlockPieceSpawner bps;
    // Start is called before the first frame update
    void Start()
    {
        bps = FindObjectOfType<BlockPieceSpawner>();
        GenerateMap(boardX, boardY);
        br = FindObjectOfType<BoardRotate>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap(int boardX, int boardY)
    {
        SetStartPos(boardX, boardY);
        map = new GameObject[boardX, 4, boardY];
        for (int i=0; i < boardX; i++)
        {
            for(int a=0; a<boardY; a++)
            {
                map[i, 0, a] = Instantiate(boardBlock, startPos + new Vector3(i * blockLenght, 0, a * blockLenght), Quaternion.identity, boardParent.transform);
                if (i % 2 == a % 2)
                {
                    map[i, 0,a].GetComponent<Renderer>().material.SetColor("_BaseColor", lightColor);
                }
                else
                {
                    map[i, 0,a].GetComponent<Renderer>().material.SetColor("_BaseColor", darkColor);
                }

            }
        }
        cam.transform.rotation = Quaternion.Euler(40, 45, 0);
        cam.transform.position = new Vector3(-boardX, 8, -boardY);
        //bps.active = true;
    }

    private void SetStartPos(int xBoard, int yBoard)
    {
        startPos = Vector3.zero;
        if (xBoard % 2 == 0)
        {
            startPos = new Vector3((-xBoard / 2) + (blockLenght / 2), 0, 0);
        }
        else
        {
            startPos = new Vector3(-xBoard / 2, 0, 0);
        }

        if (yBoard % 2 == 0)
        {
            startPos = new Vector3(startPos.x, 0, (-yBoard / 2) + (blockLenght / 2));
        }
        else
        {
            startPos = new Vector3(startPos.x, 0, -yBoard / 2);
        }
    }

    public void ChangeColorMap(int x, int y, int color, int layer)
    {
        map[x, layer, y].GetComponent<BlockColorManager>().ChangeColorToDestroy(color);
    }

    public void ChangeColor(float x,float y,int color,int layer)
    {
        switch (br.angle)
        {
            case 0:
                map[(int)(Mathf.Round(x - startPos.x)), layer, (int)(Mathf.Round(y - startPos.z))].GetComponent<BlockColorManager>().ChangeColor(color);
                break;
            case 90:
                map[(int)(Mathf.Round(-y - startPos.z)), layer, (int)(Mathf.Round(x - startPos.x))].GetComponent<BlockColorManager>().ChangeColor(color);

                break;
            case 180:
                map[(int)(Mathf.Round(-x - startPos.x)), layer, (int)(Mathf.Round(-y - startPos.z))].GetComponent<BlockColorManager>().ChangeColor(color);
                break;
            case 270:
                map[(int)(Mathf.Round(y - startPos.z)), layer, (int)(Mathf.Round(-x - startPos.x))].GetComponent<BlockColorManager>().ChangeColor(color);           
                break;
        }
    }

    public void BackToOldColorP()
    {
        for (int i = 0; i < boardX; i++)
        {
            
            for (int a = 0; a < boardY; a++)
            {
                if(map[i, 1, a] != null)
                {
                    map[i, 1, a].GetComponent<BlockColorManager>().BackToStartColor();
                }
            }
        }
    }
    public void BackToOldColor()
    {
        foreach(Transform child in boardParent.transform)
        {
            child.gameObject.GetComponent<BlockColorManager>().BackToStartColor();
        }
    }
    public bool PositionCheck(float x,float y)
    {
        switch (br.angle)
        {
            case 0:
                if ((int)(Mathf.Round(x - startPos.x)) < 0 || (int)(Mathf.Round(x - startPos.x)) >= boardX || (int)(Mathf.Round(y - startPos.z)) < 0 || (int)(Mathf.Round(y - startPos.z)) >= boardY)
                {
                    return false;
                }
                break;
            case 90:
                if ((int)(Mathf.Round(-y - startPos.z)) < 0 || (int)(Mathf.Round(-y - startPos.z)) >= boardX || (int)(Mathf.Round(x - startPos.x)) < 0 || (int)(Mathf.Round(x - startPos.x)) >= boardY)
                {
                    return false;
                }
                break;
            case 180:
                if ((int)(Mathf.Round(-x - startPos.x)) < 0 || (int)(Mathf.Round(-x - startPos.x)) >= boardX || (int)(Mathf.Round(-y - startPos.z)) < 0 || (int)(Mathf.Round(-y - startPos.z)) >= boardY)
                {
                    return false;
                }
                break;
            case 270:
                if ((int)(Mathf.Round(y - startPos.z)) < 0 || (int)(Mathf.Round(y - startPos.z)) >= boardX || (int)(Mathf.Round(-x - startPos.x)) < 0 || (int)(Mathf.Round(-x - startPos.x)) >= boardY)
                {
                    return false;
                }
                break;
        }
        return true;
    }

  


    public int RXtoMX(float x,float y)
    {
        switch (br.angle)
        {
            case 0:
                
                    return (int)(Mathf.Round(x - startPos.x));

            case 90:
                
                    return (int)(Mathf.Round(-y - startPos.z));
                
                
            case 180:
                
                    return (int)(Mathf.Round(-x - startPos.x));
                
               
            case 270:
                return (int)(Mathf.Round(y - startPos.z));
        }
        return -1;
    }

    public int RYtoMY(float x, float y)
    {
        switch (br.angle)
        {
            case 0:
                return (int)(Mathf.Round(y - startPos.z));
                
            case 90:
                return (int)(Mathf.Round(x - startPos.x));
            case 180:
                return (int)(Mathf.Round(-y - startPos.z));
            case 270:
                return (int)(Mathf.Round(-x - startPos.x));

        }
        return -1;
    }

    private bool contain(List<Point> p,Point a)
    {
        foreach(Point temp in p)
        {
            if(temp.x==a.x && temp.y == a.y)
            {
                return true;
            }
        }
        return false;
    }

    public List<Point> CheckMatch(List<Point> ghost)
    {
        bool check;
        List<Point> changeColor = new List<Point>();
        
        for (int i = 0; i < boardX; i++)
        {
            check = true;
            for (int a = 0; a < boardY; a++)
            {
                Point temp = new Point();
                temp.x = i;
                temp.y = a;
                if (map[i, 1, a] == null && !contain(ghost,temp))
                {
                    check = false;
                }
            }
            if (check)
            {
                for (int a = 0; a < boardY; a++)
                {
                    Point temp = new Point();
                    temp.x = i;
                    temp.y = a;
                    if (map[i, 1, a] != null)
                    {
                        changeColor.Add(temp);
                    }
                }
            }
        }

        for (int a = 0; a < boardY; a++)
        {
            check = true;
            for (int i = 0; i < boardX; i++)
            {
                Point temp = new Point();
                temp.x = i;
                temp.y = a;
                if (map[i, 1, a] == null && !contain(ghost, temp))
                {
                    check = false;
                }
            }
            if (check)
            {
                for (int i = 0; i < boardX; i++)
                {
                    Point temp = new Point();
                    temp.x = i;
                    temp.y = a;
                    if(map[i, 1, a] != null)
                    {
                        changeColor.Add(temp);
                    }
                }
            }
        }

        return changeColor;
    }

    private void MapDown(int x,int y)
    {
        map[x, 1, y].GetComponent<DestroyBlocks>().destroy = true;
        map[x, 1, y] = null;
        for (int i=1; i<3 && map[x, i+1, y] != null ; i++)
        {
            map[x, i, y] = map[x, i + 1, y];
            map[x, i + 1, y] = null;
            map[x, i, y].transform.position += Vector3.down * 0.2f;
        }
    }


    public void DestroyMatches()
    {
        bool check;
        List<GameObject> willDelete = new List<GameObject>();
        List<Point> willDown = new List<Point>();
        Point temp;
        for (int i = 0; i < boardX; i++)
        {
            check = true;
            for (int a = 0; a < boardY; a++)
            {   
                if (map[i, 1, a] == null )
                {
                    check = false;
                }
            }
            if (check)
            {
                for (int a = 0; a < boardY; a++)
                {
                   
                    if (map[i, 1, a] != null)
                    {
                        temp = new Point();
                        temp.x = i;
                        temp.y = a;
                        //willDelete.Add(map[i, 1, a]);
                        willDown.Add(temp);
                    }
                }
            }
        }

        for (int a = 0; a < boardY; a++)
        {
            check = true;
            for (int i = 0; i < boardX; i++)
            {
                
                if (map[i, 1, a] == null )
                {
                    check = false;
                }
            }
            if (check)
            {
                for (int i = 0; i < boardX; i++)
                {
                    
                    if (map[i, 1, a] != null)
                    {

                        temp = new Point();
                        temp.x = i;
                        temp.y = a;
                        //willDelete.Add(map[i, 1, a]);
                        willDown.Add(temp);
                    }

                }
            }
        }
        /*int tempi = willDelete.Count;
        if (willDelete.Count > 0)
        {
            for(int i=0; i<tempi; i++)
            {
                willDelete[i].GetComponent<DestroyBlocks>().destroy = true;
            }
        }*/
        if (willDown.Count > 0)
        {
            foreach(Point p in willDown)
            {
                MapDown(p.x, p.y);
            }
            DestroyMatches();
        }  
    }

    public bool Check(float x, float y,int layer)
    {
        
        if (!PositionCheck(x, y))
        {
            
            return false;
        }
        switch (br.angle)
        {
            case 0:

                if (map[(int)(Mathf.Round(x - startPos.x)), layer, (int)(Mathf.Round(y - startPos.z))] == null)
                {
                   
                    return false;     
                }
                break;
            case 90:

                if (map[(int)(Mathf.Round(-y - startPos.z)), layer, (int)(Mathf.Round(x - startPos.x))] == null)
                {
                    return false;
                }
                break;
            case 180:

                if (map[(int)(Mathf.Round(-x - startPos.x)), layer, (int)(Mathf.Round(-y - startPos.z))] == null)
                {
                    return false;
                }
                break;
            case 270:

                if (map[(int)(Mathf.Round(y - startPos.z)), layer, (int)(Mathf.Round(-x - startPos.x))] == null)
                {
                    return false;
                }
                break;
        }
        return true;
    }


    public void AddObject(float x,float y,GameObject obj)
    {
        int i;
        switch (br.angle)
        {
            case 0:
                for (i = 1; map[(int)(Mathf.Round(x - startPos.x)), i, (int)(Mathf.Round(y - startPos.z))] != null; i++) ;
                map[(int)(Mathf.Round(x - startPos.x)), i , (int)(Mathf.Round(y - startPos.z))] = obj;
                obj.transform.SetParent(boardParent.transform);
                obj.transform.localPosition = new Vector3((int)(Mathf.Round(x - startPos.x)) + startPos.x, i*0.2f, (int)(Mathf.Round(y - startPos.z)) + startPos.z);
                break;
            case 90:
                for (i = 1; map[(int)(Mathf.Round(-y - startPos.z)), i, (int)(Mathf.Round(x - startPos.x))] != null; i++) ;
                map[(int)(Mathf.Round(-y - startPos.z)), i,(int)(Mathf.Round(x - startPos.x))] = obj;
                obj.transform.SetParent(boardParent.transform);
                obj.transform.localPosition = new Vector3((int)(Mathf.Round(-y - startPos.z)) + startPos.z, i*0.2f,(int)(Mathf.Round(x - startPos.x)) + startPos.x);
                break;
            case 180:
                for (i = 1; map[(int)(Mathf.Round(-x - startPos.x)), i, (int)(Mathf.Round(-y - startPos.z))] != null; i++) ;
                map[(int)(Mathf.Round(-x - startPos.x)), i , (int)(Mathf.Round(-y - startPos.z))] = obj;
                obj.transform.SetParent(boardParent.transform);
                obj.transform.localPosition = new Vector3((int)(Mathf.Round(-x - startPos.x)) + startPos.x, i * 0.2f, (int)(Mathf.Round(-y - startPos.z)) + startPos.z);              
                break;
            case 270:
                for (i = 1; map[(int)(Mathf.Round(y - startPos.z)), i, (int)(Mathf.Round(-x - startPos.x))] != null; i++) ;
                map[(int)(Mathf.Round(y - startPos.z)), i, (int)(Mathf.Round(-x - startPos.x))] = obj;
                obj.transform.SetParent(boardParent.transform);
                obj.transform.localPosition = new Vector3((int)(Mathf.Round(y - startPos.z)) + startPos.z, i * 0.2f, (int)(Mathf.Round(-x - startPos.x)) + startPos.x);
                break;
        }

        
    }

}

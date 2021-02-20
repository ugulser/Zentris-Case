using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroupMovement : MonoBehaviour
{
    public GameObject blockGroup1;
    public GameObject blockGroup2;
    public GameObject blockGroup3;
    public BlockPiece blockObj1;
    public BlockPiece blockObj2;
    public BlockPiece blockObj3;
    public GameObject board;
    public Vector3 blockGroup1StartPos;
    public Vector3 blockGroup2StartPos;
    public Vector3 blockGroup3StartPos;
    private bool moving;
    private int whichGroup = 0;
    private Vector3 startPos;
    private BoardRotate br;
    private int matchedBlocks;
    public bool canCreate;
    public MapBuilder mb;
    private BlockPieceSpawner bpSpawner;
    // Start is called before the first frame update
    void Start()
    {
        br = FindObjectOfType<BoardRotate>();
        mb = FindObjectOfType<MapBuilder>();
        bpSpawner = FindObjectOfType<BlockPieceSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (whichGroup)
        {
            case 0:       
                return;
            case 1:
                MoveBlockGroup(blockGroup1);
                break;
            case 2:
                MoveBlockGroup(blockGroup2);
                break;
            case 3:
                MoveBlockGroup(blockGroup3);
                break;
        }

        if (moving)
        {
            if (Input.GetMouseButtonUp(0))
            {
                bpSpawner.active = true;
                moving = false;
                int a = 0;
                GameObject childObj = new GameObject();
                bool check = true;
                switch (whichGroup)
                {
                    case 1:
                        a = blockGroup1.transform.childCount;

                        for (int i = 0; i < a; i++)
                        {
                            childObj = blockGroup1.transform.GetChild(i).gameObject;
                            childObj.gameObject.GetComponent<Renderer>().material.SetFloat("_Alpha", 1f);
                        }

                        for (int i = 0; i < a; i++)
                        {
                            childObj = blockGroup1.transform.GetChild(i).gameObject;
                            if ( !mb.PositionCheck(childObj.transform.position.x, childObj.transform.position.z) || mb.Check(childObj.transform.position.x, childObj.transform.position.z,1))
                            {
                                blockGroup1.transform.position = startPos;
                                check = false;
                            }
                        }
                        if (check)
                        {
                            for (int i = 0; i < a; i++)
                            {
                                //Debug.Log("aa");
                                childObj = blockGroup1.transform.GetChild(0).gameObject;      
                                mb.AddObject(childObj.transform.position.x, childObj.transform.position.z, childObj.gameObject);
                                
                            }
                            mb.DestroyMatches();
                            blockGroup1.transform.position = startPos;
                            bpSpawner.count++;
                        }    
                        break;
                    case 2:
                        a= blockGroup2.transform.childCount;

                        for (int i = 0; i < a; i++)
                        {
                            childObj = blockGroup2.transform.GetChild(i).gameObject;
                            childObj.gameObject.GetComponent<Renderer>().material.SetFloat("_Alpha", 1f); 
                        }

                        for (int i = 0; i < a; i++)
                        {
                            childObj = blockGroup2.transform.GetChild(i).gameObject;
                            if (!mb.PositionCheck(childObj.transform.position.x, childObj.transform.position.z) || mb.Check(childObj.transform.position.x, childObj.transform.position.z,1))
                            {
                                blockGroup2.transform.position = startPos;
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            for (int i = 0; i < a; i++)
                            {
                                childObj = blockGroup2.transform.GetChild(0).gameObject;                  
                                mb.AddObject(childObj.transform.position.x, childObj.transform.position.z, childObj.gameObject);
                                
                            }
                            mb.DestroyMatches();
                            blockGroup2.transform.position = startPos;
                            bpSpawner.count++;
                        }                    
                        break;
                    case 3:
                        a= blockGroup3.transform.childCount;

                        for (int i = 0; i < a; i++)
                        {
                            childObj = blockGroup3.transform.GetChild(i).gameObject;
                            childObj.gameObject.GetComponent<Renderer>().material.SetFloat("_Alpha", 1f);
                        }

                        for (int i = 0; i < a; i++)
                        {
                            childObj = blockGroup3.transform.GetChild(i).gameObject;
                            if (!mb.PositionCheck(childObj.transform.position.x, childObj.transform.position.z) || mb.Check(childObj.transform.position.x, childObj.transform.position.z,1))
                            {
                                blockGroup3.transform.position = startPos;
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            for (int i = 0; i < a; i++)
                            {
                                childObj = blockGroup3.transform.GetChild(0).gameObject;
                                mb.AddObject(childObj.transform.position.x, childObj.transform.position.z, childObj.gameObject);
                                
                            }           
                            mb.DestroyMatches();
                            blockGroup3.transform.position = startPos;
                            bpSpawner.count++;
                        }                        
                        break;
                }
                //mb.DestroyMatches();
                whichGroup = 0;
                br.enabled = true;
                mb.BackToOldColor();
                
            }
        }
    }

    public void MoveBlock1()
    {
        if (!moving)
        {
            whichGroup = 1;
        }  
    }
    public void MoveBlock2()
    {
        if (!moving)
        {
            whichGroup = 2;
        }
    }
    public void MoveBlock3()
    {
        if (!moving)
        {
            whichGroup = 3;
        }
    }

    void MoveBlockGroup(GameObject blockGroup)
    {
        bpSpawner.active = false;
        br.enabled = false;
        if (!moving)
        {
            moving = true;
            startPos = blockGroup.transform.position;
        }  
        Plane plane = new Plane(Vector3.up, board.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            //Vector3 wantedPos = ray.GetPoint(distance) + Vector3.up * 1f;
            blockGroup.transform.position = ray.GetPoint(distance) + Vector3.up *2f;
            //blockGroup.transform.position = new Vector3(Mathf.Round(wantedPos.x), wantedPos.y, Mathf.Round(wantedPos.z));
        }

        foreach(Transform child in blockGroup.transform)
        {
            //child.gameObject.GetComponent<BlockColorManager>().ChangeColor(2);
            child.gameObject.GetComponent<Renderer>().material.SetFloat("_Alpha", 0.2f);
        }

        mb.BackToOldColor();
        int a = blockGroup.transform.childCount;
        GameObject childObj;
        List<Point> ghost = new List<Point>();
        List<Point> changeColor;

        bool check = true;
        bool match = false;
        for (int i = 0; i < a; i++)
        {

            childObj = blockGroup.transform.GetChild(i).gameObject;
            Point temp = new Point();
            temp.x = mb.RXtoMX(childObj.transform.position.x, childObj.transform.position.z);
            temp.y = mb.RYtoMY(childObj.transform.position.x, childObj.transform.position.z);
            ghost.Add(temp);
            if (!mb.PositionCheck(childObj.transform.position.x, childObj.transform.position.z) || mb.Check(childObj.transform.position.x, childObj.transform.position.z, 1))
            {
                
                check = false;
                break;
            }
        }
        if (check)
        {
            foreach (Transform child in blockGroup.transform)//floor color change, shadow
            {
                if (mb.Check(child.transform.position.x, child.transform.position.z, 0))
                {
                    mb.ChangeColor(child.transform.position.x, child.transform.position.z, 0,0);
                }
            }
            changeColor = mb.CheckMatch(ghost);
            mb.BackToOldColorP();
            if (changeColor.Count > 0)
            {
                foreach (Point p in changeColor)
                {            
                   mb.ChangeColorMap(p.x, p.y, 2, 1);
                }

            }
        }
        else
        {
            
            foreach (Transform child in blockGroup.transform)//floor color change, shadow
            {
                if (mb.Check(child.transform.position.x, child.transform.position.z, 0))
                {
                    mb.ChangeColor(child.transform.position.x, child.transform.position.z, 1, 0);
                }
            } 

        }
        //CheckMatch(blockGroup);
    }

    /*void CheckMatch(GameObject blockGroup)
    {
        foreach (Transform child in blockGroup.transform)
        {
            RaycastHit hit;
            if(Physics.Raycast(child.position, Vector3.down, out hit))
            {
                if (hit.collider.gameObject.CompareTag("BaseBlock"))
                {
                    if (hit.collider.gameObject.GetComponent<SingleBlock>().isFull)
                    {
                        canCreate = false;
                        break;
                    }
                    else
                    {
                        canCreate = true;
                    }
                    hit.collider.gameObject.GetComponent<SingleBlock>().rays++;
                    StartCoroutine(hit.collider.gameObject.GetComponent<SingleBlock>().CheckMatch());
                }
                else
                {
                    canCreate = false;
                }
            }
            else
            {
                canCreate = false;
            }
        }*/
    }



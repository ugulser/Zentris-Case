using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockGroup")]

public class BlockPiece : ScriptableObject
{
    public static int X=4, Y=4,Z=3;
    [System.Serializable]
    public class Column
    {
        
        public bool[] rows = new bool[4];

    }
    [System.Serializable]
    public class Layer
    {
        public Column[] columns = new Column[4];
    }
    
    public Layer[] layers = new Layer[3];
        
    

    //public Layer[] layers = new Layer[3];
}

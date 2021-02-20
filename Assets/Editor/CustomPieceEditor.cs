using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(BlockPiece))]
public class CustomPieceEditor : Editor
{
    BlockPiece targetScript;
    void OnEnable()
    {
        targetScript = target as BlockPiece;
    }

    public override void OnInspectorGUI()
    {
        if (BlockPiece.X >0 && BlockPiece.Y > 0 && BlockPiece.Z >0)
        {
            for(int z = 0; z < BlockPiece.Z; z++)
            {
                GUILayout.Label("Layer" + z.ToString());
                EditorGUILayout.BeginHorizontal();

                for (int y = 0; y < BlockPiece.Y; y++)
                {
                    EditorGUILayout.BeginVertical();
                    for (int x = 0; x < BlockPiece.X; x++)
                    {
                        targetScript.layers[z].columns[x].rows[y] = EditorGUILayout.Toggle(targetScript.layers[z].columns[x].rows[y]);
                    }
                    EditorGUILayout.EndVertical();

                }
                EditorGUILayout.EndHorizontal();
            }
            
        }
        EditorUtility.SetDirty(targetScript);
    }
}
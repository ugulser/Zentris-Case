using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorManager : MonoBehaviour
{
    [SerializeField]
    private Color[] colors = new Color[4];
    private Color startColor;
    private bool checkingMatch;
    public bool isMatch;
    public int rays;
    public bool isFull;

    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startColor = GetComponent<Renderer>().material.GetColor("_BaseColor");

    }
    public void ChangeColor(int whichColor)
    {
        GetComponent<Renderer>().material.SetColor("_BaseColor", colors[whichColor]);
    }

    public void ChangeColorToDestroy(int whichColor)
    {
        GetComponent<Renderer>().material.SetColor("_BaseColor", colors[whichColor]);
        GetComponent<Renderer>().material.SetInt("_Active", 1);
    }

    public void BackToStartColor()
    {
        GetComponent<Renderer>().material.SetColor("_BaseColor", startColor);
        GetComponent<Renderer>().material.SetInt("_Active", 0);
    }

    public IEnumerator CheckMatch()
    {
        while (rays > 0)
        {
            ChangeColor(0);
            float newRays = rays;
            isMatch = true;
            yield return new WaitForSeconds(0.01f);
            if (newRays == rays)
            {
                isMatch = false;
                rays = 0;
                BackToStartColor();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotate : MonoBehaviour
{
    private Vector3 initialPos;
    private bool rotating;
    [SerializeField]
    private float rotateSpeed;
    public bool canRotate;
    public int angle;
    private void Start()
    {
        canRotate = true;
        angle = (int)(transform.localEulerAngles.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !rotating)
        {
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && !rotating)
        {
            Calculate(Input.mousePosition);
        }
        
    }

    void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        if (disX > 0)
        {
            if (initialPos.x > finalPos.x)
            {
                StartCoroutine(RotateBoard(90));
            }
            else
            {
                StartCoroutine(RotateBoard(-90));
            }
        }
    }

    IEnumerator RotateBoard(float rot)
    {
        rotating = true;
        Quaternion desiredRot = Quaternion.Euler(0, transform.eulerAngles.y + rot, 0);
        float elapsedTime = 0;
        float waitTime = rotateSpeed;
        while (elapsedTime < waitTime)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = desiredRot;
        rotating = false;
        angle = (int)(transform.localEulerAngles.y);
        yield return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlocks : MonoBehaviour
{
    public float speed;
    public bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            StartCoroutine(DestroyTime());
            transform.position += Vector3.down * speed * Time.deltaTime;
            GetComponent<BlockColorManager>().ChangeColor(3);
        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}

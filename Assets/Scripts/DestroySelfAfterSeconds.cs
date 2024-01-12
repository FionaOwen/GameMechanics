using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterSeconds : MonoBehaviour
{
    public float delayTimeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAfterSeconds", delayTimeToDestroy);
    }

    // Update is called once per frame
    public void DestroyAfterSeconds()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    private float lenght;
    private float StartPos;
    [SerializeField] private GameObject camera;
    [SerializeField] private float ParEffect;
    [SerializeField] private float offset;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dist = camera.transform.position.x * ParEffect;
        transform.position = new Vector3(StartPos + dist, camera.transform.position.y -offset, transform.position.z);

        float temp = camera.transform.position.x * (1-ParEffect);
        if(temp > StartPos + lenght)
        {
            StartPos += lenght;
        }
        else if(temp < StartPos - lenght)
        {
            StartPos -= lenght;
        }
    }
}

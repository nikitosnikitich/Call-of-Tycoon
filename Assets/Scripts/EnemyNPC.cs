using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyNPC : MonoBehaviour
{
    public AIPath path;
    public SpriteRenderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateNPC();
    }
    private void RotateNPC()
    {
        if(path.desiredVelocity.x >= 0.01f)
        {
            renderer.flipX = true;
        }
        else if(path.desiredVelocity.x <= -0.01f)
        {
            renderer.flipX = false;
        }
    }
}

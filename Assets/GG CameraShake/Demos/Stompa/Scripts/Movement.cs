using UnityEngine;

public class Movement : MonoBehaviour
{
    bool move;
    int stomps;
    int direction = 1;

    // Called by animator
    public void Move()
    {
        move = true;
    }

    // Called by animator
    public void StopMove()
    {
        move = false;
        stomps += 1;
        if (stomps > 7)
        {
            stomps = 0;
            direction = -direction;
        }
    }

    void Update()
    {
        if (move)
        {
            transform.position += transform.forward * Time.deltaTime * direction * 10;
        }
    }
}

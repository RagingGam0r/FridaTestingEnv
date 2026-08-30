using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public float speed = 3f;

    private Vector3 startPos;
    public Vector3 endPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Go from start->end, then end-> start
        float pingPong = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, endPos, pingPong);
    }
}
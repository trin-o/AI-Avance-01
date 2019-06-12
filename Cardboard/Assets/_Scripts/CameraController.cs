using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 min;
    Vector3 max;

    float x = 0.025f;

    void Awake()
    {
        min = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) + Vector3.down;
        max = new Vector3(50, min.y) - min;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(x, 0);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, min.x, max.x),
            Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector2 size = new Vector2(50, min.y);
        Gizmos.DrawWireCube((Vector3)size / 2, size);
    }
}

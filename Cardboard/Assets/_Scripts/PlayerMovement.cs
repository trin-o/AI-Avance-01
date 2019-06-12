using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement
    float horizontal;
    float vertical;

    private Vector3 min;
    private Vector3 max;

    Rigidbody body;

    float size; // 5

    // touch
    bool touch;

    Vector2 firstPosition;
    Vector2 secondPosition;

    Vector3 coord;

    Vector3 offset;

    void Start()
    {
        body = GetComponent<Rigidbody>();

        size = Camera.main.fieldOfView / 12.0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        float w = size * (16f / 9f);

        float min = -w + Camera.main.transform.position.x;
        float max = w + Camera.main.transform.position.x;

        // limit in camera
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min, max),Mathf.Clamp(transform.position.y, -size, size), transform.position.z);

        // mouse
        Vector3 clickPosition = -Vector3.one;

        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        // movemente
        if (Input.GetMouseButtonDown(0))
        {           
            offset = transform.position -  clickPosition;
        }
        else if (Input.GetMouseButton(0))
        {
            transform.position = (Vector2)clickPosition + (Vector2)offset;
        }
        
        //android
       /* if (Input.GetMouseButtonDown(0))
        {
            firstPosition = (Vector2)Input.mousePosition;
            swipe = true;
        }
        else if (Input.GetMouseButton(0))
        {
            secondPosition = (Vector2)Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipe = false;
        }

        if (swipe)
        {
            Vector2 currentSwipe = secondPosition - firstPosition;

            currentSwipe.Normalize();

            horizontal = currentSwipe.x;
            vertical = currentSwipe.y;

            //Debug.Log(currentSwipe);
        }
        else if (!swipe)
        {
            horizontal = 0;
            vertical = 0;
        }*/
    }

    void FixedUpdate()
    {
        // movement
        //body.velocity = new Vector2(horizontal, vertical) * 8;
    }

    enum PLAYER_STATE { PUSH, NO_PUSH, DEAD }
}

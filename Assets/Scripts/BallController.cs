using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;
    private bool controlsEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!controlsEnabled)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    public void DisableControls()
    {
        controlsEnabled = false;
    }

    public void EnableControls()
    {
        controlsEnabled = true;
    }
}
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float HorizontalInput;
    public float rotationSpeed;

    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * rotationSpeed * HorizontalInput * Time.deltaTime);
    }
}

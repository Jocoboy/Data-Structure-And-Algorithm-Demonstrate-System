using UnityEngine;

/// <summary>
///  Applied to cube.
/// <summary>

public class Cube : MonoBehaviour
{
    public float speed = 90f;

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}

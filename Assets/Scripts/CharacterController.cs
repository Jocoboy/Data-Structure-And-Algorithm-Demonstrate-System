using UnityEngine;

/// <summary>
///  This script is used to control the character.
/// <summary>

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 50f;
    //public float rotateSpeed = 20f;
    public float jumpSpeed = 20f;

    public Rigidbody rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 targetDirection = new Vector3(h, 0, v);
            float y = Camera.main.transform.rotation.eulerAngles.y;
            targetDirection = Quaternion.Euler(0, y, 0) * targetDirection; // Quaternion.Euler*Vector3(origin) => Vector3(after rotation)

            transform.Translate(targetDirection * Time.deltaTime * moveSpeed, Space.World);
        }

    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (true)
            {
                rg.velocity += new Vector3(0, 5, 0);
                rg.AddForce(Vector3.up * jumpSpeed);
                //isGround = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

  Rigidbody rb;
  [SerializeField] float mainThrust = 100f;
  [SerializeField] float rotation = 50f;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();

  }

  // Update is called once per frame
  void Update()
  {
    ProcessThrust();
    ProcessRotation();

  }
  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.W))
    {
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotation);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-(rotation));
    }
  }

  private void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; //Freezing rotation to manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false;
  }
}


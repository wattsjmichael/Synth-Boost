using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

  Rigidbody rb;
  AudioSource audioSource;
  ParticleSystem particle;
  [SerializeField] float mainThrust = 100f;
  [SerializeField] float rotation = 50f;
  [SerializeField] AudioClip mainEngine;
  [SerializeField] ParticleSystem mainBooster;
  [SerializeField] ParticleSystem rightBooster;
  [SerializeField] ParticleSystem leftBooster;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
    particle = GetComponent<ParticleSystem>();
  }

  // Update is called once per frame
  void Update()
  {
    ProcessThrust();
    ProcessRotation();

  }
  void ProcessRotation()
  {
    RotationThrusting();
  }
  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.W))
    {
      StartThrusting();

    }
    else
    {
      audioSource.Stop();
      mainBooster.Stop();
    }
  }

  void StartThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    if (!mainBooster.isPlaying)
    {
      mainBooster.Play();
    }
  }



  void RotationThrusting()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotation);
      if (!rightBooster.isPlaying)
      {
        rightBooster.Play();
      }

    }


    else if (Input.GetKey(KeyCode.D))
    {

      ApplyRotation(-(rotation));
      if (!leftBooster.isPlaying)
      {
        leftBooster.Play();
      }
      else
      {
        rightBooster.Stop();
        leftBooster.Stop();
      }

    }
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; //Freezing rotation to manually rotate
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false;
  }
}


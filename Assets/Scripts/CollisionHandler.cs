using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  [SerializeField] float crashLoadDelay = .5f;
  [SerializeField] float successLoadDelay = 2f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip explosion;
  AudioSource audioSource;

  bool isTransitioning = false;


  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }
  void OnCollisionEnter(Collision other)
  {
      if(isTransitioning)
      {
          return;
      }
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("This is Friendly");
        break;

      case "Finish":
        StartFinishSequence();
        break;

      default:
        CrashSequence();
        break;
    }
  }

  void StartFinishSequence()
  {
    //Sound Effects
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
    GetComponent<Movement>().enabled = false;
    Invoke("NextLevel", successLoadDelay);
  }

  void CrashSequence()
  {
   
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(explosion);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", crashLoadDelay);
  }

  public void ReloadLevel()
  {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScene);

  }
  void NextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
}
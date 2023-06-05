using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
     [SerializeField] float levelLoadDelay = 2f;
     [SerializeField] AudioClip crash;
     [SerializeField] AudioClip success;
     
     [SerializeField] ParticleSystem successParticles;
     [SerializeField] ParticleSystem crashParticles;
     
     AudioSource audioSource;

     bool isTransitioning = false;
     bool collisionDisabled = false;
   
    void Start() {
        {
             audioSource = GetComponent<AudioSource>();
        }
   }

     void Update()
     {
          RespondToDebugKeys();
     }

     void RespondToDebugKeys()
     {
          if (Input.GetKeyDown(KeyCode.L))
          {
               LoadNextLevel();
          }
          else if (Input.GetKeyDown(KeyCode.C))
          {
               collisionDisabled = !collisionDisabled; // toggle collision
          }
     }

   void OnCollisionEnter(Collision other)
   {
        if (isTransitioning || collisionDisabled) { return; }

       switch (other.gameObject.tag)
       {
           case "Friendly":
                Debug.Log("You are on the launch Pad");
                break;
           case "Finish":
                StartSuccessSequence();
                break;
           default:
                StartCrashSequence();
                break;       
        }
       
   }

   void StartCrashSequence()
   {
        isTransitioning = true;
        audioSource.Stop();
        //todo add particle effect upon crash 
         
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
   }
   void StartSuccessSequence()
   {
        isTransitioning = true;
        audioSource.Stop();
        //todo add SFX upon success
        
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
   }

   void LoadNextLevel()
   {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
             nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
   }
   void ReloadLevel()
   {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);
   }
  
}

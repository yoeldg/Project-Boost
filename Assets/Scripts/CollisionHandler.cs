using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadingDelay;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;
    

    AudioSource audioSource;

    bool isTransitioning = false; //stop new stuff from happening

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning){ return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        Invoke("LoadNextLevel", loadingDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionAudio);
        explosionParticles.Play();
        Debug.Log(explosionParticles.isPlaying);
        Debug.Log(explosionParticles.isEmitting);
        Debug.Log(explosionParticles.isStopped);
        Debug.Log(explosionParticles.isPaused);
        Invoke("ReloadLevel", loadingDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextLevelIndex = (currentSceneIndex + 1) % sceneCount;
        SceneManager.LoadScene(nextLevelIndex);
    }

}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadingDelay;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] AudioClip successAudio;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
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
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successAudio);
        Invoke("LoadNextLevel", loadingDelay);
    }
    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(explosionAudio);
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

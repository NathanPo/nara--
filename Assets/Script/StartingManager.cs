using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingManager : MonoBehaviour {
    public AudioSource carStartingSound;
    public void playSound() {
        carStartingSound.Play();
        StartCoroutine(WaitForAudio(carStartingSound));
    }

    private IEnumerator WaitForAudio(AudioSource clip) {
        while (clip.isPlaying) {
            yield return null;
        }
        Debug.Log("This happens when the audioSource has finished playing");
        SceneManager.LoadScene("QuizScene");
    }
}

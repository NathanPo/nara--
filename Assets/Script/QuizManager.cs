
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour {
    [System.Serializable]
    public class Questions {
        public Question[] questions;
    }
	public int currentHealth = 0;
    public int maxHealth = 10;
	public HealthBar healthBar;

    public TextAsset textJSON;
    public GameObject[] options;
    // Prendre d'autre image correspondant au situation
    string[] images = {"airport_taxi", "convoque", "gangster_gun", "taxi1", "taxi_driver", "taxi_passenger","airport_taxi", "convoque", "gangster_gun", "taxi1", "taxi_driver", "taxi_passenger"};
    Sprite backgroundImages;
    private int currentQuestionId;
    private int numberOfQuestions;
    private int buttonClickedId;
    public GameObject questionsGameObject;
    public GameObject resultGameObject;
    public Text resultText;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public Text QuestionText;
    public AudioClip[] songs;
    public AudioSource audioSource;
    private Questions questionsList;
    private Question currentQuestion;

    // Utiliser ça pour incrementer la barre de la police 
    // setFlicDetection(2);

    private void Start() {
        questionsList = JsonUtility.FromJson<Questions>(textJSON.text);
        numberOfQuestions = questionsList.questions.Count();
        currentQuestionId = 1;
        buttonClickedId = 0;
        GoPanel.SetActive(false);
        generateQuestion();
    }

    void setFlicDetection(int damage) {
		currentHealth += damage;
		healthBar.SetHealth(currentHealth);
	}

    public void playSound() {
        disableButton();
        audioSource.clip = songs[currentQuestion.songsId[buttonClickedId]];
        audioSource.Play();
        StartCoroutine(WaitForAudio(audioSource));
    }

    private IEnumerator WaitForAudio(AudioSource clip) {
        while (clip.isPlaying) {
            yield return null;
        }
        Debug.Log("This happens when the audioSource has finished playing");
        enableButton();
        afterClickerOnQuestionButton();
    }

    void disableButton() {
        options[0].GetComponent<Button>().interactable = false;
        options[1].GetComponent<Button>().interactable = false;
        options[2].GetComponent<Button>().interactable = false;
    }

    void enableButton() {
        options[0].GetComponent<Button>().interactable = true;
        options[1].GetComponent<Button>().interactable = true;
        options[2].GetComponent<Button>().interactable = true;
    }

    public void nextQuestion() {
        questionsGameObject.SetActive(true);
        resultGameObject.SetActive(false);
        generateQuestion();
    } 


    void GameOver() {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
    }

    public void retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void setAnswers() {
        if (currentQuestion.questions.Length == 2) {
            options[0].SetActive(true);
            options[1].SetActive(true);
            options[2].SetActive(false);
            options[0].transform.GetChild(0).GetComponent<Text>().text = currentQuestion.questions[0];
            options[1].transform.GetChild(0).GetComponent<Text>().text = currentQuestion.questions[1];
        } else {
            options[0].SetActive(false);
            options[1].SetActive(false);
            options[2].SetActive(true);
            options[2].transform.GetChild(0).GetComponent<Text>().text = currentQuestion.questions[0];
        }
    }

    void setQuestionId(int id) {
        if (currentQuestion.redirections.Length == 0) {
            currentQuestionId = 0;
        } else {
            currentQuestionId = currentQuestion.redirections[id];
        }
    }

    void displayResponse() {
        questionsGameObject.SetActive(false);
        resultGameObject.SetActive(true);
    }

    public void setButtonId(int id) {
        this.buttonClickedId = id;
        // Play music condition a definir un array de int pas la meilleur solution..
        // peut etre deux champs différent.. ou un nombre pour dire pas de musique
        if (currentQuestion.songsId.Length > 0) {
            if (buttonClickedId == 1 && currentQuestion.songsId.Length == 2) {
                
            } else if (buttonClickedId == 0) {
                playSound();
            } else {
                afterClickerOnQuestionButton();
            }
        } else {
            afterClickerOnQuestionButton();
        }
    }

    void afterClickerOnQuestionButton() {
        setQuestionId(buttonClickedId);

        if (currentQuestion.responses.Length > 0) {
            displayResponse();
            if (currentQuestion.responses.Length > 1) {
                resultText.text = currentQuestion.responses[buttonClickedId];
            } else {
                resultText.text = currentQuestion.responses[0];
            }
        } else {
            generateQuestion();
        }
    }
    void setBackgroundImage() {
        backgroundImages = Resources.Load<Sprite>(images[currentQuestion.image]);
        QuizPanel.GetComponent<Image>().sprite = backgroundImages;
    }

    void generateQuestion() {
        if (currentQuestionId != 0) {
            currentQuestion = Array.Find(this.questionsList.questions, q => q.id == currentQuestionId);
            QuestionText.text = currentQuestion.story;
            setBackgroundImage();
            setAnswers();
        } else {
            GameOver();
        }
    }
}

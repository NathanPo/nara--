
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

    public TextAsset textJSON;
    public GameObject[] options;
    // Prendre d'autre image correspondant au situation
    string[] images = {"airport_taxi", "convoque", "gangster_gun", "taxi1", "taxi_driver", "taxi_passenger","airport_taxi", "convoque", "gangster_gun", "taxi1", "taxi_driver", "taxi_passenger"};
    Sprite backgroundImages;
    private int currentQuestion;
    private int numberOfQuestions;
    public GameObject questionsGameObject;
    public GameObject resultGameObject;
    public Text resultText;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public Text QuestionText;
    private Questions questionsList;
    private Question fooItem;

    private void Start() {
        questionsList = JsonUtility.FromJson<Questions>(textJSON.text);
        numberOfQuestions = questionsList.questions.Count();
        currentQuestion = 1;
        GoPanel.SetActive(false);
        generateQuestion();
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
        if (fooItem.questions.Length == 2) {
            options[0].SetActive(true);
            options[1].SetActive(true);
            options[2].SetActive(false);
            options[0].transform.GetChild(0).GetComponent<Text>().text = fooItem.questions[0];
            options[1].transform.GetChild(0).GetComponent<Text>().text = fooItem.questions[1];
        } else {
            options[0].SetActive(false);
            options[1].SetActive(false);
            options[2].SetActive(true);
            options[2].transform.GetChild(0).GetComponent<Text>().text = fooItem.questions[0];
        }
    }

    public void whichButton(int id) {
        if (fooItem.redirections.Length == 0) {
            currentQuestion = 0;
        } else {
            currentQuestion = fooItem.redirections[id];
        }

        if (fooItem.responses.Length > 0) {
            questionsGameObject.SetActive(false);
            resultGameObject.SetActive(true);
            if (fooItem.responses.Length > 1) {
                resultText.text = fooItem.responses[id];
            } else {
                resultText.text = fooItem.responses[0];
            }
            
        } else {
            generateQuestion();
        }
    }
    
    void setBackgroundImage() {
        backgroundImages = Resources.Load<Sprite>(images[currentQuestion]);
        QuizPanel.GetComponent<Image>().sprite = backgroundImages;
    }

    void generateQuestion() {
        if (currentQuestion != 0) {
            setBackgroundImage();
            fooItem = Array.Find(this.questionsList.questions, q => q.id == currentQuestion);
            QuestionText.text = fooItem.story;
            setAnswers();
        } else {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}

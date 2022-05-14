
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
    private int currentQuestionId;
    private int numberOfQuestions;
    private int currentCourse;
    public GameObject questionsGameObject;
    public GameObject resultGameObject;
    public Text resultText;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public GameObject newCoursePanel;
    public Text newCourseText;
    public Text QuestionText;
    private Questions questionsList;
    private Question currentQuestion;

    private void Start() {
        questionsList = JsonUtility.FromJson<Questions>(textJSON.text);
        numberOfQuestions = questionsList.questions.Count();
        currentQuestionId = 1;
        currentCourse = 0;
        GoPanel.SetActive(false);
        generateQuestion();
    }

    public void nextQuestion() {
        questionsGameObject.SetActive(true);
        resultGameObject.SetActive(false);
        generateQuestion();
    }

    public void nextCourse() {
        resultGameObject.SetActive(false);
        newCoursePanel.SetActive(false);
        questionsGameObject.SetActive(true);
        QuizPanel.SetActive(true);
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

    void displayNewCourse() {
        currentCourse++;
        QuizPanel.SetActive(false);
        GoPanel.SetActive(false);
        newCoursePanel.SetActive(true);
        newCourseText.text = "Course n°" + currentCourse;
    }

    public void whichButton(int id) {
        if (currentQuestion.redirections.Length == 0) {
            currentQuestionId = 0;
        } else {
            currentQuestionId = currentQuestion.redirections[id];
        }

        if (currentQuestion.responses.Length > 0) {
            questionsGameObject.SetActive(false);
            resultGameObject.SetActive(true);
            if (currentQuestion.responses.Length > 1) {
                resultText.text = currentQuestion.responses[id];
            } else {
                resultText.text = currentQuestion.responses[0];
            }
        } else {
            generateQuestion();
        }
    }
    void setBackgroundImage() {
        backgroundImages = Resources.Load<Sprite>(images[currentQuestionId]);
        QuizPanel.GetComponent<Image>().sprite = backgroundImages;
    }

    void generateQuestion() {
        if (currentQuestionId != 0) {
            setBackgroundImage();
            currentQuestion = Array.Find(this.questionsList.questions, q => q.id == currentQuestionId);
            QuestionText.text = currentQuestion.story;
            setAnswers();
            if (currentQuestion.newCourse == true) { displayNewCourse(); }
        } else {
            GameOver();
        }
    }
}


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
    
    string[] images = {"airport_taxi", "convoque", "gangster_gun", "taxi1", "taxi_driver", "taxi_passenger"};
    Sprite backgroundImages;

    public int currentQuestion = 1;
    public int numberOfQuestions = 0;

    public GameObject questionsGameObject;
    public GameObject resultGameObject;
    public Text resultText;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    // public Text ScoreText;
    // int totalQuestions = 0;
    public int score;
    public Text QuestionText;
    private Questions questionsList;
    private Question fooItem;

    private void Start() {
        questionsList = JsonUtility.FromJson<Questions>(textJSON.text);
        numberOfQuestions = questionsList.questions.Count();
        GoPanel.SetActive(false);
        generateQuestion();
    }

    public void correct() {
        // if (myQuestionList[currentQuestion].responses.Length > 0) {
        //     questionsGameObject.SetActive(false);
        //     resultGameObject.SetActive(true);
        //     resultText.text = myQuestionList[currentQuestion].responses[0];
        // } else {
        //     score += 1;
        //     myQuestionList.RemoveAt(currentQuestion);
        //     generateQuestion();
        // }
    }

    public void wrong() {
        // if (myQuestionList[currentQuestion].responses.Length > 1) {
        //     questionsGameObject.SetActive(false);
        //     resultGameObject.SetActive(true);
        //     resultText.text = myQuestionList[currentQuestion].responses[1];
        // } else {
        //     myQuestionList.RemoveAt(currentQuestion);
        //     generateQuestion();
        // }
        
    }

    public void nextQuestion() {
        questionsGameObject.SetActive(true);
        resultGameObject.SetActive(false);
        score += 1;
        // questionsList.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void GameOver() {
        // ScoreText.text = score + " / " + totalQuestions;
        // QuizPanel.SetActive(false);
        // GoPanel.SetActive(true);
    }

    public void retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void setAnswers() {
        for (int i = 0; i < options.Length; i++) {
            // options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = fooItem.questions[i];
        //     // if (myQuestionList[currentQuestion].CorrectAnswer == i+1) {
        //     //     options[i].GetComponent<AnswerScript>().isCorrect = true;
        //     // }
        }
    }
    
    void setBackgroundImage() {
        backgroundImages = Resources.Load<Sprite>(images[currentQuestion]);
        QuizPanel.GetComponent<Image>().sprite = backgroundImages;
    }

    void generateQuestion() {
        setBackgroundImage();
        Debug.Log("test");
        fooItem = Array.Find(this.questionsList.questions, q => q.id == 1);
        Debug.Log(fooItem);

        QuestionText.text = fooItem.story;
        setAnswers();
        
        // if (myQuestionList.Count > 0) { 
        //     QuestionText.text = myQuestionList[currentQuestion].story;
        //     numberQuestions++;
        //     setAnswers();
        // } else {
        //     Debug.Log("Out of Questions");
        //     GameOver();
        // }
    }
}

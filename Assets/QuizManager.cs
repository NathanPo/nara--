using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour {
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    
    string[] images = {"airport_taxi", "convoque", "gangster_gun", "taxi1", "taxi_driver", "taxi_passenger"};
    Sprite FULLHP;

    public int currentQuestion = 0;
    public int numberQuestions = 0;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text ScoreText;
    int totalQuestions = 0;
    public int score;

    public Sprite m_Sprite;
    public Text QuestionText;

    private void Start() {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestion();
    }

    public void correct() {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void GameOver() {
        ScoreText.text = score + " / " + totalQuestions;
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
    }

    public void retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void wrong() {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void setAnswers() {
        for (int i = 0; i < options.Length; i++) {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i+1) {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion() {
        FULLHP = Resources.Load<Sprite> (images[numberQuestions]);
        QuizPanel.GetComponent<Image> ().sprite = FULLHP;
        if (QnA.Count > 0) { 
            QuestionText.text = QnA[currentQuestion].Question;
            numberQuestions++;
            setAnswers();
        } else {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}

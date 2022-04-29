using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
// using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;

    public int currentQuestion;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text ScoreText;
    int totalQuestions = 0;
    public int score;
 
    public Text QuestionText;
    // [SerializeField] TextMeshProUGUI QuestionText;

    private void Start() {
        totalQuestions = QnA.Count;
        Debug.Log(QnA.Count);
        Debug.Log(totalQuestions);
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
        if (QnA.Count > 0) { 
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionText.text = QnA[currentQuestion].Question;
            setAnswers();
        } else {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}

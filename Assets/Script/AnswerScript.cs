using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour {
    public int id;
    public QuizManager quizManager;

    public void Answer() {
        Debug.Log("Clicked");
        quizManager.setButtonId(id);
    }

    public void nextQuestion() {
        Debug.Log("Next question");
        quizManager.nextQuestion();
    }
}

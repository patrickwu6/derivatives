using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class answerHandler : MonoBehaviour
{
    public Button ct;
    public Button qt;
    public Text textbox;
    void Start()
    {
        if (derivativeHandler.last_correct)
        {
            textbox.text = $"good job. Your score is: {derivativeHandler.total_correct} / {derivativeHandler.total_questions}.";
        } else
        {
            textbox.text = $"Nope. The correct answer is: {derivativeHandler.correct_answer}. Your score is: {derivativeHandler.total_correct} / {derivativeHandler.total_questions}.";
        }
        Button continue_button = ct.GetComponent<Button>();
        continue_button.onClick.AddListener(Continue);
        Button quit_button = qt.GetComponent<Button>();
        quit_button.onClick.AddListener(Exit);
    }

    void Continue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("question");
    }

    void Exit()
    {
        Application.Quit();
    }
}

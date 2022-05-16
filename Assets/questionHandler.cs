using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class IListExtensions
{
    public static void Shuffle<T>(this IList<T> l)
    {
        int ct = l.Count;
        int last = ct - 1;
        for (int i = 0; i < last; ++i)
        {
            int r = Random.Range(i, ct);
            var temp = l[i];
            l[i] = l[r];
            l[r] = temp;
        }
    }

}

public class Question
{
    public string question_text;
    public string answer_w;
    public string answer_x;
    public string answer_y;
    public string answer_z;
    public int correct_answer;

    public Question askForGreek(List<Derivative> derivative_list, List<string> possible_greek)
    {
        derivative_list.Shuffle();
        Derivative target = derivative_list[0];
        int r = UnityEngine.Random.Range(0, target.derivatives.Length);
        string target_english = target.derivatives[r];
        string correct_greek = target.greek;
        possible_greek.Shuffle();
        List<string> answer_choices = possible_greek.GetRange(0,4);

        bool duplicate = false;
        foreach (string s in answer_choices)
        {
            if (s == correct_greek)
            {
                answer_choices.Remove(s);
                duplicate = true;
                break;
            }
        }
        if (!duplicate)
        {
            answer_choices = answer_choices.GetRange(0,3);
        }
        answer_choices.Add(correct_greek);
        answer_choices.Shuffle();
        this.correct_answer = answer_choices.IndexOf(correct_greek);
        this.answer_w = answer_choices[0];
        this.answer_x = answer_choices[1];
        this.answer_y = answer_choices[2];
        this.answer_z = answer_choices[3];
        this.question_text = $"From which of the following Greek roots does the English word {target_english} derive?";
        return this;


    }

    public Question askForDefinition(List<Derivative> derivative_list, List<string> possible_definitions)
    {
        derivative_list.Shuffle();
        Derivative target = derivative_list[0];
        string target_greek = target.greek;
        string correct_definition = target.definition;
        possible_definitions.Shuffle();
        List<string> answer_choices = possible_definitions.GetRange(0,4);
        bool duplicate = false;
        foreach (string s in answer_choices)
        {
            if (s == correct_definition)
            {
                answer_choices.Remove(s);
                duplicate = true;
                break;
            }
        }
        if (!duplicate)
        {
            answer_choices = answer_choices.GetRange(0, 3);
        }
        answer_choices.Add(correct_definition);
        answer_choices.Shuffle();
        this.correct_answer = answer_choices.IndexOf(correct_definition);
        this.answer_w = answer_choices[0];
        this.answer_x = answer_choices[1];
        this.answer_y = answer_choices[2];
        this.answer_z = answer_choices[3];
        this.question_text = $"What is the English meaning of the Greek root(s) {target_greek}?";
        return this;
    }
}

public class questionHandler : MonoBehaviour
{
    public Text question_text;
    public Button option_W;
    public Button option_X;
    public Button option_Y;
    public Button option_Z;

    // Start is called before the first frame update
    void Start()
    {
        Button w = option_W.GetComponent<Button>();
        Button x = option_X.GetComponent<Button>();
        Button y = option_Y.GetComponent<Button>();
        Button z = option_Z.GetComponent<Button>();

        List<string> possible_greek = derivativeHandler.possible_greek;
        List<string> possible_definitions = derivativeHandler.possible_definitions;
        List<string> possible_derivatives = derivativeHandler.possible_derivatives;
        List<Derivative> derivative_list = derivativeHandler.derivatives;

        Question question;
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            question = new Question().askForGreek(derivative_list, possible_greek);

        } else {
            question = new Question().askForDefinition(derivative_list, possible_definitions);
        }

        question_text.text = question.question_text;
        option_W.GetComponentInChildren<Text>().text = question.answer_w;
        option_X.GetComponentInChildren<Text>().text = question.answer_x;
        option_Y.GetComponentInChildren<Text>().text = question.answer_y;
        option_Z.GetComponentInChildren<Text>().text = question.answer_z;

        switch (question.correct_answer)
        {
            case 0:
                w.onClick.AddListener(OnCorrectAnswerChoice);
                x.onClick.AddListener(OnWrongAnswerChoice);
                y.onClick.AddListener(OnWrongAnswerChoice);
                z.onClick.AddListener(OnWrongAnswerChoice);
                derivativeHandler.correct_answer = question.answer_w;
                break;
            case 1:
                w.onClick.AddListener(OnWrongAnswerChoice);
                x.onClick.AddListener(OnCorrectAnswerChoice);
                y.onClick.AddListener(OnWrongAnswerChoice);
                z.onClick.AddListener(OnWrongAnswerChoice);
                derivativeHandler.correct_answer = question.answer_x;
                break;
            case 2:
                w.onClick.AddListener(OnWrongAnswerChoice);
                x.onClick.AddListener(OnWrongAnswerChoice);
                y.onClick.AddListener(OnCorrectAnswerChoice);
                z.onClick.AddListener(OnWrongAnswerChoice);
                derivativeHandler.correct_answer = question.answer_y;
                break;
            case 3:
                w.onClick.AddListener(OnWrongAnswerChoice);
                x.onClick.AddListener(OnWrongAnswerChoice);
                y.onClick.AddListener(OnWrongAnswerChoice);
                z.onClick.AddListener(OnCorrectAnswerChoice);
                derivativeHandler.correct_answer = question.answer_z;
                break;
        }

    }

    void OnCorrectAnswerChoice()
    {
        derivativeHandler.last_correct = true;
        derivativeHandler.total_questions++;
        derivativeHandler.total_correct++;
        UnityEngine.SceneManagement.SceneManager.LoadScene("answer");
    }

    void OnWrongAnswerChoice()
    {
        derivativeHandler.last_correct = false;
        derivativeHandler.total_questions++;
        UnityEngine.SceneManagement.SceneManager.LoadScene("answer");
    }
}

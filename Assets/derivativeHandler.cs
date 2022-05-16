using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Derivative
{
    public string greek;
    public string definition;
    public string[] derivatives;

    public Derivative fromStrings(string greek, string definition, string[] derivatives)
    {
        this.greek = greek;
        this.definition = definition;
        this.derivatives = derivatives;
        return this;
    }
}

public class derivativeHandler : MonoBehaviour
{
    public static List<Derivative> derivatives = new List<Derivative>();
    public static List<string> possible_greek = new List<string>();
    public static List<string> possible_definitions = new List<string>();
    public static List<string> possible_derivatives = new List<string>();
    public static string correct_answer;
    public static bool last_correct;
    public static int total_questions = 0;
    public static int total_correct = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        TextAsset textAsset = (TextAsset)Resources.Load("text");
        string txt = textAsset.text;
        string[] lines = txt.Split('\n');
        foreach (string line in lines)
        {
            int period = line.IndexOf(".");
            int open_bracket = line.IndexOf("(");
            int close_bracket = line.IndexOf(")");

            string raw_greek = line.Substring(period+2, open_bracket-period-3);
            string raw_definition = line.Substring(open_bracket+1, close_bracket-open_bracket-1);
            string raw_derivatives = line.Substring(close_bracket + 3, line.Length - close_bracket - 3).Trim();
            string[] separated_derivatives = raw_derivatives.Split(new string[] {", "}, System.StringSplitOptions.None);

            possible_greek.Add(raw_greek);
            possible_definitions.Add(raw_definition);

            foreach (string s in separated_derivatives)
            {
                possible_derivatives.Add(s);
            }

            Derivative derivative = new Derivative().fromStrings(raw_greek, raw_definition, separated_derivatives);
            derivatives.Add(derivative);
        }

    }

}

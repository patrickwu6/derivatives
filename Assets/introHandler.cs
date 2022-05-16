using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class introHandler : MonoBehaviour
{
    public Button start;
    void Start()
    {   
        Button start_button = start.GetComponent<Button>();
        start_button.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("question");
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour
{
    public Text storyText;
    public GameObject btn;
    private Queue<string> Lines;
    void Start()
    {
        Lines = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            displaySentence();
        }
    }

    public void ShowStory(Story story)
    {
        Lines.Clear();
        foreach (string sentence in story.sentences)
        {
            Lines.Enqueue(sentence);
        }
        displaySentence();
    }

    public void displaySentence()
    {
        if (Lines.Count == 0)
        {
            End();
            return;
        }

        string sentence = Lines.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        storyText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }
    void End()
    {
        storyText.text = "";
        btn.SetActive(true);
    }

    public void loadlevel(int i)
    {
        SceneManager.LoadScene(i);
    }

}

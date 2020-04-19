using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public Story story;

    private void Start()
    {
        FindObjectOfType<StoryHandler>().ShowStory(story);

    }
}

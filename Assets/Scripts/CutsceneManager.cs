using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Image image;
    public GameObject textObject;
    public Image textBox;

    public Frame[] frames;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = textObject.GetComponent<Text>();
        StartCoroutine(ShowFrame(frames, 0));
    }

    IEnumerator ShowFrame(Frame[] frames, int index)
    {
        image.sprite = frames[index].image;
        text.text = (frames[index].text);
        yield return new WaitForSeconds(frames[index].length);
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(2);
        if (frames.Length - 1 > index)
        {
            StartCoroutine(ShowFrame(frames, index + 1));
        } else
        {
            SceneManager.LoadScene("Frank");
        }
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut() 
    {
        Color color = image.material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.05f)
        {
            color.a = alpha;
            image.material.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeIn()
    {
        Color color = image.material.color;


        for (float alpha = 0f; alpha <= 1; alpha += 0.05f)
        {
            color.a = alpha;
            image.material.color = color;
            yield return null;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

[Serializable]
public class Frame
{
    public string text;
    public Sprite image;
    public int length;
}
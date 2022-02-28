using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Fade : Singleton<Fade>
{

    // the image you want to fade, assign in inspector
    [SerializeField]
    public Image img;

    [SerializeField]
    public Image Tip;

    [SerializeField]
    public Text TipText;

    [SerializeField]
    public Image Tip2;

    [SerializeField]
    public Text TipText2;

    [SerializeField]
    public GameObject Prompt;

    [SerializeField]
    public GameObject Prompt1;

    public void OnButtonClick()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(true));
    }

    public void OnFadeTip()
    {
        // fades the image out when you click
        StartCoroutine(FadeTip(true));
    }

    public void OnFadeText()
    {
        // fades the image out when you click
        StartCoroutine(FadeText(true));
    }

    public void OnFadeTip2()
    {
        // fades the image out when you click
        StartCoroutine(FadeTip2(true));
    }

    public void OnFadeText2()
    {
        // fades the image out when you click
        StartCoroutine(FadeText2(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        for (int x = 0; x < 10; x++)
        {

        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque

            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
         }
    }

    IEnumerator FadeTip(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float l = 1; l >= 0; l -= Time.deltaTime)
            {
                // set color with i as alpha
                Tip.color = new Color(1, 1, 1, l);
                yield return null;
            }        
        }
    }
    IEnumerator FadeText(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        TipText.color = new Color(TipText.color.r, TipText.color.g, TipText.color.b, 1);
        while (TipText.color.a > 0.0f)
        {
            TipText.color = new Color(TipText.color.r, TipText.color.g, TipText.color.b, TipText.color.a - (Time.deltaTime));
            yield return null;
        }
        Prompt.SetActive(false);
    }

    IEnumerator FadeTip2(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float y = 1; y >= 0; y -= Time.deltaTime)
            {
                // set color with y as alpha
                Tip2.color = new Color(1, 1, 1, y);
                yield return null;
            }
        }
    }
    IEnumerator FadeText2(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        TipText2.color = new Color(TipText2.color.r, TipText2.color.g, TipText2.color.b, 1);
        while (TipText2.color.a > 0.0f)
        {
            TipText2.color = new Color(TipText2.color.r, TipText2.color.g, TipText2.color.b, TipText2.color.a - (Time.deltaTime));
            yield return null;
        }
        Prompt1.SetActive(false);
    }
}
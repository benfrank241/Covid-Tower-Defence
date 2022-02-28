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
    public GameObject Prompt;

    [SerializeField]
    public Image Tip;

    [SerializeField]
    public Text TipText;

    [SerializeField]
    public GameObject Prompt2;

    [SerializeField]
    public Image Tip2;

    [SerializeField]
    public Text TipText2;

    [SerializeField]
    public GameObject Prompt3;

    [SerializeField]
    public Image Tip3;

    [SerializeField]
    public Text TipText3;

    [SerializeField]
    public GameObject Prompt4;

    [SerializeField]
    public Image Tip4;

    [SerializeField]
    public Text TipText4;

    [SerializeField]
    public GameObject Prompt5;

    [SerializeField]
    public Image Tip5;

    [SerializeField]
    public Text TipText5;

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

    public void OnFadeTip3()
    {
        // fades the image out when you click
        StartCoroutine(FadeTip3(true));
    }

    public void OnFadeText3()
    {
        // fades the image out when you click
        StartCoroutine(FadeText3(true));
    }

    public void OnFadeTip4()
    {
        // fades the image out when you click
        StartCoroutine(FadeTip4(true));
    }

    public void OnFadeText4()
    {
        // fades the image out when you click
        StartCoroutine(FadeText4(true));
    }

    public void OnFadeTip5()
    {
        // fades the image out when you click
        StartCoroutine(FadeTip5(true));
    }

    public void OnFadeText5()
    {
        // fades the image out when you click
        StartCoroutine(FadeText5(true));
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
        Prompt2.SetActive(false);
    }

    IEnumerator FadeTip3(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float f = 1; f >= 0; f -= Time.deltaTime)
            {
                // set color with y as alpha
                Tip3.color = new Color(1, 1, 1, f);
                yield return null;
            }
        }
    }
    IEnumerator FadeText3(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        TipText3.color = new Color(TipText3.color.r, TipText3.color.g, TipText3.color.b, 1);
        while (TipText3.color.a > 0.0f)
        {
            TipText3.color = new Color(TipText3.color.r, TipText3.color.g, TipText3.color.b, TipText3.color.a - (Time.deltaTime));
            yield return null;
        }
        Prompt3.SetActive(false);
    }

    IEnumerator FadeTip4(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float o = 1; o >= 0; o -= Time.deltaTime)
            {
                // set color with y as alpha
                Tip4.color = new Color(1, 1, 1, o);
                yield return null;
            }
        }
    }
    IEnumerator FadeText4(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        TipText4.color = new Color(TipText4.color.r, TipText4.color.g, TipText4.color.b, 1);
        while (TipText4.color.a > 0.0f)
        {
            TipText4.color = new Color(TipText4.color.r, TipText4.color.g, TipText4.color.b, TipText4.color.a - (Time.deltaTime));
            yield return null;
        }
        Prompt4.SetActive(false);
    }

    IEnumerator FadeTip5(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float m = 1; m >= 0; m -= Time.deltaTime)
            {
                // set color with y as alpha
                Tip5.color = new Color(1, 1, 1, m);
                yield return null;
            }
        }
    }
    IEnumerator FadeText5(bool fadeAway)
    {
        yield return new WaitForSeconds(3);
        TipText5.color = new Color(TipText5.color.r, TipText5.color.g, TipText5.color.b, 1);
        while (TipText5.color.a > 0.0f)
        {
            TipText5.color = new Color(TipText5.color.r, TipText5.color.g, TipText5.color.b, TipText5.color.a - (Time.deltaTime));
            yield return null;
        }
        Prompt5.SetActive(false);
    }

}
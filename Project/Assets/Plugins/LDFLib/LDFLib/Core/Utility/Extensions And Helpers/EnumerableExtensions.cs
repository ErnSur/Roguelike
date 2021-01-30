using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EnumerableExtensions
{
    public static IEnumerable<float> Lerp(float start, float end, float duration)
    {
        return Lerp(start, end, duration, Mathf.Lerp);
    }

    public static IEnumerable<Color> Lerp(Color start, Color end, float duration)
    {
        return Lerp(start, end, duration, Color.Lerp);
    }

    public static IEnumerable<Vector3> Lerp(Vector3 start, Vector3 end, float duration)
    {
        return Lerp(start, end, duration, Vector3.Lerp);
    }
    
    public static IEnumerable<Quaternion> Lerp(Quaternion start, Quaternion end, float duration)
    {
        return Lerp(start, end, duration, Quaternion.Lerp);
    }

    public static IEnumerable<T> Lerp<T>(T start, T end, float duration, Func<T, T, float, T> lerp)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            yield return lerp(start, end, t);
        }

        yield return end;
    }

    public static IEnumerable<float> AsEnumerable(this AnimationCurve c)
    {
        for (var t = c[0].time; t < 1; t += Time.deltaTime / c[c.length - 1].time)
        {
            yield return c.Evaluate(t);
        }
    }

    public static IEnumerator LerpColor(this Image img, Color end, float duration)
    {
        foreach (var step in Lerp(img.color, end, duration))
        {
            img.color = step;
            yield return null;
        }
    }

    public static IEnumerator LerpColor(this SpriteRenderer img, Color end, float duration)
    {
        foreach (var step in Lerp(img.color, end, duration))
        {
            img.color = step;
            yield return null;
        }
    }
}
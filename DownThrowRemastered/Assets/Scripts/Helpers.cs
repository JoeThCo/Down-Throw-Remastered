using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    const int RARITY_COUNT = 5000;
    const float DEFAULT_BIAS = .65f;

    static float Bias(float x, float bias)
    {
        float k = Mathf.Pow(1 - bias, 3);
        return (x * k) / (x * k - x + 1);
    }

    public static float GetBiasNumber(float bias = DEFAULT_BIAS)
    {
        int num = UnityEngine.Random.Range(0, RARITY_COUNT);
        float fnum = ((float)num / (float)RARITY_COUNT);
        float bnum = Bias(fnum, bias);

        return bnum;
    }

    public static T RandomEnumValue<T>(int endCutOff = 0)
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length - endCutOff);
        return (T)values.GetValue(random);
    }
}

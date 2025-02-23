using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace PamisuKit.Common.Util
{
    public static class RandomUtil
    {
        public static float RandomNum(float num, float randomness)
        {
            return num + Random.Range(-num * randomness, num * randomness);
        }

        public static int RandomSigned(int start, int end)
        {
            int sign = RandomSign();
            var num = sign * Random.Range(start, end);
            return num;
        }

        public static float RandomSigned(float start, float end)
        {
            int sign = RandomSign();
            var num = sign * Random.Range(start, end);
            return num;
        }

        public static int RandomSign()
        {
            return Random.Range(0, 2) > 0 ? -1 : 1;
        }

        public static float RandomRange(this float[] values)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (values[0] == values[1])
                return values[0];
            if (values[0] < values[1])
                return Random.Range(values[0], values[1]);
            else
                return Random.Range(values[1], values[0]);
        }

        public static float RandomRange(this Vector2 value)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value.x == value.y)
                return value.x;
            if (value.x < value.y)
                return Random.Range(value.x, value.y);
            else
                return Random.Range(value.y, value.x);
        }
        
        public static int RandomRange(this Vector2Int value, bool rightInclusive)
        {
            if (value.x == value.y)
                return value.x;
            if (value.x < value.y)
                return Random.Range(value.x, rightInclusive? value.y + 1 : value.y);
            else
                return Random.Range(value.y, rightInclusive? value.x + 1 : value.x);
        }

        public static T RandomItem<T>(this T[] collections)
        {
            return collections[Random.Range(0, collections.Length)];
        }

        public static T RandomItem<T>(this List<T> collections)
        {
            return collections[Random.Range(0, collections.Count)];
        }

        public static int RandomWheel<T>(this T[] items, float totalProbability, Func<T, float> getProbabilityFunc)
        {
            if (items.Length == 1)
                return 0;
            var pRandom = Random.Range(0f, totalProbability);
            var temp = 0f;
            for (int i = 0; i < items.Length; i++)
            {
                temp += getProbabilityFunc(items[i]);
                if (pRandom <= temp)
                {
                    return i;
                }
            }
            return items.Length - 1;
        }
        
        public static int RandomWheel<T>(this T[] items, Func<T, float> getProbabilityFunc)
        {
            if (items.Length == 1)
                return 0;
            var totalProbability = 0f;
            for (int i = 0; i < items.Length; i++)
            {
                totalProbability += getProbabilityFunc(items[i]);
            }
            return RandomWheel(items, totalProbability, getProbabilityFunc);
        }
        
        public static int RandomWheel<T>(this List<T> items, float totalProbability, Func<T, float> getProbabilityFunc)
        {
            if (items.Count == 1)
                return 0;
            var pRandom = Random.Range(0f, totalProbability);
            var temp = 0f;
            for (int i = 0; i < items.Count; i++)
            {
                temp += getProbabilityFunc(items[i]);
                if (pRandom <= temp)
                {
                    return i;
                }
            }
            return items.Count - 1;
        }

        public static int RandomWheel<T>(this List<T> items, Func<T, float> getProbabilityFunc)
        {
            if (items.Count == 1)
                return 0;
            var totalProbability = 0f;
            for (int i = 0; i < items.Count; i++)
            {
                totalProbability += getProbabilityFunc(items[i]);
            }
            return RandomWheel(items, totalProbability, getProbabilityFunc);
        }

        public static void PlayRandomPitch(this AudioSource source)
        {
            source.pitch = Random.Range(.8f, 1.2f);
            source.Play();
        }

        public static void PlayRandomPitch(this AudioSource source, float pitch, float randomValue)
        {
            source.pitch = pitch + RandomSigned(0, randomValue);
            source.Play();
        }

        public static void PlayRandomPitch(this AudioSource source, AudioClip clip)
        {
            source.pitch = Random.Range(.8f, 1.2f);
            source.clip = clip;
            source.Play();
        }

        public static Quaternion RandomYRotation()
        {
            var angle = Random.Range(0, 360f);
            return Quaternion.Euler(0, angle, 0);
        }

        public static Vector2 InsideAnnulus(float minRadius, float maxRadius)
        {
            var dir = Random.insideUnitCircle.normalized;
            var minR2 = minRadius * minRadius;
            var maxR2 = maxRadius * maxRadius;
            // ICDF(x) = √(x*(rmax^2 - rmin^2)+rmin^2)
            return dir * Mathf.Sqrt(Random.value * (maxR2 - minR2) + minR2);
        }
        
        public static bool RandomPositionOnNavMesh(Vector3 center, float radius, out Vector3 result, int sampleCount = 10)
        {
            for (var i = 0; i < sampleCount; i++)
            {
                var randomPoint = center + Random.insideUnitSphere * radius;
                if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = center;
            return false;
        }

        public static bool RandomPositionOnNavMesh(Vector3 center, float minRadius, float maxRadius, out Vector3 result, int sampleCount = 10)
        {
            for (var i = 0; i < sampleCount; i++)
            {
                var ann = InsideAnnulus(minRadius, maxRadius);
                var randomPoint = center + new Vector3(ann.x, 0, ann.y);
                if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = center;
            return false;
        }

        
    }
}
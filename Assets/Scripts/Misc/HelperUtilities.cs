using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CameraShakeProps
{
    public float camShakeDuration = 1f;
    public float camShakeStrength = 3f;
    public int camShakeVibrato = 10;
    public float camShakeRandomness = 90f;
}

public class HelperUtilities
{
    public static Vector3 CloneVector3(Vector3 origVector3)
    {
        return new Vector3(origVector3.x, origVector3.y, origVector3.z);
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        bool over180 = false;
        if (angle > 180)
        {
            angle = angle - 360;
            over180 = true;
        }

        angle = Mathf.Clamp(angle, min, max);
        if (over180)
        {
            angle = 360 + angle;
        }

        return angle;
    }

    public static void UpdateCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public static void Rearrange<T>(List<T> items)
    {
        if (items.Count <= 1)
        {
            return;
        }

        System.Random _random = new System.Random();

        T last = items[items.Count - 1];

        int n = items.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + _random.Next(n - i);
            T t = items[r];
            items[r] = items[i];
            items[i] = t;
        }

        if (items[0].Equals(last))
        {
            int r = _random.Next(1, items.Count);
            T t = items[r];
            items[r] = items[0];
            items[0] = t;
        }
    }

    public static int GetOpaqueLayerMaskForRaycast()
    {
        int layerMask = -5;
        int transparentLayer = LayerMask.NameToLayer("TransparentFX");
        layerMask &= ~(1 << transparentLayer);
        int transparentMapLayer = LayerMask.NameToLayer("TransparentFXMap");
        layerMask &= ~(1 << transparentMapLayer);
        return layerMask;
    }

    public static IEnumerator WaitAndExecute(Action action, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }

    public static IEnumerator WaitForFrameAndExecute(Action action)
    {
        Debug.Log("Before");
        yield return new WaitForEndOfFrame();
        Debug.Log("After");
        action?.Invoke();
    }

    public static List<T> GetAllResources<T>() where T : ScriptableObject
    {
        T[] resources = Resources.FindObjectsOfTypeAll<T>();
        return new List<T>(resources);
    }

    public static T[] InitializeArray<T>(int length) where T : new()
    {
        T[] array = new T[length];
        for (int i = 0; i < length; ++i)
        {
            array[i] = new T();
        }

        return array;
    }
}
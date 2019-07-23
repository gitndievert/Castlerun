using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper
{

    /// <summary>
    /// Distance between two transforms in world space
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static float DistanceCheck(Transform t1, Transform t2)
    {
        return Vector3.Distance(t1.position, t2.position);        
    }

    /// <summary>
    /// Returns if the distance between two tranforms is GREATER than the length specified
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static bool DistanceGreater(Transform t1, Transform t2, float amount)
    {
        float dist = DistanceCheck(t1, t2);
        return dist > amount;
    }

    /// <summary>
    /// Returns if the distance between two tranforms is LESS than the length specified 
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static bool DistanceLess(Transform t1, Transform t2, float amount)
    {
        float dist = DistanceCheck(t1, t2);
        return dist < amount;
    }


    #region Tag Itteration on GameObjects

    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }    

    #endregion


    #region Extension Methods for Transforms

    /// <summary>
    /// Extenstion Method to LERP a transform from point A to point B
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="targetPosition"></param>
    /// <param name="speedInSec"></param>
    /// <returns></returns>
    public static IEnumerator Lerp(this Transform trans, Vector3 targetPosition, float speedInSec)
    {
        float time = 0f;
        //Disable Drags
        while (true)
        {            
            time += Time.deltaTime * speedInSec;
            trans.position = Vector3.Lerp(trans.position, targetPosition, time);

            //Stop on Final Position
            if (trans.position == targetPosition)
            {         
                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Extension Method to scale down the size of a transform over time
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="size"></param>
    /// <param name="speedInSec"></param>
    /// <returns></returns>
    public static IEnumerator ScaleDown(this Transform trans, float size, float speedInSec)
    {
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;
            var scale = trans.localScale;
            trans.localScale -= new Vector3(scale.x, scale.y, scale.z) * Time.deltaTime * size;

            if (trans.localScale.x <= size)
                yield break;

            yield return null;
        }
    }

    /// <summary>
    /// Extension Method to scale up the size of a transform over time
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="size"></param>
    /// <param name="speedInSec"></param>
    /// <returns></returns>
    public static IEnumerator ScaleUp(this Transform trans, float size, float speedInSec)
    {
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;
            var scale = trans.localScale;
            trans.localScale += new Vector3(scale.x, scale.y, scale.z) * Time.deltaTime * size;

            if (trans.localScale.x >= size)
                yield break;

            yield return null;
        }
    }

    #endregion

}

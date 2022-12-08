using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Code.Piece;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

public static class ExtensionMethods
{
    //checks if a layer is in a layer mask
     public static bool ContainsLayer(this LayerMask mask, int layer) 
     {
         return ((1 << (int)layer) & (int)mask) > 0;
     }

    public static Bounds GetBounds(this List<Transform> transforms)
    {
        if (transforms.Count == 0) return new Bounds(Vector3.zero, Vector3.zero); 

        //calculate the bounds
        Bounds bounds = new Bounds(transforms[0].position, Vector3.zero);
        foreach (Transform target in transforms)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds;
    }

    public static Bounds GetBounds(this Transform transform)
    {
        //calculate the bounds
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        bounds.Encapsulate(transform.localPosition);

        return bounds;
    }

    public static float GetGreatestDistance(this List<Transform> transforms) 
    {
        Bounds bounds = transforms.GetBounds();

        float distance = bounds.size.x > bounds.size.y ? bounds.size.x : bounds.size.y;

        return distance;
    }

    public static Vector3 GetCenterPoint(this List<Transform> transforms)
    {
        //we only have one target
        if (transforms.Count == 1)
        {
            //center poisiton is that target
            return transforms[0].position;
        }

        Bounds bounds = transforms.GetBounds();

        //return the center of the bounds
        return bounds.center;
    }


    public static void Draw(this Bounds b, float delay = 0)
    {
        // bottom
        Vector3 p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        Vector3 p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        Vector3 p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        Vector3 p4 = new Vector3(b.min.x, b.min.y, b.max.z);

        Debug.DrawLine(p1, p2, Color.blue, delay);
        Debug.DrawLine(p2, p3, Color.red, delay);
        Debug.DrawLine(p3, p4, Color.yellow, delay);
        Debug.DrawLine(p4, p1, Color.magenta, delay);

        // top
        Vector3 p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        Vector3 p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        Vector3 p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        Vector3 p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        Debug.DrawLine(p5, p6, Color.blue, delay);
        Debug.DrawLine(p6, p7, Color.red, delay);
        Debug.DrawLine(p7, p8, Color.yellow, delay);
        Debug.DrawLine(p8, p5, Color.magenta, delay);

        // sides
        Debug.DrawLine(p1, p5, Color.white, delay);
        Debug.DrawLine(p2, p6, Color.gray, delay);
        Debug.DrawLine(p3, p7, Color.green, delay);
        Debug.DrawLine(p4, p8, Color.cyan, delay);
    }
    public static IEnumerator DestroyAfterSeconds(this GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);
        MonoBehaviour.Destroy(gameObj);
    }

    public static Texture2D toTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public static Vector3 ToVector3(this Vector2 vector2)
    {
        Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0);
        return vector3;
    }

    public static Vector2 ToVector2(this Vector3 vector3)
    {
        Vector2 vector2 = new Vector2(vector3.x, vector3.y);
        return vector2;
    }

    public static Vector2 ToVector2(this Vector2Int vector2Int)
    {
        Vector2 vector2 = new Vector2(vector2Int.x, vector2Int.y);
        return vector2;
    }

    public static Vector3 ToVector3(this Vector2Int vector2Int)
    {
        Vector3 vector3 = new Vector3(vector2Int.x, vector2Int.y, 0);
        return vector3;
    }

    public static Vector2Int ToVector2Int(this Vector2 vector2)
    {
        Vector2Int vector2Int = new Vector2Int((int) vector2.x, (int) vector2.y);
        return vector2Int;
    }

    public static Vector2Int ToVector2Int(this Vector3 vector3)
    {
        Vector2Int vector2Int = new Vector2Int((int)vector3.x, (int)vector3.y);
        return vector2Int;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);

        return list[randomIndex];
    }
    
    public static void Randomize<T>(this List<T> list)
    {
        List<T> oldList = new List<T>((IEnumerable<T>)list);

        list.Clear();
        
        while (oldList.Count > 0)
        {
            T item = oldList.GetRandom<T>();
            list.Add(item);
            oldList.Remove(item);
        }
    }

    public static KeyValuePair<A, B> GetRandom<A, B>(this Dictionary<A, B> dictionary)
    {
        int randomIndex = Random.Range(0, dictionary.Count);

        return dictionary.ElementAtOrDefault(randomIndex);
    }

    public static void AddPosition(this LineRenderer lineRenderer, Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    public static Direction GetDirection(this Vector3 directionVector)
    {
        float positiveX = Mathf.Abs(directionVector.x);
        float positiveY = Mathf.Abs(directionVector.y);
        Direction direction;
        if (positiveX > positiveY)
        {
            direction = (directionVector.x > 0) ? Direction.Left : Direction.Right;
        }
        else
        {
            direction = (directionVector.y > 0) ? Direction.Down : Direction.Up;
        }
        Debug.Log(direction);
        return direction;
    }

    //checks if every item in myList is also in the other list
    public static bool IsContains<T>(this List<T> myList, List<T> otherList) 
    {
        foreach (T item in myList)
        {
            //other list does not contain one of my items
            if (!otherList.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    //checks if otherlayer is contained in hitMask
    public static bool CheckLayerHit(LayerMask hitMask, int otherLayer)
    {
        if ((hitMask.value & (1 << otherLayer)) > 0)
        {
            return true;
        }
        return false;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[UnityEngine.Random.Range(0, cardinalDirectionsList.Count)];
    }
    
    public static Vector3 GetRandomPointInBounds(this Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static Dictionary<Vector3,List<ProceduralFragment>> AddFragmentToDictionary(this Dictionary<Vector3,List<ProceduralFragment>> dic,ProceduralFragment proceduralFragment)
    {
        if (dic.ContainsKey(proceduralFragment.transform.position))
        {
            dic[proceduralFragment.transform.position].Add(proceduralFragment);
        }
        else
        {
            dic.Add(proceduralFragment.transform.position, new List<ProceduralFragment> {proceduralFragment});
        }

        return dic;
    }

}

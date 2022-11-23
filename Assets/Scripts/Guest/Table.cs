using UnityEngine;

public class Table : MonoBehaviour
{
    private void Awake()
    {
        var objs = FindObjectsOfType<Table>();

        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

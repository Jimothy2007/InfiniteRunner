using UnityEngine;
using UnityEditor;

public class MissingScriptsFinder : MonoBehaviour
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    static void FindMissingScripts()
    {
        // Use the new FindObjectsByType API
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.Log($"Missing script found on GameObject: {GetFullPath(go)}", go);
                }
            }
        }

        Debug.Log("Scan complete!");
    }

    static string GetFullPath(GameObject go)
    {
        if (go.transform.parent == null) return go.name;
        return GetFullPath(go.transform.parent.gameObject) + "/" + go.name;
    }
}
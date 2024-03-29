using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, Image> keys = new Dictionary<string, Image>();

    public void AddItem(Key item)
    {
        Vector3 pos = new Vector3(60*(keys.Count+1), this.transform.position.y, 0);

        Image sprite = Instantiate(item.GetSprite(), pos, this.transform.rotation, this.transform.parent);
        keys.Add(item.GetColor(), sprite);
    }

    public bool HasItem(string key)
    {
        return keys.ContainsKey(key);
    }

    public void RemoveItem(string key)
    {
        bool wasDeleted = false;
        foreach(KeyValuePair<string, Image> item in keys)
        {
            if (wasDeleted)
            {
                item.Value.transform.Translate(-60, 0, 0);
            }
            else if (item.Key == key)
            {
                wasDeleted = true;
                Destroy(item.Value.gameObject);
            }
        }
        keys.Remove(key);
    }

}

using UnityEngine;

[System.Serializable]
public class ModData
{
    public string category;
    public string preview_path;
    public string file_path;
    public string title;
    public string description;

    public Sprite preview;

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2}", title, file_path, preview_path); 
    }
}

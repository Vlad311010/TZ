using UnityEngine;

public class ModDataSO : ScriptableObject
{
    public string category;
    public string previewPath;
    public string filePath;
    public string title;
    public string description;

    public static ModDataSO CreateInstance(ModData modData)
    {
        ModDataSO modDataSO = ScriptableObject.CreateInstance<ModDataSO>();
        modDataSO.category = modData.category;
        modDataSO.previewPath = modData.preview_path;
        modDataSO.filePath = modData.file_path;
        modDataSO.title = modData.title;
        modDataSO.description = modData.description;
        return modDataSO;
    }
}

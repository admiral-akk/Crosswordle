using UnityEngine;

public class CrosswordGenerator : MonoBehaviour
{
    [SerializeField] private string[] Words;

    private void OnValidate()
    {
        var data = CrosswordData.GenerateCrossword(Words);
        Debug.Log(data.ToString());
    }
}

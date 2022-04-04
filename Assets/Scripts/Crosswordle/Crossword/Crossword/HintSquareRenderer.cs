using System.Linq;
using TMPro;
using UnityEngine;

public class HintSquareRenderer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI Letter;


    public void UpdateLetter(char c)
    {
        Letter.text = c.ToString();
    }

    public void UpdatePosition(int index)
    {
        transform.localScale =  Vector3.one * 0.525f;
        transform.localPosition = Vector3.right * ((index%2)*0.475f-0.5f) + Vector3.down * ((index/2) * 0.475f - 0.5f);
    }
}

using UnityEngine;

public class KeyboardRowRenderer : MonoBehaviour
{
    [SerializeField] private GameObject KeyPrefab;

    public KeyboardSquareRenderer AddKey(char c)
    {
        var key = Instantiate(KeyPrefab, transform).GetComponent<KeyboardSquareRenderer>();
        key.Initialize(c);
        return key;
    }
}

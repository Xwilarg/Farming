using TMPro;
using UnityEngine;

public class GoUpAndDisappear : MonoBehaviour
{
    private TextMesh tm;

    private void Start()
    {
        tm = GetComponent<TextMesh>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * .05f);
        var a = tm.color.a;
        a -= .05f;
        if (a <= 0f)
            Destroy(gameObject);
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, a);
    }
}

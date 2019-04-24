using UnityEngine;
using TMPro;

public class ObjBar : MonoBehaviour
{
    public ProgressBar Bar;
    public TextMeshPro Text;

    private void Update()
    {
       /* var pos = Camera.main.WorldToScreenPoint(transform.position);
        Text.transform.position = pos;
        Text.text = GetComponent<BasePrefab>().ToString();
        Bar.Title = "Rock";
        Bar.BarValue = GetComponent<BasePrefab>().Health;*/
    }
}

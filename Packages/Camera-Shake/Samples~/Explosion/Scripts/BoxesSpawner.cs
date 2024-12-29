using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxesSpawner : MonoBehaviour
{
    public int num;
    public float maxSize;
    public float minSize;
    public GameObject boxPrefab;
    public List<Color> colors;

    private void Start()
    {
        for (int i = 0; i < num; i++)
        {
            var box = Instantiate(boxPrefab, Random.insideUnitCircle * 5, Quaternion.identity);
            box.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];
            box.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLeftToRight : MonoBehaviour
{
    [SerializeField] public float speed = 2f;
    [SerializeField] public float boundry = 10f;
    [SerializeField] public List<Transform> transforms;

    void Update()
    {
        StartClouds();
    }

    private void StartClouds()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > boundry)
        {
            transform.position = new Vector2(-500f, transform.position.y);
        }
        
    }
}

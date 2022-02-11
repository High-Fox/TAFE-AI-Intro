using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public GameObject objectReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, objectReference.transform.position, 2 * Time.deltaTime);
        transform.SetPositionAndRotation(newPos, transform.rotation);
    }
}

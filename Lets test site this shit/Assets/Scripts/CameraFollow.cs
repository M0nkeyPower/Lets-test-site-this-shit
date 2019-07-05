using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform target;
    public float smoothing;
	public float height;
	float offsetX;


    // Use this for initialization
    void Start()
    {
		if (GameObject.FindGameObjectWithTag ("Player"))
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		} else
		{
			Debug.LogWarning ("Player not spawned!");
		}





    }


    void Update()
    {
		if (target != null)
		{
			float k = transform.position.y;//height
			float sinK = Mathf.Sin ( (90 - (90 - transform.eulerAngles.x) ) * Mathf.Deg2Rad);
			float sinX = Mathf.Sin ( (90 - transform.eulerAngles.x) * Mathf.Deg2Rad);
			offsetX = k / sinK * sinX;
			Vector3 targetCamPos = target.position + new Vector3(0f, height, -offsetX);
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RigidbodyForceController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody body = null;

	[SerializeField]
	private Vector3 addVelocity = Vector3.zero;
	[SerializeField]
	private bool Continous = false;

	[Button]
	public void AddVelocity() => body.velocity += addVelocity;
	[Button]
	public void SetVelocity() => body.velocity = addVelocity;

	private void Update()
	{
		if (Continous)
		{
			body.velocity += addVelocity * Time.deltaTime;
		}
	}
}
using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
	#region Singleton class: Magnet

	public static Magnet Instance;

	private void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	[SerializeField] float magnetForce;


	List<Rigidbody> affectedRigidbodies = new List<Rigidbody> ();
	Transform magnet;

	private void Start ()
	{
		magnet = transform;
		affectedRigidbodies.Clear ();
	}

	private void FixedUpdate ()
	{
		if (Game.isGameover || !Game.isMoving) return;
		foreach (Rigidbody rb in affectedRigidbodies) {
			rb.AddForce ((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
		}
	}

	private void OnTriggerEnter (Collider other)
	{
		var tag = other.tag;

		if (!Game.isGameover && (tag.Equals ("Obstacle") || tag.Equals ("Object"))) {
			AddToMagnetField (other.attachedRigidbody);
		}
	}

	private void OnTriggerExit (Collider other)
	{
		var tag = other.tag;

		if (!Game.isGameover && (tag.Equals ("Obstacle") || tag.Equals ("Object"))) {
			RemoveFromMagnetField (other.attachedRigidbody);
		}
	}

	private void AddToMagnetField (Rigidbody rb)
	{
		affectedRigidbodies.Add (rb);
	}

	private void RemoveFromMagnetField (Rigidbody rb)
	{
		affectedRigidbodies.Remove (rb);
	}
}

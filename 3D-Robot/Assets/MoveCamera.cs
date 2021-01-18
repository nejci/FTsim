﻿// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	//
	// VARIABLES
	//

	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?

	// Starting point
	Vector3 cameraStartPoint = Vector3.zero;
	Quaternion cameraStartRotation;

	Vector3 cameraPositionTop = new Vector3(-1, 10, 0);
	Quaternion cameraRotationTop = Quaternion.Euler(new Vector3(90, 0, 0));
	Vector3 cameraPositionSide = new Vector3(15, 2, 0);
	Quaternion cameraRotationSide = Quaternion.Euler(new Vector3(0, 270, 0));

	void Start()
	{
		cameraStartPoint = transform.position;
		cameraStartRotation = transform.rotation;
	}
	//
	// UPDATE
	//
	void Update () 
	{
		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}

		// Get the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}

		// Get the middle mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(0)) isRotating=false;
		if (!Input.GetMouseButton(1)) isPanning=false;
		if (!Input.GetMouseButton(2)) isZooming=false;

		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}

		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
			transform.Translate(move, Space.Self);
		}

		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);

			if (Camera.main.orthographic) {
				Camera.main.orthographicSize = Camera.main.orthographicSize - pos.y * zoomSpeed;
			} else {
				Vector3 move = pos.y * zoomSpeed * transform.forward; 
				transform.Translate (move, Space.World);
			}
		}
		// Scrollwheel
		float scrollWheel = Input.GetAxis ("Mouse ScrollWheel");
		if (scrollWheel != 0) {			
			if (Camera.main.orthographic) {
				Camera.main.orthographicSize = Camera.main.orthographicSize - scrollWheel;
			} else {
				Vector3 moveScroll = scrollWheel * zoomSpeed * transform.forward;
				transform.Translate (moveScroll, Space.World);
			}
		}

	}

	public void setCameraTop(){
		transform.position = cameraPositionTop;
		transform.rotation = cameraRotationTop;
		Camera.main.orthographic = true;
		Camera.main.orthographicSize = 6f;
	}
	public void setCameraSide(){
		transform.position = cameraPositionSide;
		transform.rotation = cameraRotationSide;
		Camera.main.orthographic = true;
		Camera.main.orthographicSize = 4.5f;
	}

	public void resetCamera(){
		transform.position = cameraStartPoint;
		transform.rotation = cameraStartRotation;
		Camera.main.orthographic = false;
	}
	public void infoCamera(){
		if (GameObject.FindWithTag ("Dialog_error_PLCSIM") == null && GameObject.FindWithTag ("Dialog_camera_info") == null) {
			Dialog.MessageBox (
				"Dialog_camera_info", 
				"Control and navigation", 
				"Use your mouse buttons to control the camera:\n" +
				"    left - rotation,\n" +
				"    middle/scroll wheel - zoom,\n" +
				"    right - translation.\n\n" +
				"Use keys to control the virtual platform:\n" +
				"    left/right arrow - move along X axis,\n" +
				"    up/down arrow - move along Z axis,\n" +
				"    PageUp/PageDown - move along Y axis.\n\n" +
				"Use keys to control the buttons:\n" +
				"    Q - key,\n" +
				"    W - high-left,\n" +
				"    E - high-right,\n" +
				"    S - low-left,\n" +
				"    D - low-right,\n" +
				"    A - green.",
				"Close", 
				() => {;},
				heightMax: 320,
				pos_y: 20);
		}
	}
}
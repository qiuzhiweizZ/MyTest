using UnityEngine;
using System.Collections;
//摄像机跟随
public class CCWalk : MonoBehaviour {

	public Vector3 GoalPosition;
	CharacterController CC;

	void Start () {
		CC = this.GetComponent<CharacterController> ();
	}
	

	void Update () {

		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //在主摄像机屏幕上，从鼠标点击位置发出的射线
			RaycastHit hit;

			if (Physics.Raycast(ray,out hit)){
				GoalPosition = hit.point; //在世界空间中，射线碰撞到碰撞器的碰撞点
			}
		}
		
		if(Vector3.Distance(GoalPosition, this.transform.position) > 1f){
			Vector3 step = Vector3.ClampMagnitude(GoalPosition - this.transform.position, 0.1f); //每次移动的距离
			CC.Move(step);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			//this.transform.position += Vector3.up; 
			CC.Move (this.transform.position + Vector3.up); //只有Move才能触发OnControllerColliderHit
		}


	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.collider.gameObject.name == "Cube") {
			Debug.Log ("Hit");
		}
		Debug.Log (CC.isGrounded); //是否触碰到地面，即该物体的底部是否触碰到带有collider的物体

	}
}

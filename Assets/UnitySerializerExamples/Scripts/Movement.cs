using UnityEngine;

public class Movement : MonoBehaviour {
    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        /*
        forward = getcomponent<transform>().forward;
        right = new vector3(forward.z, 0.0f, -forward.x);

        float horizontalinput = input.getaxisraw("horizontal");
        float verticalinput = input.getaxisraw("vertical");
        vector3 targetdirection = horizontalinput * right + verticalinput * forward;

        movedirection = vector3.rotatetowards(movedirection, targetdirection, 200.0f * mathf.deg2rad * time.deltatime, 1000.0f);

        vector3 movement = movedirection * time.deltatime * 10.0f;
        controller.move(movement);*/
    }
}

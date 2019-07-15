using UnityEngine;
using UserInput;


namespace PlayerController {
	public class PlayerController : MonoBehaviour {
		[SerializeField]
		CharacterController _controller;

		[SerializeField]
		float _baseSpeed = 1;

		[SerializeField]
		ElevationChanger _elevationChanger;


		private Vector3 _moveDirection;

		private InputManager _inputManger;

		private bool leftPressed;
		private bool rightPressed;
		private bool upPressed;
		private bool downPressed;

		void Start() {
			_inputManger = InputManager.Instance;
			_inputManger.DirectionalButtonDownEvent += HandleDirectionButtonDownEvent;
			_inputManger.DirectionalButtonUpEvent += HandleDirectionButtonUpEvent;
			_moveDirection = Vector3.zero;
		}

		void Update() {
			if (rightPressed || leftPressed || upPressed || downPressed) {
				var move = _moveDirection;
				move.y = 0;
				move.Normalize();
				move *= _baseSpeed;
				_controller.Move(move * Time.deltaTime);
			}
		}

		private void HandleDirectionButtonUpEvent(object sender, DirectionalButtonPressedEventArgs e) {
			switch (e.direction) {
				case MovementEnum.Left:
					leftPressed = false;
					break;
				case MovementEnum.Right:
					rightPressed = false;
					break;
				case MovementEnum.Up:
					upPressed = false;
					break;
				case MovementEnum.Down:
					downPressed = false;
					break;
			}
			CalculateMovementDirection();
		}

		private void CalculateMovementDirection() {
			Vector3 tempDirection = Vector3.zero;
			if (leftPressed) {
				tempDirection.x -= 1;
			}
			if (rightPressed) {
				tempDirection.x += 1;
			}
			if (upPressed) {
				tempDirection.z += 1;
			}
			if (downPressed) {
				tempDirection.z -= 1;
			}

			_moveDirection = tempDirection;
		}

		private void HandleDirectionButtonDownEvent(object sender, DirectionalButtonPressedEventArgs e) {
			switch (e.direction) {
				case MovementEnum.Left:
					leftPressed = true;
					break;
				case MovementEnum.Right:
					rightPressed = true;
					break;
				case MovementEnum.Up:
					upPressed = true;
					break;
				case MovementEnum.Down:
					downPressed = true;
					break;
			}

			CalculateMovementDirection();
		}

		void OnDestroy() {
			_inputManger.DirectionalButtonDownEvent -= HandleDirectionButtonDownEvent;
			_inputManger.DirectionalButtonUpEvent -= HandleDirectionButtonUpEvent;
		}
	}
}

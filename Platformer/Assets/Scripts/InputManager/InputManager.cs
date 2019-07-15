using CommonArchitectureUtilities;
using System;
using UnityEngine;

namespace UserInput {
	public class InputManager : SingletonBehaviour<InputManager> {

		public event EventHandler<DirectionalButtonPressedEventArgs> DirectionalButtonDownEvent;
		public event EventHandler<DirectionalButtonPressedEventArgs> DirectionalButtonUpEvent;

		void Update() {
			//RightButtons
			if (Input.GetButtonDown("Right")) {
				SendDirectionalButtonDownEvent(MovementEnum.Right);
			}

			if (Input.GetButtonUp("Right")) {
				SendDirectionalButtonUpEvent(MovementEnum.Right);
			}

			//LeftButtons
			if (Input.GetButtonDown("Left")) {
				SendDirectionalButtonDownEvent(MovementEnum.Left);
			}
			if (Input.GetButtonUp("Left")) {
				SendDirectionalButtonUpEvent(MovementEnum.Left);
			}

			//UpButtons
			if (Input.GetButtonDown("Up")) {
				SendDirectionalButtonDownEvent(MovementEnum.Up);
			}
			if (Input.GetButtonUp("Up")) {
				SendDirectionalButtonUpEvent(MovementEnum.Up);
			}

			//DownButtons
			if (Input.GetButtonDown("Down")) {
				SendDirectionalButtonDownEvent(MovementEnum.Down);
			}
			if (Input.GetButtonUp("Down")) {
				SendDirectionalButtonUpEvent(MovementEnum.Down);
			}
		}

		private void SendDirectionalButtonDownEvent(MovementEnum inputDirection) {
			DirectionalButtonDownEvent?.Invoke(this, new DirectionalButtonPressedEventArgs() { direction = inputDirection });
		}
		private void SendDirectionalButtonUpEvent(MovementEnum inputDirection) {
			DirectionalButtonUpEvent?.Invoke(this, new DirectionalButtonPressedEventArgs() { direction = inputDirection });
		}

	}

	public class DirectionalButtonPressedEventArgs : EventArgs {
		public MovementEnum direction;
	}

	public enum MovementEnum {
		Left, Right, Up, Down
	}
}

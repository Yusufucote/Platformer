using UnityEngine;

namespace PlayerController {
	public class ElevationChanger : MonoBehaviour {
		[SerializeField]
		Transform _player;

		[SerializeField]
		float _playerHeight = 60.5f;

		[SerializeField]
		LayerMask _layerMask;

		RaycastHit _hit;

		void Update() {
			SetHeight();
		}

		private void SetHeight() {
			if (Physics.Raycast(_player.position, Vector3.down, out _hit, 100.0f, _layerMask)) {
				Debug.Log(_hit.distance);
				if (_hit.distance != _playerHeight) {
					var differance = _playerHeight - _hit.distance;
					var playerY = _player.position.y;
					playerY += differance;
					_player.transform.position = new Vector3(_player.transform.position.x, playerY, _player.transform.position.z);
				}
			}
		}
	}
}

﻿using UnityEngine;

namespace CommonArchitectureUtilities {
	/// <summary>
	/// Be aware this will not prevent a non singleton constructor
	/// such as `T myT = new T();`
	/// To prevent that, add `protected T () {}` to your singleton class.
	///
	/// As a note, this is made as MonoBehaviour because we need Coroutines.
	/// </summary>
	public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
		static T _instance;

		static object _lock = new object();

		public static T Instance {
			get {
				if (applicationIsQuitting) {
					Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
									 "' already destroyed on application quit." +
									 " Won't create again - returning null.");
					return null;
				}

				lock (_lock) {
					if (_instance == null) {
						var loadedObjectsOfType = (T[])FindObjectsOfType(typeof(T));

						bool isNullOrEmpty = loadedObjectsOfType == null || loadedObjectsOfType.Length == 0;
						_instance = isNullOrEmpty ? null : loadedObjectsOfType[0];

						if (loadedObjectsOfType.Length > 1) {
							Debug.LogError("[Singleton] Something went really wrong " +
											   " - there should never be more than 1 singleton!" +
											   " Reopening the scene might fix it.");
							return _instance;
						}

						if (_instance == null) {
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = "(singleton) " + typeof(T);

							DontDestroyOnLoad(singleton);

							Debug.Log("[Singleton] An instance of " + typeof(T) +
										  " is needed in the scene, so '" + singleton +
										  "' was created with DontDestroyOnLoad.");
						}
						else {
							Debug.Log("[Singleton] Using instance already created: " +
										  _instance.gameObject.name);
						}
					}

					return _instance;
				}
			}
			private set {
				if (_instance == null) {
					_instance = value;
				}
				else {
					Debug.LogError("Tried to set _instance when it was already set");
				}
			}
		}

		protected virtual void Awake() {
			Instance = this as T;
		}

		static bool applicationIsQuitting = false;

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed,
		/// it will create a buggy ghost object that will stay on the Editor scene
		/// even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		protected virtual void OnDestroy() {
			_instance = null;
		}

		void OnApplicationQuit() {
			applicationIsQuitting = true;
		}
	}
}
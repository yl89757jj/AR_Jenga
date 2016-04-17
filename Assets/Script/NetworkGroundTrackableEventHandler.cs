/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class NetworkGroundTrackableEventHandler : NetworkBehaviour,
                                                ITrackableEventHandler
    {
		[SyncVar]
        public bool GameStart;
		[SyncVar]
		public bool tracked;

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
			if (GameObject.Find ("NetworkManager").GetComponent<StartUp> ().isServer) {
				GameStart = false;
			}
			tracked = false;
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
			if (GameObject.Find ("NetworkManager").GetComponent<StartUp> ().isServer) {
				return;
			}
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
			if (tracked)
				return;
			tracked = true;
            GameObject Envir = GameObject.Find("Enviroment");

            Renderer[] rendererComponents = Envir.GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = Envir.GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                    component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                    component.enabled = true;
                    if (component.attachedRigidbody)
                        component.attachedRigidbody.useGravity = true;
            }


            /***** Build Jenga Animation ****/

			GameObject Jenga = GameObject.Find ("Jenga");
			StartCoroutine (Build (Jenga));


            

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
			if (tracked)
				return;
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                if (component.attachedRigidbody)
                    component.attachedRigidbody.useGravity = false;
                component.enabled = false;
            }
            

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        IEnumerator Build(GameObject Jenga)
        {
            Renderer[] JengaComponents = Jenga.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer component in JengaComponents)
            {
                component.enabled = true;

                Collider brick_col = component.GetComponent<Collider>();
                brick_col.enabled = true;
                if (brick_col.attachedRigidbody)
                    brick_col.attachedRigidbody.useGravity = true;
                if (!GameStart) 
                    yield return new WaitForSeconds(0.1f);
              
            }
            GameStart = true;

        }

        #endregion // PRIVATE_METHODS
    }
}

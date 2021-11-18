using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Tests
{
    public class TestRoutineRunner : MonoBehaviour
    {
        public static TestRoutineRunner instance;
        
        public static TestRoutineRunner Instance {
            get {
                // if instance already exists and is set return it right way
                if (instance) return instance;

                // otherwise find it in the scene
                instance = FindObjectOfType<TestRoutineRunner>();

                // if it is found in the scene return it now
                if(instance) return instance;

                // otherwise create a new one
                instance = new GameObject("TestRoutineRunner").AddComponent<TestRoutineRunner>();

                return instance;
            }
        }

        // Use this for adding a callback what should be done when the routine finishes
        public void TestRoutine(IEnumerator routine, Action whenDone)
        {
            StartCoroutine(RunRoutine(routine, whenDone));
        }

        private IEnumerator RunRoutine(IEnumerator routine, Action whenDone)
        {
            // runs the routine an wait until it is finished
            yield return routine;

            // execute the callback
            whenDone?.Invoke();
        }

    }
}

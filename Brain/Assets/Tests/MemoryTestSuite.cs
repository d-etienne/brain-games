using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MemoryTestSuite
    {

        [Test]
        public void LevelIncreaseTest() {
            memoryGame memory_game = new memoryGame();
            TestRoutineRunner.Instance.TestRoutine(memory_game.probDisplay(memory_game.levels[1]), () => {
                Assert.True(memory_game.process == 2);
                Assert.True(memory_game.level == 2);
                Assert.True(memory_game.turn == 1);
            }
            );
        }


        [UnityTest]
        public IEnumerator UserClickTest() {
            // still working on how to create a raycast hit that will allow the user
            // to click 
            yield return null;

        }
    }
}
    

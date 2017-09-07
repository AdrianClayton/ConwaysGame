using GameObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Conway.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void ProgressingStateReturnsTrue()
        {
            GameController myController = new GameController();

            Assert.IsTrue(myController.ProgressState());
        }


        [TestMethod]
        public void IfSpawnRuleIsMetCellIsSpawned()
        {
            RuleCollection testRuleCollection = new RuleCollection();
            testRuleCollection.Add(new SingleRule(1, SpawnOrSurviveRule.SpawnRule));

            Map testMap = new Map();
            testMap.AddCellAtCoordinate(1, 1);
            testMap.AddCellAtCoordinate(3, 1);

            GameController myController = new GameController(testMap, testRuleCollection);
            myController.ProgressState();

            Assert.AreEqual(10, myController.Map.LivingCells.Count);
        }

        [TestMethod]
        public void CombinedRulesWork()
        {
            RuleCollection testRuleCollection = new RuleCollection();
            testRuleCollection.Add(new SingleRule(3, SpawnOrSurviveRule.SpawnRule));
            testRuleCollection.Add(new SingleRule(3, SpawnOrSurviveRule.SurviveRule));
            testRuleCollection.Add(new SingleRule(2, SpawnOrSurviveRule.SurviveRule));

            Map testMap = new Map();
            testMap.AddCellAtCoordinate(1, 0);
            testMap.AddCellAtCoordinate(2, 0);
            testMap.AddCellAtCoordinate(3, 0);

            GameController myController = new GameController(testMap, testRuleCollection);
            bool success = myController.ProgressState();

            Assert.IsTrue(success);
            Assert.AreEqual(3, myController.Map.LivingCells.Count);
        }

        [TestMethod]
        public void IfNoSurviveRuleIsMetCellIsRemoveded()
        {
            RuleCollection testRuleCollection = new RuleCollection();
            testRuleCollection.Add(new SingleRule(1, SpawnOrSurviveRule.SurviveRule));
            testRuleCollection.Add(new SingleRule(2, SpawnOrSurviveRule.SurviveRule));

            Map testMap = new Map();
            testMap.AddCellAtCoordinate(1, 1);
            testMap.AddCellAtCoordinate(2, 1);
            testMap.AddCellAtCoordinate(3, 1);
            testMap.AddCellAtCoordinate(3, 2);

            GameController myController = new GameController(testMap, testRuleCollection);
            myController.ProgressState();

            Assert.AreEqual(3, myController.Map.LivingCells.Count);
        }


        [TestMethod]
        public void IfAnInvalidRuleIsAppliedStateWillNotProgress()
        {
            RuleCollection testRuleCollection = new RuleCollection();
            testRuleCollection.Add(new SingleRule(0, SpawnOrSurviveRule.SpawnRule));
            testRuleCollection.Add(new SingleRule(4, SpawnOrSurviveRule.SurviveRule));

            Map testMap = new Map();
            testMap.AddCellAtCoordinate(1, 1);
            testMap.AddCellAtCoordinate(2, 1);
            testMap.AddCellAtCoordinate(3, 1);
            testMap.AddCellAtCoordinate(3, 2);

            GameController myController = new GameController(testMap, testRuleCollection);

            Assert.IsFalse(myController.ProgressState());

        }

        [TestMethod]
        public void OriginalMapIsUnchangedAfterProgressing()
        {
            RuleCollection testRuleCollection = new RuleCollection();
            testRuleCollection.Add(new SingleRule(1, SpawnOrSurviveRule.SpawnRule));

            Map testMap = new Map();
            testMap.AddCellAtCoordinate(1, 1);
            testMap.AddCellAtCoordinate(3, 1);

            GameController myController = new GameController(testMap, testRuleCollection);
            myController.ProgressState();

            Assert.AreEqual(2, testMap.LivingCells.Count);
        }
    }
}

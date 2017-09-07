using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameObjects;

namespace Conway.Tests
{
    [TestClass]
    public class RuleTests
    {
        [TestMethod]
        public void ThereCanOnlybeOneInstanceOfARuleInACollection()
        {
            RuleCollection ruleList = new RuleCollection();

            SingleRule testRule = new SingleRule(7, SpawnOrSurviveRule.SpawnRule);

            SingleRule testRule2 = new SingleRule(7, SpawnOrSurviveRule.SpawnRule);

            ruleList.Add(testRule);
            ruleList.Add(testRule2);

            Assert.AreEqual(1, ruleList.Count);
        }
        
        [TestMethod]
        public void ThereCanBeMultipleRulesInACollection()
        {
            RuleCollection ruleList = new RuleCollection();

            SingleRule testRule = new SingleRule(7, SpawnOrSurviveRule.SpawnRule);

            SingleRule testRule2 = new SingleRule(8, SpawnOrSurviveRule.SpawnRule);

            SingleRule testRule3 = new SingleRule(8, SpawnOrSurviveRule.SurviveRule);

            SingleRule testRule4 = new SingleRule(7, SpawnOrSurviveRule.SpawnRule);

            ruleList.Add(testRule);
            ruleList.Add(testRule2);
            ruleList.Add(testRule3);
            ruleList.Add(testRule4);

            Assert.AreEqual(3, ruleList.Count);
        }


    }
}

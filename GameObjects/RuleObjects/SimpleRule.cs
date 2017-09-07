using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class SingleRule
    {
        public int Number
        {
            get;
        }

        public SpawnOrSurviveRule RuleType
        {
            get;
        }

        public bool Equals(SingleRule rule)
        {
            return (rule.Number == Number) && (rule.RuleType == RuleType);
        }

        public SingleRule(int number, SpawnOrSurviveRule ruleType)
        {
            Number = number;
            RuleType = ruleType;
        }
    }

    public enum SpawnOrSurviveRule
    {
        SpawnRule,
        SurviveRule
    }
}

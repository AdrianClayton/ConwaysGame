using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class RuleCollection :ICollection<SingleRule>
    {
        private readonly List<SingleRule> RuleList;

        public RuleCollection()
        {
            RuleList = new List<SingleRule>();
        }

        public int Count
        {
            get
            {
                return RuleList.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(SingleRule item)
        {
            if(!Contains(item))
                RuleList.Add(item);
        }

        public void Clear()
        {
            RuleList.Clear();
        }

        public bool Contains(SingleRule item)
        {
            foreach (SingleRule rule in RuleList)
            {
                if (rule.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(SingleRule[] array, int arrayIndex)
        {
            RuleList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SingleRule> GetEnumerator()
        {
            return RuleList.GetEnumerator();
        }

        public bool Remove(SingleRule item)
        {
            foreach (SingleRule rule in RuleList)
            {
                if (rule.Equals(item))
                {
                    return RuleList.Remove(rule);
                }
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

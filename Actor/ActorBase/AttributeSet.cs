using System.Collections.Generic;

namespace Fight {
    public class AttributeSet : IAttribute {

        public delegate void AttributeHandler(FightAttribute _type, int _value);
        public event AttributeHandler AttributeChangeEvent;

        Dictionary<FightAttribute, ClampInt> attributeDict;

        public AttributeSet() {
            attributeDict = new Dictionary<FightAttribute, ClampInt>();

        }

        public void Set(FightAttribute _type, int _value) {
            if (!attributeDict.ContainsKey(_type)) {
                WDebug.Log(string.Format("FightAttribute {0} is not exist!", _type));
                return;
            }

            attributeDict[_type].Value = _value;

            if (AttributeChangeEvent != null) {
                AttributeChangeEvent(_type, attributeDict[_type].Value);
            }
        }

        public void Add(FightAttribute _type, int _value) {
            if (!attributeDict.ContainsKey(_type)) {
                WDebug.Log(string.Format("FightAttribute {0} is not exist!", _type));
                return;
            }
            attributeDict[_type].Value += _value;

            if (AttributeChangeEvent != null) {
                AttributeChangeEvent(_type, attributeDict[_type].Value);
            }

        }

        public int Query(FightAttribute _type) {
            if (!attributeDict.ContainsKey(_type)) {
                WDebug.Log(string.Format("FightAttribute {0} is not exist!", _type));
                return 0;
            }

            return attributeDict[_type].Value;
        }

        public void Reduce(FightAttribute _type, int _value) {
            if (!attributeDict.ContainsKey(_type)) {
                WDebug.Log(string.Format("FightAttribute {0} is not exist!", _type));
                return;
            }

            attributeDict[_type].Value -= _value;

            if (AttributeChangeEvent != null) {
                AttributeChangeEvent(_type, attributeDict[_type].Value);
            }

        }

   
    }
}


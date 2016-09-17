//-----------------------------------------------------------------
//         [Author]: Leonard.Wu 
//         [Date]: Friday, September 16, 2016  
//-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace WizardSystem {

    public class HeroModel : ModelBase {

        #region Member
        Dictionary<int, HeroData> heroDataDict;
        #endregion

        public override void Init () {
            heroDataDict = new Dictionary<int, HeroData>();
        }

        protected override void OnReconnected () {

        }

        public bool TryGetHeroData (int _id, out HeroData _data) {
            if (heroDataDict.TryGetValue(_id, out _data)) {
                return true;
            }
            else {
                _data = new HeroData();
                return false;
            }
        }

        public bool TryGetHeroBasicData (int _id, out HeroBasicData _basicData) {
            if (heroDataDict.ContainsKey(_id)) {
                _basicData = heroDataDict[_id].basicData;
                return true;
            }
            else {
                _basicData = new HeroBasicData();
                return false;
            }
        }

        public bool TryGetHeroEquipData (int _id, out HeroEquipData _equipData) {
            if (heroDataDict.ContainsKey(_id)) {
                _equipData = heroDataDict[_id].equipData;
                return true;
            }
            else {
                _equipData = new HeroEquipData();
                return false;
            }
        }

        public bool TryGetHeroMainProperty (int _id, out HeroMainProperty _mainProperty) {
            if (heroDataDict.ContainsKey(_id)) {
                _mainProperty = heroDataDict[_id].mainProperty;
                return true;
            }
            else {
                _mainProperty = new HeroMainProperty();
                return false;
            }
        }


    }

}






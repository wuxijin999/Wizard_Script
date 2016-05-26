//-----------------------------------------------------------------
//         [Author]: Leonard.Wu 
//         [Date]: Thursday, May 26, 2016  
//-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace WizardSystem {

    public class WareHouseModel : ModelBase {

        #region Member

        Dictionary<int, ItemEntry> itemDict;
        #endregion

        public override void Init () {
            itemDict = new Dictionary<int, ItemEntry>();
        }

        protected override void OnReconnected () {

        }

        public void Add (int _id, uint _num) {
            ItemEntry entry;

            if (itemDict.ContainsKey(_id)) {
                entry = itemDict[_id];
                entry.Add(_num);
                itemDict[_id] = entry;
            }
            else {
                entry = new ItemEntry(_id);
                entry.Add(_num);
                itemDict[_id] = entry;
            }

        }

        public void Del (int _id, uint _num) {
            if (!itemDict.ContainsKey(_id)) {
                WDebug.Log(string.Format("Can not find item of id :{0}", _id));
                return;
            }

            itemDict[_id].Del(_num);

            if (itemDict[_id].Num == 0) {
                itemDict.Remove(_id);
            }
        }

        public int GetItemSum () {
            return itemDict.Count;
        }

        public List<int> GetItemList () {
            return new List<int>(itemDict.Keys);
        }

    }

}






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
        public delegate void ItemHandler (int _id);
        public event ItemHandler ItemAddEvent;
        public event ItemHandler ItemRemoveEvent;
        public event ItemHandler ItemIncreaseEvent;
        public event ItemHandler ItemDecreaseEvent;

        Dictionary<int, ItemEntry> itemDict;
        #endregion

        public override void Init () {
            itemDict = new Dictionary<int, ItemEntry>();
        }

        protected override void OnReconnected () {

        }

        public void Add (int _id, int _num) {
            ItemEntry entry;

            if (itemDict.ContainsKey(_id)) {
                entry = itemDict[_id];
                entry.Add(_num);
                itemDict[_id] = entry;
                if (ItemIncreaseEvent != null) {
                    ItemIncreaseEvent(_id);
                }
            }
            else {
                entry = new ItemEntry(_id);
                entry.Add(_num);
                itemDict[_id] = entry;
                if (ItemAddEvent != null) {
                    ItemAddEvent(_id);
                }
            }

        }

        public void Del (int _id, int _num) {
            ItemEntry entry;
            if (itemDict.TryGetValue(_id, out entry)) {
                if (entry.Num > _num) {
                    itemDict[_id].Del(_num);
                    if (ItemDecreaseEvent != null) {
                        ItemDecreaseEvent(_id);
                    }
                }
                else {
                    itemDict.Remove(_id);
                    if (ItemRemoveEvent != null) {
                        ItemRemoveEvent(_id);
                    }
                }
            }
            else {
                WDebug.Log(string.Format("Can not find item of id :{0}", _id));
            }
        }

        public int GetItemSum () {
            return itemDict.Count;
        }

        public bool TryGetItemEntry (int _id, out ItemEntry _itemEntry) {
            if (itemDict.TryGetValue(_id, out _itemEntry)) {
                return true;
            }
            else {
                return false;
            }
        }

        public List<int> GetItemList () {
            return new List<int>(itemDict.Keys);
        }

    }

}






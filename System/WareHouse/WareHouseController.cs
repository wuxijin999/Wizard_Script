//-----------------------------------------------------------------
//         [Author]: Leonard.Wu 
//         [Date]: Thursday, September 15, 2016  
//-----------------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;
using WizardSystem;
using System.Collections.Generic;

namespace UI {

    public class WareHouseController : WindowBizBase {

        #region Member

        const int PileLimit = 5;

        public struct Grid {
            public int index;
            public int itemId;
            public int itemCount;

            public Grid (int _index, int _itemId, int _itemCount) {
                index = _index;
                itemId = _itemId;
                itemCount = _itemCount;
            }
        }

        WareHouseModel model;
        Dictionary<int, Grid> indexGridDict;
        Dictionary<int, List<int>> itemIdGridIndexSetDict;
        #endregion

        public WareHouseController () {

            model = model ?? ModelManager.Instance.GetModel<WareHouseModel>();
            indexGridDict = indexGridDict ?? new Dictionary<int, Grid>();
            itemIdGridIndexSetDict = itemIdGridIndexSetDict ?? new Dictionary<int, List<int>>();
            RefreshWareHouse();
        }

        #region Interaction

        public bool TryGetIndexSetByItemId (int _itemId, out List<int> _indexSet) {
            List<int> indexSet;
            if (itemIdGridIndexSetDict.TryGetValue(_itemId, out indexSet)) {
                _indexSet = new List<int>(indexSet);
                return true;
            }
            else {
                _indexSet = null;
                return false;
            }
        }

        public bool TryGetGridByIndex (int _index, out Grid _grid) {
            if (indexGridDict.TryGetValue(_index, out _grid)) {
                return true;
            }
            else {
                _grid = new Grid();
                return false;
            }
        }

        #endregion

        #region CallBack
        #endregion

        #region Functional Method

        private void RefreshWareHouse () {
            int index = 0;
            itemIdGridIndexSetDict.Clear();
            indexGridDict.Clear();
            List<int> itemList = model.GetItemList();

            int itemId = 0;
            int tempCount = 0;
            ItemEntry itemEntry;
            List<int> itemIdIndexSet;
            for (int i = 0; i < itemList.Count; i++) {
                itemId = itemList[i];
                if (model.TryGetItemEntry(itemId, out itemEntry)) {
                    tempCount = itemEntry.Num;
                    itemIdIndexSet = new List<int>();
                    while (tempCount > 0) {
                        indexGridDict.Add(index, new Grid(index, itemId, Mathf.Min(tempCount, PileLimit)));
                        itemIdIndexSet.Add(index);
                        tempCount -= PileLimit;
                        index++;
                    }

                    itemIdGridIndexSetDict.Add(itemId, itemIdIndexSet);
                }
                else {
                    continue;
                }
            }

        }


        #endregion



    }

}





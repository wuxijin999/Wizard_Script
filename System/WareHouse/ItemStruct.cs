//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Thursday, May 26, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Collections;

namespace WizardSystem {

    public struct ItemEntry {
        public int Id;
        public uint Num;

        public ItemEntry (int _id) {
            Id = _id;
            Num = 0;
        }

        public void Add (uint _num) {
            Num += _num;
        }

        public void Del (uint _num) {
            if (_num > Num) {
                Num = 0;
            }
            else {
                Num -= _num;
            }
        }

    }

}



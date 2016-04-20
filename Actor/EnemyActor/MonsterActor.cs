using UnityEngine;
using System.Collections;

namespace Fight {
    public class MonsterActor : OrganicActor {


        private EnemyCategory category;
        public EnemyCategory Category {
            get {
                return category;
            }
        }
    }

}

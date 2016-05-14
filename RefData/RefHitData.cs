//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :			 Sunday, May 15, 2016
//--------------------------------------------------------

using System.Collections;

public class RefHitData : RefDataBase {

    public int Id {
        get; private set;
    }

    static public RefHitData Get (int _id) {
        RefHitData r = null;

        if (!RefDataManager.Instance.hitData.TryGetValue(_id, out r)) {
            WDebug.Log(string.Format("Failed to get RefHitData data by id:<color=yellow>{0}</color> ", _id));
        }

        return r;
    }


}




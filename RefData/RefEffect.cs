//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :			 Wednesday, September 28, 2016
//--------------------------------------------------------

using System.Collections;

public class RefEffect : RefDataBase {

    public int id { get; private set; }
    public string resourceName { get; private set; }

    static public bool TryGet (int _id, out RefEffect _data) {
        bool successful = false;
        successful = RefDataManager.Instance.effect.TryGetValue(_id, out _data);

        if (!successful) {
            WDebug.Log(string.Format("Failed to get RefEffect data by id:<color=yellow>{0}</color> ", _id));
        }

        return successful;
    }
}




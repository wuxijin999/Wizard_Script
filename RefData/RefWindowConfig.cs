//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :			 Saturday, September 24, 2016
//--------------------------------------------------------

using System.Collections;

public class RefWindowConfig : RefDataBase {

    public int id { get; private set; }
    public string windowName { get; private set; }
    public string prefabName { get; private set; }
    public int windowType { get; private set; }
    public int animationType { get; private set; }
    public int layer { get; private set; }
    public bool fullScreen { get; private set; }
    public bool withMask { get; private set; }
    public int maskAlpha { get; private set; }
    public bool clickEmptyToClose { get; private set; }

    static public bool TryGet (int _id, out RefWindowConfig _data) {
        bool successful = false;
        successful = RefDataManager.Instance.windowConfig.TryGetValue(_id, out _data);

        if (!successful) {
            WDebug.Log(string.Format("Failed to get RefWindowConfig data by id:<color=yellow>{0}</color> ", _id));
        }

        return successful;
    }


}




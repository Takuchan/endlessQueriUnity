import UnityEngine.SceneManagement;

private var async: AsyncOperation;		//　非同期動作で使用するAsyncOperation
public var loadObj: GameObject;		//　シーンロード中に表示するUI画面
public var slider: UI.Slider;			//　読み込み率を表示するスライダー

function NextScene() {
    //　ロード画面UIをアクティブにする
    loadObj.SetActive(true);

    //　コルーチンを開始
    StartCoroutine("LoadData");
}

function LoadData() {
    // シーンの読み込みをする
    async = SceneManager.LoadSceneAsync("Main");

    //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
    while (!async.isDone) {
        var progressVal: float = Mathf.Clamp01(async.progress / 0.9f);
        slider.value = progressVal;
        yield;
    }


}
function GameEnd() {
    Application.Quit();
}
PhysicsLayers
===


## Description
Unityに簡易的な物理判定用レイヤーを追加するためのライブラリです。

Unityレイヤーはカメラレイヤーと物理レイヤーの二つの用途があるため、しばしばレイヤー数が足りなくなる事があります。  
そこで物理レイヤーだけ別の手段を用いる事でUnityレイヤーから分離させ、レイヤー数不足を改善するのが目的です。

方針としてはUnityのColliderは[`Physics.IgnoreCollision`](https://docs.unity3d.com/jp/540/ScriptReference/Physics.IgnoreCollision.html)関数によってCollider同士の衝突を無効にする事が出来ます。  
上記関数を活用して簡易的な物理レイヤーを実現します。

## Usage
1. PhysicsLayers/Prefabs/Layers Manager.prefabをゲーム空間に置いてください。
2. LayersManagerコンポーネントで物理レイヤーと衝突判定を設定してください。
    - Physics Layers項目で物理レイヤーの追加・削除・名前変更が出来ます。
    - Collision Infos項目で各レイヤーとの衝突判定の設定を出来ます。
3. 下記リストを元にして、物理判定を行うゲームオブジェクトにコンポーネントをアタッチしてください。
    - 三次元用の物理レイヤー : PhysicsLayer.cs
    - 二次元用の物理レイヤー : PhysicsLayer2D.cs
    - 三次元用のUnityレイヤー : UnityLayer.cs
    - 二次元用のUnityレイヤー : UnityLayer2D.cs
        - PhysicsLayers/Scripts/Layers/ フォルダに入っています。
        - Unityレイヤー用コンポーネントは既にある程度開発が進んでしまっているプロジェクトにも導入する事が出来るようにするための物なので、初期開発の段階から導入している場合は明確な用途はありません。

より詳しい使い方は[Example](Assets/PhysicsLayers/Example/)を参照してください。

## Behaviour
- AutoManagemenetが有効の場合は、自動で各種設定が行われます。
    - Awakeのタイミングで衝突判定の設定が行われます。
    - OnDestroyのタイミングで管理下から外されます。
- 物理レイヤーのレイヤーIDは、Unityレイヤーの最大値の31の次の32から始まります。
- 物理レイヤーのレイヤー名は空文字には出来ません。

## API
### `LayersManager`クラス
各レイヤーの管理や衝突設定等を行います。  
シングルトンを実装してありますので、`LayersManager.Instance`でアクセス出来ます。

### プロパティ
#### `const int UnityLayerCount = 32`
Unityレイヤーのレイヤー数

#### `PhysicsLayerInfos PhysicsLayerInfos { get; }`
物理レイヤーに関する情報をまとめるクラスのインスタンスを取得

#### `int PhysicsLayerCount { get; }`
物理レイヤーの登録数

#### `Dictionary<int, string> PhysicsLayers { get; }`
物理レイヤーのレイヤーIDとレイヤー名の一覧を組み合わせで取得

#### `List<int> PhysicsLayerIDs  { get; }`
物理レイヤーのレイヤーID一覧を取得

#### `List<string> PhysicsLayerNames { get; }`
物理レイヤーのレイヤー名一覧を取得

#### `Dictionary<int, string> UnityLayers { get; }`
UnityレイヤーのレイヤーIDとレイヤー名の一覧を取得

#### `List<int> UnityLayerIDs { get; }`
Unityレイヤーの有効なレイヤーID一覧を取得

#### `List<string> UnityLayerNames { get; }`
Unityレイヤーの有効なレイヤー名一覧を取得

### メソッド
#### `Dictionary<int, string> GetIgnoreLayers(int layerID)`
レイヤーIDが衝突しないレイヤーを一覧で取得

#### `bool IsPhysicsLayer(int layerID)`
レイヤーIDが物理レイヤーかどうか

#### `bool IsUnityLayer(int layerID)`
レイヤーIDがUnityレイヤーかどうか

#### `bool IsLayer(int layerID)`
レイヤーIDが有効なIDかどうか

#### `int NameToLayer(string layerName)`
レイヤー名からレイヤーIDに変換
物理レイヤーとUnityレイヤーの両方に対応します

#### `string LayerToName(int layerID)`
レイヤーIDからレイヤー名に変換
物理レイヤーとUnityレイヤーの両方に対応します

#### `void Managemant(AbstractCollisionLayer<Collider> layer)`
衝突判定の設定を行い、マネージメント対象化に加える

#### `void Managemant(AbstractCollisionLayer<Collider2D> layer)`
衝突判定の設定を行い、マネージメント対象化に加える

#### `void UnManagemant(AbstractCollisionLayer<Collider> layer)`
マネージメント対象化から外す

#### `void UnManagemant(AbstractCollisionLayer<Collider2D> layer)`
マネージメント対象化から外す

#### `void ResetIgnoreCollision(AbstractCollisionLayer<Collider> layer)`
衝突判定の設定を初期化する

#### `void ResetIgnoreCollision(AbstractCollisionLayer<Collider2D> layer)`
衝突判定の設定を初期化する

<br />

### `PhysicsLayerMask`構造体
Unityの[LayerMask](https://docs.unity3d.com/jp/540/ScriptReference/LayerMask.html)と同様に、レイヤーIDを保持出来る構造体です。  
ただし、UnityのLayerMaskとは違いマスキング機能は実装されていません。

### プロパティ
#### `int Value { get; set;}`
レイヤーID

### メソッド
#### `static string LayertoName(int layerID)`
レイヤーIDから物理レイヤー名を取得

#### `static int NameToLayer(string layerName)`
レイヤー名から物理レイヤーIDを取得

<br />

### `abstract AbstractLayer`クラス
各種レイヤー用クラスが継承している抽象クラスです。  

### プロパティ
#### `int LayerID { get; }`
レイヤーID

#### `bool AutoManagement { get; }`
自動管理を行うかどうか

### メソッド
#### `bool ChangeLayer(int layerID)`
レイヤーIDを変更する  
衝突判定も再設定されます

## Screenshots
![screen shot](./Docs/Screenshot.gif)

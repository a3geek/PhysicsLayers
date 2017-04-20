PhysicsLayers
===


## Description
Unityに簡易的な物理判定用レイヤーを追加するためのライブラリです。

UnityレイヤーはCamera LayerとPhysics Layerの二つの用途があるため、しばしばレイヤー数が足りなくなる事があります。  
そこでPhysics Layerだけ別の手段を用いる事でUnityレイヤーから分離させ、レイヤー数不足を改善するのが目的です。

方針としてはUnityのColliderは[`Physics.IgnoreCollision`](https://docs.unity3d.com/jp/540/ScriptReference/Physics.IgnoreCollision.html)関数によってCollider同士の衝突を無効にする事が出来ます。  
上記関数を活用して簡易的なPhysics Layerを実現します。

## Usage
1. PhysicsLayers/Prefabs/Layers Manager.prefabをゲーム空間に置いてください。
2. LayersManagerコンポーネントでPhysics Layerと衝突判定を設定してください。
    - Physics Layers項目でPhysics Layerの追加・削除・名前変更が出来ます。
    - Collision Infos項目で各レイヤーとの衝突判定の設定を出来ます。
3. 下記リストを元にして、物理判定を行うゲームオブジェクトにコンポーネントをアタッチしてください。
    - 三次元用の物理レイヤー : PhysicsLayer.cs
    - 二次元用の物理レイヤー : PhysicsLayer2D.cs
    - 三次元用のUnityレイヤー : Layer.cs
    - 二次元用のUnityレイヤー : Layer2D.cs
        - PhysicsLayers/Scripts/Layers/ フォルダに入っています。
        - Unityレイヤー用コンポーネントは既にある程度開発が進んでしまっているプロジェクトにも導入する事が出来るようにするための物なので、初期開発の段階から導入している場合は明確な用途はありません。

より詳しい使い方は[Example](Assets/PhysicsLayers/Example/)を参照してください。

## Behaviour
- AutoManagemenetが有効の場合は、自動で各種設定が行われます。
    - Awakeのタイミングで衝突判定の設定が行われます。
    - OnDestroyのタイミングで管理下から外されます。
- Physics LayerのLayerIDは、Unityレイヤーの最大値の31の次の32から始まります。
- Physics LayerのLayerNameは空文字には出来ません。

## API
### `LayersManager`クラス
各レイヤーの管理や衝突設定等を行います。  
シングルトンを実装してありますので、`LayersManager.Instance`でアクセス出来ます。

### プロパティ
#### 

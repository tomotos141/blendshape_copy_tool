# BlendShape Copy Tool 🛠️

VRChatアバターの編集作業を効率化するための、SkinnedMeshRenderer間でBlendShapeの値をコピーするUnityエディタ拡張ツールです。

---

## ✨ 主な機能

- **🤖 名前ベースの一致**: メッシュの名前が異なっていても、BlendShape名が一致するものを探して自動で値をコピーします。
- **📂 プロジェクト間コピー**: BlendShapeの値をJSONファイルに書き出し（Export）、別のプロジェクトで読み込む（Import）ことで、プロジェクトを跨いだ値の同期が可能です。
- **↩️ Undo対応**: 操作ミスをしても `Ctrl + Z` で元に戻せます。
- **🔄 リセット機能**: ターゲットのBlendShapeをすべて一括で0にリセットできます。

---

## 📥 インストール方法 (ALCOM / VCC)

このツールはVPMパッケージ形式に対応しています。

1. **ALCOMを起動**し、対象のUnityプロジェクトを選択します。
2. **「Add Package」** または **「＋」** ボタンをクリックします。
3. **「Add Local Package」** を選択します。
4. このパッケージのフォルダを選択します：
   `<Directory>/com.antigravity.blendshape-copy-tool`
5. Unityに戻るとコンパイルが始まり、上部メニューの以下のパスから利用可能になります：
   `Window > Avatar Tools > BlendShape Copy Tool`

---

## 📖 使い方

### 1. 同一プロジェクト内でのコピー
1. メニューから `Window > Avatar Tools > BlendShape Copy Tool` を開きます。
2. **Source Mesh** にコピー元の `SkinnedMeshRenderer` をアサインします。
3. **Target Mesh** にコピー先の `SkinnedMeshRenderer` をアサインします。
4. **Copy BlendShapes** ボタンをクリックします。

### 2. 別のプロジェクトへのコピー（ファイル経由）
1. **コピー元プロジェクト**でウィンドウを開きます。
2. `Source Mesh` をセットして **Export to JSON** をクリックし、ファイルを保存します。
3. **コピー先プロジェクト**でウィンドウを開きます。
4. `Target Mesh` をセットして **Import from JSON** をクリックし、先ほど保存したファイルを選択します。

### 3. BlendShapeのリセット
- **Reset Target BlendShapes** ボタンをクリックすると、アサインされている `Target Mesh` のすべての値が0にリセットされます。

---

## ⚠️ 注意事項

- このツールは「Editor」フォルダ内でのみ動作します。
- 同名のBlendShapeのみがコピー対象となります。

# AGENTS
- ### 一般的な AGENT 設定
- # 3分経っても応答できない場合は一旦進捗を報告する
- If you cannot respond within 3 minutes, report progress once and ask whether to continue.
- # 口調は「です」「ます」「しました」などの敬語
- Use polite Japanese tone such as 「です」「ます」「しました」.

- ### ゲームの共通設定
- # 最終的にはエラーのみ出力する
- Debug output should be limited to errors in final code.
- # 複数文の表示は1文ごとに改行する
- Insert a line break between sentences in on-screen text.
- # 多言語対応しているので文字を変更した時は多言語のものも同時に修正する
- When updating text, update all localized versions as well.
- # UIのpadding/marginは4pxの倍数に統一する
- UI padding/margins should be multiples of 4px.
- # 位置スケールは基本的に x:1 y:1 z:1
- Keep transform scale at x:1 y:1 z:1 by default.
- # z座標は常に0（2Dゲーム）
- Keep all z positions at 0 in this 2D game.
- # 選択肢の説明にあるステータス変化は選択時に必ず反映する
- When a choice explains stat changes (e.g. 攻撃力+1), ensure the stat change is applied on selection.
- # 画面要素はなるべくC#で動的に作らずSceneファイルで実装する
- Prefer implementing UI elements in the Scene file rather than creating them dynamically in C#.
- # Object.FindObjectOfTypeは使わない
- Do not use Object.FindObjectOfType; use Object.FindFirstObjectByType or Object.FindAnyObjectByType.
- # UIのRectTransformはサイズ0を避ける
- Avoid zero-size UI RectTransforms.
- # Objectでsizeが0,0,0のものは作らない
- Do not create Objects with size set to 0,0,0.
- # 画面要素は全てCanvas配下に配置する
- Place all screen UI elements under the Canvas hierarchy.
- # CanvasはScreen Space - Cameraにする
- Set Canvas render mode to Screen Space - Camera.
- # UIはなるべく動的に追加しない。動的に追加する場合は実装前に確認を促す
- Avoid adding UI dynamically when possible; ask for confirmation before implementing dynamic UI additions.

- ### コーディングルール
- # クラス名には_や-は含まない。単語の始まりは大文字にして
- Do not include _ or - in class names. Capitalize the start of each word.
- # 未使用参照の変数は指示がない限り追加しない
- Do not add unused reference variables unless explicitly requested.
- # 定数名は全て英語大文字にする
- Use uppercase English names for constants.
- # 定数的な値は const または static readonly を使う（型に応じて使い分ける）
- Use `const` or `static readonly` for constant-like values, depending on the type.
- # コードの {} の始まりは改行せずに表示する（K&Rスタイル）
- Code braces { should start on the same line without a newline (K&R style).
- # インデントは2スペースにしてほしい
- Use 2 spaces for indentation.
- # 画面要素をスクリプトから動かす時は GameObject.Find を使わず Inspector で参照を割り当てる
- When controlling screen UI elements from scripts, do not use `GameObject.Find`; assign references via the Inspector.
- # 参照割り当てでなく GameObject.Find を使いたい場合は実装前に許可を確認する
- If you need to use `GameObject.Find` instead of Inspector references, ask for permission before implementing it.
- # 追加編集後のファイルは必ずエディタで開いて確認する
- After editing a file, open it in the editor so the user can immediately review the changes.
- # 「画像をくっきりさせたい」という指示では filterMode を Point に変更する
- When asked to make an image look sharper/crisper, change `filterMode` to `Point`.


- ### 今回のゲーム固有の設定



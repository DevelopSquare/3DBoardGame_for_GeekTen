# メッセージダイアログの利用方法
1. Assets/Resources/Message内の「MessageCanvas」をシーン内のヒエラルキーに追加  

2. Assets\Scripts_Message内の「CreateMessage.cs」内の内容をコピーし新たなcsファイルを作 成してペーストする。、

3. ペーストしたcsファイルのOnDialog関数内の「string msg = "";」に表示させたい文字を、「string kind = "";」はYes or Noのダイアログにしたい場合は"Choice"と、Closeダイアログにしたい場合は"Close"と入力する。

4. ペーストしたcsファイルのAction関数内の各入力の目的に応じた処理を追加する。

5. 作成したcsファイルをシーン内のオブジェクトに追加し、OnDialog関数をトリガーにする。

※MessageCanvasのSortOrderは10に指定（MenuContainerは5）
# Windows Spotlight

## Dependency [Everything](https://www.voidtools.com/)

## Mock Tools [JustMock](http://www.telerik.com/justmock/free-mocking)

--------------------------------------------------

## 焦點問題
WPF 分為邏輯焦點以及鍵盤焦點
FocusManager是掌控邏輯焦點
Keyboard.Focus()是掌控鍵盤焦點
有鍵盤焦點才可以接收鍵盤輸入的資訊
視窗開啟時，可能沒有活躍，焦點事件沒有效果
要使用window.Activate()之後才可以使用焦點事件
且要等到視窗render完後，鍵盤事件才有效果
因此要用下列程式，等待Render結束後才執行焦點程式
```
Dispatcher.BeginInvoke((Action)delegate
{
    FocusManager.SetFocusedElement(this, InputTextBox);
    Keyboard.Focus(InputTextBox);
}, DispatcherPriority.Render);
```

## ListBox.Template
[參考](https://blogs.msdn.microsoft.com/ericsk/2013/04/18/windows-store-app-windows-phone-app-listview-listbox-2/)
## ListBox Binding
[參考](https://blogs.msdn.microsoft.com/ericsk/2013/04/18/windows-store-app-windows-phone-app-listview-listbox-2/)
## Keyboard Hook
[來源](http://www.dylansweb.com/2014/10/low-level-global-keyboard-hook-sink-in-c-net/)
## Everything SDK
[來源](http://www.voidtools.com/support/everything/sdk/)

## 找不到.dll
> 要把.dll放到跟.exe同一個目錄之下

## 把圖片加入Resource
[來源](http://stackoverflow.com/questions/10673957/load-image-from-folder-in-solution)
> 將圖片加入Resource後就不須用將圖片跟.exe放在同一層

## click item
[source](http://stackoverflow.com/questions/10207888/wpf-listview-detect-when-selected-item-is-clicked)

## PreviewKeyDown, KeyDown
PreviewKeyDown Event支援up, down, left, right按鍵
KeyDown Event不支援

## 自訂Selected item
利用WPF的<Setter>來設定ListView的Property，並Binding到自訂的變數上，達到修改Property的效果
[source](http://stackoverflow.com/questions/1069577/wpf-listview-programmatically-select-item)

## 移除listbox item hover效果
[source](http://stackoverflow.com/questions/15632493/wpf-listbox-turn-off-hover-effect)

## 修改list item IsSelected的Style
[source](http://stackoverflow.com/questions/28686752/changing-wpf-listbox-selecteditem-text-color-and-highlight-background-color-usin)

## textbox浮水印
第一個方法大家說很爛
第二個方法我用不起來
第三個方法好棒棒，不用額外的擴展，直接把xml放上去就會動惹，懶人專用QQ
[source](http://stackoverflow.com/questions/833943/watermark-hint-text-placeholder-textbox-in-wpf)

## 移除left padding fro wpf listbox with groupstyle
[source](http://stackoverflow.com/questions/12814283/remove-left-padding-for-a-wpf-listboxitem-with-groupstyle-defined-for-its-listbo)

## WebBrowser 與 AllowTransparency的衝突
[source](http://www.cnblogs.com/greed/p/5213235.html)
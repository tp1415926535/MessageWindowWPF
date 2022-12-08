# MessageWindowWPF
Includes MessageBox with a more modern interface and rich text support, InputBox, can auto-closing Prompt Window.      
包括支持富文本的更现代化界面的消息框、输入框、可自动关闭的提示窗。    
   
MessageBox:     
![MessageBox](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/MessageBox2.jpg)      
    
InputBox:     
![InputBox](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/InputBox2.jpg)     
    
Prompt:     
![Prompt](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/Prompt2.jpg)     
    
     
[![release](https://img.shields.io/github/v/release/tp1415926535/MessageWindowWPF?color=green&logo=github)](https://github.com/tp1415926535/MessageWindowWPF/releases) 
[![nuget](https://img.shields.io/nuget/v/MessageWindowWPF?color=lightblue&logo=nuget)](https://www.nuget.org/packages/MessageWindowWPF) 
[![license](https://img.shields.io/static/v1?label=license&message=MIT&color=silver)](https://github.com/tp1415926535/MessageWindowWPF/blob/master/LICENSE) 
![C#](https://img.shields.io/github/languages/top/tp1415926535/MessageWindowWPF)        

[![Description](https://img.shields.io/static/v1?label=English&message=Description&color=yellow)](https://github.com/tp1415926535/MessageWindowWPF#english-description) 
[![说明](https://img.shields.io/static/v1?label=中文&message=说明&color=red)](https://github.com/tp1415926535/MessageWindowWPF#中文说明)      
       
       
# English Description
Download package from Nuget, or using the release Dll.   
  
## MessageBox 
![MessageBox](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/MessageBox1.jpg)       
      
Except for the absence of the MessageBoxOptions parameter, all other features are seamlessly integrated with the original version.   
```c#
using MessageBox = MessageWindowWPF.MessageBox; //Just add the namespace

  MessageBox.Show("Message!");
  MessageBox.Show("Message2!", "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
```

![MessageBox-richtext](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/MessageBox3.jpg)     
If you want to display rich text, you can also just pass in the parameters:
```c#
using MessageBox = MessageWindowWPF.MessageBox; 

  List<Inline> inlines = new List<Inline>();
  inlines.Add(new Run("normal text. "));
  inlines.Add(new Run("red text.") { Foreground = Brushes.Red });
  MessageBox.Show(inlines, "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
```

Some configurations can also be customized:
```c#
using MessageWindowWPF;
using MessageBox = MessageWindowWPF.MessageBox;

  MessageSetting.settings.NoSystemHeader = MessageSetting.settings.WithCornerRadius = true; //Without system title bar, and change to rounded corners
  MessageSetting.settings.BackGroundColor = Colors.Black;
  MessageBox.Show("Message!", "Tip", MessageBoxButton.OK, MessageBoxImage.Information);
```

## InputBox
![InputBox](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/InputBox1.jpg)      

Input box similar to VB's component.
```c#
using MessageWindowWPF;

  InputBox inputBox = new InputBox();
  if (inputBox.ShowDialog("Write Something:", "Title") == true)
    Console.WriteLine(inputBox.value);
```
Function: `inputBox.ShowDialog(string message = null, string title = null, string defaultValue = null)`, return bool?.

Some configurations can also be customized:
```c#
using MessageWindowWPF;

  MessageSetting.settings.NoSystemHeader = MessageSetting.settings.WithCornerRadius = true;
  MessageSetting.settings.BackGroundColor = Colors.Black;
  InputBox inputBox = new InputBox();
  if (inputBox.ShowDialog("Write Something:", "Title") == true)
    Console.WriteLine(inputBox.value);
```

## Prompt
![Prompt](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/Prompt1.jpg)      

Fade-in and fade-out cues, support countdown and double click to close.
```c#
using MessageWindowWPF;

  Prompt.Show("Show text");
```
Function: `Prompt.Show(string content, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)`, return Window.

![Prompt-richtext](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/Prompt3.jpg)     
If you want to display rich text, you can also just pass in the parameters:
```c#
using MessageWindowWPF;

  List<Inline> inlines = new List<Inline>();
  inlines.Add(new Run("normal text. "));
  inlines.Add(new Run("red text.") { Foreground = Brushes.Red });
  Prompt.Show(inlines);
```

Custom configurations only have an effect on the color, but the color parameter of the function has a higher priority:
```c#
using MessageWindowWPF;

  MessageSetting.settings.BackGroundColor = Colors.Black;
  Prompt.Show("Show text");
```

## Additional information
* Because there are only four buttons, the current text only comes with Chinese and English. 
The default is displayed in the current language. Alternatively, you can set it manually by changing the value of **"MessageSetting.settings.UIculture"**. 

## Version   
* v1.0.0 2022/12/07 Basic features. 
---
   
# 中文说明   

从Nuget下载包，或者引用release的Dll。   
  
## 消息窗（MessageBox）
![MessageBox](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/消息窗1.jpg)      
除了没有 MessageBoxOptions 参数之外，其他功能都与原版无缝衔接。     
```c#
using MessageBox = MessageWindowWPF.MessageBox; //添加这行即可

  MessageBox.Show("消息！");
  MessageBox.Show("消息2！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
```
  
![MessageBox-richtext](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/消息窗3.jpg)     
如果想要显示富文本，也只需传入参数即可:
```c#
using MessageBox = MessageWindowWPF.MessageBox; 

  List<Inline> inlines = new List<Inline>();
  inlines.Add(new Run("普通文本。 "));
  inlines.Add(new Run("红色文本。") { Foreground = Brushes.Red });
  MessageBox.Show(inlines, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
```

还可以自定义一些配置：
```c#
using MessageWindowWPF;
using MessageBox = MessageWindowWPF.MessageBox;

  MessageSetting.settings.NoSystemHeader = MessageSetting.settings.WithCornerRadius = true;//不使用系统标题栏，以及变为圆角
  MessageSetting.settings.BackGroundColor = Colors.Black;
  MessageBox.Show("消息!", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
```

## 输入框（InputBox）
![InputBox](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/输入框1.jpg)      
类似于VB组件的输入框
```c#
using MessageWindowWPF;

  InputBox inputBox = new InputBox();
  if (inputBox.ShowDialog("输入提示：", "标题") == true)
    Console.WriteLine(inputBox.value);
```
函数: `inputBox.ShowDialog(string message = null, string title = null, string defaultValue = null)`， 返回 bool?。

还可以自定义一些配置：
```c#
using MessageWindowWPF;

  MessageSetting.settings.NoSystemHeader = MessageSetting.settings.WithCornerRadius = true;
  MessageSetting.settings.BackGroundColor = Colors.Black;
  InputBox inputBox = new InputBox();
  if (inputBox.ShowDialog("输入提示：", "标题") == true)
    Console.WriteLine(inputBox.value);
```

## 提示（Prompt）
![Prompt](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/提示窗1.jpg)      
淡入和淡出的提示，支持倒计时和双击关闭。
```c#
using MessageWindowWPF;

  Prompt.Show("提示文字");
```
函数： `Prompt.Show(string content, double liveSeconds = 3, Window owner = null, Point? point = null, Color? backColor = null)`， 返回提示窗体。   
   
![Prompt-richtext](https://github.com/tp1415926535/MessageWindowWPF/blob/master/ScreenShot/提示窗3.jpg)     
如果想要显示富文本，也只需传入参数即可:
```c#
using MessageWindowWPF;

  List<Inline> inlines = new List<Inline>();
  inlines.Add(new Run("普通文本。 "));
  inlines.Add(new Run("红色文本。") { Foreground = Brushes.Red });
  Prompt.Show(inlines);
```

自定义配置只有颜色有作用，但是调用函数的颜色参数优先级更高:
```c#
using MessageWindowWPF;

  MessageSetting.settings.BackGroundColor = Colors.Black;
  Prompt.Show("提示文字");
```

## 其他说明
* 因为只有四个按钮，所以目前的文字只带有中文和英文。
默认按当前语言显示，另外还可以通过改变 **MessageSetting.settings.UIculture** 的值来手动设置它。

## Version   
* v1.0.0 2022/12/07 基本功能. 

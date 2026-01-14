# VintaSoft WinForms GIF Animator Demo

This C# project uses <a href="https://www.vintasoft.com/vsimaging-dotnet-index.html">VintaSoft Imaging .NET SDK</a> and demonstrates how to create, display, pack, add page and save animated GIF file:
* Create, display, pack, add page and save animated GIF file.
* Navigate GIF images: first, previous, next, last.
* Change the background color.
* Manipulate GIF file pages: optimize/deoptimize, invert/convert colors, view/change global palette.
* Manipulate GIF page: insert, remove, replace, move up/down, save page/frame, deoptimize, view/change frame palette.
* Animate GIF images: create, start/stop, show in browser, set delay, repeat, reverse.


## Screenshot
<img src="vintasoft-gif-animator-demo.png" title="VintaSoft GIF Animator Demo">


## Usage
1. Get the 30 day free evaluation license for <a href="https://www.vintasoft.com/vsimaging-dotnet-index.html" target="_blank">VintaSoft Imaging .NET SDK</a> as described here: <a href="https://www.vintasoft.com/docs/vsimaging-dotnet/Licensing-Evaluation.html" target="_blank">https://www.vintasoft.com/docs/vsimaging-dotnet/Licensing-Evaluation.html</a>

2. Update the evaluation license in "CSharp\MainForm.cs" file:
   ```
   Vintasoft.Imaging.ImagingGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");
   ```

3. Build the project ("GifAnimatorDemo.Net10.csproj" file) in Visual Studio or using .NET CLI:
   ```
   dotnet build GifAnimatorDemo.Net10.csproj
   ```

4. Run compiled application and try to create, display, pack, add page and save animated GIF file.


## Documentation
VintaSoft Imaging .NET SDK on-line User Guide and API Reference for .NET developer is available here: https://www.vintasoft.com/docs/vsimaging-dotnet/


## Support
Please visit our <a href="https://myaccount.vintasoft.com/">online support center</a> if you have any question or problem.

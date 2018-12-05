# ExeGenerator
一个用C#写的C#编译器 :P 用于跨平台mono环境，在 需要动态编译代码的地方，比如热更新，复杂功能，反破解，免杀等方面非常实用，所以分享出来。

Example: 

(Linux/MacOS) mono EG.exe -i source.cs -o output.exe -r System.dll,DaWebSocket.dll

(Windows) EG -i source.cs -o output.exe -r System.dll,DaWebSocket.dll

Parameters:
        -i | input file (required)
        -o | output file (defalt : "./output.exe")
        -r | reference libiary
        -h | help

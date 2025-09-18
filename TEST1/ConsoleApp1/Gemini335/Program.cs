//需同时引用库ob_csharp、OrbbecSDK
using Gemini335;
using Orbbec;

Gemini335Camera gemini335 = new Gemini335Camera(640, 480, 30);

await gemini335.GetRGBDImgAsync(true);








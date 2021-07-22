using System.Threading.Tasks;
using PuppeteerSharp;

namespace QZoneUploader
{
    public static class PuppeteerSharpExtension
    {
        public static async Task<Frame> WaitForFrameAsync(this Page page, params string[] frameNameList)
        {
            for (int i = 0; i < 30; i++)
            {
                var frame = FindFrame(page.Frames, frameNameList);

                if (frame != null)
                {
                    return frame;
                }

                await Task.Delay(1000);
            }

            throw new WaitTaskTimeoutException();
        }

        private static Frame FindFrame(Frame[] list, string[] nameList)
        {
            foreach (var frame in list)
            {
                foreach (var name in nameList)
                {
                    if (frame.Name == name)
                    {
                        return frame;
                    }
                }
            }
            return null;
        }
    }
}

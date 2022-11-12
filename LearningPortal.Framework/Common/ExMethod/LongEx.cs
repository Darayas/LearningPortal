namespace LearningPortal.Framework.Common.ExMethod
{
    public static class LongEx
    {
        public static string GetFileSizeTitle(this long FileSize)
        {
            string[] Titles = { "B", "KB", "MB", "GB", "TB", "ExB" };
            double Number = FileSize;
            int Index = 0;

            while (Number > 1024)
            {
                Number /= 1024;
                Index++;
            }

            return Number.ToString("0.#")+" "+ Titles[Index];
        }
    }
}

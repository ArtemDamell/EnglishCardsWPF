using System.IO;
using System.Reflection;

namespace EnglishCards
{
    public class Dictionary
    {
        public int Total { get; private set; } = 0;
        readonly string projectFolderPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName, "EnglishCards");

        string[] english;
        string[] russian;
        string[] audio_eng;
        string[] audio_rus;
        string[] image;
        string[] video;
        public Dictionary()
        {
            Init();
        }

        //This code checks if the given number is within the range of the total number of elements in the array. If it is, it returns the element at the given index, otherwise it returns an empty string. 
        public string English(int nr)
        {
            //Check if the given number is within the range of the total number of elements in the array
            if (nr < 0 || nr >= Total)
                //If not, return an empty string
                return "";
            else
                //If it is, return the element at the given index
                return english[nr];
        }

        /// <summary>
        /// Returns the Russian string from the given index.
        /// </summary>
        /// <param name="nr">The index of the Russian string.</param>
        /// <returns>The Russian string from the given index.</returns>
        public string Russian(int nr)
        {
            if (nr < 0 || nr >= Total) return "";
            return russian[nr];
        }

        public string Audio_eng(int nr)
        {
            if (nr < 0 || nr >= Total) return "";
            return audio_eng[nr];
        }

        public string Audio_rus(int nr)
        {
            if (nr < 0 || nr >= Total) return "";
            return audio_rus[nr];
        }

        public string Image(int nr)
        {
            if (nr < 0 || nr >= Total) return "";
            return image[nr];
        }

        public string Video(int nr)
        {
            if (nr < 0 || nr >= Total) return "";
            return video[nr];
        }

        /// <summary>
        /// Initializes the total, english, russian, audio_eng, audio_rus, image, and video arrays with the data from the text files in the project folder.
        /// </summary>
        private void Init()
        {
            Total = 0;
            DirectoryInfo di = new DirectoryInfo($"{projectFolderPath}\\data\\text\\");
            FileInfo[] info = di.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
            Total = info.Length;
            english = new string[Total];
            russian = new string[Total];
            audio_eng = new string[Total];
            audio_rus = new string[Total];
            image = new string[Total];
            video = new string[Total];

            int j = 0;
            foreach (FileInfo file in info)
            {
                string filename = file.FullName;
                string name = file.Name.Replace(".txt", "");

                string[] lines = File.ReadAllLines(filename);
                english[j] = lines[0];
                russian[j] = lines[1];

                audio_eng[j] = $"{projectFolderPath}\\data\\english\\" + name + ".mp3";
                audio_rus[j] = $"{projectFolderPath}\\data\\russian\\" + name + ".mp3";
                image[j] = $"{projectFolderPath}\\data\\images\\" + name + ".jpg";
                video[j] = $"{projectFolderPath}\\data\\video\\" + name + "mp4";

                if (!File.Exists(audio_eng[j]))
                    audio_eng[j] = "";
                else
                    audio_eng[j] = $"{projectFolderPath}\\data\\english\\" + name + ".mp3";

                if (!File.Exists(audio_rus[j]))
                    audio_rus[j] = "";
                else
                    audio_rus[j] = $"{projectFolderPath}\\data\\russian\\" + name + ".mp3";

                if (!File.Exists(image[j]))
                    image[j] = "";
                else
                    image[j] = $"{projectFolderPath}\\data\\images\\" + name + ".jpg";

                if (!File.Exists(video[j]))
                    video[j] = "";
                else
                    video[j] = $"{projectFolderPath}\\data\\video\\" + name + "mp4";

                j++;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Music.SDK.ViewModel.Response
{
    public class MusicLyricResult
    {
        public MusicLyricResult()
        {
            Lyrics = new List<MusicLyricItemResult>();
        }
        public MusicLyricResult(string LrcText) : this()
        {
            string[] lines = LrcText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                if (line.StartsWith("[ti:"))
                {
                    Title = SplitInfo(line);
                }
                else if (line.StartsWith("[ar:"))
                {
                    Artist = SplitInfo(line);
                }
                else if (line.StartsWith("[al:"))
                {
                    Album = SplitInfo(line);
                }
                else if (line.StartsWith("[by:"))
                {
                    LrcBy = SplitInfo(line);
                }
                else if (line.StartsWith("[offset:"))
                {
                    Offset = SplitInfo(line);
                }
                else
                {
                    try
                    {
                        Regex regexword = new Regex(@".*\](.*)");
                        Match mcw = regexword.Match(line);
                        string word = mcw.Groups[1].Value;
                        if (word.Replace(" ", "") == "")
                            continue; // 如果为空歌词则跳过不处理
                        Regex regextime = new Regex(@"\[([0-9.:]*)\]", RegexOptions.Compiled);
                        MatchCollection mct = regextime.Matches(line);
                        foreach (Match item in mct)
                        {
                            MusicLyricItemResult lineLyricItem = new MusicLyricItemResult
                            {
                                Lyric = word,
                                Time = item.Groups[1].Value
                            };
                            Lyrics.Add(lineLyricItem);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string LrcBy { get; set; }

        public string Offset { get; set; }

        public List<MusicLyricItemResult> Lyrics { get; set; }

        public override string ToString()
        {
            string result = string.Empty;
            result += $"[ti:{Title}]\n";
            result += $"[ar:{Artist}]\n";
            result += $"[al:{Album}]\n";
            result += $"[by:{LrcBy}]\n";
            result += $"[offset:{Offset}]\n";
            foreach (var lineLyricItem in Lyrics)
            {
                result += $"[{lineLyricItem.Time}]{lineLyricItem.Lyric}\n";
            }
            return result;
        }

        private static string SplitInfo(string line)
        {
            return line.Substring(line.IndexOf(":") + 1).TrimEnd(']');
        }
    }
    public class MusicLyricItemResult
    {
        public string Lyric { get; set; }

        public string Time { get; set; }
    }
}

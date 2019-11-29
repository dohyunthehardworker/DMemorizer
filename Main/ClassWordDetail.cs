using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class ClassWordDetail
    {
        public int WordDetailIdx { get; set; }      //단어상세기본키
        public int WordIdx { get; set; }            //단어기본키
        public string WordParts { get; set; }       //단어품사
        public string WordMeaning { get; set; }     //단어뜻
        public string WordPronounce { get; set; }   //단어발음

        public ClassWordDetail(int wordDetailIdx, int wordIdx, string wordParts, string wordMeaning, string wordPronounce)
        {
            WordDetailIdx = wordDetailIdx;
            WordIdx = wordIdx;
            WordParts = wordParts;
            WordMeaning = wordMeaning;
            WordPronounce = wordPronounce;
        }
        public override bool Equals(object obj)
        {
            return obj is ClassWordDetail detail &&
                   WordDetailIdx == detail.WordDetailIdx &&
                   WordIdx == detail.WordIdx &&
                   WordParts == detail.WordParts &&
                   WordMeaning == detail.WordMeaning &&
                   WordPronounce == detail.WordPronounce;
        }
        public override string ToString()
        {
            return "WordDetailIdx = " + WordDetailIdx + " , WordIdx = " + WordIdx + " WordParts , = " + WordParts
                + " WordMeaning , = " + WordMeaning + " , WordPronounce = " + WordPronounce;
        }
        public override int GetHashCode()
        {
            var hashCode = 1853776200;
            hashCode = hashCode * -1521134295 + WordDetailIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + WordIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WordParts);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WordMeaning);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WordPronounce);
            return hashCode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class ClassWord
    {
        public int LangIdx { get; set; }                //언어기본키
        public string LangName { get; set; }            //언어명
        public int TestGroupIdx { get; set; }      //시험그룹기본키
        public string TestGroupName { get; set; }     //시험그룹명
        public int TestIdx { get; set; }            //시험기본키
        public string TestName { get; set; }           //시험명
        public int TestLevelIdx { get; set; }      //시험등급기본키
        public string TestLevelName { get; set; }     //시험등급명
        public int WordIdx { get; set; }            //단어기본키
        public string Word { get; set; }                //단어
        public int WordDupNo { get; set; }         //중복단어순번
        public List<ClassWordDetail> WordDetailList { get; set; }

        public ClassWord(int langIdx, string langName, int testGroupIdx, string testGroupName, int testIdx, string testName, int testLevelIdx, string testLevelName, int wordIdx, string word, int wordDupNo, List<ClassWordDetail> wordDetailList)
        {
            LangIdx = langIdx;
            LangName = langName;
            TestGroupIdx = testGroupIdx;
            TestGroupName = testGroupName;
            TestIdx = testIdx;
            TestName = testName;
            TestLevelIdx = testLevelIdx;
            TestLevelName = testLevelName;
            WordIdx = wordIdx;
            Word = word;
            WordDupNo = wordDupNo;
            WordDetailList = wordDetailList;
        }

        public ClassWord(int wordIdx, string word, int wordDupNo, List<ClassWordDetail> wordDetailList)
        {
            WordIdx = wordIdx;
            Word = word;
            WordDupNo = wordDupNo;
            WordDetailList = wordDetailList;
        }

        public override bool Equals(object obj)
        {
            return obj is ClassWord word &&
                   LangIdx == word.LangIdx &&
                   LangName == word.LangName &&
                   TestGroupIdx == word.TestGroupIdx &&
                   TestGroupName == word.TestGroupName &&
                   TestIdx == word.TestIdx &&
                   TestName == word.TestName &&
                   TestLevelIdx == word.TestLevelIdx &&
                   TestLevelName == word.TestLevelName &&
                   WordIdx == word.WordIdx &&
                   Word == word.Word &&
                   WordDupNo == word.WordDupNo &&
                   EqualityComparer<List<ClassWordDetail>>.Default.Equals(WordDetailList, word.WordDetailList);
        }

        public override int GetHashCode()
        {
            var hashCode = 1022877050;
            hashCode = hashCode * -1521134295 + LangIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LangName);
            hashCode = hashCode * -1521134295 + TestGroupIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TestGroupName);
            hashCode = hashCode * -1521134295 + TestIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TestName);
            hashCode = hashCode * -1521134295 + TestLevelIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TestLevelName);
            hashCode = hashCode * -1521134295 + WordIdx.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Word);
            hashCode = hashCode * -1521134295 + WordDupNo.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ClassWordDetail>>.Default.GetHashCode(WordDetailList);
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

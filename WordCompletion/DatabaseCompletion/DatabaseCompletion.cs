using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordCompletion
{
    public class DatabaseCompletion : IComplementarable
    {
        private SqlConnection cnn;
        private string databasePath = @"\data\WordsDatabase.mdf";
        private bool sortingByUsesCount = true;
        private bool usingPLVocabulary = false;

        public DatabaseCompletion()
        {
            cnn = GetConnection();
            cnn.Open();
        }

        private SqlConnection GetConnection()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + directory + databasePath + @";Integrated Security=True";
            return new SqlConnection(connetionString);
        }

        public void Insert(string word, int usesCount = 1)
        {
            SqlCommand cmdUpdate = new SqlCommand("UPDATE UserWords SET UsesCount = UsesCount + " + usesCount + " WHERE Word = '" + word + "'", cnn);
            if(cmdUpdate.ExecuteNonQuery() == 0)
            {
                SqlCommand cmdInsert = new SqlCommand("INSERT INTO UserWords (Word,UsesCount) VALUES ('" + word + "','" + usesCount + "')", cnn);
                cmdInsert.ExecuteNonQuery();
            }
        }

        public void InsertWords(Dictionary<string, int> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                Insert(dictionary.Keys.ElementAt(i), dictionary.Values.ElementAt(i));
            }
        }

        public void ResetWords(Dictionary<string, int> dictionary)
        {
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM UserWords", cnn);
            cmdDelete.ExecuteNonQuery();

            for (int i = 0; i < dictionary.Count; i++)
            {
                SqlCommand cmdInsert = new SqlCommand("INSERT INTO UserWords (Word,UsesCount) VALUES ('" + dictionary.Keys.ElementAt(i) + "','" + dictionary.Values.ElementAt(i) + "')", cnn);
                cmdInsert.ExecuteNonQuery();
            }
        }

        public void SortByUsesCount(bool enable)
        {
            sortingByUsesCount = enable;
        }

        public void UsePLVocabulary(bool enable)
        {
            usingPLVocabulary = enable;
        }

        public Dictionary<string, int> GetAllUserWords()
        {
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM UserWords", cnn);
            SqlDataReader dataReader = cmdSelect.ExecuteReader();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            while (dataReader.Read())
            {
                string word = dataReader["Word"].ToString();
                int count = Int32.Parse(dataReader["UsesCount"].ToString());

                if (dictionary.ContainsKey(word))
                {
                    dictionary[word] += count;
                }
                else
                {
                    dictionary.Add(word, count);
                }
            }
            return dictionary;
        }

        public Dictionary<string, int> FindMatches(string prefix, int max = 0)
        {
            if (sortingByUsesCount)
            {
                return FindMostUsedMatches(prefix, max);
            }
            else
            {
                return FindUnorderedMatches(prefix, max);
            }
        }

        public Dictionary<string, int> FindUnorderedMatches(string prefix, int max = 0)
        {
            string selectQuery = "SELECT ";
            if (max > 0)
            {
                selectQuery += "TOP(" + max + ") ";
            }
            if (usingPLVocabulary)
            {
                selectQuery +=
                    "W.Word, SUM(W.UsesCount) as UsesSum " +
                    "FROM ( " +
                            "(SELECT * FROM UserWords " +
                            "WHERE UserWords.Word LIKE '" + prefix + "%') " +
                            "UNION ALL " +
                            "(SELECT * FROM Words " +
                            "WHERE Words.Word LIKE '" + prefix + "%') " +
                        ") AS W " +
                    "GROUP BY W.Word";
            }
            else
            {
                selectQuery += "* FROM UserWords WHERE Word LIKE '" + prefix + "%'";
            }

            SqlCommand cmdSelect = new SqlCommand(selectQuery, cnn);
            SqlDataReader dataReader = cmdSelect.ExecuteReader();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            while (dataReader.Read())
            {
                string word = dataReader["Word"].ToString();
                int count = Int32.Parse(dataReader["UsesCount"].ToString());

                dictionary.Add(word, count);
            }
            return dictionary;
        }

        public Dictionary<string, int> FindMostUsedMatches(string prefix, int max = 0)
        {
            string selectQuery = "SELECT ";
            if(max > 0)
            {
                selectQuery += "TOP(" + max + ") ";
            }
            if (usingPLVocabulary)
            {
                selectQuery +=
                    "W.Word, SUM(W.UsesCount) as UsesSum " +
                    "FROM ( " +
                            "(SELECT * FROM UserWords " +
                            "WHERE UserWords.Word LIKE '" + prefix + "%') " +
                            "UNION ALL " +
                            "(SELECT * FROM Words " +
                            "WHERE Words.Word LIKE '" + prefix + "%') " +
                        ") AS W " +
                    "GROUP BY W.Word" +
                    "ORDER BY UsesSum DESC";
            }
            else
            {
                selectQuery += "* FROM UserWords WHERE Word LIKE '" + prefix + "%' ORDER BY UsesCount DESC";
            }

            SqlCommand cmdSelect = new SqlCommand(selectQuery, cnn);
            SqlDataReader dataReader = cmdSelect.ExecuteReader();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            while (dataReader.Read())
            {
                string word = dataReader["Word"].ToString();
                int count = Int32.Parse(dataReader["UsesCount"].ToString());

                dictionary.Add(word, count);
            }
            return dictionary;
        }

        public void Clear()
        {
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM UserWords", cnn);
            cmdDelete.ExecuteNonQuery();
        }
    }
}

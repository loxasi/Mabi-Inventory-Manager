using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mabi_Inventory_Manager
{
    static class ItemDB
    {

        private const string itemdb = @"D:\Documents\Mabi\pack\data\db\itemdb.xml";
        private const string itemnames = @"D:\Documents\Mabi\pack\data\xml\itemdb.english.txt";

        /// <summary>
        /// Returns the item name and category info for an item based on its ID. (itemdb.xml)
        /// </summary>
        /// <param name="itemID">Item ID</param>
        /// <returns>Tuple(name, category)</returns>
        public static Tuple<string, string> ID(int itemID)
        {
            string currIDAttr;
            int currID;
            string lt = "";
            int ltNum;
            string name;
            string cat;
            XmlReader reader = XmlReader.Create(itemdb);
            while (reader.Read())
            {
                if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Mabi_Item"))
                {
                    if(reader.HasAttributes)
                    {
                        // get id of current item
                        currIDAttr = reader.GetAttribute("ID");
                        if (Int32.TryParse(currIDAttr, out currID))
                        {
                            // matching item found
                            if (currID == itemID) {
                                // lookup table id
                                lt = reader.GetAttribute("Text_Name1");
                                ltNum = ParseLT(lt);
                                // get name from ltid
                                name = (ltNum == -1) ? "" : GetName(ltNum);
                                cat = reader.GetAttribute("Category");
                                if (String.IsNullOrEmpty(cat))
                                {
                                    cat = "";
                                }
                                return Tuple.Create(name, cat);
                            }
                        }
                    }
                }
            }
            // item id not found in itemdb xml
            return Tuple.Create("", "");
        }

        /// <summary>
        /// Returns the item name based on the lookup table id. (itemdb.english.txt)
        /// </summary>
        /// <param name="ltid">lookup table id</param>
        /// <returns>item name</returns>
        private static string GetName(int ltid)
        {
            string line;
            int id;
            using(System.IO.StreamReader reader = new System.IO.StreamReader(itemnames))
            {
                while((line = reader.ReadLine()) != null)
                {
                    // ltid, name
                    string[] tokens = line.Split('\t');
                    if (Int32.TryParse(tokens[0], out id))
                    {
                        if (id == ltid)
                        {
                            // item name
                            return tokens[1].TrimEnd('\r', '\n');
                        }
                    }
                }
                // if ltid not found, use xml.itemdb.# as name
                return String.Format("xml.itemdb.{0}", ltid);
            }
        }

        /// <summary>
        /// Gets the lookup table id from _LT[xml.itemdb.#].
        /// </summary>
        /// <param name="lt">lookup table string</param>
        /// <returns>lookup table id</returns>
        private static int ParseLT(string lt)
        {
            int num;
            string[] tokens = lt.Split('.');
            var numS = tokens[2].Substring(0, tokens[2].Length - 1);
            return (Int32.TryParse(numS, out num)) ? num : -1;
        }
    }
}

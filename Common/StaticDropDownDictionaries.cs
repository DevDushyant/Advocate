using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Common
{
    public class StaticDropDownDictionaries
    {
        public static Dictionary<string, string> VolumeDictionary()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "volume1", "Volume I" },
                { "volume2", "Volume II" },
                { "volume3", "Volume III" },
                { "volume4", "Volume IV" },
                { "volume5", "Volume V" },
                { "volume6", "Volume VI" },
                { "volume7", "Volume VII" },
                { "volume8", "Volume VIII" },
                { "volume9", "Volume IX" },
                { "volume10", "Volume X" },
                { "Rajasthan", "Rajasthan" },
                { "Center", "Centeral" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> ModuleDictionary()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "advocate", "Advocate" },
                { "lower_diary", "Lower Dairy" },
                { "head_note", "Head Note" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> AssentByDictionary()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "PREDENT", "Presedent" },
                { "GOVNER", "Governer" },
                { "HHRAJP", "H.H. RajPramukh" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> GazetteNature()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "ORDINA", "Ordinary" },
                { "EXORDI", "Extra Ordinary" },
                 { "WEEKLY", "Weekly" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string> Rule_GSR_SO()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "GSR", "GSR" },
                { "SO", "SO" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<string, string>ManageType()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "NEW", "New" },
                { "AMD", "Amended" },
                { "RPD", "Repealed" }
            };
            return _volumeDictionary;
        }

        public static Dictionary<int, int> YearDictionary()
        {
            var _volumeDictionary = new Dictionary<int, int>();
            for (int year = DateTime.Now.Year; year >= 1850; year--)
            {
                _volumeDictionary.Add(year, year);
            }
            return _volumeDictionary;
        }

        public static Dictionary<string,string> ComeInforce()
        {
            var _comeinforceDictionary = new Dictionary<string, string>
            {
                { "ATONCE", "AtOnce" },
                { "NOSPVN", "No specific provision" },
                 { "ASTBNO", "As to be notified" } ,
                 { "WEFPIG", "W.E.F. published in gazette" },
                 { "WEFIEF", "W.E.F. Immediate effect" },
                { "ASMEND", "As Mentioned" }
            };
            return _comeinforceDictionary;
        }

        public static Dictionary<string, string> RuleSearchingKind()
        {
            var _volumeDictionary = new Dictionary<string, string>
            {
                { "NEW", "New" },
                { "AMD", "Amended" },
                { "RPD", "Repealed" },
                { "ALL", "All" }
            };
            return _volumeDictionary;
        }
    }
}

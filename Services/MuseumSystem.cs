using AdministrationServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Models
{
    public class MuseumSystem
    {
        private static MuseumContext _context = new MuseumContext();
        public static IEnumerable<Maintable> GetMaintable()
        {
            return _context.Maintable;
        }
        public class MidexAndName
        {
            public int midex;
            public string name;
            public MidexAndName(int _midex, string _name)
            {
                midex = _midex;
                name = _name;
            }
        }
        public static IEnumerable<MidexAndName> GetAllMuseums()
        {
            IEnumerable<Maintable> table = GetMaintable();
            List<MidexAndName> simple_table = new List<MidexAndName>();
            foreach (var elem in table)
                simple_table.Add(new MidexAndName(elem.Midex, elem.Mname));
            return simple_table.AsEnumerable();
        }
        public static Maintable GetMuseumByMidex(int midex)
        {
            return _context.Maintable.FirstOrDefault(n => n.Midex == midex);
        }
        public static Maintable GetMuseumByName(string name)
        {
            return _context.Maintable.FirstOrDefault(n => n.Mname == name);
        }
        public static IEnumerable<Collection> GetCollectionByMidex(int midex)
        {
            var result = new List<Collection>();
            foreach (var elem in _context.Collection)
                if (elem.Midex == midex)
                    result.Add(elem);
            IEnumerable<Collection> ECollection = result.AsEnumerable<Collection>();
            return ECollection;
        }
        public static IEnumerable<Exhibition> GetExhibitionByMidex(int midex)
        {
            var result = new List<Exhibition>();
            foreach (var elem in _context.Exhibition)
                if (elem.Midex == midex)
                    result.Add(elem);
            IEnumerable<Exhibition> EExhibition = result.AsEnumerable<Exhibition>();
            return EExhibition;
        }
        public static IEnumerable<Education> GetEducationByMidex(int midex)
        {
            var result = new List<Education>();
            foreach (var elem in _context.Education)
                if (elem.Mid == midex)
                    result.Add(elem);
            IEnumerable<Education> EEducation = result.AsEnumerable<Education>();
            return EEducation;
        }
        public static Collection GetCollectionById(int id)
        {
            return _context.Collection.FirstOrDefault(n => n.Oid == id);
        }
        public static Exhibition GetExhibitionById(int id)
        {
            return _context.Exhibition.FirstOrDefault(n => n.Eid == id);
        }
        public static MuseumInformation GetMuseumInformationByMidex(int midex)
        {
            return _context.MuseumInformation.FirstOrDefault(n => n.Midex == midex);
        }
    }
}

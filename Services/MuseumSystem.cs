using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Helpers;
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
        public static async Task<IEnumerable<Maintable>> GetMaintable()
        {
            return await _context.Maintable.ToListAsync();
        }
        public class MidexAndName
        {
            public int midex { get; set; }
            public string name { get; set; }
            public MidexAndName(int _midex, string _name)
            {
                midex = _midex;
                name = _name;
            }
        }
        //public static IEnumerable<MidexAndName> GetAllMuseums()
        //{
        //    IEnumerable<Maintable> table = GetMaintable();
        //    List<MidexAndName> simple_table = new List<MidexAndName>();
        //    foreach (var elem in table)
        //        simple_table.Add(new MidexAndName(elem.Midex, elem.Mname));
        //    return simple_table.AsEnumerable();
        //}
        public static Maintable GetMuseumByMidex(int midex)
        {
            return _context.Maintable.FirstOrDefault(n => n.Midex == midex);
        }
        public static Maintable GetMuseumByName(string name)
        {
            return _context.Maintable.FirstOrDefault(n => n.Mname == name);
        }
        public static async Task<PagedList<Collection>> GetCollectionsAsync(CollectionDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExpression = _context.Collection.Where(x => x.Midex == parameters.Midex) as IQueryable<Collection>;
            return await PagedList<Collection>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        public static async Task<IEnumerable<Comment>> GetCommentByMidex(int midex)
        {
            return await _context.Comment.Where(x => x.Midex == midex).ToListAsync();
        }
        public static async Task<PagedList<News>> GetNewsByMidex(CollectionDtoParameters parameters)
        {
            Maintable maintable = GetMuseumByMidex(parameters.Midex);
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExpression = _context.News.Where(x => x.Museum == maintable.Mname) as IQueryable<News>;
            return await PagedList<News>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
            //return await _context.News.Where(x => x.Museum == maintable.Mname).ToListAsync();
        }
        public static IEnumerable<Collection> GetCollectionByMidex(int midex)
        {
            var result = new List<Collection>();
            foreach (var elem in _context.Collection)
                if (elem.Midex == midex)
                    result.Add(elem);
            return result.AsEnumerable<Collection>();
        }

        public static IEnumerable<Exhibition> GetExhibitionByMidex(int midex)
        {
            var result = new List<Exhibition>();
            foreach (var elem in _context.Exhibition)
                if (elem.Midex == midex)
                    result.Add(elem);
            return result.AsEnumerable<Exhibition>();
        }
        public static IEnumerable<Education> GetEducationByMidex(int midex)
        {
            var result = new List<Education>();
            foreach (var elem in _context.Education)
                if (elem.Midex == midex)
                    result.Add(elem);
            return result.AsEnumerable<Education>();
        }
        public static IEnumerable<News> GetNewsByName(string name)
        {
            var result = new List<News>();
            foreach (var elem in _context.News)
                if (elem.Museum == name)
                    result.Add(elem);
            return result.AsEnumerable<News>();
        }
        public static IEnumerable<Academic> GetAcademicByMidex(int midex)
        {
            var result = new List<Academic>();
            foreach (var elem in _context.Academic)
                if (elem.Mid == midex)
                    result.Add(elem);
            return result.AsEnumerable<Academic>();
        }
        public static Collection GetCollectionById(int id)
        {
            return _context.Collection.FirstOrDefault(n => n.Oid == id);
        }
        public static Exhibition GetExhibitionById(int id)
        {
            return _context.Exhibition.FirstOrDefault(n => n.Eid == id);
        }
        public static Education GetEducationById(int id)
        {
            return _context.Education.FirstOrDefault(n => n.Aid == id);
        }
        public static News GetNewsById(int id)
        {
            return _context.News.FirstOrDefault(n => n.Id == id);
        }
        public static Academic GetAcademicById(int id)
        {
            return _context.Academic.FirstOrDefault(n => n.AdId == id);
        }
        public static MuseumInformation GetMuseumInformationByMidex(int midex)
        {
            return _context.MuseumInformation.FirstOrDefault(n => n.Midex == midex);
        }
        public static bool DeleteMuseumByMidex(Maintable maintable)
        {
            maintable = GetMuseumByMidex(maintable.Midex);
            if (maintable == null)
                return false;
            _context.Maintable.Remove(maintable);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteCollectionsByOid(Collection collection)
        {
            collection = GetCollectionById((int)collection.Oid);
            if (collection == null)
                return false;
            _context.Collection.Remove(collection);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteExhibitionByEid(Exhibition exhibition)
        {
            exhibition = GetExhibitionById(exhibition.Eid);
            if (exhibition == null)
                return false;
            _context.Exhibition.Remove(exhibition);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteEducationByAid(Education education)
        {
            education = GetEducationById(education.Aid);
            if (education == null)
                return false;
            _context.Education.Remove(education);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteNewsById(News news)
        {
            news = GetNewsById((int)news.Id);
            if (news == null)
                return false;
            _context.News.Remove(news);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteAcademicById(Academic academic)
        {
            academic = GetAcademicById((int)academic.AdId);
            if (academic == null)
                return false;
            _context.Academic.Remove(academic);
            _context.SaveChanges();
            return true;
        }
        public static bool DeleteMuseumInformationById(MuseumInformation info)
        {
            info = GetMuseumInformationByMidex(info.Midex);
            if (info == null)
                return false;
            _context.MuseumInformation.Remove(info);
            _context.SaveChanges();
            return true;
        }

    }
}

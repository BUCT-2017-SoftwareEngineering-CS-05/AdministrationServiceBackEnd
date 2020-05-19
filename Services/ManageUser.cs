using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Helpers;
using AdministrationServiceBackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Models
{
    public class ManageUser : IManageUser
    {
        private MuseumContext _context;
        public ManageUser(MuseumContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<PagedList<User>> GetCompaniesAsync(UserDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExpression = _context.User as IQueryable<User>;

            //if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            //{
            //    parameters.CompanyName = parameters.CompanyName.Trim();
            //    queryExpression = queryExpression.Where(x => x.Name == parameters.CompanyName);
            //}

            //if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            //{
            //    parameters.SearchTerm = parameters.SearchTerm.Trim();
            //    queryExpression = queryExpression.Where(x => x.Name.Contains(parameters.SearchTerm) ||
            //                                                 x.Introduction.Contains(parameters.SearchTerm));
            //}

            //var mappingDictionary = _propertyMappingService.GetPropertyMapping<CompanyDto, Company>();

            //queryExpression = queryExpression.ApplySort(parameters.OrderBy, mappingDictionary);

            return await PagedList<User>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.User;
        }

        public User GetUserById(string id)
        {
            return _context.User.FirstOrDefault(n => n.Userid == id);
        }

        public void MuteUser(string id, int mute)
        {
            GetUserById(id).Coright = mute;
            _context.SaveChanges();
        }
    }
}

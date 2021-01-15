using DAL.Organisation;
using Interface.Organisation;
using System;
using System.Collections.Generic;
using System.Text;
using TestDAL.Organisation;

namespace DalFactory
{
    public static class OrganisationFactory
    {
        public static IOrganisationDB GetOrganisationDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new OrganisationDB();
                default:
                    throw new NotImplementedException();
            }
        }
        public static IOrganisationManagerDB GetOrganisationManagerDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new OrganisationDB();
                case "test":
                    return new TestOrganisationDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

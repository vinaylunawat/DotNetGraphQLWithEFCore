﻿using Framework.DataAccess.Repository;
using Framework.Service.Utilities.Criteria;
using Geography.Business.Country.Types;
using Geography.Business.GraphQL;
using Geography.DataAccess;
using Geography.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geography.Business.Country.Manager
{
    public class CountryQuery : ITopLevelQuery
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryQuery> _logger;

        public CountryQuery(ICountryRepository countryRepository, ILogger<CountryQuery> logger)
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<CountryType>>("countries")
            .ResolveAsync(async context => await ResolveCountries(_countryRepository));

            graphType.Field<CountryType>("country")
            .Argument<NonNullGraphType<IdGraphType>>("countryId", "id of the country")
            .ResolveAsync(ResolveCountry(_countryRepository)
            );
        }

        private async Task<IEnumerable<Entity.Entities.Country>> ResolveCountries(ICountryRepository repository)
        {
            _logger.LogInformation("Inside ResolveCountries");
            FilterCriteria<Entity.Entities.Country> filterCriteria = new FilterCriteria<Entity.Entities.Country>();
            filterCriteria.Includes.Add(item => item.States);
            var countries = await repository.FetchByCriteriaAsync(filterCriteria).ConfigureAwait(false);
            _logger.LogInformation("End ResolveCountries");
            return countries;
        }

        private static Func<IResolveFieldContext<object>, Task<object>> ResolveCountry(ICountryRepository repository)
        {
            return async context =>
            {
                var id = context.GetArgument<long>("countryId");
                if (id > 0)
                {
                    return await repository.FetchByIdAsync(id);
                }
                else
                {
                    context.Errors.Add(new ExecutionError("Wrong value for id"));
                    return null;
                }
            };
        }
    }
}

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Logistics.MVCGridConfig), "RegisterGrids")]

namespace Logistics
{
    using Logistics.Areas.Config.Entities;
    using Logistics.Areas.Config.Repositories;
    using Logistics.Areas.Inventory.Entities;
    using Logistics.Areas.Inventory.Repositories;
    using Logistics.Entities;
    using Logistics.Entities.Access;
    using Logistics.Entities.Appointment;
    using Logistics.Entities.Contact;
    using Logistics.Entities.Personnel;
    using Logistics.Repositories;
    using MVCGrid.Models;
    using MVCGrid.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class MVCGridConfig 
    {
        private static string cellCssClassExpression = "col-lg-2 col-md-2 col-sm-2";
        private static string jqueryEditButton = "<a id='btn-edit' class='btn btn-primary btn-xs' data-id='{Value}'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>";
        private static string jqueryDeleteButton = "<a id='btn-delete' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>";

        public static void RegisterGrids()
        {
            GridDefaults gridDefaults = new GridDefaults()
            {
                Paging = true,
                ItemsPerPage = 10,
                Sorting = true,
                NoResultsMessage = "No data was found"
            };

            MVCGridDefinitionTable.Add("ConfigLookups", BuildConfigLookupsGrid(gridDefaults));

            MVCGridDefinitionTable.Add("SystemUsers", BuildUsersGrid(gridDefaults));

            MVCGridDefinitionTable.Add("Appointments", BuildAppointmentsGrid(gridDefaults));
            MVCGridDefinitionTable.Add("AppointmentPersonnel", BuildAppointmentPersonnelGrid(gridDefaults));

            MVCGridDefinitionTable.Add("Personnel", BuildPersonnelGrid(gridDefaults));
            MVCGridDefinitionTable.Add("PersonnelExams", BuildPersonnelExamResultsGrid(gridDefaults));
            MVCGridDefinitionTable.Add("PersonnelEmails", BuildPersonnelEmailsGrid(gridDefaults));
            MVCGridDefinitionTable.Add("PersonnelAddresses", BuildPersonnelAddressesGrid(gridDefaults));
            MVCGridDefinitionTable.Add("PersonnelPhones", BuildPersonnelPhonesGrid(gridDefaults));

            MVCGridDefinitionTable.Add("Exams", new MVCGridBuilder<Exam>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("Name")
                        .WithSorting(true)
                        .WithValueExpression(ex => ex.Name);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(ex => ex.Id.ToString())
                        .WithValueTemplate("<a href='/Exams/Details/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-delete' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "Name", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                //.WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;

                    ExamRepository examRepo = new ExamRepository();
                    IEnumerable<Exam> exams = examRepo.GetAll();

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "Name":
                            exams = options.SortDirection == SortDirection.Dsc ?
                                exams.OrderByDescending(ex => ex.Name) : exams.OrderBy(ex => ex.Name);
                            break;
                    }

                    // paging
                    if (options.GetLimitOffset().HasValue)
                        exams = exams.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    return new QueryResult<Exam>()
                    {
                        Items = exams.ToList<Exam>(),
                        TotalRecords = exams.Count()
                    };
                })
            );

            MVCGridDefinitionTable.Add("Warehouses", BuilWarehousesGrid(gridDefaults));
            MVCGridDefinitionTable.Add("Bins", BuildBinsGrid(gridDefaults));
            MVCGridDefinitionTable.Add("Assets", BuildAssetsGrid(gridDefaults));
        }

        #region Invntory

        private static MVCGridBuilder<Warehouse> BuilWarehousesGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Warehouse>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("Name")
                        .WithHeaderText("Name")
                        .WithSorting(true)
                        .WithValueExpression(wh => wh.Name);
                    cols.Add("Description")
                        .WithHeaderText("Description")
                        .WithValueExpression(wh => wh.Description);
                    cols.Add("Location")
                        .WithHeaderText("Location")
                        .WithValueExpression(wh => wh.Location);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(wh => wh.Id.ToString())
                        .WithValueTemplate("<a href='/Inventory/Warehouse/Details/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-delete' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "Name", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string search = options.GetFilterString("Name");

                    WarehouseRepository repo = new WarehouseRepository();
                    IEnumerable<Warehouse> warehouses = repo.GetAll();

                    QueryResult<Warehouse> results = new QueryResult<Warehouse>()
                    {
                        TotalRecords = warehouses.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "Name":
                            warehouses = options.SortDirection == SortDirection.Dsc ?
                                warehouses.OrderByDescending(wh => wh.Name) : warehouses.OrderBy(wh => wh.Name);
                            break;
                    }

                    // paging
                    warehouses = Paginate<Warehouse>(warehouses, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    warehouses = warehouses.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = warehouses.ToList<Warehouse>();

                    return results;
                });
        }

        private static MVCGridBuilder<Bin> BuildBinsGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Bin>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("Name")
                        .WithSorting(true)
                        .WithValueExpression(bin => bin.Name);
                    cols.Add("Description")
                        .WithValueExpression(bin => bin.Description);
                    cols.Add("Warehouse")
                        .WithValueExpression(bin => bin.Warehouse?.Name);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(wh => wh.Id.ToString())
                        .WithValueTemplate("<a href='/Inventory/Bins/Details/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-delete' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "Name", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string search = options.GetFilterString("Name");

                    BinRepository repo = new BinRepository();
                    IEnumerable<Bin> bins = repo.GetAll();

                    QueryResult<Bin> results = new QueryResult<Bin>()
                    {
                        TotalRecords = bins.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "Name":
                            bins = options.SortDirection == SortDirection.Dsc ?
                                bins.OrderByDescending(wh => wh.Name) : bins.OrderBy(wh => wh.Name);
                            break;
                    }

                    // paging
                    bins = Paginate<Bin>(bins, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    bins = bins.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = bins.ToList<Bin>();

                    return results;
                });
        }

        private static MVCGridBuilder<Asset> BuildAssetsGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Asset>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("Name")
                        .WithSorting(true)
                        .WithValueExpression(asset => asset.Name);
                    cols.Add("Manufacturer")
                        .WithValueExpression(asset => asset.Manufacturer);
                    cols.Add("SerialNumbr")
                        .WithHeaderText("Serial #")
                        .WithValueExpression(asset => asset.Manufacturer);
                    cols.Add("Warehouse")
                        .WithValueExpression(asset => asset.Warehouse?.Name);
                    cols.Add("Bin")
                        .WithValueExpression(asset => asset.Bin?.Name);
                    cols.Add("Price")
                        .WithValueExpression(asset => asset.Price?.ToString("C"));
                    cols.Add("Quantity")
                        .WithValueExpression(asset => asset.Quantity?.ToString());
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(wh => wh.Id.ToString())
                        .WithValueTemplate("<a href='/Inventory/Assets/Details/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-delete' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "Name", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string search = options.GetFilterString("Name");

                    AssetRepository repo = new AssetRepository();
                    IEnumerable<Asset> assets = repo.GetAll();

                    QueryResult<Asset> results = new QueryResult<Asset>()
                    {
                        TotalRecords = assets.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "Name":
                            assets = options.SortDirection == SortDirection.Dsc ?
                                assets.OrderByDescending(wh => wh.Name) : assets.OrderBy(wh => wh.Name);
                            break;
                    }

                    // paging
                    assets = Paginate<Asset>(assets, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    assets = assets.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = assets.ToList<Asset>();

                    return results;
                });
        }

        #endregion

        #region Appointments

        private static MVCGridBuilder<Appointment> BuildAppointmentsGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Appointment>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("AppointmentDate")
                        .WithVisibility(false)
                        .WithFiltering(true)
                        .WithValueExpression(app => app.AppointmentDate.ToShortDateString());
                    cols.Add("AppointmentTime")
                        .WithHeaderText("Time")
                        .WithSorting(true)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(app => app.AppointmentTime.ToString(@"hh\:mm"));
                    cols.Add("Location")
                        .WithValueExpression(app => app.Location);
                    cols.Add("Notes")
                        .WithValueExpression(app => app.Notes);
                cols.Add("Actions")
                    .WithHtmlEncoding(false)
                    .WithCellCssClassExpression(col => cellCssClassExpression)
                    .WithValueExpression(app => app.Id.ToString())
                    .WithValueTemplate(jqueryEditButton + "&nbsp;" + jqueryDeleteButton);
                })
                .WithSorting(sorting: true, defaultSortColumn: "AppointmentTime", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithAdditionalQueryOptionNames("d")
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string appDate = options.GetAdditionalQueryOptionString("d");
                    DateTime date = DateTime.Parse(appDate);
                    Expression<Func<Appointment, bool>> predicate = app => app.AppointmentDate == date;

                    AppointmentRepository appointmentRepo = new AppointmentRepository();
                    IEnumerable<Appointment> apps = appointmentRepo.Find(predicate);

                    QueryResult<Appointment> results = new QueryResult<Appointment>()
                    {
                        TotalRecords = apps.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "AppointmentTime":
                            apps = options.SortDirection == SortDirection.Dsc ?
                                apps.OrderByDescending(app => app.AppointmentTime) : apps.OrderBy(app => app.AppointmentTime);
                            break;
                    }

                    // paging
                    apps = Paginate<Appointment>(apps, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    apps = apps.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = apps.ToList<Appointment>();

                    return results;
                });
        }

        private static MVCGridBuilder<Person> BuildAppointmentPersonnelGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Person>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("FullName")
                        .WithHeaderText("Name")
                        .WithSorting(true)
                        .WithFiltering(true)
                        .WithValueExpression(per => per.FullName);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(per => per.Id.ToString())
                        .WithValueTemplate("<a id='btn-scheduleAppointment' class='btn btn-primary btn-xs' data-id='{Value}'><i class='fa fa-calendar-check-o fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Schedule</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "FullName", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string search = options.GetFilterString("FullName");

                    PersonnelRepository perRepo = new PersonnelRepository();
                    IEnumerable<Person> personnel = null;
                    if (!string.IsNullOrEmpty(search))
                        personnel = perRepo.Find(per => per.LastName.Contains(search) || per.MiddleName.Contains(search) || per.FirstName.Contains(search));
                    else
                        personnel = perRepo.GetAll();

                    QueryResult<Person> results = new QueryResult<Person>()
                    {
                        TotalRecords = personnel.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "FullName":
                            personnel = options.SortDirection == SortDirection.Dsc ?
                                personnel.OrderByDescending(per => per.FullName) : personnel.OrderBy(per => per.FullName);
                            break;
                    }

                    // paging
                    personnel = Paginate<Person>(personnel, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    personnel = personnel.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = personnel.ToList<Person>();

                    return results;
                });
        }

        #endregion

        #region ConfigLookups

        private static MVCGridBuilder<ConfigLookup> BuildConfigLookupsGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<ConfigLookup>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("Name")
                        .WithSorting(true)
                        .WithValueExpression(lookup => lookup.Name); // use the Value Expression to return the cell text for this column
                    cols.Add("Description")
                        .WithValueExpression(lookup => lookup.Description);
                    cols.Add("Code")
                        .WithValueExpression(lookup => lookup.Code);
                    cols.Add("Type")
                        .WithFiltering(true)
                        .WithValueExpression(lookup => lookup.Type);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(lookup => lookup.Id.ToString())
                        .WithValueTemplate("<a href='/Config/Lookup/Details/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            jqueryDeleteButton);
                })
                .WithSorting(sorting: true, defaultSortColumn: "Name", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string filterType = options.GetFilterString("Type");
                    
                    LookupRepository lookupRepo = new LookupRepository();
                    IEnumerable<ConfigLookup> lookups = null;
                    if (!string.IsNullOrEmpty(filterType))
                        lookups = lookupRepo.Find(lookup => lookup.Type.Equals(filterType));
                    else
                        lookups = lookupRepo.GetAll();

                    QueryResult<ConfigLookup> results = new QueryResult<ConfigLookup>()
                    {
                        TotalRecords = lookups.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "Name":
                            lookups = options.SortDirection == SortDirection.Dsc ?
                                lookups.OrderByDescending(lookup => lookup.Name) : lookups.OrderBy(lookup => lookup.Name);
                            break;
                    }

                    // paging
                    lookups = Paginate<ConfigLookup>(lookups, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    lookups = lookups.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = lookups.ToList<ConfigLookup>();

                    return results;
                });
        }

        #endregion

        #region Personnel

        private static MVCGridBuilder<Person> BuildPersonnelGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Person>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("FullName")
                        .WithHeaderText("Name")
                        .WithSorting(true)
                        .WithFiltering(true)
                        .WithValueExpression(per => per.FullName);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(per => per.Id.ToString())
                        .WithValueTemplate("<a href='/Personnel/Details/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            jqueryDeleteButton);
                })
                .WithSorting(sorting: true, defaultSortColumn: "FullName", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string search = options.GetFilterString("FullName");                    

                    PersonnelRepository perRepo = new PersonnelRepository();
                    IEnumerable<Person> personnel = null;
                    if (!string.IsNullOrEmpty(search))
                        personnel = perRepo.Find(per => per.FirstName.Contains(search) || per.LastName.Contains(search) || per.MiddleName.Contains(search));
                    else
                        personnel = perRepo.GetAll();

                    QueryResult<Person> results = new QueryResult<Person>()
                    {
                        TotalRecords = personnel.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "FullName":
                            personnel = options.SortDirection == SortDirection.Dsc ?
                                personnel.OrderByDescending(per => per.FullName) : personnel.OrderBy(per => per.FullName);
                            break;
                    }

                    // paging
                    personnel = Paginate<Person>(personnel, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    personnel = personnel.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = personnel.ToList<Person>();

                    return results;
                });
        }

        private static MVCGridBuilder<EmailAddress> BuildPersonnelEmailsGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<EmailAddress>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("PersonId")
                        .WithVisibility(false)
                        .WithFiltering(true)
                        .WithValueExpression(email => email.PersonId.ToString());
                    cols.Add("Type")
                        .WithValueExpression(email => email.TypeLookup.Name);
                    cols.Add("Email")
                        .WithSorting(true)
                        .WithValueExpression(email => email.Email);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(email => email.Id.ToString())
                        .WithValueTemplate("<a href='/Personnel/EmailDetails?id={Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-deleteEmail' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "Email", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string filterType = options.GetFilterString("PersonId");
                    long personId = 0;
                    if (!string.IsNullOrEmpty(filterType))
                        personId = long.Parse(filterType);

                    EmailRepository emailRepo = new EmailRepository();
                    IEnumerable<EmailAddress> emails = emailRepo.Find(email => email.PersonId == personId);

                    QueryResult<EmailAddress> results = new QueryResult<EmailAddress>()
                    {
                        TotalRecords = emails.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "Email":
                            emails = options.SortDirection == SortDirection.Dsc ?
                                emails.OrderByDescending(email => email.Email) : emails.OrderBy(email => email.Email);
                            break;
                    }

                    // paging
                    emails = Paginate<EmailAddress>(emails, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    emails = emails.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = emails.ToList<EmailAddress>();

                    return results;
                });
        }

        private static MVCGridBuilder<ExamResult> BuildPersonnelExamResultsGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<ExamResult>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("PersonId")
                        .WithVisibility(false)
                        .WithFiltering(true)
                        .WithValueExpression(result => result.PersonId.ToString());
                    cols.Add("ExamDate")
                        .WithHeaderText("Exam Date")
                        .WithSorting(true)
                        .WithValueExpression(result => result.ExamDate.ToShortDateString());
                    cols.Add("Exam")
                        .WithValueExpression(result => result.Exam.Name);
                    cols.Add("Asset")
                        .WithValueExpression(result => result.Asset?.Name);
                    cols.Add("Result")
                        .WithValueExpression(result => result.ResultLookup.Name);
                    cols.Add("ResultDate")
                        .WithHeaderText("Result Date")
                        .WithValueExpression(result => result.ResultDate?.ToShortDateString());
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(per => per.Id.ToString())
                        .WithValueTemplate("<a href='/Personnel/ExamDetails/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>");
                    //.WithValueTemplate("<a href='/Personnel/ExamDetails/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                    //    "<a id='btn-deleteExam' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "ExamDate", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string filterType = options.GetFilterString("PersonId");
                    long personId = 0;
                    if (!string.IsNullOrEmpty(filterType))
                        personId = long.Parse(filterType);

                    ExamResultRepository resultRepo = new ExamResultRepository();
                    IEnumerable<ExamResult> exams = resultRepo.Find(exam => exam.PersonId == personId);

                    QueryResult<ExamResult> results = new QueryResult<ExamResult>()
                    {
                        TotalRecords = exams.Count()
                    };

                    //// sorting
                    //switch (options.SortColumnName)
                    //{
                    //    case "FullName":
                    //        personnel = options.SortDirection == SortDirection.Dsc ?
                    //            personnel.OrderByDescending(per => per.FullName) : personnel.OrderBy(per => per.FullName);
                    //        break;
                    //}

                    // paging
                    exams = Paginate<ExamResult>(exams, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    results = results.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = exams.ToList<ExamResult>();

                    return results;
                });
        }

        private static MVCGridBuilder<Address> BuildPersonnelAddressesGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Address>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("PersonId")
                        .WithVisibility(false)
                        .WithFiltering(true)
                        .WithValueExpression(address => address.PersonId.ToString());
                    cols.Add("Type")
                        .WithValueExpression(address => address.TypeLookup.Name);
                    cols.Add("Address1")
                        .WithColumnName("Address 1")
                        .WithValueExpression(address => address.Address1);
                    cols.Add("Address2")
                        .WithColumnName("Address 2")
                        .WithValueExpression(address => address.Address2);
                    cols.Add("City")
                        .WithValueExpression(address => address.City);
                    cols.Add("ZipCode")
                        .WithColumnName("Zip Code")
                        .WithValueExpression(address => address.ZipCode);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(address => address.Id.ToString())
                        .WithValueTemplate("<a href='/Personnel/AddressDetails?id={Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-deleteAddress' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(false)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string filterType = options.GetFilterString("PersonId");
                    long personId = 0;
                    if (!string.IsNullOrEmpty(filterType))
                        personId = long.Parse(filterType);

                    AddressRepository addressRepo = new AddressRepository();
                    IEnumerable<Address> adresses = addressRepo.Find(address => address.PersonId == personId);

                    QueryResult<Address> results = new QueryResult<Address>()
                    {
                        TotalRecords = adresses.Count()
                    };

                    // paging
                    adresses = Paginate<Address>(adresses, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    adresses = adresses.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = adresses.ToList<Address>();

                    return results;
                });
        }

        private static MVCGridBuilder<Phone> BuildPersonnelPhonesGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<Phone>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("PersonId")
                        .WithVisibility(false)
                        .WithFiltering(true)
                        .WithValueExpression(phone => phone.PersonId.ToString());
                    cols.Add("Type")
                        .WithValueExpression(phone => phone.TypeLookup.Name);
                    cols.Add("Number")
                        .WithValueExpression(phone => phone.Number);
                    cols.Add("Ext")
                        .WithValueExpression(phone => phone.Ext);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(address => address.Id.ToString())
                        .WithValueTemplate("<a href='/Personnel/PhoneDetails?id={Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-deletePhone' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(false)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string filterType = options.GetFilterString("PersonId");
                    long personId = 0;
                    if (!string.IsNullOrEmpty(filterType))
                        personId = long.Parse(filterType);

                    PhoneRepository phoneRepo = new PhoneRepository();
                    IEnumerable<Phone> phones = phoneRepo.Find(phone => phone.PersonId == personId);

                    QueryResult<Phone> results = new QueryResult<Phone>()
                    {
                        TotalRecords = phones.Count()
                    };

                    // paging
                    phones = Paginate<Phone>(phones, options);
                    //if (options.GetLimitOffset().HasValue)
                    //    phones = phones.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

                    results.Items = phones.ToList<Phone>();

                    return results;
                });
        }

        #endregion

        #region Users

        private static MVCGridBuilder<SystemUser> BuildUsersGrid(GridDefaults gridDefaults)
        {
            return new MVCGridBuilder<SystemUser>(gridDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add("UserName")
                        .WithHeaderText("User Name")
                        .WithSorting(true)
                        .WithFiltering(true)
                        .WithValueExpression(sysUser => sysUser.UserName); // use the Value Expression to return the cell text for this column
                    cols.Add("Status")
                        .WithValueExpression(sysUser => sysUser.StatusLookup?.Name);
                    cols.Add("Actions")
                        .WithHtmlEncoding(false)
                        .WithCellCssClassExpression(col => cellCssClassExpression)
                        .WithValueExpression(sysUser => sysUser.Id.ToString())
                        .WithValueTemplate("<a href='/Admin/UserProfile/{Value}' class='btn btn-primary btn-xs'><i class='fa fa-edit fa-fw text-primary hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Edit</span></a>&nbsp;" +
                            "<a id='btn-delete' class='btn btn-danger btn-xs' data-id='{Value}'><i class='fa fa-trash fa-fw text-danger hidden-lg hidden-md hidden-sm'></i> <span class='hidden-xs'>Delete</span></a>");
                })
                .WithSorting(sorting: true, defaultSortColumn: "UserName", defaultSortDirection: SortDirection.Asc)
                .WithPaging(true, gridDefaults.ItemsPerPage)
                .WithFiltering(true)
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    string search = options.GetFilterString("UserName");                    

                    AdminRepository accessRepo = new AdminRepository();
                    IEnumerable<SystemUser> users = null;
                    if (!string.IsNullOrEmpty(search))
                        users = accessRepo.Find(user => user.UserName.Contains(search));
                    else
                        users = accessRepo.GetAll();

                    QueryResult<SystemUser> results = new QueryResult<SystemUser>()
                    {
                        TotalRecords = users.Count()
                    };

                    // sorting
                    switch (options.SortColumnName)
                    {
                        case "UserName":
                            users = options.SortDirection == SortDirection.Dsc ?
                                users.OrderByDescending(user => user.UserName) : users.OrderBy(user => user.UserName);
                            break;
                    }

                    // paging
                    users = Paginate<SystemUser>(users, options);

                    results.Items = users.ToList<SystemUser>();

                    return results;
                });
        }

        #endregion

        #region Functions

        private static IEnumerable<T> Paginate<T>(IEnumerable<T> results, QueryOptions options)
        {
            if (options.GetLimitOffset().HasValue)
                results = results.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value);

            return results;
        }

        #endregion
    }
}
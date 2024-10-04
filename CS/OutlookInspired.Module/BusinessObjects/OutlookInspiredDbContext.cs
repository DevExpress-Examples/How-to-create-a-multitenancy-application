using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.MultiTenancy;
using Microsoft.Extensions.Configuration;


namespace OutlookInspired.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class OutlookInspiredContextInitializer : DbContextTypesInfoInitializerBase{

	protected override DbContext CreateDbContext() 
		=> new OutlookInspiredEFCoreDbContext(new DbContextOptionsBuilder<OutlookInspiredEFCoreDbContext>()
			.UseSqlServer(";").UseChangeTrackingProxies().UseObjectSpaceLinkProxies().Options);
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class OutlookInspiredDesignTimeDbContextFactory(IConfigurationManager configurationManager)
	: IDesignTimeDbContextFactory<OutlookInspiredEFCoreDbContext>{
	

	public OutlookInspiredEFCoreDbContext CreateDbContext(string[] args) {
		// throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		var optionsBuilder = new DbContextOptionsBuilder<OutlookInspiredEFCoreDbContext>();
		optionsBuilder.UseSqlite(configurationManager["ConnectionString"]);
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
		return new OutlookInspiredEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(OutlookInspiredContextInitializer))]
public class OutlookInspiredEFCoreDbContext(DbContextOptions<OutlookInspiredEFCoreDbContext> options)
	: DbContext(options){
	public DbSet<Tenant> Tenants { get; set; }
	public DbSet<ModelDifference> ModelDifferences { get; set; }
	public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	public DbSet<PermissionPolicyRole> Roles { get; set; }
	public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationUserLoginInfo> UserLoginInfos { get; set; }
	public DbSet<FileData> FileData { get; set; }
	public DbSet<ReportDataV2> ReportDataV2 { get; set; }
	public DbSet<DashboardData> DashboardData { get; set; }
	public DbSet<Analysis> Analysis { get; set; }
	
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Crest> Crests { get; set; }
    public DbSet<CustomerStore> CustomerStores { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Probation> Probations { get; set; }
    public DbSet<ProductCatalog> ProductCatalogs { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteItem> QuoteItems { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<CustomerEmployee> CustomerEmployees { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<EmployeeTask> EmployeeTasks { get; set; }
    public DbSet<CustomerCommunication> CustomerCommunications { get; set; }
    public DbSet<TaskAttachedFile> TaskAttachedFiles { get; set; }
    public DbSet<ViewFilter> ViewFilters { get; set; }
    public DbSet<RichTextMailMergeData> RichTextMailMergeData { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<ApplicationUserLoginInfo>(builder => builder.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique());
        modelBuilder.Entity<ModelDifference>().HasMany(difference => difference.Aspects).WithOne(aspect => aspect.Owner).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Customer>()
	        .Property(e => e.AnnualRevenue)
	        .HasColumnType("decimal(18, 2)");
        
        modelBuilder.OnProductImage()
	        .OnTaskAttachedFile()
	        .OnEvaluation()
	        .OnEmployee()
	        .OnEmployeeTask()
	        .OnCustomerEmployee()
	        .OnCustomerStore()
	        .OnOrder()
	        .OnProduct()
	        .OnQuote()
	        .OnQuoteItem()
	        .OnProductCatalog()
	        .OnCustomerCommunication()
	        .OnOrderItem();
    }

}
static class ModelCreating{
	internal static ModelBuilder OnTaskAttachedFile(this ModelBuilder modelBuilder){
	    var taskAttachedFile = modelBuilder.Entity<TaskAttachedFile>();
	    taskAttachedFile.HasOne(file => file.EmployeeTask).WithMany(task => task.AttachedFiles)
		    .HasForeignKey(file => file.EmployeeTaskId).OnDelete(DeleteBehavior.Cascade);
	    return modelBuilder;
	}

	internal static ModelBuilder OnEvaluation(this ModelBuilder modelBuilder){
	    var evaluation = modelBuilder.Entity<Evaluation>();
	    evaluation.HasOne(e => e.Manager)
		    .WithMany(employee => employee.EvaluationsCreatedBy)
		    .HasForeignKey(e => e.ManagerId)
		    .OnDelete(DeleteBehavior.Cascade);
	    return modelBuilder;
    }

	internal static ModelBuilder OnOrderItem(this ModelBuilder modelBuilder){
	    var orderItem = modelBuilder.Entity<OrderItem>();
	    orderItem.HasOne(o => o.Product).WithMany(product => product.OrderItems).HasForeignKey(o => o.ProductID).OnDelete(DeleteBehavior.Cascade);
	    return modelBuilder;
    }

	internal static ModelBuilder OnCustomerCommunication(this ModelBuilder modelBuilder){
	    var customerCommunication = modelBuilder.Entity<CustomerCommunication>();
	    customerCommunication.HasOne(communication => communication.CustomerEmployee).WithMany(employee => employee.CustomerCommunications);
	    customerCommunication.HasOne(communication => communication.Employee).WithMany(employee => employee.CustomerCommunications);
	    return modelBuilder;
    }

	internal static ModelBuilder OnQuoteItem(this ModelBuilder modelBuilder){
	    var quoteItem = modelBuilder.Entity<QuoteItem>();
	    quoteItem.HasOne(item => item.Product).WithMany(product => product.QuoteItems);
	    return modelBuilder;
    }

	internal static ModelBuilder OnProductCatalog(this ModelBuilder modelBuilder){
	    var catalog = modelBuilder.Entity<ProductCatalog>();
	    catalog.HasOne(productCatalog => productCatalog.Product)
		    .WithMany(product => product.Catalogs)
		    .HasForeignKey(productCatalog => productCatalog.ProductId)
		    .OnDelete(DeleteBehavior.Cascade);
	    return modelBuilder;
    }

	internal static ModelBuilder OnQuote(this ModelBuilder modelBuilder){
	    var quote = modelBuilder.Entity<Quote>();
	    quote.HasOne(q => q.CustomerStore).WithMany(store => store.Quotes);
	    quote.HasOne(q => q.Employee).WithMany(employee => employee.Quotes);
	    return modelBuilder;
    }

	internal static ModelBuilder OnProduct(this ModelBuilder modelBuilder){
	    var product = modelBuilder.Entity<Product>();
	    product.HasOne(p => p.Engineer).WithMany(employee => employee.Products);
	    product.HasOne(p => p.PrimaryImage).WithMany(picture => picture.Products);
	    product.HasOne(p => p.Support).WithMany(employee => employee.SupportedProducts).OnDelete(DeleteBehavior.Cascade);
	    modelBuilder.Entity<Product>().HasMany(p => p.QuoteItems).WithOne(qi => qi.Product).HasForeignKey(qi => qi.ProductId)
		    .OnDelete(DeleteBehavior.Cascade);
	    return modelBuilder;
    }

	internal static ModelBuilder OnOrder(this ModelBuilder modelBuilder){
	    var order = modelBuilder.Entity<Order>();
	    order.HasOne(o => o.Employee).WithMany(employee => employee.Orders);
	    order.HasOne(o => o.Store).WithMany(store => store.Orders);
	    return modelBuilder;
    }

	internal static ModelBuilder OnCustomerStore(this ModelBuilder modelBuilder){
	    var customerStore = modelBuilder.Entity<CustomerStore>();
	    customerStore.HasOne(store => store.Crest).WithMany(crest => crest.CustomerStores);
	    customerStore.Property(e => e.AnnualSales).HasPrecision(18, 2);

	    return modelBuilder;
    }

	internal static ModelBuilder OnCustomerEmployee(this ModelBuilder modelBuilder){
	    var customerEmployee = modelBuilder.Entity<CustomerEmployee>();
	    customerEmployee.HasOne(employee => employee.CustomerStore).WithMany(store => store.CustomerEmployees);
	    customerEmployee.HasOne(employee => employee.Picture).WithMany(picture => picture.CustomerEmployees);
	    return modelBuilder;
    }

	internal static ModelBuilder OnEmployeeTask(this ModelBuilder modelBuilder){
	    var employeeTask = modelBuilder.Entity<EmployeeTask>();
	    employeeTask.HasOne(task => task.AssignedEmployee).WithMany(employee => employee.AssignedTasks).HasForeignKey(task => task.AssignedEmployeeId).OnDelete(DeleteBehavior.SetNull);
	    return modelBuilder;
    }

	internal static ModelBuilder OnEmployee(this ModelBuilder modelBuilder){
		// modelBuilder.Entity<Employee>()
		// 	.HasKey(e => e.ID);  // Set as primary key
		//
		// modelBuilder.Entity<Employee>()
		// 	.HasOne(e => e.User)
		// 	.WithOne(u => u.Employee)
		// 	.HasForeignKey<Employee>(e => e.ID);  // Set as foreign key

		
	    var employee = modelBuilder.Entity<Employee>();
	    modelBuilder.Entity<Employee>()
		    .HasOne(e => e.User)
		    .WithOne(u => u.Employee)
		    .HasForeignKey<Employee>(e => e.UserId);
	    // employee.ToTable(nameof(Employee));
	    employee.HasOne(e => e.Picture).WithMany(picture => picture.Employees);
	    employee.HasOne(e => e.ProbationReason).WithMany(probation => probation.Employees).HasForeignKey(e => e.ProbationReasonId);
	    employee.HasMany(e => e.OwnedTasks).WithOne(et => et.Owner).HasForeignKey(et => et.OwnerId);
	    
	    return modelBuilder;
	}
	
	internal static ModelBuilder OnProductImage(this ModelBuilder modelBuilder){
		var productImage = modelBuilder.Entity<ProductImage>();
		productImage.HasOne(image => image.Picture).WithMany(picture => picture.ProductImages);
		productImage.HasOne(image => image.Product).WithMany(product => product.Images).HasForeignKey(image => image.ProductId).OnDelete(DeleteBehavior.Cascade);
		return modelBuilder;
	}    
}
